using Chirp.Core1;

namespace Chirp.Infrastructure.Repositories;

public interface ICheepRepository
{
    public List<Cheep> GetCheeps(int page); // Query
    public List<Cheep> GetCheepsFromAuthor(string author, int page); // Query
    Task<Author?> FindAuthorByUserName(string userName);
    Task CreateAuthor(Author author);
    Task CreateCheep(string message, string? name);
    public Task UnfollowAsync(int followerId, int followedId);
    public Task FollowAsync(int followerId, int followedId);
    Task<List<Cheep>> GetTimelineCheeps(Author currentUser, int page);

}