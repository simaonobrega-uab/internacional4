namespace GerarPDF;

public class MenuInicial : Form
{
    private readonly Button _botaoFormularioA;
    private readonly Button _botaoFormularioB;
    private Controller controller;
    
    public delegate void DocumentoSolicitadoEventHandler(TipoDeDocumento tipoDeDocumento);
    public event DocumentoSolicitadoEventHandler DocumentoSolicitado;
    
    public MenuInicial(Controller c)
    {
        controller = c;
        DocumentoSolicitado += controller.SolicitarAberturaFormulario;
        
        _botaoFormularioA = new Button
        {
            Text = "Formulário A",
            Location = new Point(50, 50),
            Width = 180,
            Height = 40
        };
        _botaoFormularioA.Click += (sender, e) => BotaoAberturaFormularioClick(TipoDeDocumento.CartaoVisita);
        Controls.Add(_botaoFormularioA);

        _botaoFormularioB = new Button
        {
            Text = "Formulário B",
            Location = new Point(50, 100),
            Width = 180,
            Height = 40 
        };
        _botaoFormularioB.Click += (sender, e) => BotaoAberturaFormularioClick(TipoDeDocumento.PasseServico);
        Controls.Add(_botaoFormularioB);

        CentrarBotoes();
    }
    
    private void CentrarBotoes()
    {
        int centerX = ClientSize.Width / 2;
        int centerY = ClientSize.Height / 2;
        int buttonSpacing = 20;

        _botaoFormularioA.Location = new Point(centerX - _botaoFormularioA.Width / 2, centerY - _botaoFormularioA.Height - buttonSpacing / 2);
        _botaoFormularioB.Location = new Point(centerX - _botaoFormularioB.Width / 2, centerY + buttonSpacing / 2);
    }

    private void BotaoAberturaFormularioClick(TipoDeDocumento tipoDeDocumento)
    {
        DocumentoSolicitado(tipoDeDocumento);
    }
}