using GerarPDF.Interfaces;

namespace GerarPDF;
using PdfSharp.Pdf;

public interface ILayoutBuilder
{
    void ConstruirLayout(PdfDocument documento, string titulo, ICampos campos);

}