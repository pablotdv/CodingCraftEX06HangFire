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
                Empresa(context, "02558157000162", "TELEFÔNICA BRASIL S.A", "TELEF BRASIL", new Dictionary<string, string> { { "BRVIVTACNOR0", "VIVT3" }, { "BRVIVTACNPR7", "VIVT4" } }),
                Empresa(context, "16404287000155", "SUZANO PAPEL E CELULOSE S.A.", "SUZANO PAPEL", new Dictionary<string, string> { { "BRSUZBACNPA3", "SUZB5" }, { "BRSUZBACNPB1", "SUZB6" } }),
                Empresa(context, "00001180000126", "CENTRAIS ELET BRAS S.A. - ELETROBRAS", "ELETROBRAS", new Dictionary<string, string> { { "BRELETACNOR6", "ELET3" }, { "BRELETACNPA9", "ELET5" }, { "BRELETACNPB7", "ELET6" } }),
                Empresa(context, "21728500000114", "PACIFIC RDSL PARTICIPAÇÕES S.A.", "PACIFIC RDSL", new Dictionary<string, string> { { "BRPACFACNOR8", "PACF3" } }),
                Empresa(context, "14388334000199", "PARANA BCO S.A.", "PARANA", new Dictionary<string, string> { { "BRPRBCACNPR8", "PRBC4" } }),
                Empresa(context, "10345009000198", "ADVANCED DIGITAL HEALTH MEDICINA PREVENTIVA S.A.", "ADVANCED-DH", new Dictionary<string, string> { { "BRADHMACNOR9", "ADHM3" } }),
                Empresa(context, "00776574000156", "B2W - COMPANHIA DIGITAL", "B2W DIGITAL", new Dictionary<string, string> { { "BRBTOWACNOR8", "BTOW3" } }),
                Empresa(context, "00000000000191", "BCO BRASIL S.A.", "BRASIL", new Dictionary<string, string> { { "BRBBASACNOR3", "BBAS3" } }),
                Empresa(context, "04030182000102", "CABINDA PARTICIPACOES S.A.", "CABINDA PART", new Dictionary<string, string> { { "BRCABIACNOR2", "CABI3B" } }),
                Empresa(context, "04031213000131", "CACONDE PARTICIPACOES S.A.", "CACONDE PART", new Dictionary<string, string> { { "BRCACOACNOR8", "CACO3B" } }),
                Empresa(context, "61486650000183", "DIAGNOSTICOS DA AMERICA S.A.", "DASA", new Dictionary<string, string> { { "BRDASAACNOR1", "DASA3" } }),
                Empresa(context, "84683408000103", "DOHLER S.A.", "DOHLER", new Dictionary<string, string> { { "BRDOHLACNOR2", "DOHL3" }, { "BRDOHLACNPR9", "DOHL4" } }),
                Empresa(context, "04149454000180", "ECORODOVIAS INFRAESTRUTURA E LOGÍSTICA S.A.", "ECORODOVIAS", new Dictionary<string, string> { { "BRECORACNOR8", "ECOR3" } }),
                Empresa(context, "03983431000103", "EDP - ENERGIAS DO BRASIL S.A.", "ENERGIAS BR", new Dictionary<string, string> { { "BRENBRACNOR2", "ENBR3" } }),
                Empresa(context, "00924429000175", "FERROVIA CENTRO-ATLANTICA S.A.", "FER C ATLANT", new Dictionary<string, string> { { "BRVSPTACNOR1", "VSPT3" } }),
                Empresa(context, "09288252000132", "GAEC EDUCAÇÃO S.A.", "ANIMA", new Dictionary<string, string> { { "BRANIMACNOR6", "ANIM3" } }),
                Empresa(context, "30540991000166", "HAGA S.A. INDUSTRIA E COMERCIO", "HAGA S/A", new Dictionary<string, string> { { "BRHAGAACNOR7", "HAGA3" }, { "BRHAGAACNPR4", "HAGA4" } }),
                Empresa(context, "49263189000102", "HELBOR EMPREENDIMENTOS S.A.", "HELBOR", new Dictionary<string, string> { { "BRHBORACNOR3", "HBOR3" } }),
                Empresa(context, "02365069000144", "IDEIASNET S.A.", "IDEIASNET", new Dictionary<string, string> { { "BRIDNTACNOR5", "IDNT3" } }),
                Empresa(context, "43185362000107", "IGB ELETRÔNICA S/A", "IGB S/A", new Dictionary<string, string> { { "BRIGBRACNOR7", "IGBR3" } }),
                Empresa(context, "60543816000193", "JEREISSATI PARTICIPACOES S.A.", "JEREISSATI", new Dictionary<string, string> { { "BRMLFTACNOR6", "MLFT3" }, { "BRMLFTACNPR3", "MLFT4" } }),
                Empresa(context, "82640558000104", "KARSTEN S.A.", "KARSTEN", new Dictionary<string, string> { { "BRCTKAACNOR0", "CTKA3" }, { "BRCTKAACNPR7", "CTKA4" } }),
                Empresa(context, "91983056000169", "KEPLER WEBER S.A.", "KEPLER WEBER", new Dictionary<string, string> { { "BRKEPLACNOR1", "KEPL3" }, { "BRKEPLN01M16", "KEPL11" } }),
                Empresa(context, "03378521000175", "LIGHT S.A.", "LIGHT S/A", new Dictionary<string, string> { { "BRLIGTACNOR2", "LIGT3" } }),
                Empresa(context, "92754738000162", "LOJAS RENNER S.A.", "LOJAS RENNER", new Dictionary<string, string> { { "BRLRENACNOR1", "LREN3" } }),
                Empresa(context, "07206816000115", "M.DIAS BRANCO S.A. IND COM DE ALIMENTOS", "M.DIASBRANCO", new Dictionary<string, string> { { "BRMDIAACNOR7", "MDIA3" } }),
                Empresa(context, "08795211000170", "MAESTRO LOCADORA DE VEICULOS S.A.", "MAESTROLOC", new Dictionary<string, string> { { "BRMSROACNOR7", "MSRO3" } }),
                Empresa(context, "47960950000121", "MAGAZINE LUIZA S.A.", "MAGAZ LUIZA", new Dictionary<string, string> { { "BRMGLUACNOR2", "MGLU3" } }),
                Empresa(context, "61189288000189", "MARISA LOJAS S.A.", "LOJAS MARISA", new Dictionary<string, string> { { "BRAMARACNOR4", "AMAR3" } }),
                Empresa(context, "61067161000197", "NADIR FIGUEIREDO IND E COM S.A.", "NADIR FIGUEI", new Dictionary<string, string> { { "BRNAFGACNOR4", "NAFG3" }, { "BRNAFGACNPR1", "NAFG4" } }),
                Empresa(context, "51128999000190", "NUTRIPLANT INDUSTRIA E COMERCIO S.A.", "NUTRIPLANT", new Dictionary<string, string> { { "BRNUTRACNOR0", "NUTR3" } }),

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
                var user = new Usuario { Id = Guid.NewGuid(), UserName = userName, Email = userName, EmailConfirmed = true, Dinheiro = 1000, Saldo = 1000, Ordens = new List<Ordem>() };

                manager.Create(user, senha);
                if (!string.IsNullOrWhiteSpace(role))
                    manager.AddToRole(user.Id, role);
            }
        }

        private static Empresa Empresa(ApplicationDbContext context, string cnpj, string razaoSocial, string nomePregao, Dictionary<string, string> acoes)
        {            
            var empresa = new Models.Empresa
            {
                EmpresaId = Guid.NewGuid(),
                Cnpj = cnpj,
                RazaoSocial = razaoSocial,
                NomePregao = nomePregao,
                Acoes = new List<Acao>()
            };
            var rd = new Random(Environment.TickCount);
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
                    Preco = (decimal)rd.Next(10000, 999999) / 10000
                });
                empresa.Acoes.Add(acao);                
            }


            return empresa;
        }
    }
}
