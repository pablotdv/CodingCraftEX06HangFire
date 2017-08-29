namespace CodingCraftEX06HangFire.Migrations
{
    using CodingCraftEX06HangFire.Managers;
    using CodingCraftEX06HangFire.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading;

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
                Empresa(context, "33000167000101", "PETROLEO BRASILEIRO S.A. PETROBRAS", "PETROBRAS", new Dictionary<string, string> { { "BRPETRACNOR9", "PETR3" }, { "BRPETRACNPR6", "PETR4" } }),
                Empresa(context, "02916265000160", "JBS S.A.", "JBS", new Dictionary<string, string> { { "BRJBSSACNOR8", "JBSS3" } }),
                Empresa(context, "71673990000177", "NATURA COSMETICOS S.A.", "NATURA", new Dictionary<string, string> { { "BRNATUACNOR6", "NATU3" } }),
                Empresa(context, "45453214000151", "PROFARMA DISTRIB PROD FARMACEUTICOS S.A.", "PROFARMA", new Dictionary<string, string> { { "BRPFRMACNOR1", "PFRM3" } }),
                Empresa(context, "07689002000189", "EMBRAER S.A.", "EMBRAER", new Dictionary<string, string> { { "BREMBRACNOR4", "EMBR3" } }),
                Empresa(context, "02558157000162", "TELEFÔNICA BRASIL S.A", "TELEF BRASIL", new Dictionary<string, string> { { "BRVIVTACNOR0", "VIVT3" } , { "BRVIVTACNPR7", "VIVT4" } }),
                Empresa(context, "16404287000155", "SUZANO PAPEL E CELULOSE S.A.", "SUZANO PAPEL", new Dictionary<string, string> { { "BRSUZBACNPA3", "SUZB5" } , { "BRSUZBACNPB1", "SUZB6" } }),
                Empresa(context, "00001180000126", "CENTRAIS ELET BRAS S.A. - ELETROBRAS", "ELETROBRAS", new Dictionary<string, string> { { "BRELETACNOR6", "ELET3" }, { "BRELETACNPA9", "ELET5" }, { "BRELETACNPB7", "ELET6" } }),
                Empresa(context, "21728500000114", "PACIFIC RDSL PARTICIPAÇÕES S.A.", "PACIFIC RDSL", new Dictionary<string, string> { { "BRPACFACNOR8", "PACF3" } }),
                Empresa(context, "14388334000199", "PARANA BCO S.A.", "PARANA", new Dictionary<string, string> { { "BRPRBCACNPR8", "PRBC4" } }),
                Empresa(context, "10345009000198", "ADVANCED DIGITAL HEALTH MEDICINA PREVENTIVA S.A.", "ADVANCED-DH", new Dictionary<string, string> { { "BRADHMACNOR9", "ADHM3" } }),

                Empresa(context, "76535764000143", "OI S.A.", "OI", new Dictionary<string, string> { { "BROIBRACNOR1", "OIBR3" }, { "BROIBRACNPR8", "OIBR4" } })
                );

            var roleManager = new ApplicationRoleManager(new RoleStore<Grupo, Guid, UsuarioGrupo>(context));
            var roleNames = new string[] { "Administradores" };
            foreach (var roleName in roleNames)
                if (!roleManager.Roles.Any(r => r.Name == roleName))
                    roleManager.Create(new Grupo { Name = roleName });

            CreateUser(context, "pablotdvsm@gmail.com", "Senha123@", "Administradores");
            CreateUser(context, "jogador1@gmail.com", "Senha123@", "");
        }

        private static void CreateUser(ApplicationDbContext context, string userName, string senha, string role)
        {
            if (!context.Users.Any(u => u.UserName == userName))
            {
                var store = new UserStore<Usuario, Grupo, Guid, UsuarioLogin, UsuarioGrupo, UsuarioIdentidade>(context);
                var manager = new UserManager<Usuario, Guid>(store);
                var user = new Usuario { Id = Guid.NewGuid(), UserName = userName, Email = userName, EmailConfirmed = true };

                manager.Create(user, senha);
                if (!string.IsNullOrWhiteSpace(role))
                    manager.AddToRole(user.Id, role);
            }
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
                    Preco = ((decimal)new Random(DateTime.Now.Millisecond).Next(100, 9999)) / 100
                });
                empresa.Acoes.Add(acao);
                Thread.Sleep(1);
            }


            return empresa;
        }
    }
}
