using CodingCraftEX06HangFire.Infraestrura.Bovespa;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.ViewModels.Bovespa
{
    /// <summary>
    /// REGISTRO - 01 - COTAÇÕES HISTÓRICAS POR PAPEL-MERCADO
    /// </summary>
    [FixedLengthRecord]
    public class Detail
    {
        /// <summary>
        /// TIPO DE REGISTRO
        /// </summary>
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public int TipReg;

        [FieldFixedLength(8)]
        [FieldConverter(ConverterKind.Date, "yyyyMMdd")]
        public DateTime DataDoPregao;

        /// <summary>
        /// CÓDIGO BDI
        /// UTILIZADO PARA CLASSIFICAR OS PAPÉIS NA EMISSÃO DO BOLETIM DIÁRIO DE INFORMAÇÕES
        /// </summary>
        [FieldFixedLength(2)]
        public string CodBdi;

        /// <summary>
        /// CÓDIGO DE NEGOCIAÇÃO DO PAPEL
        /// </summary>
        [FieldFixedLength(12)]
        public string CodNeg;

        /// <summary>
        /// TIPO DE MERCADO
        /// CÓD. DO MERCADO EM QUE O PAPEL ESTÁ CADASTRADO
        /// </summary>
        [FieldFixedLength(3)]
        public int TpMerc;

        /// <summary>
        /// NOME RESUMIDO DA EMPRESA EMISSORA DO PAPEL
        /// </summary>
        [FieldFixedLength(12)]
        public string NomRes;
        
        /// <summary>
        /// ESPECIFICAÇÃO DO PAPEL
        /// </summary>
        [FieldFixedLength(10)]
        public string Especi;

        /// <summary>
        /// PRAZO EM DIAS DO MERCADO A TERMO
        /// </summary>
        [FieldFixedLength(3)]
        public string PrazoT;

        /// <summary>
        /// MOEDA DE REFERÊNCIA
        /// </summary>
        [FieldFixedLength(4)]
        public string ModRef;

        /// <summary>
        /// PREÇO DE ABERTURA DO PAPELMERCADO NO PREGÃO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreAbe;

        /// <summary>
        /// PREÇO MÁXIMO DO PAPELMERCADO NO PREGÃO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreMax;

        /// <summary>
        /// PREÇO MÍNIMO DO PAPELMERCADO NO PREGÃO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreMin;

        /// <summary>
        /// PREÇO MÉDIO DO PAPELMERCADO NO PREGÃO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreMed;

        /// <summary>
        /// PREÇO DO ÚLTIMO NEGÓCIO DO PAPEL-MERCADO NO PREGÃO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreUlt;

        [FieldFixedLength(245-121)]
        public string resto;
    }
}