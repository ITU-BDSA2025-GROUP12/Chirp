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
    private const int PageSize = 32;


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
    /// <summary>
    /// Makes sure cheeps appear in order with newest cheeps first
    /// </summary>
    /// <param name="cheeps"></param>
    public void SortByTime(List<Cheep> cheeps) // Command
    {
        cheeps.Sort((x, y) => DateTime.Compare(x.TimeStamp, y.TimeStamp));
        cheeps.Reverse(); // Reverses the list.
    }
    /// <summary>
    /// Fetches cheeps to display
    /// </summary>
    /// <param name="page">How many pages to skip</param>
    /// <returns>A list of 32 cheeps, according to what page is displayed</returns>
    public List<Cheep> GetCheeps(int page)
    {
        int offset = (page - 1) * PageSize;

        return _context.Cheeps
            .Include(c => c.Author)
            .OrderByDescending(c => c.TimeStamp)
            .Skip(offset)
            .Take(PageSize)
            .ToList();
    }
    
   /// <summary>
   /// Fetches cheeps from a specific author
   /// </summary>
   /// <param name="author">The author whose cheeps will be fetched</param>
   /// <param name="page">How many pages of cheeps to skip</param>
   /// <returns>A list of at most 32 cheeps written by a specific author</returns>
   public List<Cheep> GetCheepsFromAuthor(string author, int page)
   {
       int offset = (page - 1) * PageSize;

       return _context.Cheeps
           .Include(c => c.Author)
           .Where(c =>
               c.Author.UserName == author ||
               c.Author.FirstName == author)
           .OrderByDescending(c => c.TimeStamp)
           .Skip(offset)
           .Take(PageSize)
           .ToList();
   }
    
   /// <summary>
   /// Finds an author by their username
   /// </summary>
   /// <param name="email">Username of the author we wish to find</param>
   /// <returns>The author</returns>
    public async Task<Author?> FindAuthorByEmail(string email)
    {
        return await _context.Authors
            .Include(a => a.Following)
            .SingleOrDefaultAsync(a => a.UserName == email);
    }
   /// <summary>
   /// Creates a new author to the database
   /// </summary>
   /// <param name="author">Instance of the Author class to be added to the database</param>
   /// <exception cref="ArgumentNullException">Thrown if no author is input</exception>
   /// <exception cref="ArgumentException">Thrown if the author doesn't have a valid name</exception>
   /// <exception cref="InvalidOperationException">Thrown if the email or username is already in use</exception>
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

public async Task<Author?> GetAuthorWithFollowingByEmail(string email)
{
    return await _context.Authors
        .Include(a => a.Following)
        .SingleOrDefaultAsync(a => a.UserName == email);
}

public async Task FollowAsync(int followerId, int followingId)
{
    if (followerId == followingId) return;

    var follower = await _context.Users
        .Include(a => a.Following)
        .SingleAsync(a => a.Id == followerId);

    var target = await _context.Users
        .SingleAsync(a => a.Id == followingId);

    if (!follower.Following.Any(a => a.Id == followingId))
    {
        follower.Following.Add(target);
        await _context.SaveChangesAsync();
    }
}

public async Task UnfollowAsync(int followerId, int followingId)
{
    var follower = await _context.Users
        .Include(a => a.Following)
        .SingleAsync(a => a.Id == followerId);

    var target = follower.Following
        .SingleOrDefault(a => a.Id == followingId);

    if (target != null)
    {
        follower.Following.Remove(target);
        await _context.SaveChangesAsync();
    }
}

public async Task<List<Cheep>> GetTimelineCheeps(Author currentUser, int page)
{
    const int pageSize = 32;
    int offset = (page - 1) * pageSize;

    var followedIds = currentUser.Following
        .Select(a => a.Id)
        .ToList();

    return await _context.Cheeps
        .Include(c => c.Author)
        .Where(c =>
            c.AuthorId == currentUser.Id ||
            followedIds.Contains(c.AuthorId))
        .OrderByDescending(c => c.TimeStamp)
        .Skip(offset)
        .Take(pageSize)
        .ToListAsync();
}

}