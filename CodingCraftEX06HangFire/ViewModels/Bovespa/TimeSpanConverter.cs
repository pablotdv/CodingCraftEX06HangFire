using FileHelpers;
using System;

namespace CodingCraftEX06HangFire.ViewModels.Bovespa
{
    public class TimeSpanConverter : ConverterBase
    {
        public override string FieldToString(object from)
        {
            return base.FieldToString(from);
        }

        public override object StringToField(string from)
        {
            int hora = int.Parse(from.Substring(0, 2));
            int minutos = int.Parse(from.Substring(2, 2));
            return new TimeSpan(hora, minutos, 0);
        }
    }
}