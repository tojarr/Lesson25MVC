using Lesson25.Models;
using Lesson25.RestSharp;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson25.Helpers
{
    public class TokenHelper
    {
        public readonly RestSharpHelper restSharpHtlper;

        public TokenHelper()
        {
            restSharpHtlper = new RestSharpHelper();
        }

        public Token GetRefreshToken(string login, string password)
        {
            var response = restSharpHtlper.Execute<Token>("api/token", Method.POST,
                new Dictionary<string, object>()
                {
                    {"application/json", $"grant_type=password&username={login}&password={password}" }
                }, false, DataFormat.None
                );
            return response.Data;
        }
    }
}