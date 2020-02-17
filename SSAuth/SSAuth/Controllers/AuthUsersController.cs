using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Church.API.Models.AppModel.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSAuth.Data.DBContext;
using SSAuth.Models;
using SSAuth.Models.AppModels.Request.AuthUser;
using SSAuth.Models.AppModels.Response.AuthUser;
using SSAuth.Repository;

namespace SSAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUsersController : ControllerBase
    {
        private readonly SSAuthContext _context;
        private readonly IAuthMethod _authMethod;

        public AuthUsersController(SSAuthContext context, IAuthMethod authMethod)
        {
            _context = context;
            _authMethod = authMethod;
        }

        // GET: api/AuthUsers
        [HttpGet]
        public async Task<IEnumerable<GetUserResponse>> GetAuthUser()
        {
            var userResponseList = new List<GetUserResponse>();

            var AuthUserList = await _context.AuthUser
                .ToListAsync();

            foreach(var authUser in AuthUserList)
            {
                userResponseList.Add(new GetUserResponse
                {
                    AuthUserId = authUser.AuthUserId,
                    FirstName = authUser.FirstName,
                    LastName = authUser.LastName,
                    EmailId = authUser.Email,
                    LoginId = authUser.LoginId,
                    UserStatus = authUser.Status
                });
            }

            return userResponseList;
        }

        // GET: api/AuthUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserResponse>> GetAuthUser(int id)
        {
            var authUser = await _context
                .AuthUser
                .FindAsync(id);

            if (authUser == null)
            {
                return NotFound();
            }

            return new GetUserResponse()
            {
                AuthUserId = authUser.AuthUserId,
                FirstName = authUser.FirstName,
                LastName = authUser.LastName,
                EmailId = authUser.Email,
                LoginId = authUser.LoginId,
                UserStatus = authUser.Status
            };       
        }

        // GET: api/AuthUsers/5
        [HttpGet]
        [Route("GetByLoginId/{loginId}")]
        public async Task<ActionResult<GetUserResponse>> GetAuthUserByLoginId(string loginId)
        {
            var authUser = await _context
                .AuthUser
                .Where(x => x.LoginId.Equals(loginId))
                .FirstOrDefaultAsync();

            if (authUser == null)
            {
                return NotFound();
            }

            return new GetUserResponse()
            {
                AuthUserId = authUser.AuthUserId,
                FirstName = authUser.FirstName,
                LastName = authUser.LastName,
                EmailId = authUser.Email,
                LoginId = authUser.LoginId,
                UserStatus = authUser.Status
            };
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
        [Route("Register")]
        public async Task<ActionResult<GetUserResponse>> PostAuthUser(AuthUser authUser)
        {
            if (string.IsNullOrWhiteSpace(authUser.FirstName))
                return BadRequest("First name cannot be empty");

            if (string.IsNullOrWhiteSpace(authUser.LastName))
                return BadRequest("Last name cannot be empty");

            if (string.IsNullOrWhiteSpace(authUser.UserAdded))
                return BadRequest("User Added cannot be empty");

            bool existsLoginIdOrEmailId = await _context.AuthUser
                .AnyAsync(x => x.Email == authUser.Email || x.LoginId == authUser.LoginId);

            if (existsLoginIdOrEmailId)
                return BadRequest("Email Id/Login Id already exists.");

            if (string.IsNullOrWhiteSpace(authUser.Status))
                authUser.Status = "A";

            authUser.Password = await _authMethod.EncryptPassword(authUser.Password);
            authUser.DateAdded = DateTime.UtcNow;
            authUser.AuthUserId = await Utils.GetNextIdAsync(_context, "auth_user");

            foreach (var userRole in authUser.UserRole)
            {
                userRole.DateAdded = DateTime.UtcNow;
                userRole.AuthUserId = authUser.AuthUserId;
                userRole.UserRoleId = await Utils.GetNextIdAsync(_context, "user_role");
            }

            foreach (var userQuestion in authUser.UserSecurityQuestion)
            {
                userQuestion.DateAdded = DateTime.UtcNow;
                userQuestion.AuthUserId = authUser.AuthUserId;
                userQuestion.UserSecurityQuestionId = await Utils.GetNextIdAsync(_context, "user_security_question");
            }

            _context.AuthUser.Add(authUser);

            await _context.SaveChangesAsync();

            var responseObject = new GetUserResponse(authUser);

            return CreatedAtAction("GetAuthUser", new { id = authUser.AuthUserId }, responseObject);
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<ActionResult<GetUserResponse>> Authenticate(SignIn request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var currentUserInfo = await this._context.AuthUser
                .Where(u => u.LoginId == request.LoginId)
                .FirstOrDefaultAsync();

            if (currentUserInfo != null)
            {
                var requestPasswordHash = await _authMethod.EncryptPassword(request.Password);

                if (requestPasswordHash != currentUserInfo.Password)
                {
                    return BadRequest("Invalid Password");
                }

                // Add Login History
                var userLoginHistory = new LoginHistory()
                {
                    LoginHistoryId = await Utils.GetNextIdAsync(_context, "login_history"),
                    AuthUserId = currentUserInfo.AuthUserId,
                    LoginId = currentUserInfo.LoginId,
                    LoginDate = DateTime.UtcNow,
                    DateAdded = DateTime.UtcNow,
                    UserAdded = "SSAdmin"
                };

                _context.LoginHistory.Add(userLoginHistory);

                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Invalid User/does not exist");
            }

            var responseObject = new GetUserResponse(currentUserInfo);

            return CreatedAtAction("GetAuthUser", new { id = currentUserInfo.AuthUserId }, responseObject);
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server error</response>
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordFromOriginal request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var currentUserInfo = await this._context.AuthUser
                .Where(u => u.LoginId == request.LoginId)
                .FirstOrDefaultAsync();

            if (currentUserInfo != null)
            {
                var requestOriginalPasswordHash = await _authMethod.EncryptPassword(request.OriginalPassword);

                if (requestOriginalPasswordHash != currentUserInfo.Password)
                {
                    return BadRequest("Old Password does not match your current password");
                }

                var newPasswordHash = await _authMethod.EncryptPassword(request.NewPassword);
                var confirmNewPasswordHash = await _authMethod.EncryptPassword(request.ConfirmNewPassword);

                if (newPasswordHash != confirmNewPasswordHash)
                {
                    return BadRequest("New password and Confirm password don't match");
                }

                currentUserInfo.Password = newPasswordHash;
                currentUserInfo.DateChanged = DateTime.UtcNow;
                currentUserInfo.UserChanged = request.LoginId;

                _context.Entry(currentUserInfo).Property(x => x.Password).IsModified = true;
                _context.Entry(currentUserInfo).Property(x => x.DateChanged).IsModified = true;
                _context.Entry(currentUserInfo).Property(x => x.UserChanged).IsModified = true;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Invalid User/does not exist");
            }

            return NoContent();
        }


        /// <summary>
        /// Change Password
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server error</response>
        [HttpPost]
        [Route("ChangePasswordByQuestion")]
        public async Task<IActionResult> ChangePasswordByQuestion(ChangePasswordByQuestion request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var currentUserInfo = await this._context.AuthUser
                .Where(u => u.LoginId == request.LoginId)
                .FirstOrDefaultAsync();

            if (currentUserInfo != null)
            {
                // questions
                var userDBSecurityQuestion = await _context.UserSecurityQuestion
                    .Include(x => x.AuthUser)
                    .Where(x => x.AuthUser.LoginId == request.LoginId && x.SecurityQuestionId == request.QuestionId)
                    .FirstOrDefaultAsync();

                if (userDBSecurityQuestion == null)
                    return BadRequest("Invalid security question for user");

                if (userDBSecurityQuestion.Answer.Equals(request.Answer, StringComparison.InvariantCultureIgnoreCase))
                {
                    var newPasswordHash = await _authMethod.EncryptPassword(request.NewPassword);
                    var confirmNewPasswordHash = await _authMethod.EncryptPassword(request.ConfirmNewPassword);

                    if (newPasswordHash != confirmNewPasswordHash)
                    {
                        return BadRequest("New password and Confirm password don't match");
                    }

                    currentUserInfo.Password = newPasswordHash;
                    currentUserInfo.DateChanged = DateTime.UtcNow;
                    currentUserInfo.UserChanged = request.LoginId;

                    _context.Entry(currentUserInfo).Property(x => x.Password).IsModified = true;
                    _context.Entry(currentUserInfo).Property(x => x.DateChanged).IsModified = true;
                    _context.Entry(currentUserInfo).Property(x => x.UserChanged).IsModified = true;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return NotFound("Invalid Security Answer");
                }
            }
            else
            {
                return BadRequest("Invalid User/does not exist");
            }

            return NoContent();
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
