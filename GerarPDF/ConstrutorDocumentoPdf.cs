using GerarPDF.Interfaces;
using PdfSharp.Pdf;

namespace GerarPDF;

public class ConstrutorDocumentoPdf
{
    private string _tipoDocumento;
    private string _titulo;
    private ICampos _campos;
    private PdfDocument _documento;
    
    public void DefinirTipoDocumento(TipoDeDocumento tipoDocumento)
    {
        _tipoDocumento = tipoDocumento.GetTitulo();
    }

    // Restante código necessário à criação do PDF
    // (eliminado para demonstração das interfaces)
    
    
    public void DefinirTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("Titulo não pode ser nulo ou em branco.", nameof(titulo));
        _titulo = titulo;
    }

    public void AdicionarCampos(ICampos campos)
    {
        _campos = campos ?? throw new ArgumentNullException(nameof(campos));
    }

    public PdfDocument Build(ILayoutBuilder layoutBuilder)
    {
        _documento = new PdfDocument();
        _documento.Info.Title = _tipoDocumento;
        layoutBuilder.ConstruirLayout(_documento, _titulo, _campos);
        return _documento;
    }
}