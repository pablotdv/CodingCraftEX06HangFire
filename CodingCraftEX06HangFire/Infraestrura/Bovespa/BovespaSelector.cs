using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.Infraestrura.Bovespa
{
    public class BovespaSelector
    {
        public static Type Selector(MultiRecordEngine engine, string recordLine)
        {
            if (recordLine.StartsWith("00"))
                return typeof(ViewModels.Bovespa.Header);
            else if (recordLine.StartsWith("01"))
                return typeof(ViewModels.Bovespa.Detail);
            else if (recordLine.StartsWith("99"))
                return typeof(ViewModels.Bovespa.Trailer);

            return null;
        }
    }
}