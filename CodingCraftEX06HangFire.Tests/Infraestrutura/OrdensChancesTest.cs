using CodingCraftEX06HangFire.Infraestrura;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCraftEX06HangFire.Tests.Infraestrutura
{
    [TestClass]
    public class OrdensChancesTest
    {
        [TestMethod]
        public void OrdensChances_Vendas()
        {
            Assert.AreEqual(100, OrdensChances.Venda(10, 9.98M));
            Assert.AreEqual(100, OrdensChances.Venda(10, 9.99M));
            Assert.AreEqual(100, OrdensChances.Venda(10, 10M));
            Assert.AreEqual(99, OrdensChances.Venda(10, 10.01M));
            Assert.AreEqual(97, OrdensChances.Venda(10, 10.02M));
            Assert.AreEqual(94, OrdensChances.Venda(10, 10.03M));
            Assert.AreEqual(90, OrdensChances.Venda(10, 10.04M));
            Assert.AreEqual(85, OrdensChances.Venda(10, 10.05M));
            Assert.AreEqual(79, OrdensChances.Venda(10, 10.06M));
            Assert.AreEqual(72, OrdensChances.Venda(10, 10.07M));
            Assert.AreEqual(64, OrdensChances.Venda(10, 10.08M));
            Assert.AreEqual(55, OrdensChances.Venda(10, 10.09M));
            Assert.AreEqual(45, OrdensChances.Venda(10, 10.10M));
            Assert.AreEqual(34, OrdensChances.Venda(10, 10.11M));
            Assert.AreEqual(22, OrdensChances.Venda(10, 10.12M));
            Assert.AreEqual(9, OrdensChances.Venda(10, 10.13M));

            var random = OrdensChances.Venda(10, 10.14M);
            Assert.IsTrue(random > 0 && random < 1);
            random = OrdensChances.Venda(10, 10.20M);
            Assert.IsTrue(random > 0 && random < 1);

            Assert.AreEqual(0, OrdensChances.Venda(10, 10.21M));
            Assert.AreEqual(0, OrdensChances.Venda(10, 10.22M));
            Assert.AreEqual(0, OrdensChances.Venda(10, 10.23M));
        }

        [TestMethod]
        public void OrdensChances_Compras()
        {
            Assert.AreEqual(100, OrdensChances.Compra(10, 10.02M));
            Assert.AreEqual(100, OrdensChances.Compra(10, 10.01M));
            Assert.AreEqual(100, OrdensChances.Compra(10, 10));
            Assert.AreEqual(99, OrdensChances.Compra(10, 9.99M));
            Assert.AreEqual(97, OrdensChances.Compra(10, 9.98M));
            Assert.AreEqual(94, OrdensChances.Compra(10, 9.97M));
            Assert.AreEqual(90, OrdensChances.Compra(10, 9.96M));
            Assert.AreEqual(85, OrdensChances.Compra(10, 9.95M));
            Assert.AreEqual(79, OrdensChances.Compra(10, 9.94M));
            Assert.AreEqual(72, OrdensChances.Compra(10, 9.93M));
            Assert.AreEqual(64, OrdensChances.Compra(10, 9.92M));
            Assert.AreEqual(55, OrdensChances.Compra(10, 9.91M));
            Assert.AreEqual(45, OrdensChances.Compra(10, 9.90M));
            Assert.AreEqual(34, OrdensChances.Compra(10, 9.89M));
            Assert.AreEqual(22, OrdensChances.Compra(10, 9.88M));
            Assert.AreEqual(9, OrdensChances.Compra(10, 9.87M));

            var random = OrdensChances.Compra(10, 9.86M);
            Assert.IsTrue(random > 0 && random < 1);            
            Assert.IsTrue(random > 0 && random < 1);
            random = OrdensChances.Compra(10, 9.80M);
            Assert.IsTrue(random > 0 && random < 1);

            Assert.AreEqual(0, OrdensChances.Compra(10, 9.79M));
            Assert.AreEqual(0, OrdensChances.Compra(10, 9.78M));
            Assert.AreEqual(0, OrdensChances.Compra(10, 9.77M));
        }
    }
}
