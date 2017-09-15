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
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.DisableFilter("UsuariosOrdens");
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await AcoesFlutuacoes(db);

                    await AcoesCompras(db);

                    scope.Complete();
                }
                db.EnableFilter("UsuariosOrdens");
            }
        }

        private static async Task AcoesCompras(ApplicationDbContext db)
        {
            Random random = new Random(Environment.TickCount);
            var percentual = random.Next(0, 100);

            var compras = await db.Ordens
                .Where(a => a.Tipo == OrdemTipo.Compra)
                .Where(a => db.UsuariosAcoes.Any(b => b.UsuarioId == a.UsuarioId && b.AcaoId == a.AcaoId && b.Ativo))
                .Where(a => a.Chance >= percentual)
                .ToListAsync();

            foreach(var compra in compras)
            {
                compra.Ativo = false;

                db.UsuariosAcoes.Add(new UsuarioAcao() {
                    UsuarioAcaoId = Guid.NewGuid(),
                    AcaoId = compra.AcaoId,
                    Ativo = true,
                    Compra = DateTime.Now,
                    Preco = compra.Preco,
                    UsuarioId = compra.UsuarioId
                });                
            }

            await db.SaveChangesAsync();
        }

        private static async Task AcoesFlutuacoes(ApplicationDbContext db)
        {
            var acoes = await db.Acoes.Include(a => a.AcoesHistoricos).ToListAsync();

            var rd = new Random(Environment.TickCount);

            foreach (var acao in acoes)
            {
                decimal random = rd.Next(-9999, 9999);
                decimal percentualVariacao = random / 1000;
                var valorVariacao = (decimal)acao.Preco * (percentualVariacao / 100);
                var preco = acao.Preco + valorVariacao;
                var historico = new AcaoHistorico()
                {
                    AcaoHistoricoId = Guid.NewGuid(),
                    DataHora = DateTime.Now,
                    PercentualVariacao = percentualVariacao,
                    Preco = preco,
                    ValorVariacao = valorVariacao,
                    UsuariosAcoesHistoricos = new List<UsuarioAcaoHistorico>()
                };

                var usuariosAcoes = await db.UsuariosAcoes.Include(a => a.UsuariosAcoesHistoricos).Where(a => a.AcaoId == acao.AcaoId && a.Ativo).ToListAsync();
                foreach (var usuarioAcao in usuariosAcoes)
                {
                    historico.UsuariosAcoesHistoricos.Add(new UsuarioAcaoHistorico()
                    {
                        UsuarioAcaoHistoricoId = Guid.NewGuid(),
                        UsuarioAcaoId = usuarioAcao.UsuarioAcaoId
                    });
                }
                acao.AcoesHistoricos.Add(historico);

                var compras = await db.Ordens.Where(a => a.AcaoId == acao.AcaoId && a.Ativo && a.Tipo == OrdemTipo.Compra).ToListAsync();
                foreach (var compra in compras)
                {
                    if (compra.Preco >= acao.Preco)
                        compra.Chance = 100;
                    else if (acao.Preco - compra.Preco > 20)
                    {
                        compra.Chance = 0;
                    }
                    else if (acao.Preco - compra.Preco <= 14)
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
                    else if (venda.Preco - acao.Preco > 20)
                    {
                        venda.Chance = 0;
                    }
                    else if (venda.Preco - acao.Preco <= 14)
                    {
                        venda.Chance = 100 - (acao.Preco - venda.Preco + 1);
                    }
                    else
                    {
                        venda.Chance = (decimal)rd.NextDouble();
                    }
                }
            }

            await db.SaveChangesAsync();
        }
    }
}