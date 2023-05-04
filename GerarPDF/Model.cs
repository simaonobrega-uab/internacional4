namespace GerarPDF;

public class Model
{
    
    List<string> camposInvalidos = new List<string>();

    public List<string> EnviarCamposIncorretos()
    {
        return camposInvalidos;
    }

    public bool ValidarInputDados(Dictionary<string, string> dados)
    {
        camposInvalidos.Clear();

        // Verifica se os dados estão todos preenchidos
        foreach (KeyValuePair<string, string> campo in dados)
        {
            if (string.IsNullOrEmpty(campo.Value))
            {
                camposInvalidos.Add(campo.Key);
            }
        }

        if (camposInvalidos.Count != 0)
        {
            return false;
        }

        return true;
    }
}