using Microsoft.VisualBasic;

namespace GerarPDF;

public class Model
{
    private Controller controller;
    private View view;
    public event Action DisponibilizarCamposIncorretosSolicitado;

    public Model(Controller c, View view)
    {
        controller = c;
        view = view;

        //DisponibilizarCamposIncorretosSolicitado += FormularioA.MarcarCamposInvalidos;
    }

    public void ValidarInputDados(Dictionary<string,string> dados)
    {
        string nome = dados["Nome"];
        string idade = dados["Idade"];
        string cargo = dados["Cargo"];
        
        
    }
}