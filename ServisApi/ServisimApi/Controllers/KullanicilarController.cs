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
    public class KullanicilarController : ControllerBase
    {
        private readonly ServisContext _context;

        public KullanicilarController(ServisContext context)
        {
            _context = context;
        }

        // GET: api/Kullanicilar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kullanicilar>>> GetKullanicilar()
        {
            return await _context.Kullanicilar.ToListAsync();
        }

        // GET: api/Kullanicilar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kullanicilar>> GetKullanicilar(int id)
        {
            var kullanicilar = await _context.Kullanicilar.FindAsync(id);

            if (kullanicilar == null)
            {
                return NotFound();
            }

            return kullanicilar;
        }

        // PUT: api/Kullanicilar/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKullanicilar(int id, Kullanicilar kullanicilar)
        {
            if (id != kullanicilar.Id)
            {
                return BadRequest();
            }

            _context.Entry(kullanicilar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KullanicilarExists(id))
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

        // POST: api/Kullanicilar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Kullanicilar>> PostKullanicilar(Kullanicilar kullanicilar)
        {
            _context.Kullanicilar.Add(kullanicilar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKullanicilar", new { id = kullanicilar.Id }, kullanicilar);
        }

        // DELETE: api/Kullanicilar/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Kullanicilar>> DeleteKullanicilar(int id)
        {
            var kullanicilar = await _context.Kullanicilar.FindAsync(id);
            if (kullanicilar == null)
            {
                return NotFound();
            }

            _context.Kullanicilar.Remove(kullanicilar);
            await _context.SaveChangesAsync();

            return kullanicilar;
        }

        private bool KullanicilarExists(int id)
        {
            return _context.Kullanicilar.Any(e => e.Id == id);
        }
    }
}
