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
    [FixedLengthRecord(FixedMode.AllowMoreChars)]
    public class Detail
    {
        /// <summary>
        /// TIPO DE REGISTRO
        /// </summary>
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public int TipReg { get; set; }

        [FieldFixedLength(8)]
        [FieldConverter(ConverterKind.Date, "yyyyMMdd")]
        public DateTime DataDoPregao { get; set; }

        /// <summary>
        /// CÓDIGO BDI
        /// UTILIZADO PARA CLASSIFICAR OS PAPÉIS NA EMISSÃO DO BOLETIM DIÁRIO DE INFORMAÇÕES
        /// </summary>
        [FieldFixedLength(2)]
        public string CodBdi { get; set; }

        /// <summary>
        /// CÓDIGO DE NEGOCIAÇÃO DO PAPEL
        /// </summary>
        [FieldFixedLength(12)]
        public string CodNeg { get; set; }

        /// <summary>
        /// TIPO DE MERCADO
        /// CÓD. DO MERCADO EM QUE O PAPEL ESTÁ CADASTRADO
        /// </summary>
        [FieldFixedLength(3)]
        public int TpMerc { get; set; }

        /// <summary>
        /// NOME RESUMIDO DA EMPRESA EMISSORA DO PAPEL
        /// </summary>
        [FieldFixedLength(12)]
        public string NomRes { get; set; }

        /// <summary>
        /// ESPECIFICAÇÃO DO PAPEL
        /// </summary>
        [FieldFixedLength(10)]
        public string Especi { get; set; }

        /// <summary>
        /// PRAZO EM DIAS DO MERCADO A TERMO
        /// </summary>
        [FieldFixedLength(3)]
        public string PrazoT { get; set; }

        /// <summary>
        /// MOEDA DE REFERÊNCIA
        /// </summary>
        [FieldFixedLength(4)]
        public string ModRef { get; set; }

        /// <summary>
        /// PREÇO DE ABERTURA DO PAPELMERCADO NO PREGÃO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreAbe { get; set; }

        /// <summary>
        /// PREÇO MÁXIMO DO PAPELMERCADO NO PREGÃO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreMax { get; set; }

        /// <summary>
        /// PREÇO MÍNIMO DO PAPELMERCADO NO PREGÃO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreMin { get; set; }

        /// <summary>
        /// PREÇO MÉDIO DO PAPELMERCADO NO PREGÃO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreMed { get; set; }

        /// <summary>
        /// PREÇO DO ÚLTIMO NEGÓCIO DO PAPEL-MERCADO NO PREGÃO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreUlt { get; set; }

        /// <summary>
        /// PREÇO DA MELHOR OFERTA DE COMPRA DO PAPELMERCADO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreOfc { get; set; }

        /// <summary>
        /// PREÇO DA MELHOR OFERTA DE VENDA DO PAPELMERCADO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreOfv { get; set; }

        /// <summary>
        /// NÚMERO DE NEGÓCIOS EFETUADOS COM O PAPEL- MERCADO NO PREGÃO
        /// </summary>
        [FieldFixedLength(5)]
        public int TotNeg { get; set; }

        /// <summary>
        /// QUANTIDADE TOTAL DE TÍTULOS NEGOCIADOS NESTE PAPEL- MERCADO
        /// </summary>
        [FieldFixedLength(18)]
        public Int64 QuatTot { get; set; }

        /// <summary>
        /// VOLUME TOTAL DE TÍTULOS NEGOCIADOS NESTE PAPEL- MERCADO
        /// </summary>
        [FieldFixedLength(18)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal VolTot { get; set; }

        /// <summary>
        /// PREÇO DE EXERCÍCIO PARA O MERCADO DE OPÇÕES OU VALOR DO CONTRATO PARA O MERCADO DE TERMO SECUNDÁRIO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PreExe { get; set; }

        /// <summary>
        /// INDICADOR DE CORREÇÃO DE PREÇOS DE EXERCÍCIOS OU VALORES DE CONTRATO PARA OS MERCADOS DE OPÇÕES OU TERMO SECUNDÁRIO
        /// </summary>
        [FieldFixedLength(1)]
        public int IndOpc { get; set; }

        /// <summary>
        /// DATA DO VENCIMENTO PARA OS MERCADOS DE OPÇÕES OU TERMO SECUNDÁRIO
        /// </summary>
        [FieldFixedLength(8)]
        [FieldConverter(ConverterKind.Date, "yyyyMMdd")]
        public DateTime DataVen { get; set; }

        /// <summary>
        /// FATOR DE COTAÇÃO DO PAPEL
        /// </summary>
        [FieldFixedLength(7)]
        public int FatCot { get; set; }

        /// <summary>
        /// PREÇO DE EXERCÍCIO EM PONTOS PARA OPÇÕES REFERENCIADAS EM DÓLAR OU VALOR DE CONTRATO EM PONTOS PARA TERMO SECUNDÁRIO
        /// </summary>
        [FieldFixedLength(13)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PtoExe { get; set; }

        /// <summary>
        /// CÓDIGO DO PAPEL NO SISTEMA ISIN OU CÓDIGO INTERNO DO PAPEL
        /// </summary>
        [FieldFixedLength(12)]
        public string CodIsi { get; set; }

        /// <summary>
        /// - NÚMERO DE DISTRIBUIÇÃO DO PAPEL
        /// </summary>
        [FieldFixedLength(3)]
        public int DisMes { get; set; }
    }
}