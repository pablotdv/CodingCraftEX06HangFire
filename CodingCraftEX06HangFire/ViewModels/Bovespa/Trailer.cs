using System;
using FileHelpers;

namespace CodingCraftEX06HangFire.ViewModels.Bovespa
{
    /// <summary>
    /// Registro - 99 - Trailer
    /// </summary>
    [FixedLengthRecord(FixedMode.AllowMoreChars)]
    public class Trailer
    {
        /// <summary>
        /// TIPO DE REGISTRO
        /// </summary>
        [FieldFixedLength(2)]
        public int TipoDeRegistro;

        /// <summary>
        /// NOME DO ARQUIVO
        /// </summary>
        [FieldFixedLength(13)]
        public string NomeDoArquivo;

        /// <summary>
        /// CÓDIGO DA ORIGEM
        /// </summary>
        [FieldFixedLength(8)]
        public string CodigoDaOrigem;

        /// <summary>
        /// DATA DA GERAÇÃO DO ARQUIVO
        /// </summary>
        [FieldFixedLength(8)]
        [FieldConverter(ConverterKind.Date, "yyyyMMdd")]
        public DateTime DataDaGeracaoDoArquivo;

        /// <summary>
        /// TOTAL DE REGISTROS
        /// </summary>
        [FieldFixedLength(11)]
        public int TotalDeRegistros;

        /// <summary>
        /// RESERVA
        /// </summary>
        [FieldFixedLength(203)]
        public string Reserva;
    }

}