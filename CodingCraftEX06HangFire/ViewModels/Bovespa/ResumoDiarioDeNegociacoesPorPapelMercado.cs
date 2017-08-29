using FileHelpers;

namespace CodingCraftEX06HangFire.ViewModels.Bovespa
{
    /// <summary>
    /// Registro - 02 - Resumo Diário de Negociações por Papel - Mercado
    /// </summary>
    [FixedLengthRecord]
    public class ResumoDiarioDeNegociacoesPorPapelMercado
    {
        [FieldFixedLength(2)]
        public int TipoDeRegistro;
    }

}