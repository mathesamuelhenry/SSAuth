using System;
using System.Collections.Generic;
using System.Text;

namespace SSAuth.Client
{
    public class ApiCallerBase
    {
        public ApiCallerBase(string uri)
        {
            ApiHelper.InitializeClient(uri);
        }
    }
}
