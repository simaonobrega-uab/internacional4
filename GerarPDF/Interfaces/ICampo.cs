namespace GerarPDF.Interfaces;

// - Campo de dados genério com um Nome e ValorFormatado.
// - A variável "Nome" está associado ao label do controlador presente
//   no formulário
// - O ValorFormatado é o Valor, definido em cada classe que
//   que implementa ICampo, formato para string para que possa
//   ser facilmente lido nos cartões a gerar.
// - O método "IsValid" implementa a validação específica
//   de um determinado tipo de campo do formulário.
public interface ICampo
{
    string Nome { get; }
    string ValorFormatado { get; }
    bool IsValid();
}

