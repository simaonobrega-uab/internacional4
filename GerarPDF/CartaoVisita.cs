namespace GerarPDF
{
    // Define os campos específicos do formulário B.
    public class CartaoVisita : FormularioBase
    {
        private Label _labelNome;
        private Label _labelContacto;
        private Label _labelCargo;
        private Label _labelEmail;
        private TextBox _textBoxNome;
        private TextBox _textBoxContacto;
        private TextBox _textBoxCargo;
        private TextBox _textBoxEmail;
        private Dictionary<TextBox, bool>? _camposValidos;

        public CartaoVisita(Controller c) : base(c)
        {
        }

        protected override Dictionary<TextBox, string> TextBoxChave => new()
        {
            { _textBoxNome, "Nome" },
            { _textBoxContacto, "Contacto" },
            { _textBoxCargo, "Cargo" },
            { _textBoxEmail, "E-mail" }
        };
        
        protected override Dictionary<TextBox, bool> CamposValidos
        {
            get
            {
                if (_camposValidos == null)
                {
                    _camposValidos = new Dictionary<TextBox, bool>
                    {
                        { _textBoxNome, true },
                        { _textBoxContacto, true },
                        { _textBoxCargo, true },
                        { _textBoxEmail, true }
                    };
                }
                return _camposValidos;
            }
        }

        protected override void AdicionarCamposAoFormulario()
        {
            
            _labelNome = new Label { Text = "Nome:", AutoSize = true };
            _textBoxNome = new TextBox { Width = 200 };

            _labelContacto = new Label { Text = "Contacto:", AutoSize = true };
            _textBoxContacto = new TextBox { Width = 200 };

            _labelCargo = new Label { Text = "Cargo:", AutoSize = true };
            _textBoxCargo = new TextBox { Width = 200 };
            
            _labelEmail = new Label { Text = "Email:", AutoSize = true };
            _textBoxEmail = new TextBox { Width = 200 };
            
            TableLayoutPanel.Controls.Add(_labelNome, 0, 1);
            TableLayoutPanel.Controls.Add(_textBoxNome, 1, 1);

            TableLayoutPanel.Controls.Add(_labelContacto, 0, 2);
            TableLayoutPanel.Controls.Add(_textBoxContacto, 1, 2);

            TableLayoutPanel.Controls.Add(_labelCargo, 0, 3);
            TableLayoutPanel.Controls.Add(_textBoxCargo, 1, 3);
            
            TableLayoutPanel.Controls.Add(_labelEmail, 0, 4);
            TableLayoutPanel.Controls.Add(_textBoxEmail, 1, 4);
        }
    }
}