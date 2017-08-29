using CodingCraftEX06HangFire.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CodingCraftEX06HangFire.Infraestrura.BackgroundJobs
{
    public static class AcoesJobs
    {
        public static async Task FlutuacaoJob()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var acoes = await db.Acoes.Include(a => a.AcoesHistoricos).ToListAsync();

                foreach (var acao in acoes)
                {                    
                    decimal random = new Random(DateTime.Now.Millisecond).Next(-9999, 9999);
                    decimal percentualVariacao = random / 1000;
                    var valorVariacao = acao.Preco * (percentualVariacao / 100);
                    var preco = acao.Preco + valorVariacao;
                    acao.AcoesHistoricos.Add(new AcaoHistorico()
                    {
                        AcaoHistoricoId = Guid.NewGuid(),
                        DataHora = DateTime.Now,
                        PercentualVariacao = percentualVariacao,
                        Preco = preco,
                        ValorVariacao = valorVariacao
                    });
                    Thread.Sleep(1);
                }

                await db.SaveChangesAsync();
            }
        }
    }
}