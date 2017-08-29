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

        [FieldFixedLength(4)]
        public string CodigoDoArquivo;

        [FieldFixedLength(4)]
        public string CodigoDoUsuario;

        [FieldFixedLength(8)]
        public string CodigoDaOrigem;

        [FieldFixedLength(4)]
        public int CodigoDoDestino;

        [FieldFixedLength(8)]
        [FieldConverter(ConverterKind.Date, "YYYYMMDD")]
        public DateTime DataDaGeracaoDoArquivo;

        [FieldFixedLength(8)]
        [FieldConverter(ConverterKind.Date, "YYYYMMDD")]
        public DateTime DataDoPregao;

        [FieldFixedLength(4)]
        [FieldConverter(typeof(TimeSpanConverter))]
        public TimeSpan HoraDeGeracao;

        [FieldFixedLength(308)]
        public string Reserva;
    }
}