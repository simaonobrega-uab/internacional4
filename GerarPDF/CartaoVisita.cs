namespace GerarPDF
{
    // Define os campos específicos do formulário B.
    public class CartaoVisita : FormularioBase
    {
        private Label labelNome;
        private Label labelContacto;
        private Label labelCargo;
        private Label labelEmail;
        private TextBox textBoxNome;
        private TextBox textBoxContacto;
        private TextBox textBoxCargo;
        private TextBox textBoxEmail;
        private Dictionary<TextBox, bool> camposValidos;
        private Dictionary<TextBox, string> textBoxKey;

        public CartaoVisita(Controller c) : base(c)
        {
        }

        protected override Dictionary<TextBox, string> TextBoxChave => new Dictionary<TextBox, string>
        {
            { textBoxNome, "Nome" },
            { textBoxContacto, "Contacto" },
            { textBoxCargo, "Cargo" },
            { textBoxEmail, "E-mail" }
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
                        { textBoxContacto, true },
                        { textBoxCargo, true },
                        { textBoxEmail, true }
                    };
                }
                return _camposValidos;
            }
        }

        protected override void AdicionarCamposAoFormulario()
        {
            labelNome = new Label { Text = "Nome:", AutoSize = true };
            textBoxNome = new TextBox { Width = 200 };

            labelContacto = new Label { Text = "Contacto:", AutoSize = true };
            textBoxContacto = new TextBox { Width = 200 };

            labelCargo = new Label { Text = "Cargo:", AutoSize = true };
            textBoxCargo = new TextBox { Width = 200 };
            
            labelEmail = new Label { Text = "Email:", AutoSize = true };
            textBoxEmail = new TextBox { Width = 200 };

            tableLayoutPanel.Controls.Add(labelNome, 0, 1);
            tableLayoutPanel.Controls.Add(textBoxNome, 1, 1);

            tableLayoutPanel.Controls.Add(labelContacto, 0, 2);
            tableLayoutPanel.Controls.Add(textBoxContacto, 1, 2);

            tableLayoutPanel.Controls.Add(labelCargo, 0, 3);
            tableLayoutPanel.Controls.Add(textBoxCargo, 1, 3);
            
            tableLayoutPanel.Controls.Add(labelEmail, 0, 4);
            tableLayoutPanel.Controls.Add(textBoxEmail, 1, 4);
        }
    }
}