namespace GerarPDF;

using PdfSharp.Pdf;

// Controlador da aplicação Gerar PDF.
public class Controller
{
    private readonly View _view;

    public delegate void ProgramaIniciadoEventHandler();

    public event ProgramaIniciadoEventHandler ProgramaIniciado;

    public delegate void AberturaFormDocumentoSolicitadoEventHandler(TipoDeDocumento tipoDeDocumento);

    public event AberturaFormDocumentoSolicitadoEventHandler AberturaFormDocumentoSolicitado;

    public delegate Dictionary<string, string> DadosFormularioSolicitadoEventHandler();

    public event DadosFormularioSolicitadoEventHandler DadosFormularioSolicitado;

    public delegate bool DadosFormularioRecebidosEventHandler(Dictionary<string, string> dados);

    public event DadosFormularioRecebidosEventHandler DadosFormularioRecebidos;

    public delegate void SinalizarCamposInvalidosEventHandler();

    public event SinalizarCamposInvalidosEventHandler DadosValidados;

    public delegate PdfDocument GerarPdfEventHandler(Dictionary<string, string> dados, string tipoDocumento);

    public event GerarPdfEventHandler ValidacaoFormularioBemSucedida;

    public delegate void DocumentoSalvoEventHandler(string nomeDocumento);

    public event DocumentoSalvoEventHandler DocumentoSalvo;

    public Controller()
    {
        // Conexão dos componentes do estilo arq. MVC
        var model = new Model();
        _view = new View(this, model);

        // Associação dos métodos relevantes aos eventos do controlador
        ProgramaIniciado += _view.ActivarInterface;
        AberturaFormDocumentoSolicitado += _view.AbrirFormDocumento;
        DadosFormularioSolicitado += () => _view.FormularioAberto().EnviarDados();
        DadosFormularioRecebidos += model.ValidarInputDados;
        DadosValidados += _view.MostrarCamposInvalidos;
        ValidacaoFormularioBemSucedida +=
            (dados, tipoDocumento) => _view.FormularioAberto().GerarPdf(dados, tipoDocumento);
        DocumentoSalvo += nomeDocumento => _view.ApresentarPdf(nomeDocumento);
    }

    public void IniciarPrograma()
    {
        ProgramaIniciado.Invoke();
    }

    // Solicita a abertura de um novo formulário para introdução
    // dos dados referentes ao documento a ser gerado
    public void SolicitarAberturaFormulario(TipoDeDocumento tipoDeDocumento)
    {
        AberturaFormDocumentoSolicitado(tipoDeDocumento);
    }

    // Gera PDF do documento com os dados presentes no formulário
    public void GerarPdf()
    {
        // Solicita os dados preenchidos pelo utilizador
        var dadosFormulario = DadosFormularioSolicitado.Invoke();

        // Verifica se os campos do formulario estão todos preenchidos
        var estaoDadosPreenchidos = DadosFormularioRecebidos.Invoke(dadosFormulario);

        DadosValidados.Invoke();

        // Se todos os dados estiverem válidos, inicia-se o processo de geraração do PDF
        if (estaoDadosPreenchidos)
        {
            // Define o título do documento PDF a ser criado
            string tipoDocumentoAberto =
                _view.TipoDeDocumentoAberto == TipoDeDocumento.CartaoVisita ? "Cartão de Visita" : "Passe de Serviço";
            string nomeDocumento = $"{tipoDocumentoAberto}.pdf";

            // Criação do documento PDF
            try
            {
                PdfDocument documentoPdf = ValidacaoFormularioBemSucedida.Invoke(dadosFormulario, tipoDocumentoAberto);

                try
                {
                    documentoPdf.Save(nomeDocumento);
                    DocumentoSalvo.Invoke(nomeDocumento);
                }
                catch (UnauthorizedAccessException ex)
                {
                    PdfGestorExcecoes.TratarExcecaoPermissao(ex);
                }
                catch (IOException ex)
                {
                    PdfGestorExcecoes.TratarExcecaoIo(ex);
                }
                catch (Exception ex)
                {
                    PdfGestorExcecoes.TratarExcecaoGenerica(ex);
                }
            }
            catch (Exception ex)
            {
                PdfGestorExcecoes.TratarExcecaoGenerica(ex);
            }
        }
    }
}