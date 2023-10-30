using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadanie3;
using Zadanie3.Model;

namespace Zadanie3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiosController : ControllerBase
    {
        private readonly FiosContext _context;

        public FiosController(FiosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fio>>> GetFios()
        {
          if (_context.Fios == null)
          {
              return NotFound();
          }
            return await _context.Fios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fio>> GetFio(int id)
        {
          if (_context.Fios == null)
          {
              return NotFound();
          }
            var fio = await _context.Fios.FindAsync(id);

            if (fio == null)
            {
                return NotFound();
            }

            return fio;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFio(int id, Fio fio)
        {
            if (id != fio.Id)
            {
                return BadRequest();
            }

            _context.Entry(fio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Fio>> PostFio(Fio fio)
        {
          if (_context.Fios == null)
          {
              return Problem("Entity set 'FiosContext.Fios'  is null.");
          }
            _context.Fios.Add(fio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFio", new { id = fio.Id }, fio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFio(int id)
        {
            if (_context.Fios == null)
            {
                return NotFound();
            }
            var fio = await _context.Fios.FindAsync(id);
            if (fio == null)
            {
                return NotFound();
            }

            _context.Fios.Remove(fio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FioExists(int id)
        {
            return (_context.Fios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
