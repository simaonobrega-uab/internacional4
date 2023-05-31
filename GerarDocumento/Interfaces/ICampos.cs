namespace GerarPDF.Interfaces;

// Coleção de objectos ICampo.
public interface ICampos : IEnumerable<ICampo>
{
    void AdicionarCampo(ICampo campo);
    List<ICampo> ValidarCampos();
}