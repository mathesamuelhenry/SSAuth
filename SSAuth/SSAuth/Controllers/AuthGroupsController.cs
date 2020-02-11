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
    public class AuthGroupsController : ControllerBase
    {
        private readonly SSAuthContext _context;

        public AuthGroupsController(SSAuthContext context)
        {
            _context = context;
        }

        // GET: api/AuthGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthGroup>>> GetAuthGroup()
        {
            return await _context.AuthGroup.ToListAsync();
        }

        // GET: api/AuthGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthGroup>> GetAuthGroup(int id)
        {
            var authGroup = await _context.AuthGroup.FindAsync(id);

            if (authGroup == null)
            {
                return NotFound();
            }

            return authGroup;
        }

        // PUT: api/AuthGroups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthGroup(int id, AuthGroup authGroup)
        {
            if (id != authGroup.AuthGroupId)
            {
                return BadRequest();
            }

            _context.Entry(authGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthGroupExists(id))
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

        // POST: api/AuthGroups
        [HttpPost]
        public async Task<ActionResult<AuthGroup>> PostAuthGroup(AuthGroup authGroup)
        {
            _context.AuthGroup.Add(authGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthGroup", new { id = authGroup.AuthGroupId }, authGroup);
        }

        // DELETE: api/AuthGroups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthGroup>> DeleteAuthGroup(int id)
        {
            var authGroup = await _context.AuthGroup.FindAsync(id);
            if (authGroup == null)
            {
                return NotFound();
            }

            _context.AuthGroup.Remove(authGroup);
            await _context.SaveChangesAsync();

            return authGroup;
        }

        private bool AuthGroupExists(int id)
        {
            return _context.AuthGroup.Any(e => e.AuthGroupId == id);
        }
    }
}
