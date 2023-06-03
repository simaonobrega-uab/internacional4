namespace GerarPDF
{
    // Define os campos específicos do formulário associado ao passe de serviço
    public class PasseServico : FormularioBase
    {
        private Label labelNome;
        private Label labelNrTrabalhador;
        private Label labelCargo;
        private Label labelValidade;
        private TextBox textBoxNome;
        private TextBox textBoxNrTrabalhador;
        private TextBox textBoxCargo;
        private DateTimePicker dateTimePickerValidade;

        private Dictionary<Control, bool> camposValidos;
        private Dictionary<TextBox, string> textBoxKey;

        public PasseServico(Controller c, View v) : base(c, v)
        {
        }

        protected override Dictionary<Control, string> TextBoxChave => new()
        {
            { textBoxNome, "Nome" },
            { textBoxNrTrabalhador, "Nr. Trabalhador" },
            { textBoxCargo, "Cargo" },
            { dateTimePickerValidade, "Validade" }
        };

        private Dictionary<Control, bool> _camposValidos;

        protected override Dictionary<Control, bool> CamposValidos
        {
            get
            {
                if (_camposValidos == null)
                {
                    _camposValidos = new Dictionary<Control, bool>
                    {
                        { textBoxNome, true },
                        { textBoxNrTrabalhador, true },
                        { textBoxCargo, true },
                        { dateTimePickerValidade, true }
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
            textBoxNrTrabalhador = new TextBox { Width = 200 };

            labelCargo = new Label { Text = "Cargo:", AutoSize = true };
            textBoxCargo = new TextBox { Width = 200 };

            labelValidade = new Label { Text = "Validade", AutoSize = true };
            dateTimePickerValidade = new DateTimePicker
                { Width = 200, Format = DateTimePickerFormat.Custom, CustomFormat = "yyyy/MM/dd" };


            TableLayoutPanel.Controls.Add(labelNome, 0, 1);
            TableLayoutPanel.Controls.Add(textBoxNome, 1, 1);

            TableLayoutPanel.Controls.Add(labelNrTrabalhador, 0, 2);
            TableLayoutPanel.Controls.Add(textBoxNrTrabalhador, 1, 2);

            TableLayoutPanel.Controls.Add(labelCargo, 0, 3);
            TableLayoutPanel.Controls.Add(textBoxCargo, 1, 3);

            TableLayoutPanel.Controls.Add(labelValidade, 0, 4);
            TableLayoutPanel.Controls.Add(dateTimePickerValidade, 1, 4);
        }
    }
}