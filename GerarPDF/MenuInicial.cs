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
            Text = "Cartão de visita",
            Location = new Point(50, 50),
            Width = 180,
            Height = 40
        };
        _botaoFormularioA.Click += (sender, e) => BotaoAberturaFormularioClick(TipoDeDocumento.CartaoVisita);
        Controls.Add(_botaoFormularioA);

        _botaoFormularioB = new Button
        {
            Text = "Passe de serviço",
            Location = new Point(50, 100),
            Width = 180,
            Height = 40 
        };
        _botaoFormularioB.Click += (sender, e) => BotaoAberturaFormularioClick(TipoDeDocumento.PasseServico);
        Controls.Add(_botaoFormularioB);

    }

    private void BotaoAberturaFormularioClick(TipoDeDocumento tipoDeDocumento)
    {
        DocumentoSolicitado(tipoDeDocumento);
    }
}