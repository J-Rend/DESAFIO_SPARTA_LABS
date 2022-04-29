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

namespace GestaoOficinas.Api.Controllers
{
    [ApiController]
    [Route("oficina_servico"), Authorize]
    public class OficinaServicoController : Controller
    {
        private readonly GestaoOficinasApiContext _context;

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

        [HttpGet]
        [Route("get_by_period")]
        public async Task<ActionResult<List<OficinaServico>>> IndexByPeriodo([Bind("DataInicial,DataFinal")] Period period)
        {
            var response = new List<OficinaServico>();

            if (period.DataInicial.ToString() != "" && period.DataFinal.ToString() != "")
            {
                response = await _context.OficinaServico
                    .Where(os => os.DataServico <= period.DataFinal && os.DataServico >= period.DataInicial)
                    .ToListAsync();

            }
            else
                return new BadRequestResult();

            return new OkObjectResult(response);
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
                #region Validação por final de semana
                var diaSemana = oficinaServico.DataServico.DayOfWeek;
                if ( diaSemana == DayOfWeek.Sunday || diaSemana == DayOfWeek.Saturday)
                    return new ObjectResult(new
                    {
                        Message = "ERROR",
                        Description = "Não é possível agendar um serviço em um final de semana!"
                    }
                    );
                #endregion
                else
                {
                    #region Validação por tempo disponível no dia

                    var servicosOficinaDia = _context.OficinaServico
                        .Where(os => os.DataServico == oficinaServico.DataServico && os.OficinaId == oficinaServico.OficinaId).ToList();

                    var servicos = _context.Servico
                        .Where(s => s.Id_oficina == oficinaServico.OficinaId).ToList();


                    var tempoDiarioTotal = _context.Oficina
                        .Where(o => o.Id == oficinaServico.OficinaId).ToList()
                        [0].UnidadeTempoDiaria;
                    var tempoConsumidoTotal = 0;
                 
                    var dump = new List<Servico>(); // ALGO HÁ SER MUDADO, UM DIA, TALVEZ
                    foreach(var item in servicosOficinaDia)
                    {
                        foreach (var item_day in servicos)
                        {
                            if (item_day.Id == item.ServicoId)
                                dump.Add(item_day);
                        }
                    }
                    foreach(var item in dump)
                    {
                        tempoConsumidoTotal += item.UnidadesTrabalhoRequerida;
                    }

                    if(tempoConsumidoTotal > tempoDiarioTotal)
                        return new ObjectResult(new
                        {
                            Message = "ERROR",
                            Description = "Não é possível agendar um serviço em um final de semana!"
                        }
                       );
                    else

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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OficinaServicoExists(oficinaServico.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(oficinaServico);
        }


        // POST: OficinaServico/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var oficinaServico = await _context.OficinaServico.FindAsync(id);
            _context.OficinaServico.Remove(oficinaServico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OficinaServicoExists(long id)
        {
            return _context.OficinaServico.Any(e => e.Id == id);
        }
    }
}
