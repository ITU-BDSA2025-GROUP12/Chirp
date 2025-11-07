using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;

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

   /* public List<Cheep> getCheeps(int page)
    {
        const int pageSize = 32; //cheeps per side
        int offset = (page-1) * pageSize; //hvor mange cheeps der springes over af databasen
        
        var cheeps = _context.Cheeps
            .Include(c => c.Author)
            .OrderByDescending(c => c.TimeStamp)
            .Skip(offset)
            .Take(pageSize);
        
        return  cheeps.ToList();
        
    }*/

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
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        //add more rules to creating author

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