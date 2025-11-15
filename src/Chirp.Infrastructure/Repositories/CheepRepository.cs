using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Chirp.Infrastructure.Data;
using SQLitePCL;

namespace Chirp.Infrastructure.Repositories;

public class CheepRepository : ICheepRepository
{
    private readonly ChirpDBContext _context;

    public CheepRepository(ChirpDBContext context)
    {
        _context = context;
    }
    public List<Cheep> GetCheeps(int page) // Query
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
        return result;
    }


    public List<Cheep> GetCheepsFromAuthor(string author, int page) // Query
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

        return result;
    }


    public Task<int> GetCheepCount() // Not implemented. async??
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCheepCountFromAuthor(string author) // Not implemented. async?
    {
        throw new NotImplementedException();
    }

    public async Task<Author?> FindAuthorByName(string name) // Query
    {
        return await _context.Authors
        .AsNoTracking()
        .FirstOrDefaultAsync(a => a.Name == name);
    }

    public async Task<Author?> FindAuthorByEmail(string email) // Query
    {
        return await _context.Authors
        .AsNoTracking()
        .FirstOrDefaultAsync(a => a.Email == email);
    }

    public async Task CreateAuthor(Author author) // Command
    {

        if (author == null) throw new ArgumentNullException(nameof(author), "Author cannot be empty.");

        if (string.IsNullOrWhiteSpace(author.Name)) throw new ArgumentException("Author name cannot be empty or whitespace.");

        if (author.Name.Length > 30) throw new ArgumentException("Author name cannot be longer than 30 characters");

        if (string.IsNullOrWhiteSpace(author.Email)) throw new ArgumentException("Author email cannot be empty.", nameof(author.Email));

        var existing = await _context.Authors.FirstOrDefaultAsync(async => async.Name == author.Name || async.Email == author.Email);
        if (existing != null) throw new InvalidOperationException("An author with this name or email already exists.");

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();


    }

    public async Task CreateCheep(Cheep cheep) // Command
    {
        if (cheep.Author != null)
        {
            _context.Cheeps.Add(cheep);
            await _context.SaveChangesAsync();
        }
        else {
            if (cheep.Author == null)
            {
                throw new InvalidOperationException("Cannot create cheep: author not found");
            }
        }

    }


}