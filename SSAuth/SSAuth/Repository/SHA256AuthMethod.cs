using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAuth.Repository
{
    public class SHA256AuthMethod : IAuthMethod
    {
        public SHA256AuthMethod()
        {

        }

        public Task<string> EncryptPassword(string input)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return Task.FromResult(hash.ToString());
        }
    }
}
