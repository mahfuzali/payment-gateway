using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Models;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<AuthController> _logger;
        private readonly ITokenService _tokenService;

        public AuthController(IRepositoryWrapper repositoryWrapper, ILogger<AuthController> logger,
                                ITokenService tokenService)
        {
            _repositoryWrapper = repositoryWrapper ??
                throw new ArgumentNullException(nameof(repositoryWrapper));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _tokenService = tokenService ??
                throw new ArgumentNullException(nameof(tokenService));
        }

        /// <summary>
        /// Retrieving JWT token for authentication.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST  /api/auth/login
        ///     {
        ///        "username": "username",
        ///        "password": "password"
        ///     }
        /// </remarks>
        /// <param name="loginModel"></param>  
        /// <returns>Retrievs JWT and refresh token</returns>
        /// <response code="200">JWT and refresh token</response>
        /// <response code="401">Invalid credentials</response>  
        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginAsync(LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = await _repositoryWrapper.Users.GetLogin(loginModel.Username, loginModel.Password);

            if (user == null)
            {
                _logger.LogWarning("Did not find {login.Username}", loginModel.Username);
                return Unauthorized();
            }


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Username),
                new Claim(ClaimTypes.Role, "Manager")
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            _repositoryWrapper.Users.Update(user);

            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}
