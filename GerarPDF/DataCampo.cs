using GerarPDF.Interfaces;

namespace GerarPDF;

// - Classe que representa um campo de um formulário do tipo Data.
// - A variável "Nome" está associado ao label do controlador presente
//   no formulário, e.g. textBox, sendo "Valor" a data presente na textBox.
public class DataCampo : ICampo
{
    public string Nome { get; }
    private DateTime Valor { get; }

    public DataCampo(string nome, DateTime valor)
    {
        Nome = nome;
        Valor = valor;
    }
    
    public string ValorFormatado => Valor.ToShortDateString();

    public bool IsValid()
    {
        return Valor >= DateTime.Today;
    }
}

