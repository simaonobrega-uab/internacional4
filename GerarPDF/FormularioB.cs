namespace GerarPDF
{
    public class FormularioB : FormularioBase
    {
        private Label labelNome;
        private Label labelIdade;
        private Label labelCargo;
        private TextBox textBoxNome;
        private TextBox textBoxIdade;
        private TextBox textBoxCargo;

        public FormularioB(Controller c) : base(c)
        {
        }

        protected override void AdicionarCamposAoFormulario()
        {
            labelNome = new Label { Text = "Nome:", AutoSize = true };
            textBoxNome = new TextBox { Width = 200 };

            labelIdade = new Label { Text = "Idade:", AutoSize = true };
            textBoxIdade = new TextBox { Width = 200 };

            labelCargo = new Label { Text = "Cargo:", AutoSize = true };
            textBoxCargo = new TextBox { Width = 200 };

            tableLayoutPanel.Controls.Add(labelNome, 0, 1);
            tableLayoutPanel.Controls.Add(textBoxNome, 1, 1);

            tableLayoutPanel.Controls.Add(labelIdade, 0, 2);
            tableLayoutPanel.Controls.Add(textBoxIdade, 1, 2);

            tableLayoutPanel.Controls.Add(labelCargo, 0, 3);
            tableLayoutPanel.Controls.Add(textBoxCargo, 1, 3);
        }
        
        
        public override Dictionary<string, string> DisponibilizarDados()
        {
            var dados = new Dictionary<string, string>
            {
                {"Nome", textBoxNome.Text},
                {"Idade", textBoxIdade.Text},
                {"Cargo", textBoxCargo.Text}
            };
            return dados;
        }

        public override void MarcarCamposInvalidos()
        {
            
        }
        
    }
}