using PdfSharp.Drawing;
namespace GerarPDF;

// Define os parâmetros específicos do Layout do Pdf "Cartão de Visita"
public class LayoutCartaoVisita : LayoutBase
{

  protected override XColor DefinirCorFundo()
  {
    return XColor.FromArgb(245, 241, 227);
  }
  
}
