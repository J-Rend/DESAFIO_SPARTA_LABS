using GestaoOficinas.Api.Models;

namespace GestaoOficinas.Api.ValueObjects
{
    public class ServicosDia
    {

        public List<Servico> Servicos { get; set; }
        public DateTime Data { get; set; }

    }
}
