using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace GerarPDF
{
    // Define a estrutura dos formulários que podem ser escolhidos
    // no menu inicial. Esta classe define também o layout do documento PDF
    public abstract class FormularioBase : Form
    {
        private Label _labelTitulo;
        private Button _botaoGerarPdf;
        private readonly Controller controller;
        protected TableLayoutPanel TableLayoutPanel;

        // Cada formulário define os campos que necessita de gerir
        protected abstract void AdicionarCamposAoFormulario();


        // TextBox é um mapa entre as TextBox do formulário e os nomes das chaves
        // dos campos de texto.
        protected abstract Dictionary<TextBox, string> TextBoxChave { get; }

        protected abstract Dictionary<TextBox, bool> CamposValidos { get; }

        public void ApresentarCamposInvalidos(List<string> camposInvalidos)
        {
            foreach (TextBox campo in TextBoxChave.Keys)
            {
                CamposValidos[campo] = !camposInvalidos.Contains(TextBoxChave[campo]);
                DefinirCorTextBox(campo, CamposValidos[campo]);
            }
        }

        private void DefinirCorTextBox(TextBox campo, bool isValid)
        {
            campo.BackColor = isValid ? Color.White : Color.Red;
        }

        protected delegate void GerarPdfSolicitadoEventHandler();

        protected event GerarPdfSolicitadoEventHandler GerarPdfSolicitado;

        protected FormularioBase(Controller c)
        {

            controller = c;
            GerarPdfSolicitado += controller.GerarPdf;
            
            TableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 6,
                AutoSize = true,
                Margin = new Padding(10),
                Padding = new Padding(10)
            };
            
            InicializarComponentes();
        }

        // Componentes do layout de cada formulário
        private void InicializarComponentes()
        {

            Controls.Add(TableLayoutPanel);

            _labelTitulo = new Label
            {
                Text = "Preencha os dados solicitados",
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
            };

            TableLayoutPanel.Controls.Add(_labelTitulo, 0, 0);
            TableLayoutPanel.SetColumnSpan(_labelTitulo, 2);

            AdicionarCamposAoFormulario();

            _botaoGerarPdf = new Button
            {
                Text = "Gerar PDF",
                Width = 100,
                Height = 30
            };
            _botaoGerarPdf.Click += (sender, e) => BotaoGerarPdf_Click();
            TableLayoutPanel.Controls.Add(_botaoGerarPdf, 1, 6);

            // Definir o tamanho e estilo do formulário
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private void BotaoGerarPdf_Click()
        {
            GerarPdfSolicitado?.Invoke();
        }

        // Disponibilza os dados inseridos pelo utilizador ao controlador
        public Dictionary<string, string> EnviarDados()
        {
            var dados = new Dictionary<string, string>();
            foreach (var campo in TextBoxChave)
            {
                dados[campo.Value] = campo.Key.Text;
            }

            return dados;
        }

        // Define o layout do documento genérico a ser criado pelo programa
        public PdfDocument GerarPdf(Dictionary<string, string> dados, string tipoDocumento)
        {
            // Cria o documento PDF
            PdfDocument document = new PdfDocument();
            document.Info.Title = tipoDocumento;
            
            
            // Adiciona uma nova página ao documento
            PdfPage page = document.AddPage();
            page.Width = XUnit.FromMillimeter(74);
            page.Height = XUnit.FromMillimeter(52);
            page.Orientation = PageOrientation.Portrait;

            // Cria objecto XGraphics para desenhar na página
            XGraphics gfx = XGraphics.FromPdfPage(page);

            gfx.DrawString("DOC Corporativo", new XFont("Verdana", 16), XBrushes.Black, new XRect(0, 0, page.Width, 50),
                XStringFormats.Center);

            // Definir o fundo do cartão
            XRect rect = new XRect(0, 0, page.Width, page.Height);
            XColor corFundo = XColor.FromArgb(245, 241, 227);
            XSolidBrush brush = new XSolidBrush(corFundo);
            gfx.DrawRectangle(brush, rect);

            // Adiciona as informações do cartão
            XFont fontTitulo = new XFont("Verdana", 10);
            XFont font = new XFont("Verdana", 8);
            XBrush brush2 = XBrushes.Black;

            gfx.DrawString(document.Info.Title, fontTitulo, brush2, new XRect(0, 0, page.Width - 20, 20),
                XStringFormats.TopCenter);

            int posicaoCampo = 25;
            foreach (var campo in dados)
            {
                gfx.DrawString($"{campo.Key}: {campo.Value}", font, brush2,
                    new XRect(10, posicaoCampo, page.Width - 20, 20), XStringFormats.TopLeft);
                posicaoCampo += 25;
            }

            // Desenha uma barra branca no final do documento
            XRect rect2 = new XRect(0, page.Height - 5, page.Width, 5);
            XSolidBrush brush3 = new XSolidBrush(XColor.FromArgb(255, 255, 255));
            gfx.DrawRectangle(brush3, rect2);

            return document;
        }
    }
}