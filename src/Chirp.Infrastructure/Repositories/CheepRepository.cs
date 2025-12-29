using Microsoft.EntityFrameworkCore;
using Chirp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Chirp.Core1;


namespace Chirp.Infrastructure.Repositories;


/// <summary>
/// Fetches and inserts data from and into the database
/// </summary>
public class CheepRepository : ICheepRepository
{
    private readonly ChirpDBContext _context;
    private readonly UserManager<Author> _userManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">Database-context - this one has talks directly with the database</param>
    /// <param name="userManager">Keeps track of logged-in user</param>
   public CheepRepository(ChirpDBContext context, UserManager<Author> userManager)
{
    _context = context;
    _userManager = userManager;
}


    
    // https://stackoverflow.com/questions/1618863/how-to-sort-a-collection-by-datetime-in-c-sharp
    public void SortByTime(List<Cheep> cheeps) // Command
    {
        cheeps.Sort((x, y) => DateTime.Compare(x.TimeStamp, y.TimeStamp));
        cheeps.Reverse(); // Reverses the list.
    }
    public List<Cheep> GetCheeps(int page) //Query
    {
        return _context.Cheeps
            .Include(c => c.Author)
            .OrderByDescending(c => c.TimeStamp)
            .ToList();
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

   public List<Cheep> GetCheepsFromAuthor(string author, int page) //Query
    {
        var result = _context.Cheeps
            .Include(c => c.Author)
            .Where(c =>
                c.Author.UserName == author ||
                c.Author.FirstName == author)
            .OrderByDescending(c => c.TimeStamp)
            .ToList();

        return result;
    }


   public List<Cheep> GetCheepsFromEmail(string email, int page) //Query
    {
        return _context.Cheeps
            .Include(c => c.Author)
            .Where(c => c.Author.Email == email)
            .OrderByDescending(c => c.TimeStamp)
            .ToList();
    }



    public async Task<int> GetCheepCount() // Query
    {
        return _context.Cheeps.Count();
    }

    public Task<int> GetCheepCountFromAuthor(string author) // Not implemented. async?
    {
        throw new NotImplementedException();
    }
    

    public async Task<string> FindAuthorNameByEmail(string email) // Query
    {
        
        var author = await _userManager.FindByEmailAsync(email);
        if (author == null) throw new ArgumentNullException("Email can't be null");
        if (string.IsNullOrEmpty(author.FirstName))
        {
            author.FirstName = email;
            Console.WriteLine("Bro didn't have a name so I named him " + author.FirstName + " myself!");
        }
        Console.WriteLine(author + "'s name is: " + author.FirstName);
        return author.FirstName;
    }


    public async Task<Author?> FindAuthorByEmail(string email)
    {
        var result =  _context.Authors
            .Where(x => x.UserName == email);

        if (!result.Any())
        {
            return null;
        }

        return result.First();
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

// CheepRepository.cs
public async Task CreateCheep(string message, string? name)
{
    var author = await _context.Authors
        .SingleOrDefaultAsync(a => a.UserName == name);

    if (author == null)
        throw new InvalidOperationException("Author not found.");

    var cheep = new Cheep
    {
        Text = message,
        TimeStamp = DateTime.UtcNow,
        Author = author
    };

    _context.Cheeps.Add(cheep);
    await _context.SaveChangesAsync();
}




}