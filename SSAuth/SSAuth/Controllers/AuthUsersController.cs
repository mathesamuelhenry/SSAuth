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
    public class AuthUsersController : ControllerBase
    {
        private readonly SSAuthContext _context;

        public AuthUsersController(SSAuthContext context)
        {
            _context = context;
        }

        // GET: api/AuthUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthUser>>> GetAuthUser()
        {
            return await _context.AuthUser.ToListAsync();
        }

        // GET: api/AuthUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthUser>> GetAuthUser(int id)
        {
            var authUser = await _context.AuthUser.FindAsync(id);

            if (authUser == null)
            {
                return NotFound();
            }

            return authUser;
        }

        // PUT: api/AuthUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthUser(int id, AuthUser authUser)
        {
            if (id != authUser.AuthUserId)
            {
                return BadRequest();
            }

            _context.Entry(authUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthUserExists(id))
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

        // POST: api/AuthUsers
        [HttpPost]
        public async Task<ActionResult<AuthUser>> PostAuthUser(AuthUser authUser)
        {
            _context.AuthUser.Add(authUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthUser", new { id = authUser.AuthUserId }, authUser);
        }

        // DELETE: api/AuthUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthUser>> DeleteAuthUser(int id)
        {
            var authUser = await _context.AuthUser.FindAsync(id);
            if (authUser == null)
            {
                return NotFound();
            }

            _context.AuthUser.Remove(authUser);
            await _context.SaveChangesAsync();

            return authUser;
        }

        private bool AuthUserExists(int id)
        {
            return _context.AuthUser.Any(e => e.AuthUserId == id);
        }
    }
}
