namespace GerarPDF;

public static class PdfGestorExcecoes
{
    // Trata excepções associadas à falta de permissão para guardar o ficheiro no directório escolhido
    public static void TratarExcecaoPermissao(UnauthorizedAccessException ex)
    {
        MessageBox.Show("Não possui autorização para guardar o ficheiro no directório escolhido", "Erro",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        Logger.Log(ex.Message);
    }

    // Trata excepções associadas a modificações não permitidas ao documento criado
    public static void TratarExcecaoIo(InvalidOperationException ex)
    {
        MessageBox.Show($"Ocorreu um erro na geração do documento. Tente realizar o processo novamente", "Erro",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        Logger.Log(ex.Message);
    }

    // Caminho escolhido não existe
    public static void TratarExcecaoCaminhoIndisponivel(Exception ex)
    {
        MessageBox.Show(
            $"O caminho escolhido para gravar o ficheiro não está disponível.", "Erro",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        Logger.Log(ex.Message);
    }

    // Caso não seja possível encontrar o ficheiro especificado
    public static void TratarExcecaoFileNotFound(Exception ex)
    {
        MessageBox.Show(
            $"Não foi possível encontrar o ficheiro PDF especificado. Verifique o caminho e nome do ficheiro.", "Erro",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        Logger.Log(ex.Message);
    }

    // Trata todas as outras situações que não foram previamente identificadas
    public static void TratarExcecaoGenerica(Exception ex)
    {
        MessageBox.Show($"Ocorreu um erro inesperado no processo de geração do documento PDF: {ex.Message}", "Erro",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        Logger.Log(ex.Message);
    }
}

