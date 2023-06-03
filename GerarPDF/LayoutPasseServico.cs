using PdfSharp.Drawing;
namespace GerarPDF;

// Define os parâmetros específicos do Layout do Pdf "Passe de Serviço"
public class LayoutPasseServico : LayoutBase
{

    protected override XColor DefinirCorFundo()
    {
        return XColor.FromArgb(245, 241, 200);
    }
  
}