namespace GerarPDF;

public class PdfGestorExcecoes
{
    // Trata excepções associadas à falta de permissão para guardar o ficheiro do directório escolhido
    public static void TratarExcecaoPermissao(UnauthorizedAccessException ex)
    {
        MessageBox.Show($"Não possui autorização para guardar o ficheiro: {ex.Message}", "Erro", MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    // Caso o PDF não consiga ser guardado, é gerada uma excepção do tipo InvalidOperationException 
    public static void TratarExcecaoIo(InvalidOperationException ex)
    {
        MessageBox.Show($"Erro ao salvar o arquivo PDF: {ex.Message}", "Erro", MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
    
    // Caso não seja possível encontrar o caminho especificado
    public static void TratarExcecaoFileNotFound(Exception ex)
    {
        MessageBox.Show($"Não foi possível encontrar o ficheiro PDF especificado. Verifique o caminho e nome do ficheiro.", "Erro",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    // Trata todas as outras situações que não foram previamente identificadas
    public static void TratarExcecaoGenerica(Exception ex)
    {
        MessageBox.Show($"Ocorreu um erro inesperado no processo de geração do documento PDF: {ex.Message}", "Erro",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}