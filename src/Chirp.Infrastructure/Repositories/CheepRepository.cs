using Microsoft.EntityFrameworkCore;


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
                cheep => cheep.Id,
                author => author.Id,
                (cheep, author) =>
                new Cheep()
                {
                    Id = cheep.Id,
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
                cheep => cheep.Id,
                a => a.Id,
                (cheep, a) => new { cheep, a })
            .Where(x => x.a.FirstName == author)
            .Select(x => new Cheep
            {
                Id = x.cheep.Id,
                CheepId  = x.cheep.CheepId,
                Text     = x.cheep.Text,
                TimeStamp= x.cheep.TimeStamp,
                Author   = x.a
            })
            .ToList();

        return result;
    }

    public List<Cheep> GetCheepsFromEmail(string email, int page) // Query
    {
        var result = _context.Cheeps
            .Join(_context.Authors,
                cheep => cheep.Id,
                a => a.Id,
                (cheep, a) => new { cheep, a })
            .Where(x => x.a.Email == email)
            .Select(x => new Cheep
            {
                Id = x.cheep.Id,
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
        .FirstOrDefaultAsync(a => a.FirstName == name);
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

        if (string.IsNullOrWhiteSpace(author.FirstName)) throw new ArgumentException("Author name cannot be empty or whitespace.");

        if (author.FirstName.Length > 30) throw new ArgumentException("Author name cannot be longer than 30 characters");

        if (string.IsNullOrWhiteSpace(author.Email)) throw new ArgumentException("Author email cannot be empty.", nameof(author.Email));

        var existing = await _context.Authors.FirstOrDefaultAsync(async => async.FirstName == author.FirstName|| async.Email == author.Email);
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