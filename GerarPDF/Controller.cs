using GerarPDF.Interfaces;
using PdfSharp.Pdf;

namespace GerarPDF;

// Controlador da aplicação Gerar PDF.
public class Controller
{
    private readonly View _view;
    private readonly Model _model;

    public delegate void ProgramaIniciadoEventHandler();
    public event ProgramaIniciadoEventHandler ProgramaIniciado;

    public delegate void AberturaFormDocumentoSolicitadoEventHandler(TipoDeDocumento tipoDeDocumento);
    public event AberturaFormDocumentoSolicitadoEventHandler AberturaFormDocumentoSolicitado;

    public delegate ICampos DadosFormularioSolicitadoEventHandler();
    public event DadosFormularioSolicitadoEventHandler DadosFormularioSolicitado;

    public delegate bool DadosFormularioRecebidosEventHandler(ICampos dados);
    public event DadosFormularioRecebidosEventHandler DadosFormularioRecebidos;

    public delegate void SinalizarCamposInvalidosEventHandler();
    public event SinalizarCamposInvalidosEventHandler DadosValidados;

    public delegate PdfDocument GerarPdfEventHandler(ICampos dados);
    public event GerarPdfEventHandler ValidacaoFormularioBemSucedida;

    public delegate void DocumentoSalvoEventHandler(string nomeDocumento);
    public event DocumentoSalvoEventHandler DocumentoSalvo;

    public Controller()
    {
        // Conexão dos componentes do estilo arq. MVC
        _model = new Model();
        _view = new View();

        // Associação dos métodos relevantes aos eventos do controlador
        ProgramaIniciado += _view.ActivarInterface;
        AberturaFormDocumentoSolicitado += _view.AbrirFormDocumento;
        DadosFormularioSolicitado += () => _view.FormularioAberto().EnviarDados();
        DadosFormularioRecebidos += _model.ValidarInputDados;
        DadosValidados += _view.MostrarCamposInvalidos;
        ValidacaoFormularioBemSucedida +=
            (dados) => _view.GerarPdf(dados);
        DocumentoSalvo += nomeDocumento => _view.ApresentarPdf(nomeDocumento);

        _view.DocumentoSolicitado += SolicitarAberturaFormulario;
        _view.GerarPdfSolicitado += GerarPdf;
        _view.CamposInvalidosSolicitados += () => _model.EnviarCamposInValidos();
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
            // Define o none do documento PDF a ser criado
            string tipoDocumentoAberto =
                _view.TipoDeDocumentoAberto == TipoDeDocumento.CartaoVisita ? "Cartão de Visita" : "Passe de Serviço";
            string nomeDocumento = $"{tipoDocumentoAberto}.pdf";

            // Criação do documento PDF e abertura do ficheiro
            try
            {
                PdfDocument documento = ValidacaoFormularioBemSucedida.Invoke(dadosFormulario);
                SalvarDocumento(documento, nomeDocumento);
            }
            catch (UnauthorizedAccessException ex)
            {
                var mensagemErro = PdfGestorExcecoes.TratarExcecaoPermissao(ex);
                _model.LogErro(ex.Message);
                _view.MostrarErro(mensagemErro);
            }
            catch (InvalidOperationException ex)
            {
                var mensagemErro = PdfGestorExcecoes.TratarExcecaoIo(ex);
                _model.LogErro(ex.Message);
                _view.MostrarErro(mensagemErro);
            }
            catch (FileNotFoundException ex)
            {
                var mensagemErro = PdfGestorExcecoes.TratarExcecaoFileNotFound(ex);
                _model.LogErro(ex.Message);
                _view.MostrarErro(mensagemErro);
            }
            catch (Exception ex)
            {
                var mensagemErro = PdfGestorExcecoes.TratarExcecaoGenerica(ex);
                _model.LogErro(ex.Message);
                _view.MostrarErro(mensagemErro);
            }
        }
    }

    private void SalvarDocumento(PdfDocument documento, string nomeDocumento)
    {
        documento.Save(nomeDocumento);
        DocumentoSalvo.Invoke(nomeDocumento);
    }
}