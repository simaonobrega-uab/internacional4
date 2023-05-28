using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace GerarPDF
{
    // Define a estrutura dos formulários que podem ser escolhidos
    // no menu inicial. 
    public abstract class FormularioBase : Form
    {
        private Label _labelTitulo;
        private Button _botaoGerarPdf;
        private readonly Controller controller;
        protected TableLayoutPanel TableLayoutPanel;

        private View _view;

        // Cada formulário define os campos que necessita de gerir
        protected abstract void AdicionarCamposAoFormulario();


        // TextBox é um mapa entre as TextBox do formulário e os nomes das chaves
        // dos campos de texto.
        protected abstract Dictionary<TextBox, string> TextBoxChave { get; }
        protected abstract Dictionary<TextBox, bool> CamposValidos { get; }
        
        protected FormularioBase(Controller c, View v)
        {

            controller = c;
            _view = v;
            
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
            _view.BotaoGerarPdf_Click();
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

        
        public PdfDocument GerarPdf(Dictionary<string, string> dados, string tipoDocumento)
        {
            GeradorPdf gerador = new GeradorPdf(dados, tipoDocumento);
            return gerador.CriaPdf();
        }
    }
}