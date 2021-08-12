using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SocialMedia3.Core.Entities;
using SocialMedia3.Core.Interfaces;
using SocialMedia3.Api.Responses;
using SocialMedia3.Core.DTOs;
using AutoMapper;
using SocialMedia3.Core.Enumerations;
using Microsoft.AspNetCore.Authorization;
using SocialMedia3.Infrastructure.Interfaces;

namespace SocialMedia3.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class SecurityController  : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public SecurityController(ISecurityService securityService, IMapper mapper, IPasswordService passwordService)
        {
            _securityService = securityService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<IActionResult> Security(SecurityDto securityDto)
        {
            var security = _mapper.Map<Security>(securityDto);
            security.Password =  _passwordService.Hash(security.Password);
            await _securityService.RegisterUser(security);
            var securityDtoNew = _mapper.Map<SecurityDto>(security);
            var response = new ApiResponse<SecurityDto>(securityDtoNew);
            return Ok(response);
        }

    }
}