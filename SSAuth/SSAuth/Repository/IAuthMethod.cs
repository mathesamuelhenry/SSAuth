using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSAuth.Repository
{
    public interface IAuthMethod
    {
        Task<string> EncryptPassword(string input);
    }
}
