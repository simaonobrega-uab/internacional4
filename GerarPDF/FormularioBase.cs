namespace GerarPDF
{
    public abstract class FormularioBase : Form
    {
        protected Label labelTitulo;
        protected TableLayoutPanel tableLayoutPanel;
        protected Button botaoGerarPdf;
        protected event Action GerarPdfSolicitado;
       
        protected Controller controller;

        protected abstract void AdicionarCamposAoFormulario();

        public abstract Dictionary<string, string> DisponibilizarDados();

        public abstract void MarcarCamposInvalidos();



        public FormularioBase(Controller c)
        {
            controller = c;
            GerarPdfSolicitado += controller.GerarPdf;
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            tableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 5,
                AutoSize = true,
                Margin = new Padding(10),
                Padding = new Padding(10)
            };
            Controls.Add(tableLayoutPanel);

            labelTitulo = new Label
            {
                Text = "Preencha os dados solicitados",
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
            };

            tableLayoutPanel.Controls.Add(labelTitulo, 0, 0);
            tableLayoutPanel.SetColumnSpan(labelTitulo, 2);

            AdicionarCamposAoFormulario();

            botaoGerarPdf = new Button
            {
                Text = "Gerar PDF",
                Width = 100,
                Height = 30
            };
            botaoGerarPdf.Click += (sender, e) => BotaoGerarPdfPressionado();
            tableLayoutPanel.Controls.Add(botaoGerarPdf, 1, 4);

            // Definir o tamanho e estilo do formulário
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private void BotaoGerarPdfPressionado()
        {
            GerarPdfSolicitado?.Invoke();
        }
        
        public void SolicitarDados()
        {
            var dados = DisponibilizarDados();
            // Fazer algo com os dados, como validar ou criar um arquivo PDF
        }
        
    }
}