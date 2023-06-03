using System.Diagnostics;
using GerarPDF.Interfaces;
using PdfSharp.Pdf;

namespace GerarPDF;

public class View
{
    private readonly Controller _controller;
    private readonly Model _model;
    private MenuInicial _menuInicial;
    private FormularioBase _formularioAberto;
    public TipoDeDocumento TipoDeDocumentoAberto;

    public delegate List<string> SolicitarCamposIncorretosSolicitadoEventHandler();
    public event SolicitarCamposIncorretosSolicitadoEventHandler CamposInvalidosSolicitados;
    
    public delegate void DocumentoSolicitadoEventHandler(TipoDeDocumento tipoDeDocumento);
    public event DocumentoSolicitadoEventHandler DocumentoSolicitado;
    
    public delegate void GerarPdfSolicitadoEventHandler();
    public event GerarPdfSolicitadoEventHandler GerarPdfSolicitado;

    
    internal View()
    {
    }
    
    public void BotaoGerarPdf_Click()
    {
        GerarPdfSolicitado?.Invoke();
    }
    
    // Disponibiliza o Menu Inicial
    public void ActivarInterface()
    {
        _menuInicial = new MenuInicial();
        _menuInicial.View = this;
        _menuInicial.Show();
    }
    
    // Abertura do formulario escolhido no Menu Inicial
    public void AbrirFormDocumento(TipoDeDocumento tipoDeDocumento)
    {
        FormularioBase? novoFormulario = null;
        switch (tipoDeDocumento)
        {
            case TipoDeDocumento.CartaoVisita:
                novoFormulario = new CartaoVisita(_controller, this);
                TipoDeDocumentoAberto = TipoDeDocumento.CartaoVisita;
                break;
            case TipoDeDocumento.PasseServico:
                novoFormulario = new PasseServico(_controller, this);
                TipoDeDocumentoAberto = TipoDeDocumento.PasseServico;
                break;
            default:
                throw new InvalidOperationException("Documento desconhecido.");

        }
        _formularioAberto = novoFormulario;
        novoFormulario.Show();
    }
    
    public FormularioBase FormularioAberto()
    {
        return _formularioAberto;
    }
    
    // Solicita uma indicação visual dos campos inválidos
    public void MostrarCamposInvalidos()
    {
        var dadosInvalidos = CamposInvalidosSolicitados?.Invoke();
        if (dadosInvalidos != null)
        {
            _formularioAberto.ApresentarCamposInvalidos(dadosInvalidos);
        }
        
    }
    public void BotaoAbertuaFormularioClick(TipoDeDocumento tipoDeDocumento)
    {
        DocumentoSolicitado(tipoDeDocumento);
    }
   

    // Gera e retorna um documento PDF
    public PdfDocument GerarPdf(ICampos dados)
    {

        // Cria o layout adequado com base no tipo de documento/formulário
        ILayoutBuilder layoutBuilder = EscolherLayoutBuilder(TipoDeDocumentoAberto);
        
        // Serviço de geração de PDF
        GeradorPdf geraradorPdf = new GeradorPdf(dados, TipoDeDocumentoAberto, layoutBuilder);
        
        // Cria e retorna o Pdf final
        return geraradorPdf.CriaPdf();
    }
    
    // Define o layout adequado com base no tipo de documento/formulário
    private ILayoutBuilder EscolherLayoutBuilder(TipoDeDocumento tipoDocumento)
    {
        
        if (tipoDocumento == TipoDeDocumento.CartaoVisita)
        {
            return new LayoutCartaoVisita();
        }
        
        if (tipoDocumento == TipoDeDocumento.PasseServico)
        {
            return new LayoutPasseServico();
        }

        // todo: definir um Layout padrão
        return null;
    }
    
    public void ApresentarPdf(string nomeDocumento)
    {
        Process.Start(new ProcessStartInfo(nomeDocumento) {UseShellExecute = true});
    }
    
    public void MostrarErro(string mensagemErro)
    {
        MessageBox.Show(mensagemErro, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
