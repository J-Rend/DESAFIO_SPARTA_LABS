#nullable disable
using GestaoOficinas.Api.Data;
using GestaoOficinas.Api.Models;
using GestaoOficinas.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoOficinas.Api.Controllers
{
    [ApiController]
    [Route("oficina")]
    public class OficinasController : Controller
    {
        private readonly GestaoOficinasApiContext _context;

        public OficinasController(GestaoOficinasApiContext context)
        {
            _context = context;
        }

        // GET: Oficinas
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Oficina>>> Index()
        {
            var response = await _context.Oficina.ToListAsync();
            return new OkObjectResult(response);
        }

        // GET: Oficinas/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oficina = await _context.Oficina.FindAsync(id);
            if (oficina == null)
            {
                return NotFound();
            }
            return new OkObjectResult(oficina);
        }

        // POST: Oficinas
        // BODY: {object}
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,CNPJ,Password,UnidadeTempoDiaria")] Oficina oficina)
        {
            if (ModelState.IsValid)
            {
                oficina.Password = TokenService.CreateMD5(oficina.Password);
                _context.Add(oficina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return new OkObjectResult(oficina);
        }

        // PUT: Oficinas/{id}
        // BODY: {object}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Edit(long id, [Bind("Name,CNPJ,Password,UnidadeTempoDiaria")] Oficina oficina)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    oficina.Id = id;
                    _context.Update(oficina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OficinaExists(id))
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
            return new OkObjectResult(oficina);
        }

        // DELETE: Oficinas/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            var oficina = await _context.Oficina.FindAsync(id);
            _context.Oficina.Remove(oficina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [NonAction]
        private bool OficinaExists(long id)
        {
            return _context.Oficina.Any(e => e.Id == id);
        }
    }
}
