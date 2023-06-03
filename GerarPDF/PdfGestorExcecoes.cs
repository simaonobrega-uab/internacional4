namespace GerarPDF;

public static class PdfGestorExcecoes
{
    // Trata excepções associadas à falta de permissão para guardar o ficheiro no directório escolhido
    public static string TratarExcecaoPermissao(UnauthorizedAccessException ex)
    {
        return "Não possui autorização para guardar o ficheiro no directório escolhido";
    }

    // Trata excepções associadas a modificações não permitidas ao documento criado
    public static string TratarExcecaoIo(InvalidOperationException ex)
    {
        return "Ocorreu um erro na geração do documento. Tente realizar o processo novamente";
    }

    // Caminho escolhido não existe
    public static string TratarExcecaoCaminhoIndisponivel(Exception ex)
    {
        return "O caminho escolhido para gravar o ficheiro não está disponível";
    }

    // Caso não seja possível encontrar o ficheiro especificado
    public static string TratarExcecaoFileNotFound(Exception ex)
    {
        return "Não foi possível encontrar o ficheiro PDF especificado. Verifique o caminho e nome do ficheiro.";
    }

    // Trata todas as outras situações que não foram previamente identificadas
    public static string TratarExcecaoGenerica(Exception ex)
    {
        return "Ocorreu um erro inesperado no processo de geração do documento PDF: " + ex.Message;
    }
}

