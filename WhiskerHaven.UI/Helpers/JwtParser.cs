using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace WhiskerHaven.UI.Helpers
{
    /// <summary>
    /// Helper class for parsing claims from a JWT token.
    /// </summary>
    public class JwtParser
    {
        /// <summary>
        /// Parses claims from a JWT token.
        /// </summary>
        /// <param name="jwt">The JWT token.</param>
        /// <returns>The claims extracted from the JWT token.</returns>
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];

            var jsontBytes = ParseBase64WithoutMargin(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsontBytes);
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        /// <summary>
        /// Parses base64 string without padding.
        /// </summary>
        /// <param name="base64">The base64 string.</param>
        /// <returns>The byte array of the base64 string without padding.</returns>
        private static byte[] ParseBase64WithoutMargin(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}
