// Services/CommerceService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Services
{
    public class CommerceService
    {
        private readonly AppDbContext _context;

        public CommerceService(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los comercios
        public async Task<IEnumerable<Commerce>> GetCommercesAsync()
        {
            return await _context.Commerces
                .Include(c => c.Users)  // Incluye los usuarios asociados
                .ToListAsync();
        }

        // Obtener un comercio por ID
        public async Task<Commerce> GetCommerceAsync(Guid id)
        {
            return await _context.Commerces
                .Include(c => c.Users)  // Incluye los usuarios asociados
                .FirstOrDefaultAsync(c => c.CommerceId == id);
        }

        // Crear un nuevo comercio
        public async Task<Commerce> CreateCommerceAsync(Commerce commerce)
        {
            // Asignar un estado inicial activo al comercio
            commerce.State.StateName = "Activo";

            // Si el comercio tiene un usuario propietario, establecer el estado del usuario como "Activo"
            var owner = commerce.Users.FirstOrDefault(u => u.Role == "Owner");
            if (owner != null)
            {
                owner.State.StateName = "Activo";  // Establecer el estado del usuario como activo
                _context.Users.Add(owner);  // Añadir el usuario propietario si no existe en la base de datos
            }

            _context.Commerces.Add(commerce);
            await _context.SaveChangesAsync();

            return commerce;
        }

        // Actualizar un comercio existente
        public async Task<bool> UpdateCommerceAsync(Guid id, Commerce commerce)
        {
            if (id != commerce.CommerceId)
            {
                return false;
            }

            _context.Entry(commerce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommerceExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        // Eliminar un comercio y su usuario propietario
        public async Task<bool> DeleteCommerceAsync(Guid id)
        {
            var commerce = await _context.Commerces.Include(c => c.Users).FirstOrDefaultAsync(c => c.CommerceId == id);
            if (commerce == null)
            {
                return false;
            }

            // Eliminar el usuario propietario si existe
            var owner = commerce.Users.FirstOrDefault(u => u.Role == "Owner");
            if (owner != null)
            {
                _context.Users.Remove(owner);
            }

            _context.Commerces.Remove(commerce);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool CommerceExists(Guid id)
        {
            return _context.Commerces.Any(e => e.CommerceId == id);
        }
    }
}
