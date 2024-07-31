﻿using Contracts.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Auth;
using Models.Ride;
using System.Security.Claims;

namespace TaxiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IAuthService authService;
        public RideController(IAuthService authService)
        {
            this.authService = authService;
        }

        private bool DoesUserHasRightsToAccess(UserType[] allowedTypes)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault((c) => c.Type == ClaimTypes.Email);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault((c) => c.Type == ClaimTypes.Role);

            if (userEmailClaim == null || userTypeClaim == null)
            {
                return false;
            }

            var isParsed = Enum.TryParse(userTypeClaim.Value, out UserType userType);

            if (!isParsed)
            {
                return false;
            }

            if (!allowedTypes.Contains(userType))
            {
                return false;
            }

            return true;
        }

        [HttpGet]
        [Authorize]
        [Route("estimate-ride")]
        public async Task<IActionResult> EstimateRide([FromBody] EstimateRideRequest request)
        {
            if(!DoesUserHasRightsToAccess(new UserType[] { UserType.CLIENT }))
            {
                return Unauthorized();
            }

            return Ok(await authService.EstimateRide(request));
        }
    }
}
