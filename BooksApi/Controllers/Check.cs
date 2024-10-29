using BooksServiceApi.Requests;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BooksServiceApi.Controllers
{
    public class Check : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Check(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsUserAdmin()
        {
            //var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies["wild-cookies"];
            if (string.IsNullOrEmpty(cookieValue))
            {
                return false;
            }
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadJwtToken(cookieValue);
                var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Authentication)?.Value;
                var role = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;
                return role == "admin";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading token: {ex.Message}");
                return false;
            }
        }
    }
}
