namespace GerarPDF;

public enum TipoFormulario
{
    FormularioA,
    FormularioB,
}

public class View
{
    private Controller controller;
    private Model model;
    private MenuInicial menuInicial;
    private FormularioBase formularioAberto;

    internal View(Controller c, Model m)
    {
        controller = c;
        model = m;
    }

    public void MostarMenuInicialSolicitado()
    {
        menuInicial = new MenuInicial(controller);
        menuInicial.Show();
    }
    
    public void AbrirNovoFormulario(TipoFormulario tipoFormulario)
    {
        FormularioBase novoFormulario = null;
        switch (tipoFormulario)
        {
            case TipoFormulario.FormularioA:
                novoFormulario = new FormularioA(controller);
                break;
            case TipoFormulario.FormularioB:
                novoFormulario = new FormularioB(controller);
                break;
        }
        formularioAberto = novoFormulario;
        novoFormulario.Show();
    }
    
    public FormularioBase FormularioAberto()
    {
        return formularioAberto;
    }
}