namespace GerarPDF;

using PdfSharp.Pdf;

// Controlador da aplicação Gerar PDF.
public class Controller
{
    Model model;
    View view;
    MenuInicial menuInicial;

    public delegate void ProgramaIniciadoEventHandler();

    public event ProgramaIniciadoEventHandler ProgramaIniciado;

    public delegate void AberturaFormDocumentoSolicitadoEventHandler(TipoDeDocumento tipoDeDocumento);

    public event AberturaFormDocumentoSolicitadoEventHandler AberturaFormDocumentoSolicitado;

    public delegate Dictionary<string, string> DadosFormularioSolicitadoEventHandler();

    public event DadosFormularioSolicitadoEventHandler DadosFormularioSolicitado;

    public delegate bool DadosFormularioRecebidosEventHandler(Dictionary<string, string> dados);

    public event DadosFormularioRecebidosEventHandler DadosFormularioRecebidos;

    public delegate void DadosFormularioInvalidosEventHandler();

    public event DadosFormularioInvalidosEventHandler ValidacaoFormularioFalhou;

    public delegate PdfDocument GerarPDFEventHandler(Dictionary<string, string> dados, string tipoDocumento);

    public event GerarPDFEventHandler ValidacaoFormularioBemSucedida;

    public delegate void DocumentoSalvoEventHandler(string nomeDocumento);

    public event DocumentoSalvoEventHandler DocumentoSalvo;

    public Controller()
    {
        // Conexão dos componentes do estilo arq. MVC
        model = new Model();
        view = new View(this, model);


        // Associação dos métodos relevantes aos eventos do controlador
        ProgramaIniciado += view.ActivarInterface;
        AberturaFormDocumentoSolicitado += view.AbrirFormDocumento;
        DadosFormularioSolicitado += () => view.FormularioAberto().EnviarDados();
        DadosFormularioRecebidos += model.ValidarInputDados;
        ValidacaoFormularioFalhou += view.MostrarCamposFalhou;
        ValidacaoFormularioBemSucedida += (dados,tipoDocumento) => view.FormularioAberto().GerarPdf(dados,tipoDocumento);
        DocumentoSalvo += nomeDocumento => view.ApresentarPdf(nomeDocumento);
    }

    public void IniciarPrograma()
    {
        ProgramaIniciado?.Invoke();
    }

    // Solicita a abertura de um novo formulário para introdução
    // dos dados referentes ao documento a ser criado
    public void SolicitarAberturaFormulario(TipoDeDocumento tipoDeDocumento)
    {
        AberturaFormDocumentoSolicitado(tipoDeDocumento);
    }

    // Gera PDF do documento com os dados presentes no formulário
    public void GerarPdf()
    {
        // Solicita os dados preenchidos pelo utilizador
        var dadosFormulario = DadosFormularioSolicitado?.Invoke();

        // Verifica se todos os campos do formulario estão preenchidos
        bool isDadosValidos = DadosFormularioRecebidos?.Invoke(dadosFormulario) ?? false;

        ValidacaoFormularioFalhou?.Invoke();

        // Se todos os dados estiverem válidos, inicia-se o processo de geraração do PDF
        if (isDadosValidos)
        {
        
            // Define o título do documento PDF a ser criado
            string tipoDocumentoAberto =
                (view.tipoDeDocumentoAberto == TipoDeDocumento.CartaoVisita) ? "Cartão de Visita" : "Passe de Serviço";

            string nomeDocumento = $"{tipoDocumentoAberto}.pdf";
            
            // Criação do documento PDF
            PdfDocument documentoPdf = ValidacaoFormularioBemSucedida?.Invoke(dadosFormulario,tipoDocumentoAberto);

            documentoPdf.Save(nomeDocumento);

            DocumentoSalvo?.Invoke(nomeDocumento);
        }
    }
}