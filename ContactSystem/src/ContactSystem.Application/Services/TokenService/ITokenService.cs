﻿using ContactSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Application.Services.TokenService;

public interface ITokenService
{
    public string GenerateToken(UserDto userDto);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    public string RemoveRefreshTokenAsync(string token);
}
