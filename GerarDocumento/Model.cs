namespace GerarPDF;

public class Model
{
    // Lista que possui o nome dos campos do formulário que possuem
    // um input inválido.
    private readonly List<string> _camposInvalidos = new();

    // Retorna os campos inválidos
    public List<string> EnviarCamposInValidos()
    {
        return _camposInvalidos;
    }

    // Valida o input do utilizador. Retorna "true" caso todos os campos
    // estejam preenchidos. // todo: adicionar outras verificações
    public bool ValidarInputDados(Dictionary<string, string> dados)
    {
        _camposInvalidos.Clear();
        
        // Adicionar a _camposInvalidos todos os campos por preencher
        foreach (KeyValuePair<string, string> campo in dados)
        {
            if (string.IsNullOrEmpty(campo.Value))
            {
                _camposInvalidos.Add(campo.Key);
            }
        }

        return (_camposInvalidos.Count == 0);
    }
}


