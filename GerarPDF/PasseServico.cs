﻿namespace GerarPDF
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

        public PasseServico(Controller c) : base(c)
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

            tableLayoutPanel.Controls.Add(labelNome, 0, 1);
            tableLayoutPanel.Controls.Add(textBoxNome, 1, 1);

            tableLayoutPanel.Controls.Add(labelNrTrabalhador, 0, 2);
            tableLayoutPanel.Controls.Add(textBoxValidade, 1, 2);

            tableLayoutPanel.Controls.Add(labelCargo, 0, 3);
            tableLayoutPanel.Controls.Add(textBoxCargo, 1, 3);
            
            tableLayoutPanel.Controls.Add(labelValidade, 0, 4);
            tableLayoutPanel.Controls.Add(textBoxNrTrabalhador, 1, 4);
        }
    }
}