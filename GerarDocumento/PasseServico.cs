namespace GerarPDF
{
    // Define os campos específicos do formulário B.
    public class PasseServico : FormularioBase
    {
        private Label labelNome;
        private Label labelNrTrabalhador;
        private Label labelCargo;
        private Label labelValidade;
        private TextBox textBoxNome;
        private TextBox textBoxNrTrabalhador;
        private TextBox textBoxCargo;
        private TextBox textBoxValidade;
        private Dictionary<TextBox, bool> camposValidos;
        private Dictionary<TextBox, string> textBoxKey;

        public PasseServico(Controller c, View v) : base(c,v)
        {
        }

        protected override Dictionary<TextBox, string> TextBoxChave => new Dictionary<TextBox, string>
        {
            { textBoxNome, "Nome" },
            { textBoxValidade, "NrTrabalhador" },
            { textBoxCargo, "Cargo" },
            { textBoxNrTrabalhador, "Validade" }
        };
        
        private Dictionary<TextBox, bool> _camposValidos;
        protected override Dictionary<TextBox, bool> CamposValidos
        {
            get
            {
                if (_camposValidos == null)
                {
                    _camposValidos = new Dictionary<TextBox, bool>
                    {
                        { textBoxNome, true },
                        { textBoxValidade, true },
                        { textBoxCargo, true },
                        { textBoxNrTrabalhador, true }
                    };
                }
                return _camposValidos;
            }
        }

        protected override void AdicionarCamposAoFormulario()
        {
            labelNome = new Label { Text = "Nome:", AutoSize = true };
            textBoxNome = new TextBox { Width = 200 };

            labelNrTrabalhador = new Label { Text = "Nr de Trabalhador:", AutoSize = true };
            textBoxValidade = new TextBox { Width = 200 };

            labelCargo = new Label { Text = "Cargo:", AutoSize = true };
            textBoxCargo = new TextBox { Width = 200 };
            
            labelValidade = new Label { Text = "Validade", AutoSize = true };
            textBoxNrTrabalhador = new TextBox { Width = 200 };

            TableLayoutPanel.Controls.Add(labelNome, 0, 1);
            TableLayoutPanel.Controls.Add(textBoxNome, 1, 1);

            TableLayoutPanel.Controls.Add(labelNrTrabalhador, 0, 2);
            TableLayoutPanel.Controls.Add(textBoxValidade, 1, 2);

            TableLayoutPanel.Controls.Add(labelCargo, 0, 3);
            TableLayoutPanel.Controls.Add(textBoxCargo, 1, 3);
            
            TableLayoutPanel.Controls.Add(labelValidade, 0, 4);
            TableLayoutPanel.Controls.Add(textBoxNrTrabalhador, 1, 4);
        }
    }
}