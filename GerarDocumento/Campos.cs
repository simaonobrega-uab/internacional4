using System.Collections;
using GerarPDF.Interfaces;

namespace GerarPDF
{
    public class Campos : ICampos
    {
        private List<ICampo> _campos = new();
        private ICampos _camposInvalidos = new Campos();

        public void AdicionarCampo(ICampo campo)
        {
            _campos.Add(campo);
        }

        public IEnumerator<ICampo> GetEnumerator()
        {
            return ((IEnumerable<ICampo>)_campos).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Validação de todos os campos associado à lista _campos
        public ICampos ValidarCampos()
        {
            _camposInvalidos = new Campos();

            foreach (ICampo campo in _campos)
            {
                if (!campo.IsValid())
                {
                    _camposInvalidos.AdicionarCampo(campo);
                }
            }

            return _camposInvalidos;
        }
    }
}