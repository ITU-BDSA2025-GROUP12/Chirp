using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Chirp.Razor.Data;
using SQLitePCL;

namespace Chirp.Razor.Data;

public class CheepRepository : ICheepRepository
{
    private readonly ChirpDBContext _context;

    public CheepRepository(ChirpDBContext context)
    {
        _context = context;
    }
    public Task<IEnumerable<Cheep>> GetCheeps(int page)
    {
        var query = _context.Cheeps
            .Join(_context.Authors,
                cheep => cheep.AuthorId,
                author => author.AuthorId,
                (cheep, author) =>
                new Cheep()
                {
                    AuthorId = cheep.AuthorId,
                    CheepId = cheep.CheepId,
                    Text = cheep.Text,
                    TimeStamp = cheep.TimeStamp,
                    Author = author
                });

        var result = query.ToList();
        return Task.FromResult<IEnumerable<Cheep>>(result);
    }


    public Task<IEnumerable<Cheep>> GetCheepsFromAuthor(string author, int page)
    {
        var result = _context.Cheeps
            .Join(_context.Authors,
                cheep => cheep.AuthorId,
                a => a.AuthorId,
                (cheep, a) => new { cheep, a })
            .Where(x => x.a.Name == author)
            .Select(x => new Cheep
            {
                AuthorId = x.cheep.AuthorId,
                CheepId  = x.cheep.CheepId,
                Text     = x.cheep.Text,
                TimeStamp= x.cheep.TimeStamp,
                Author   = x.a
            })
            .ToList();

        return Task.FromResult<IEnumerable<Cheep>>(result);
    }


    public async Task<int> GetCheepCount()
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetCheepCountFromAuthor(string author)
    {
        throw new NotImplementedException();
    }

    public async Task<Author?> FindAuthorByName(string name)
    {
        return await _context.Authors
        .AsNoTracking()
        .FirstOrDefaultAsync(a => a.Name == name);
    }

    public async Task<Author?> FindAuthorByEmail(string email)
    {
        return await _context.Authors
        .AsNoTracking()
        .FirstOrDefaultAsync(a => a.Email == email);
    }

    public async Task CreateAuthor(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }

    public Task CreateCheep(Cheep cheep)
    {
        _context.Cheeps.Add(cheep);
        _context.SaveChanges();
        return Task.CompletedTask;
    }


}