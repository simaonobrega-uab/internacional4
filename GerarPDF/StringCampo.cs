using GerarPDF.Interfaces;

namespace GerarPDF;

// - Classe que representa um campo de um formulário do tipo String.
// - A variável "Nome" está associado ao label da textBox, sendo valor
//   o texto presente na textBox.
public class StringCampo : ICampo
{
    public string Nome { get; }
    public string Valor { get; }

    public StringCampo(string nome, string valor)
    {
        Nome = nome;
        Valor = valor;
    }

    public string ValorFormatado => Valor;

    public bool IsValid()
    {
        // Como exemplo de demonstração, é apenas verificado se o valor existe ou não.
        return !string.IsNullOrEmpty(Valor);
    }
}