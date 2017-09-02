using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.ViewModels.Bovespa
{
    /// <summary>
    /// Registro - 00 - Header
    /// </summary>
    [FixedLengthRecord]
    public class Header
    {
        [FieldFixedLength(2)]
        public int TipoDeRegistro;

        [FieldFixedLength(13)]
        public string NomeDoArquivo;

        [FieldFixedLength(8)]
        public string CodigoDaOrigem;
                
        [FieldFixedLength(8)]
        [FieldConverter(ConverterKind.Date, "yyyyMMdd")]
        public DateTime DataDaGeracaoDoArquivo;
        
        [FieldFixedLength(214)]
        public string Reserva;
    }
}