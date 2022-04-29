#nullable disable
using GestaoOficinas.Api.Data;
using GestaoOficinas.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoOficinas.Api.Controllers
{
    [ApiController]
    [Route("servico")]
    [Authorize]
    public class ServicosController : Controller
    {
        private readonly GestaoOficinasApiContext _context;

        public ServicosController(GestaoOficinasApiContext context)
        {
            _context = context;
        }

        // GET: Servicos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _context.Servico.ToListAsync();
            return new OkObjectResult(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            return new OkObjectResult(servico);
        }



        // POST: Servicos
        // BODY: {object}
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,UnidadesTrabalhoRequerida")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servico);
                await _context.SaveChangesAsync();
                return new OkObjectResult(new
                {
                    Message = "SUCCESS",
                    Description = "Serviço criado com sucesso!"
                });
            }
            return new OkObjectResult(servico);
        }

        // PUT: Servicos/{id}
        // BODY: {object}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(long id, [Bind("Name,UnidadesTrabalhoRequerida")] Servico servico)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    servico.Id = id;
                    _context.Update(servico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicoExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return new BadRequestResult();
                    }
                }
                return new OkObjectResult(new
                {
                    Message = "SUCCESS",
                    Description = "Serviço criado com sucesso!"
                }); ;
            }
            return new BadRequestResult();
        }


        // DELETE: Servicos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var servico = await _context.Servico.FindAsync(id);
            _context.Servico.Remove(servico);
            await _context.SaveChangesAsync();
            return new OkObjectResult(new
            {
                Message = "SUCCESS",
                Description = "Serviço deletado com sucesso!"
            }); ;
        }

        private bool ServicoExists(long id)
        {
            return _context.Servico.Any(e => e.Id == id);
        }
    }
}
