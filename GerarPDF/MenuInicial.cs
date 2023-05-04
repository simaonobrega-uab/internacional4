namespace GerarPDF;

public class MenuInicial : Form
{
    private Button botaoFormularioA;
    private Button botaoFormularioB;
    private Controller controller;
    
    public delegate void DocumentoSolicitadoEventHandler(TipoDeDocumento tipoDeDocumento);
    public event DocumentoSolicitadoEventHandler DocumentoSolicitado;
    
    public MenuInicial(Controller c)
    {
        controller = c;
        DocumentoSolicitado += controller.SolicitarAberturaFormulario;
        
        botaoFormularioA = new Button
        {
            Text = "Formulário A",
            Location = new Point(50, 50),
            Width = 180,
            Height = 40
        };
        botaoFormularioA.Click += (sender, e) => BotaoAberturaFormularioClick(TipoDeDocumento.CartaoVisita);
        Controls.Add(botaoFormularioA);

        botaoFormularioB = new Button
        {
            Text = "Formulário B",
            Location = new Point(50, 100),
            Width = 180,
            Height = 40 
        };
        botaoFormularioB.Click += (sender, e) => BotaoAberturaFormularioClick(TipoDeDocumento.PasseServico);
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

    private void BotaoAberturaFormularioClick(TipoDeDocumento tipoDeDocumento)
    {
        DocumentoSolicitado(tipoDeDocumento);
    }
}