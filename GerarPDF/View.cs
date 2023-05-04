using System.Diagnostics;

namespace GerarPDF;

public enum TipoDeDocumento
{
    CartaoVisita,
    PasseServico,
}

public class View
{
    private Controller controller;
    private Model model;
    private MenuInicial menuInicial;
    private FormularioBase formularioAberto;
    public TipoDeDocumento tipoDeDocumentoAberto;

    public delegate List<string> SolicitarCamposIncorretosSolicitadoEventHandler();
    public event SolicitarCamposIncorretosSolicitadoEventHandler CamposInvalidosSolicitados;

    
    internal View(Controller c, Model m)
    {
        controller = c;
        model = m;
        
        CamposInvalidosSolicitados += () => model.EnviarCamposIncorretos();
    }
    
    // Disponibiliza o Menu Inicial
    public void ActivarInterface()
    {
        menuInicial = new MenuInicial(controller);
        menuInicial.Show();
    }
    
    // Abertura do formulario escolhido no Menu Inicial
    public void AbrirFormDocumento(TipoDeDocumento tipoDeDocumento)
    {
        FormularioBase novoFormulario = null;
        switch (tipoDeDocumento)
        {
            case TipoDeDocumento.CartaoVisita:
                novoFormulario = new CartaoVisita(controller);
                tipoDeDocumentoAberto = TipoDeDocumento.CartaoVisita;
                break;
            case TipoDeDocumento.PasseServico:
                novoFormulario = new PasseServico(controller);
                tipoDeDocumentoAberto = TipoDeDocumento.PasseServico;
                break;
        }
        formularioAberto = novoFormulario;
        novoFormulario.Show();
    }
    
    public FormularioBase FormularioAberto()
    {
        return formularioAberto;
    }
    
    public void MostrarCamposFalhou()
    {
        var dadosInvalidos = CamposInvalidosSolicitados?.Invoke();
        formularioAberto.ApresentarCamposInvalidos(dadosInvalidos);
    }

    public void ApresentarPdf(string nomeDocumento)
    {
        Process.Start(new ProcessStartInfo(nomeDocumento) { UseShellExecute = true });
    }
}