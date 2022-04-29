using GestaoOficinas.Api.Models;
using GestaoOficinas.Api.ValueObjects;

namespace GestaoOficinas.Api.Interfaces.Services
{
    public interface IOficinaServicoService
    {

        public bool ValidacaoFinalSemana(OficinaServico oficinaServico);

        public bool ValidacaoHorasDisponiveisPorDia(List<OficinaServico> oficinaServicos, Oficina oficina, List<Servico> servicos, OficinaServico oficinaServico, Servico servico);

        public List<UnidadeTrabalhoDia> GetUnidadeTrabalhoPeriodo(List<Servico> servicos, List<OficinaServico> oficinaServicos);
    }
}
