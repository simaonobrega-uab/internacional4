using System.Collections;
using GerarPDF.Interfaces;

namespace GerarPDF
{
    public class Campos : ICampos
    {
        private List<ICampo> _campos = new();
        private List<ICampo> _camposInvalidos = new();

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
        public List<ICampo> ValidarCampos()
        {
            _camposInvalidos.Clear();

            foreach (ICampo campo in _campos)
            {
                if (!campo.IsValid())
                {
                    _camposInvalidos.Add(campo);
                }
            }

            return _camposInvalidos;
        }
    }
}
