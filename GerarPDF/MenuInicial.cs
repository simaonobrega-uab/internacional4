namespace GerarPDF;

public class MenuInicial : Form
{
    private Button botaoFormularioA;
    private Button botaoFormularioB;
    private Controller controller;
    public event Action<TipoFormulario> DocumentoSolicitado;

    public MenuInicial(Controller c)
    {
        controller = c;
        DocumentoSolicitado += controller.AbrirDocumento;
        
        botaoFormularioA = new Button
        {
            Text = "Formulário A",
            Location = new Point(50, 50),
            Width = 180,
            Height = 40
        };
        botaoFormularioA.Click += (sender, e) => BotaoAberturaNovoFormularioPressionado(TipoFormulario.FormularioA);
        Controls.Add(botaoFormularioA);

        botaoFormularioB = new Button
        {
            Text = "Formulário B",
            Location = new Point(50, 100),
            Width = 180,
            Height = 40 
        };
        botaoFormularioB.Click += (sender, e) => BotaoAberturaNovoFormularioPressionado(TipoFormulario.FormularioB);
        Controls.Add(botaoFormularioB);

        CentrarBotoes();
    }
    
    private void CentrarBotoes()
    {
        int centerX = ClientSize.Width / 2;
        int centerY = ClientSize.Height / 2;
        int buttonSpacing = 20;

        botaoFormularioA.Location = new Point(centerX - botaoFormularioA.Width / 2, centerY - botaoFormularioA.Height - buttonSpacing / 2);
        botaoFormularioB.Location = new Point(centerX - botaoFormularioB.Width / 2, centerY + buttonSpacing / 2);
    }

    private void BotaoAberturaNovoFormularioPressionado(TipoFormulario tipoFormulario)
    {
        DocumentoSolicitado(tipoFormulario);
    }
}