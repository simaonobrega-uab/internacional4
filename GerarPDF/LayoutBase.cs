using GerarPDF.Interfaces;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace GerarPDF;

// - Define o layout base dos cartões disponíveis.
// - Por agora, é dada somente a possibilidade de definir a cor de fundo a partir do método
//   DefinirCorFundo(). Criar novos métodos abstractos caso seja necessária maior flexibilidade.
public abstract class LayoutBase : ILayoutBuilder
{
    public void ConstruirLayout(PdfDocument documento, string titulo, ICampos campos)
    {
        XColor corFundo = DefinirCorFundo();
        documento.Info.Title = titulo;

        PdfPage page = documento.AddPage();
        page.Width = XUnit.FromMillimeter(74);
        page.Height = XUnit.FromMillimeter(52);
        
        page.Orientation = PageOrientation.Portrait;

        XGraphics gfx = XGraphics.FromPdfPage(page);

        gfx.DrawString(titulo, new XFont("Verdana", 16), XBrushes.Black, new XRect(0, 0, page.Width, 50),
            XStringFormats.Center);

        XRect rect = new XRect(0, 0, page.Width, page.Height);

        XSolidBrush brush = new XSolidBrush(corFundo);
        gfx.DrawRectangle(brush, rect);

        XFont fontTitulo = new XFont("Verdana", 10);
        XFont font = new XFont("Verdana", 8);
        XBrush brush2 = XBrushes.Black;

        gfx.DrawString(documento.Info.Title, fontTitulo, brush2, new XRect(0, 0, page.Width - 20, 20),
            XStringFormats.TopCenter);

        int posicaoCampo = 25;
        foreach (var campo in campos)
        {
            gfx.DrawString($"{campo.Nome}: {campo.ValorFormatado}", font, brush2,
                new XRect(10, posicaoCampo, page.Width - 20, 20), XStringFormats.TopLeft);
            posicaoCampo += 25;
        }

        XRect rect2 = new XRect(0, page.Height - 5, page.Width, 5);
        XSolidBrush brush3 = new XSolidBrush(XColor.FromArgb(255, 255, 255));
        gfx.DrawRectangle(brush3, rect2);
    }

    // Método a ser implementado pela própria classe
    protected abstract XColor DefinirCorFundo();
}
