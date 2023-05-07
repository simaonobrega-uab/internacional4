// Responsável por fazer o log das exceções lançadas
public static class Logger
{
    private static readonly string LogFilePath = "log.txt";

    public static void Log(string message)
    {
        using (var streamWriter = new StreamWriter(LogFilePath, true))
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            streamWriter.WriteLine($"{timestamp} Erro: {message}");
        }
    }
}