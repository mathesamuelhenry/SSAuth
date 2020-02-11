using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSAuth.Data.DBContext;
using SSAuth.Models;

namespace SSAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthMethodsController : ControllerBase
    {
        private readonly SSAuthContext _context;

        public AuthMethodsController(SSAuthContext context)
        {
            _context = context;
        }

        // GET: api/AuthMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthMethod>>> GetAuthMethod()
        {
            return await _context.AuthMethod.ToListAsync();
        }

        // GET: api/AuthMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthMethod>> GetAuthMethod(int id)
        {
            var authMethod = await _context.AuthMethod.FindAsync(id);

            if (authMethod == null)
            {
                return NotFound();
            }

            return authMethod;
        }

        // PUT: api/AuthMethods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthMethod(int id, AuthMethod authMethod)
        {
            if (id != authMethod.AuthMethodId)
            {
                return BadRequest();
            }

            _context.Entry(authMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthMethodExists(id))
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

        // POST: api/AuthMethods
        [HttpPost]
        public async Task<ActionResult<AuthMethod>> PostAuthMethod(AuthMethod authMethod)
        {
            _context.AuthMethod.Add(authMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthMethod", new { id = authMethod.AuthMethodId }, authMethod);
        }

        // DELETE: api/AuthMethods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthMethod>> DeleteAuthMethod(int id)
        {
            var authMethod = await _context.AuthMethod.FindAsync(id);
            if (authMethod == null)
            {
                return NotFound();
            }

            _context.AuthMethod.Remove(authMethod);
            await _context.SaveChangesAsync();

            return authMethod;
        }

        private bool AuthMethodExists(int id)
        {
            return _context.AuthMethod.Any(e => e.AuthMethodId == id);
        }
    }
}
