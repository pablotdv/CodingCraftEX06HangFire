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
                    
                    scope.Complete();
                }
            }           
        }
        
        private static async Task AcoesFlutuacoes(BaseContext db)
        {
            var acoes = await db.Acoes                
                .OrderBy(a => Guid.NewGuid())
                .ToListAsync();

            var rd = new Random((int)DateTime.Now.Ticks);

            foreach (var acao in acoes)
            {
                decimal random = rd.Next(-1000000000, 1000000000);
                decimal percentualVariacao = random / 1000000000;
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
                                
                Thread.Sleep(1);
            }

            await db.SaveChangesAsync();
        }
    }
}