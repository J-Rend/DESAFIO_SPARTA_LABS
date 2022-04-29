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
        public async Task<IActionResult> Index()
        {
            return View(await _context.OficinaServico.ToListAsync());
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
            return View(oficinaServico);
        }


        // POST: OficinaServico/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("OficinaId,ServicoId,DataServico")] OficinaServico oficinaServico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oficinaServico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oficinaServico);
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
