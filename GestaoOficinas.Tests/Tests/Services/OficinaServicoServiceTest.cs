
using GestaoOficinas.Api.Models;
using GestaoOficinas.Api.Services;
using GestaoOficinas.Api.ValueObjects;
using System;
using System.Collections.Generic;
using Xunit;

namespace GestaoOficinas.Tests.Tests.Services
{
    public class OficinaServicoServiceTest
    {
        private OficinaServicoService _service = new OficinaServicoService();
        [Fact]
        public void Test_ValidacaoHorasDisponiveisPorDia()
        {

            List<OficinaServico> oficinaServicosDia = new List<OficinaServico>();
            List<Servico> servicos = new List<Servico>();
            Servico servicoSolicitado;
            Oficina oficina;
            OficinaServico request;

            #region Entidade exemplificando o modelo da requisição que chega no controller e que será inserido na função do service
            request = new OficinaServico()
            {
                Id = 0,
                OficinaId = 1,
                ServicoId = 2,
                DataServico = new DateTime(),
            };
            #endregion
            #region Entidade do servico solicitado
            servicoSolicitado = new Servico()
            {
                Id = 2,
                UnidadesTrabalhoRequerida = 5,
                Name = "Lavagem",
                Id_oficina = 1,
            };
            #endregion
            #region Entidade da oficina escolhida para fazer o servico
            oficina = new Oficina()
            {
                Id = 1,
                Name = "Oficina do tião",
                CNPJ = "123456789101112",
                UnidadeTempoDiaria = 10,
                Password = "teste123"
            };
            #endregion
            #region Entidades dos servicos que a oficina já tem no dia

            oficinaServicosDia.Add(new OficinaServico()
            {
                Id = 0,
                OficinaId = 1,
                ServicoId = 2,
                DataServico = new DateTime(),
            }
            );
            oficinaServicosDia.Add(new OficinaServico()
            {
                Id = 0,
                OficinaId = 1,
                ServicoId = 2,
                DataServico = new DateTime(),
            }
            );
            #endregion
            #region Entidade mostrando todos os servicos que a oficina realiza
            servicos.Add(new Servico()
            {
                Id = 2,
                UnidadesTrabalhoRequerida = 5,
                Name = "Lavagem",
                Id_oficina = 1,
            }
            );
            servicos.Add(new Servico()
            {
                Id = 3,
                UnidadesTrabalhoRequerida = 10,
                Name = "Retifica de motor",
                Id_oficina = 1,
            }
            );
            #endregion

            Assert.True(_service.ValidacaoHorasDisponiveisPorDia(oficinaServicosDia,oficina,servicos,request,servicoSolicitado));

        }
        [Fact]
        public void Test_ValidacaoFinalSemana()
        {
            OficinaServico oficinaServico;
            #region Entidade exemplificando o modelo da requisição que chega no controller e que será inserido na função do service
            oficinaServico = new OficinaServico()
            {
                Id = 0,
                OficinaId = 1,
                ServicoId = 2,
                DataServico = new DateTime(),
            };
            #endregion
            Assert.True(_service.ValidacaoFinalSemana(oficinaServico));

        }
        [Fact]
        public void GetUnidadeTrabalhoPeriodo()
        {
            List<UnidadeTrabalhoDia> response = new List<UnidadeTrabalhoDia>();
            List<Servico> servicos = new List<Servico>();
            List<OficinaServico> oficinaServicosDia = new List<OficinaServico>();

            #region Entidade mostrando todos os servicos que a oficina realiza
            servicos.Add(new Servico()
            {
                Id = 2,
                UnidadesTrabalhoRequerida = 5,
                Name = "Lavagem",
                Id_oficina = 1,
            }
            );
            servicos.Add(new Servico()
            {
                Id = 3,
                UnidadesTrabalhoRequerida = 10,
                Name = "Retifica de motor",
                Id_oficina = 1,
            }
            );
            #endregion
            #region Entidades dos servicos que a oficina já tem no dia

            oficinaServicosDia.Add(new OficinaServico()
            {
                Id = 0,
                OficinaId = 1,
                ServicoId = 2,
                DataServico = DateTime.MinValue,
            }
            );
            oficinaServicosDia.Add(new OficinaServico()
            {
                Id = 0,
                OficinaId = 1,
                ServicoId = 2,
                DataServico = DateTime.MinValue,
            }
            );
            #endregion
            #region Modelo de resposta do método
            response.Add(new UnidadeTrabalhoDia()
            {
                UnidadeTrabalho = servicos[0].UnidadesTrabalhoRequerida,
                Data = oficinaServicosDia[0].DataServico,
            });
            response.Add(new UnidadeTrabalhoDia()
            {
                UnidadeTrabalho = servicos[0].UnidadesTrabalhoRequerida,
                Data = oficinaServicosDia[0].DataServico,
            });
            #endregion
            List<UnidadeTrabalhoDia> objToTest = _service.GetUnidadeTrabalhoPeriodo(servicos, oficinaServicosDia);

            Assert.Equal(response.Count
                , objToTest.Count);

        }

    }
}
