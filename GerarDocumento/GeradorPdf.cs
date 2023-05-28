using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace GerarPDF
{
    public class GeradorPdf
    {
        private readonly Dictionary<string, string> _dados;
        private readonly string _tipoDocumento;

        public GeradorPdf(Dictionary<string, string> dados, string tipoDocumento)
        {
            _dados = dados;
            _tipoDocumento = tipoDocumento;
        }

        public PdfDocument CriaPdf()
        {
            // Cria o builder
            PdfDocumentBuilder builder = new PdfDocumentBuilder();

            // Define o tipo de documento
            builder.DefinirTipoDocumento(_tipoDocumento);

            // Define as informações do cartão
            builder.DefinirTitulo("DOC Corporativo");
            builder.AdicionarCampos(_dados);

            // Define o layout do documento
            builder.DefinirLayout();

            // Constrói o documento PDF
            PdfDocument documento = builder.Build();

            return documento;
        }
    }

    public class PdfDocumentBuilder
    {
        private string _tipoDocumento;
        private string _titulo;
        private Dictionary<string, string> _campos;

        private PdfDocument _documento;

        public void DefinirTipoDocumento(string tipoDocumento)
        {
            _tipoDocumento = tipoDocumento;
        }

        public void DefinirTitulo(string titulo)
        {
            _titulo = titulo;
        }

        public void AdicionarCampos(Dictionary<string, string> campos)
        {
            _campos = campos;
        }

        public void DefinirLayout()
        {
            _documento = new PdfDocument();
            _documento.Info.Title = _tipoDocumento;

            PdfPage page = _documento.AddPage();
            page.Width = XUnit.FromMillimeter(74);
            page.Height = XUnit.FromMillimeter(52);
            page.Orientation = PageOrientation.Portrait;

            XGraphics gfx = XGraphics.FromPdfPage(page);
            
            gfx.DrawString(_titulo, new XFont("Verdana", 16), XBrushes.Black, new XRect(0, 0, page.Width, 50),
                XStringFormats.Center);

            XRect rect = new XRect(0, 0, page.Width, page.Height);
            XColor corFundo = XColor.FromArgb(245, 241, 227);
            XSolidBrush brush = new XSolidBrush(corFundo);
            gfx.DrawRectangle(brush, rect);

            XFont fontTitulo = new XFont("Verdana", 10);
            XFont font = new XFont("Verdana", 8);
            XBrush brush2 = XBrushes.Black;

            gfx.DrawString(_documento.Info.Title, fontTitulo, brush2, new XRect(0, 0, page.Width - 20, 20),
                XStringFormats.TopCenter);

            int posicaoCampo = 25;
            foreach (var campo in _campos)
            {
                gfx.DrawString($"{campo.Key}: {campo.Value}", font, brush2,
                    new XRect(10, posicaoCampo, page.Width - 20, 20), XStringFormats.TopLeft);
                posicaoCampo += 25;
            }

            XRect rect2 = new XRect(0, page.Height - 5, page.Width, 5);
            XSolidBrush brush3 = new XSolidBrush(XColor.FromArgb(255, 255, 255));
            gfx.DrawRectangle(brush3, rect2);
        }

        public PdfDocument Build()
        {
            return _documento;
        }
    }
}