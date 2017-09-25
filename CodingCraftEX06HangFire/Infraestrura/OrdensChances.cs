using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.Infraestrura
{
    public class OrdensChances
    {
        public static decimal Venda(decimal preco, decimal venda)
        {
            if (venda <= preco)
                return 100;

            return Chance(preco, venda);            
        }        

        public static decimal Compra(decimal preco, decimal compra)
        {
            if (compra >= preco)
                return 100;

            return Chance(preco, compra);
        }

        private static decimal Chance(decimal valor1, decimal valor2)
        {
            var diferenca = Math.Abs((valor1 - valor2) * 100);
            
            if (diferenca > 20)
                return 0;

            if (diferenca < 14)
            {
                decimal chance = 100;
                decimal decremento = 0;

                for (int i = 1; i <= diferenca; i++)
                {
                    decremento += i;
                }

                return chance - decremento;
            }


            var rd = new Random((int)DateTime.Now.Ticks);
            return (decimal)rd.NextDouble();
        }
    }
}