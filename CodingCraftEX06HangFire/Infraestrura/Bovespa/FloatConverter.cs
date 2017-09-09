using FileHelpers;
using System;

namespace CodingCraftEX06HangFire.Infraestrura.Bovespa
{
    public class FloatConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            return float.Parse(from) / 100;
        }

        public override string FieldToString(object fieldValue)
        {
            return ((float)fieldValue).ToString("#.##").Replace(".", "");
        }

    }
}