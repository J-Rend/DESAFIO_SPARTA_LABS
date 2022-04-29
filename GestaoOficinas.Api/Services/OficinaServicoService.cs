using GestaoOficinas.Api.Interfaces.Services;
using GestaoOficinas.Api.Models;
using GestaoOficinas.Api.ValueObjects;

namespace GestaoOficinas.Api.Services
{
    public class OficinaServicoService : IOficinaServicoService
    {
        public bool ValidacaoHorasDisponiveisPorDia(List<OficinaServico> oficinaServicos, Oficina oficina, List<Servico> servicos, OficinaServico oficinaServico, Servico servico)
        {
            var tempoMaxDiario = oficina.UnidadeTempoDiaria;
            var idsServicos = new List<long>();
            var servicosDia = new List<Servico>();
            var somatorioHoras = 0;

            foreach (var item in oficinaServicos)
            {
                idsServicos.Add(item.ServicoId);
            }

            servicosDia = servicos
                .Join(
                idsServicos,
                servicos => servicos.Id_oficina,
                idsServicos => idsServicos,
                (servicos, idsServicos) => servicos)
                .ToList();

            foreach (var item in servicosDia)
            {
                somatorioHoras += item.UnidadesTrabalhoRequerida;
            }
            if (oficinaServico.DataServico.DayOfWeek == DayOfWeek.Thursday || oficinaServico.DataServico.DayOfWeek == DayOfWeek.Friday)
            {
                if ((somatorioHoras + servico.UnidadesTrabalhoRequerida) > (oficina.UnidadeTempoDiaria + oficina.UnidadeTempoDiaria * (3 / 10)))
                {
                    return false;
                }
                else
                    return true;

            }
            else if (oficinaServico.DataServico.DayOfWeek == DayOfWeek.Saturday || oficinaServico.DataServico.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            else
            {
                if ((somatorioHoras + servico.UnidadesTrabalhoRequerida) > oficina.UnidadeTempoDiaria)
                {
                    return false;
                }
                else
                    return true;
            }
        }

        public bool ValidacaoFinalSemana(OficinaServico oficinaServico)
        {
            var diaSemana = oficinaServico.DataServico.DayOfWeek;

            if (diaSemana == DayOfWeek.Sunday || diaSemana == DayOfWeek.Saturday)
                return false;
            else
                return true;
        }

        public List<UnidadeTrabalhoDia> GetUnidadeTrabalhoPeriodo(List<Servico> servicos, List<OficinaServico> oficinaServicos)
        {
            var response = new List<UnidadeTrabalhoDia>();
            var servicosDia = new List<Servico>();


            foreach (var item in oficinaServicos)
            {
                response.Add(new UnidadeTrabalhoDia()
                {
                    Data = item.DataServico,
                    UnidadeTrabalho = servicos.Where(s => s.Id == item.ServicoId).ToList()[0].UnidadesTrabalhoRequerida,
                }
                );
            }
            return response;
        }

    }
}
