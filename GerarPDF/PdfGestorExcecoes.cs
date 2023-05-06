namespace GerarPDF;

public class PdfGestorExcecoes
{
    // Trata excepções associadas à falta de permissão para guardar o ficheiro do directório escolhido
    public static void TratarExcecaoPermissao(UnauthorizedAccessException ex)
    {
        MessageBox.Show($"Não possui autorização para guardar o ficheiro: {ex.Message}", "Erro", MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    // Trata excepções causadas por operações de entrada e saída em sistemas de computador
    public static void TratarExcecaoIo(IOException ex)
    {
        MessageBox.Show($"Erro ao salvar o arquivo PDF: {ex.Message}", "Erro", MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    // Trata todas as outras situações que não foram previamente identificadas
    public static void TratarExcecaoGenerica(Exception ex)
    {
        MessageBox.Show($"Ocorreu um erro inesperado ao gerar o documento PDF: {ex.Message}", "Erro",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}