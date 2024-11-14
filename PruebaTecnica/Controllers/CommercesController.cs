using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;
using PruebaTecnica.Request;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommercesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommercesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Commerces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commerce>>> GetCommerces()
        {
            return await _context.Commerces.ToListAsync();
        }

        // GET: api/Commerces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Commerce>> GetCommerce(Guid id)
        {
            var commerce = await _context.Commerces.Include(c => c.Users).FirstOrDefaultAsync(c => c.CommerceId == id);

            if (commerce == null)
            {
                return NotFound();
            }

            return commerce;
        }

        // PUT: api/Commerces/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommerce(Guid id, Commerce commerce)
        {
            if (id != commerce.CommerceId)
            {
                return BadRequest();
            }

            _context.Entry(commerce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommerceExists(id))
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

        // POST: api/Commerces
        [HttpPost]
        public async Task<ActionResult<Commerce>> PostCommerce(UserEcoomeDTO userEcoomeDTO)
        {
            var commerce = new Commerce();
            var user = new User();
            // Asignar un estado inicial activo al comercio
            commerce.State.StateName = "Activo";

            // Si el comercio tiene un usuario propietario, establecer el estado del usuario como "Activo"
            var owner = commerce.Users.FirstOrDefault(u => u.Role == "Owner"); // Buscar el propietario en la lista de usuarios
            if (owner != null)
            {
                owner.State.StateName = "Activo";  // Establecer el estado del usuario como activo
                _context.Users.Add(owner);  // Añadir el usuario propietario si no existe en la base de datos
            }
            

            _context.Commerces.Add(commerce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommerce", new { id = commerce.CommerceId }, commerce);
        }

        // DELETE: api/Commerces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommerce(Guid id)
        {
            var commerce = await _context.Commerces.Include(c => c.Users).FirstOrDefaultAsync(c => c.CommerceId == id);
            if (commerce == null)
            {
                return NotFound();
            }

            // Eliminar el usuario propietario si existe
            var owner = commerce.Users.FirstOrDefault(u => u.Role == "Owner");
            if (owner != null)
            {
                _context.Users.Remove(owner);
            }

            _context.Commerces.Remove(commerce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommerceExists(Guid id)
        {
            return _context.Commerces.Any(e => e.CommerceId == id);
        }
    }
}
