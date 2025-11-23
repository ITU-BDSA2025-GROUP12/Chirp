using Microsoft.EntityFrameworkCore;
using Chirp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;


namespace Chirp.Infrastructure.Repositories;

public class CheepRepository : ICheepRepository
{
    private readonly ChirpDBContext _context;
    private UserManager<Author> _userManager;

    public CheepRepository(ChirpDBContext context, UserManager<Author> userManager)
    {
        _userManager = userManager;
        _context = context;
    }

    
    // https://stackoverflow.com/questions/1618863/how-to-sort-a-collection-by-datetime-in-c-sharp
    public void SortByTime(List<Cheep> cheeps) // Command
    {
        cheeps.Sort((x, y) => DateTime.Compare(x.TimeStamp, y.TimeStamp));
        cheeps.Reverse(); // Reverses the list.
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
        SortByTime(result);
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
        SortByTime(result);
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
        SortByTime(result);
        return result;
    }


    public int GetCheepCount() // Query
    {
        return _context.Cheeps.Count();
    }

    public Task<int> GetCheepCountFromAuthor(string author) // Not implemented. async?
    {
        throw new NotImplementedException();
    }

    public async Task<string> FindAuthorNameByEmail(string email) // Query
    {
        var author =  await _userManager.FindByEmailAsync(email);
        Console.WriteLine(author + "'s name is: " + author.FirstName);
        return author.FirstName;
    }

    public async Task<Author?> FindAuthorByEmail(string email) // Query (Should be tested!!!)
    {
        var author = await _userManager.FindByEmailAsync(email);
        return author;
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

    public async void CreateCheep(String message, String email) // Command
    {
        if (message != "")
        {

            var author = await _userManager.FindByEmailAsync(email);
            
            if (author == null) throw new InvalidOperationException("Author not found.");

            var calcinId = GetCheepCount();
            var theId = calcinId + 1;

            var cheep = new Cheep
            {
                Text = message.Trim(),
                TimeStamp = DateTime.Now,
                Author = author,
                CheepId = theId,
            };
            
            _context.Cheeps.Add(cheep);
            _context.SaveChanges();
        }

    }


}