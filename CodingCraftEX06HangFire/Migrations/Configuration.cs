namespace CodingCraftEX06HangFire.Migrations
{
    using CodingCraftEX06HangFire.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodingCraftEX06HangFire.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CodingCraftEX06HangFire.Models.ApplicationDbContext context)
        {

            //if (!System.Diagnostics.Debugger.IsAttached)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            context.Empresas.AddOrUpdate(a => a.Cnpj,
                Empresa(context, "43776517000180", "CIA SANEAMENTO BASICO EST SAO PAULO", "SABESP", new Dictionary<string, string> { { "BRSBSPACNOR5", "SBSP3" } }),
                Empresa(context, "33000167000101", "PETROLEO BRASILEIRO S.A. PETROBRAS", "PETROBRAS", new Dictionary<string, string> { { "BRPETRACNOR9", "PETR3" }, { "BRPETRACNPR6", "PETR4" } })
                );
        }

        private static Empresa Empresa(ApplicationDbContext context, string cnpj, string razaoSocial, string nomePregao, Dictionary<string, string> acoes)
        {
            Random rnd = new Random();
            var empresa = new Models.Empresa
                {
                    EmpresaId = Guid.NewGuid(),
                    Cnpj = cnpj,
                    RazaoSocial = razaoSocial,
                    NomePregao = nomePregao,
                    Acoes = new List<Acao>()
                };
            foreach (var a in acoes)
            {
                var acao = new Acao()
                {
                    AcaoId = Guid.NewGuid(),
                    CodigoIsin = a.Key,
                    CodigoNegociacao = a.Value,
                    AcoesHistoricos = new List<AcaoHistorico>()
                };

                acao.AcoesHistoricos.Add(new AcaoHistorico
                {
                    AcaoHistoricoId = Guid.NewGuid(),
                    DataHora = DateTime.Now,
                    Preco = ((decimal)new Random((int)DateTime.Now.Ticks).Next(100, 9999)) / 100
                });
                empresa.Acoes.Add(acao);
            }

            
            return empresa;
        }
    }
}
