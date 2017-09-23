using CodingCraftEX06HangFire.Models;
using CodingCraftEX06HangFire.Models.Enums;
using EntityFramework.DynamicFilters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace CodingCraftEX06HangFire.Infraestrura.BackgroundJobs
{
    public static class AcoesJobs
    {
        public static async Task MecanicaJob()
        {
            using (BaseContext db = new BaseContext())
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await AcoesFlutuacoes(db);

                    await AcoesCompras(db);

                    await AcoesVendas(db);

                    scope.Complete();
                }
            }

            Hangfire.BackgroundJob.Enqueue(() => AcoesJobs.MecanicaJob());
        }

        private static async Task AcoesVendas(BaseContext db)
        {
            Random random = new Random(Environment.TickCount);
            var percentual = random.Next(0, 100);

            var vendas = await db.Ordens
                .Include(a => a.OrdensUsuariosAcoes)
                .Where(a => a.Tipo == OrdemTipo.Venda)
                .Where(a => a.Ativo)
                .Where(a => a.Chance >= percentual)
                .ToListAsync();

            foreach (var venda in vendas)
            {
                venda.Ativo = false;
                var usuarioAcao = venda.OrdensUsuariosAcoes.FirstOrDefault().UsuarioAcao;
                usuarioAcao.Quantidade -= venda.Quantidade;
                usuarioAcao.Ativo = usuarioAcao.Quantidade > 0;
                db.Entry(usuarioAcao).State = EntityState.Modified;
            }

            await db.SaveChangesAsync();
        }

        private static async Task AcoesCompras(BaseContext db)
        {
            Random random = new Random(Environment.TickCount);
            var percentual = random.Next(0, 100);

            var compras = await db.Ordens
                .Where(a => a.Tipo == OrdemTipo.Compra)
                .Where(a => a.Ativo)
                .Where(a => !a.OrdensUsuariosAcoes.Any())
                .Where(a => a.Chance >= percentual)
                .ToListAsync();

            foreach (var compra in compras)
            {
                compra.Ativo = false;

                db.UsuariosAcoes.Add(new UsuarioAcao()
                {
                    UsuarioAcaoId = Guid.NewGuid(),
                    AcaoId = compra.AcaoId,
                    Ativo = true,
                    Compra = DateTime.Now,
                    Preco = compra.Preco,
                    UsuarioId = compra.UsuarioId,
                    Quantidade = compra.Quantidade,
                    Total = compra.Preco * compra.Quantidade,
                    OrdensUsuariosAcoes = new List<OrdemUsuarioAcao>()
                    {
                        new OrdemUsuarioAcao
                        {
                            OrdemUsuarioAcaoId = Guid.NewGuid(),
                            OrdemId = compra.OrdemId
                        }
                    }
                });
            }

            await db.SaveChangesAsync();
        }

        private static async Task AcoesFlutuacoes(BaseContext db)
        {
            var acoes = await db.Acoes                
                .OrderBy(a => Guid.NewGuid())
                .ToListAsync();

            var rd = new Random((int)DateTime.Now.Ticks);

            foreach (var acao in acoes)
            {
                decimal random = rd.Next(-1999, 2999);
                decimal percentualVariacao = random / 1000;
                var valorVariacao = Math.Round((decimal)acao.Preco * (percentualVariacao / 100), 2);
                var preco = acao.Preco + valorVariacao;
                var historico = new AcaoHistorico()
                {
                    AcaoHistoricoId = Guid.NewGuid(),
                    DataHora = DateTime.Now,
                    PercentualVariacao = percentualVariacao,
                    Preco = preco,
                    ValorVariacao = valorVariacao,
                    AcaoId = acao.AcaoId,
                    UsuariosAcoesHistoricos = new List<UsuarioAcaoHistorico>()
                };

                var usuariosAcoes = await db.UsuariosAcoes                    
                    .Where(a => a.AcaoId == acao.AcaoId && a.Ativo)
                    .ToListAsync();
                foreach (var usuarioAcao in usuariosAcoes)
                {
                    db.UsuariosAcoesHistoricos.Add(new UsuarioAcaoHistorico()
                    {
                        UsuarioAcaoHistoricoId = Guid.NewGuid(),
                        AcaoHistoricoId = historico.AcaoHistoricoId,
                        UsuarioAcaoId = usuarioAcao.UsuarioAcaoId,
                        Rentabilidade = (historico.Preco - usuarioAcao.Preco) * usuarioAcao.Quantidade
                    });
                    usuarioAcao.Rentabilidade = (historico.Preco - usuarioAcao.Preco) * usuarioAcao.Quantidade;
                }
                db.AcoesHistoricos.Add(historico);

                var compras = await db.Ordens.Where(a => a.AcaoId == acao.AcaoId && a.Ativo && a.Tipo == OrdemTipo.Compra).ToListAsync();
                foreach (var compra in compras)
                {
                    if (compra.Preco >= acao.Preco)
                        compra.Chance = 100;
                    else if (acao.Preco - compra.Preco > 0.20M)
                    {
                        compra.Chance = 0;
                    }
                    else if (acao.Preco - compra.Preco <= 0.14M)
                    {
                        compra.Chance = 100 - (acao.Preco - compra.Preco + 1);
                    }
                    else
                    {
                        compra.Chance = (decimal)rd.NextDouble();
                    }
                }

                var vendas = await db.Ordens.Where(a => a.AcaoId == acao.AcaoId && a.Ativo && a.Tipo == OrdemTipo.Venda).ToListAsync();
                foreach (var venda in vendas)
                {
                    if (venda.Preco <= acao.Preco)
                        venda.Chance = 100;
                    else if (venda.Preco - acao.Preco > 0.20M)
                    {
                        venda.Chance = 0;
                    }
                    else if (venda.Preco - acao.Preco <= 0.14M)
                    {
                        venda.Chance = 100 - (acao.Preco - venda.Preco + 1);
                    }
                    else
                    {
                        venda.Chance = (decimal)rd.NextDouble();
                    }
                }
                Thread.Sleep(1);
            }

            await db.SaveChangesAsync();
        }
    }
}