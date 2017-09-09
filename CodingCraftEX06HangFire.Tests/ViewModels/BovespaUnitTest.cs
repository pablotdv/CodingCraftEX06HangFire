using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CodingCraftEX06HangFire.Tests.ViewModels.Bovespa
{
    [TestClass]
    public class BovespaUnitTest
    {
        private string linha_00 = "00COTAHIST.2017BOVESPA 20170901                                                                                                                                                                                                                      ";
        private string linha_01 = "012017010202AALR3       010ALLIAR      ON      NM   R$  000000000146200000000014880000000001440000000000145800000000014600000000001460000000000147300087000000000000035900000000000052350500000000000000009999123100000010000000000000BRAALRACNOR6100";
        private string linha_99 = "99COTAHIST.2017BOVESPA 2017090100000323986                                                                                                                                                                                                           ";

        [TestMethod]
        public void _00_Header()
        {
            var motor = new FileHelpers.FileHelperEngine<CodingCraftEX06HangFire.ViewModels.Bovespa.Header>();
            var registro = motor.ReadString(linha_00).First();

            Assert.AreEqual(0, registro.TipoDeRegistro);
            Assert.AreEqual("COTAHIST.2017", registro.NomeDoArquivo);
            Assert.AreEqual("BOVESPA ", registro.CodigoDaOrigem);
            Assert.AreEqual(new DateTime(2017, 9, 1), registro.DataDaGeracaoDoArquivo);
            Assert.AreEqual("".PadLeft(214), registro.Reserva);
        }

        [TestMethod]
        public void _01_Cotacoes_Historicas_Por_Papel_Mercado()
        {
            var motor = new FileHelpers.FileHelperEngine<CodingCraftEX06HangFire.ViewModels.Bovespa.Detail>();

            var registro = motor.ReadString(linha_01).First();

            Assert.AreEqual(01, registro.TipReg);
            Assert.AreEqual(new DateTime(2017, 1, 2), registro.DataDoPregao);
            Assert.AreEqual("02", registro.CodBdi);
            Assert.AreEqual("AALR3       ", registro.CodNeg);
            Assert.AreEqual(010, registro.TpMerc);
            Assert.AreEqual("ALLIAR      ", registro.NomRes);
            Assert.AreEqual("ON      NM", registro.Especi);
            Assert.AreEqual("   ", registro.PrazoT);
            Assert.AreEqual("R$  ", registro.ModRef);
            Assert.AreEqual(14.62M, registro.PreAbe);
            Assert.AreEqual(14.88M, registro.PreMax);
            Assert.AreEqual(14.40M, registro.PreMin);
            Assert.AreEqual(14.58M, registro.PreMed);
            Assert.AreEqual(14.60F, registro.PreUlt);
            Assert.AreEqual(14.60M, registro.PreOfc);
            Assert.AreEqual(14.73M, registro.PreOfv);
            Assert.AreEqual(00087, registro.TotNeg);
            Assert.AreEqual(000000000000035900, registro.QuatTot);
            Assert.AreEqual(0000000000523505.00M, registro.VolTot);
            Assert.AreEqual(0M, registro.PreExe);
            Assert.AreEqual(0, registro.IndOpc);
            Assert.AreEqual(new DateTime(9999, 12, 31), registro.DataVen);
            Assert.AreEqual(1, registro.FatCot);
            Assert.AreEqual(0, registro.PtoExe);
            Assert.AreEqual("BRAALRACNOR6", registro.CodIsi);
            Assert.AreEqual(100, registro.DisMes);
        }

        [TestMethod]
        public void _99_Trailer()
        {
            var motor = new FileHelpers.FileHelperEngine<CodingCraftEX06HangFire.ViewModels.Bovespa.Trailer>();

            var registro = motor.ReadString(linha_99).First();

            Assert.AreEqual(99, registro.TipoDeRegistro);
            Assert.AreEqual("COTAHIST.2017", registro.NomeDoArquivo);
            Assert.AreEqual("BOVESPA ", registro.CodigoDaOrigem);
            Assert.AreEqual(new DateTime(2017, 9, 1), registro.DataDaGeracaoDoArquivo);
            Assert.AreEqual(00000323986, registro.TotalDeRegistros);
            Assert.AreEqual("".PadRight(203), registro.Reserva);
        }
    }
}
