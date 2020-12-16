using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Models;

namespace PaymentGateway.API.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<TokenController> _logger;
        private readonly ITokenService _tokenService;

        public TokenController(IRepositoryWrapper repositoryWrapper, ILogger<TokenController> logger,
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
        /// Gets the user information from the expired access token and validates the refresh token against the user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST  /api/token/refresh
        ///     {
        ///        "accessToken": "accessToken",
        ///        "refreshToken": "refreshToken"
        ///     }
        /// </remarks>
        /// <param name="tokenApiModel"></param>  
        /// <returns>Retrievs JWT and refresh token</returns>
        /// <response code="200">Generate a new access token and refresh token</response>
        /// <response code="400">Invalid request</response>  
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
            {
                return BadRequest("Invalid client request");
            }
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = _repositoryWrapper.Users.SingleOrDefault(u => u.Username == username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid client request");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;

            _repositoryWrapper.Users.Update(user);

            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        /// <summary>
        /// Revoke endpoint which invalidates the refresh token.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET  /api/token/revoke
        ///
        /// </remarks>
        /// <response code="200">Successfully revoked</response>
        /// <response code="404">Unsucessful</response>  
        [HttpPost, Authorize]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;

            var user = _repositoryWrapper.Users.SingleOrDefault(u => u.Username == username);

            if (user == null)
                return BadRequest();

            user.RefreshToken = null;

            _repositoryWrapper.Users.Update(user);

            return NoContent();
        }
    }
}
