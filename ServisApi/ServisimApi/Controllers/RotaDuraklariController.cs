using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServisimApi.Models;

namespace ServisimApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaDuraklariController : ControllerBase
    {
        private readonly ServisContext _context;

        public RotaDuraklariController(ServisContext context)
        {
            _context = context;
        }

        // GET: api/RotaDuraklari
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RotaDuraklari>>> GetRotaDuraklari()
        {
            return await _context.RotaDuraklari.ToListAsync();
        }

        // GET: api/RotaDuraklari/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RotaDuraklari>> GetRotaDuraklari(int id)
        {
            var rotaDuraklari = await _context.RotaDuraklari.FindAsync(id);

            if (rotaDuraklari == null)
            {
                return NotFound();
            }

            return rotaDuraklari;
        }

        // PUT: api/RotaDuraklari/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRotaDuraklari(int id, RotaDuraklari rotaDuraklari)
        {
            if (id != rotaDuraklari.Id)
            {
                return BadRequest();
            }

            _context.Entry(rotaDuraklari).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RotaDuraklariExists(id))
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

        // POST: api/RotaDuraklari
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RotaDuraklari>> PostRotaDuraklari(RotaDuraklari rotaDuraklari)
        {
            _context.RotaDuraklari.Add(rotaDuraklari);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRotaDuraklari", new { id = rotaDuraklari.Id }, rotaDuraklari);
        }

        // DELETE: api/RotaDuraklari/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RotaDuraklari>> DeleteRotaDuraklari(int id)
        {
            var rotaDuraklari = await _context.RotaDuraklari.FindAsync(id);
            if (rotaDuraklari == null)
            {
                return NotFound();
            }

            _context.RotaDuraklari.Remove(rotaDuraklari);
            await _context.SaveChangesAsync();

            return rotaDuraklari;
        }

        private bool RotaDuraklariExists(int id)
        {
            return _context.RotaDuraklari.Any(e => e.Id == id);
        }
    }
}
