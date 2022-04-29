#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoOficinas.Api.Data;
using GestaoOficinas.Api.Models;
using Microsoft.AspNetCore.Authorization;
using GestaoOficinas.Api.ValueObjects;
using GestaoOficinas.Api.Interfaces.Services;
using GestaoOficinas.Api.Services;

namespace GestaoOficinas.Api.Controllers
{
    [ApiController]
    [Route("oficina_servico"), Authorize]
    public class OficinaServicoController : Controller
    {
        private readonly GestaoOficinasApiContext _context;

        private readonly OficinaServicoService _service = new OficinaServicoService();
        public OficinaServicoController(GestaoOficinasApiContext context)
        {
            _context = context;
        }

        // GET: OficinaServico
        [HttpGet]
        public async Task<ActionResult<List<OficinaServico>>> Index()
        {
            var response = await _context.OficinaServico.ToListAsync();
            return new OkObjectResult(response);
        }

        [HttpPost("get_servicos_by_period/{id}")]
        public async Task<ActionResult<List<OficinaServico>>> IndexByPeriodo(long id,[Bind("DataInicial,DataFinal")] Period period)
        {
            var response = new List<OficinaServico>();

            if (period.DataInicial.ToString() != "" && period.DataFinal.ToString() != "")
            {
                response = await _context.OficinaServico
                    .Where(os => ((os.DataServico <= period.DataFinal && os.DataServico >= period.DataInicial) && os.OficinaId == id))
                    .ToListAsync();

            }
            else
                return new BadRequestResult();

            return new OkObjectResult(response);
        }
        [HttpPost("get_time_by_period/{id}")]
        public async Task<ActionResult<List<UnidadeTrabalhoDia>>> GetTimeByPeriodo(long id,[Bind("DataInicial,DataFinal")] Period period)
        {
            var response = new List<UnidadeTrabalhoDia>();
            var listaOficinaServico = new List<OficinaServico>();
            var listaServicosTotais = new List<Servico>();

            if (period.DataInicial.ToString() != "" && period.DataFinal.ToString() != "")
            {
                listaOficinaServico = await _context.OficinaServico
                    .Where(os => ((os.DataServico <= period.DataFinal && os.DataServico >= period.DataInicial) && os.OficinaId == id))
                    .ToListAsync();

                listaServicosTotais = await _context.Servico
                    .Where(s => s.Id_oficina == id)
                    .ToListAsync();

                var response_service = _service.GetUnidadeTrabalhoPeriodo(listaServicosTotais, listaOficinaServico);
                if (response_service != null)
                {
                    return response_service;
                }
                else
                {
                    return new BadRequestResult();
                }
            }
            else
                return new BadRequestResult();
        }

        [HttpGet]
        [Route("get_by_day")]
        public async Task<ActionResult<List<OficinaServico>>> IndexByDay(DateTime data)
        {
            var response = new List<OficinaServico>();

            if (data.ToString() != "" && data.ToString() != "")
            {
                response = await _context.OficinaServico
                    .Where(os => os.DataServico.DayOfYear == data.DayOfYear)
                    .ToListAsync();

            }
            else
                return new BadRequestResult();

            return new OkObjectResult(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oficinaServico = await _context.OficinaServico.FindAsync(id);
            if (oficinaServico == null)
            {
                return NotFound();
            }
            return new OkObjectResult(oficinaServico);
        }


        // POST: OficinaServico/Create
        [HttpPost]
        public async Task<ActionResult<dynamic>> Create([Bind("OficinaId,ServicoId,DataServico")] OficinaServico oficinaServico)
        {
            if (ModelState.IsValid)
            {
                #region Validando se o dia solicitado cai num final de semana
                if(!_service.ValidacaoFinalSemana(oficinaServico))
                    return new ObjectResult(new
                    {
                        Message = "ERROR",
                        Description = "Não é possível agendar um serviço em um final de semana!"
                    }
                    );
                #endregion
                else
                {
                    #region Validando se no dia solicitado haverá tempo para a execução do serviço

                    var oficinaServicosDia = await _context.OficinaServico
                        .Where
                        (os => os.DataServico == oficinaServico.DataServico && os.OficinaId == oficinaServico.OficinaId)
                        .ToListAsync();

                    var servicos = _context.Servico
                        .Where(s => s.Id_oficina == oficinaServico.OficinaId).ToList();

                    var servicoSolicitado = await _context.Servico.FindAsync(oficinaServico.ServicoId);

                    var oficina = await _context.Oficina.FindAsync(oficinaServico.OficinaId);

                    if (!_service.ValidacaoHorasDisponiveisPorDia(oficinaServicosDia, oficina,servicos, oficinaServico, servicoSolicitado))
                        return new ObjectResult(new
                        {
                            Message = "ERROR",
                            Description = "A filial não possui tempo disponível no dia escolhido!"
                        }
                        );
                    else
                    {
                        #endregion
                        _context.Add(oficinaServico);
                        await _context.SaveChangesAsync();
                        return new OkObjectResult(new
                        {
                            Message = "SUCCESS",
                            Description = "Serviço agendado com sucesso!"
                        });
                    }
                }
            }
            return NotFound();
        }

        // POST: OficinaServico/Edit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,OficinaId,ServicoId,DataServico")] OficinaServico oficinaServico)
        {
            if (id != oficinaServico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    oficinaServico.Id = id;
                    _context.Update(oficinaServico);
                    await _context.SaveChangesAsync();
                    return new OkObjectResult(new
                    {
                        Message = "SUCCESS",
                        Description = "Serviço editado com sucesso!"
                    });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OficinaServicoExists(oficinaServico.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return new BadRequestResult();
                    }
                }
            }
            return new BadRequestResult();
        }


        // POST: OficinaServico/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var oficinaServico = await _context.OficinaServico.FindAsync(id);
            _context.OficinaServico.Remove(oficinaServico);
            await _context.SaveChangesAsync();
            return new OkObjectResult(new
            {
                Message = "SUCCESS",
                Description = "Serviço excluido com sucesso!"
            });
        }

        private bool OficinaServicoExists(long id)
        {
            return _context.OficinaServico.Any(e => e.Id == id);
        }
    }
}
