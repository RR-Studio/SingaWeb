using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singa.Models
{
    public static class TokenOptions
    {
        public const string Issuer = "SingaIssuer";
        public const string Audience = "SingaAudience";
        public static readonly string SecretKey = "Singa_mysupersecret_secretkey!@123";
        public static SymmetricSecurityKey SigningKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public static TimeSpan Expires = TimeSpan.FromDays(365);
    }
}
