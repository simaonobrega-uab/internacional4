namespace GerarPDF;

public class Controller
{
    Model model;
    View view;
    MenuInicial menuInicial;

    public event Action MostrarMenuInicialSolicitado;
    public event Action<TipoFormulario> AbrirFormularioSolicitado;
    public event Func<Dictionary<string, string>> DadosFormularioSolicitado;
    public event Action<Dictionary<string, string>> ValidacaoDadosFormularioSolicitado;
   

    public Controller()
    {
        model = new Model(this, view);
        view = new View(this, model);

        MostrarMenuInicialSolicitado += view.MostarMenuInicialSolicitado;
        AbrirFormularioSolicitado += view.AbrirNovoFormulario;
        ValidacaoDadosFormularioSolicitado += model.ValidarInputDados;
        DadosFormularioSolicitado += () => view.FormularioAberto().DisponibilizarDados();

    }

    public void IniciarPrograma()
    {
        MostrarMenuInicialSolicitado?.Invoke();
    }

    public void AbrirDocumento(TipoFormulario tipoFormulario)
    {
        AbrirFormularioSolicitado(tipoFormulario);
    }

    public void GerarPdf()
    {
        var dadosFormulario = DadosFormularioSolicitado?.Invoke();
        ValidacaoDadosFormularioSolicitado?.Invoke(dadosFormulario);
        // Gerar PDF
    }
}

