
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace SantasLittleHelper
{
    public class CustomAuthStateProvider : AuthenticationStateProvider {
        private string _token = "";
        private AuthenticationState authState;
        private string _email = "";
        private string _id = "";


        public void AuthenticateUser(string token)
        {
            _token = token;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        
        public string GetToken()
        {
            return _token;
        }

        public string GetEmail()
        {
            return _email;
        }

        public string GetId()
        {
            return _id;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claims = ParseJWT(_token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            authState = state;

            NotifyAuthenticationStateChanged(Task.FromResult(authState));

            return Task.FromResult(authState);
        }

        // grabosky is dumb
        // https://github.com/patrickgod/BlazorAuthenticationTutorial/blob/master/BlazorAuthenticationTutorial/Client/CustomAuthStateProvider.cs
        public List<Claim> ParseJWT(string jwt) {
            var claims = new List<Claim>();

            if(jwt != null) {
                if (jwt != "")
                {
                    var payload = jwt.Split('.')[1];
                    var jsonBytes = ParseBase64WithoutPadding(payload);
                    var decoded = JsonSerializer.Deserialize<Datamodels.CustomJWT>(jsonBytes);
                    Console.WriteLine("Decoded JWT: " + JsonSerializer.Serialize(decoded));

                    claims.Add(new Claim(ClaimTypes.Name, decoded.email));
                    _email = decoded.email;
                    _id = decoded.id;
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, decoded.id));

                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Name, "UNAUTHENTICATED"));
                }
            } else {
                claims.Add(new Claim(ClaimTypes.Name, "UNAUTHENTICATED"));
            }

            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
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