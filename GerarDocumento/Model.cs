using GerarPDF.Interfaces;

namespace GerarPDF;

public class Model
{
    // Lista que possui o nome dos campos do formulário que possuem
    // um input inválido.
    private ICampos _camposInvalidos = new Campos();

    // Retorna os campos inválidos
    public List<string> EnviarCamposInValidos()
    {
        return _camposInvalidos.Select(campo => campo.Nome).ToList();
    }

    // Valida o input do utilizador. Retorna "true" caso todos os campos
    // estejam correctamente preenchidos.
    public bool ValidarInputDados(ICampos dados)
    {
        _camposInvalidos = dados.ValidarCampos();

        return (_camposInvalidos.Count() == 0);
    }
}