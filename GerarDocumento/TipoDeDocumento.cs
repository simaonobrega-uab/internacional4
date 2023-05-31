namespace GerarPDF;


public enum TipoDeDocumento
{
    CartaoVisita,
    PasseServico,
    NaoEspecificado
}

public static class TipoDeDocumentoExtensao
{
    public static string GetTitulo(this TipoDeDocumento tipo)
    {
        switch (tipo)
        {
            case TipoDeDocumento.CartaoVisita: 
                return "Cartão de Visita";
            case TipoDeDocumento.PasseServico: 
                return "Passe de Serviço";
            case TipoDeDocumento.NaoEspecificado: 
            default: 
                return "";
        }
    }
}