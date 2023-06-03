namespace GerarPDF;

public class MenuInicial : Form
{

    private View view;
    
    public MenuInicial()
    {
        CriaBotaoFormulario(nomeBotao: "Cartão de visita", posicao: 50,
            tipoDeDocumento: TipoDeDocumento.CartaoVisita);

        CriaBotaoFormulario(nomeBotao: "Passe de serviço", posicao: 100,
            tipoDeDocumento: TipoDeDocumento.PasseServico);

    }
    
    public View View {get => view; set => view = value; }

    private void BotaoAberturaFormularioClick(TipoDeDocumento tipoDeDocumento)
    {
        view.BotaoAbertuaFormularioClick(tipoDeDocumento);
    }

    private void CriaBotaoFormulario(string nomeBotao, int posicao, TipoDeDocumento tipoDeDocumento)
    {
        var botaoFormulario = new Button
        {
            Text = nomeBotao,
            Location = new Point(50, posicao),
            Width = 180,
            Height = 40
        };
        botaoFormulario.Click += (sender, e) => BotaoAberturaFormularioClick(tipoDeDocumento);
        Controls.Add(botaoFormulario);

    }
}