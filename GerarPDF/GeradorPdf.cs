using GerarPDF.Interfaces;
using PdfSharp.Pdf;

namespace GerarPDF
{
    public class GeradorPdf
    {
        private readonly ICampos _dados;
        private readonly TipoDeDocumento _tipoDocumento;
        private readonly ILayoutBuilder _layoutBuilder;

        public GeradorPdf(ICampos dados, TipoDeDocumento tipoDocumento, ILayoutBuilder layoutBuilder)
        {
            _dados = dados;
            _tipoDocumento = tipoDocumento;
            _layoutBuilder = layoutBuilder;
        }

        public PdfDocument CriaPdf()
        {
            // Cria o builder
            ConstrutorDocumentoPdf builder = new ConstrutorDocumentoPdf();

            // Define o tipo de documento
            builder.DefinirTipoDocumento(_tipoDocumento);

            // Define as informações do documento
            builder.DefinirTitulo(_tipoDocumento.GetTitulo());
            builder.AdicionarCampos(_dados);

            // Constrói o documento PDF
            PdfDocument documento = builder.Build(_layoutBuilder);

            return documento;
        }
    }
}