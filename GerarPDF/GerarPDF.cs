/*
 * Projecto desenvolvido pela equipa InternacioNal4 no
 * âmbito da UC Laboratório de desenvolvimento de SW.
 * 
 * Alunos: Jeovanny João e Simão Nóbrega
 */
 
namespace GerarPDF;

static class Program
{

    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        
        Controller controller = new Controller();
        controller.IniciarPrograma();
        
        Application.Run();
    }
}