using System.Diagnostics;

namespace GerarPDF;

public enum TipoDeDocumento
{
    CartaoVisita,
    PasseServico,
}

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
    
    protected delegate void GerarPdfSolicitadoEventHandler();
    protected event GerarPdfSolicitadoEventHandler GerarPdfSolicitado;

    
    internal View(Controller controller, Model model)
    {
        _controller = controller;
        _model = model;
        
        CamposInvalidosSolicitados += () => _model.EnviarCamposInValidos();
        DocumentoSolicitado += controller.SolicitarAberturaFormulario;
        GerarPdfSolicitado += controller.GerarPdf;

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
   

    public void ApresentarPdf(string nomeDocumento)
    {
        Process.Start(new ProcessStartInfo(nomeDocumento) {UseShellExecute = true});
    }
}