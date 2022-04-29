using GestaoOficinas.Api.Models;

namespace GestaoOficinas.Api.Interfaces.Services
{
    public interface IOficinaServicoService
    {

        public bool ValidacaoFinalSemana(OficinaServico oficinaServico);

        public bool ValidacaoHorasDisponiveisPorDia(List<OficinaServico> oficinaServicos, Oficina oficina, List<Servico> servicos, OficinaServico oficinaServico, Servico servico);

    }
}
