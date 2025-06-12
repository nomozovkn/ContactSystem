using ContactSystem.Application.Interfaces;
using ContactSystem.Core.Errors;
using ContactSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Infrastructure.Persistance.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;
    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task InsertRefreshTokenAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public Task RemoveRefreshTokenAsync(string token)
    {
       var refreshToken = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == token);
        if (refreshToken == null)
        {
            throw new NotFiniteNumberException("Refresh token not found.");
        }
        _context.RefreshTokens.Remove(refreshToken);

        return _context.SaveChangesAsync();
    }

    public async Task<RefreshToken> SelectRefreshTokenAsync(string refreshToken, long userId)
    {
        var tokens = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken && rt.UserId == userId);
        if (tokens == null)
        {
            throw new KeyNotFoundException("Refresh token not found.");
        }
        return tokens;
    }

    public async  Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
    {
        var existingToken = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken.Token);
        if (existingToken == null)
        {
            throw new EntityNotFoundException($"Refresh token {refreshToken.Token} not found for user {refreshToken.UserId}");
        }
        existingToken.IsRevoked = refreshToken.IsRevoked;

        existingToken.Expire = refreshToken.Expire;

        _context.RefreshTokens.Update(existingToken);
        await _context.SaveChangesAsync();
    }
}
