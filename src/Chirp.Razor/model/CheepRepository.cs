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

    public List<Cheep> GetCheeps(int page)
    {
        // TODO: implement method to get cheeps
        var query = _context.Cheeps.Select(cheep => cheep);
        /*.Join(_context.Authors,
              cheep => cheep.AuthorId,
              author => author.AuthorId,
              (cheep, author) =>
              new
              {
                  cheep.AuthorId,
                  cheep.CheepId,
                  cheep.Text,
                  cheep.TimeStamp,
                  Author = author

              }
        );
*/
        var result = query.ToList();
        foreach(Cheep c in result)
        {
            Console.WriteLine(c.CheepId);
            Console.WriteLine(c.AuthorId);
            Console.WriteLine(c.TimeStamp);
            Console.WriteLine(c.Author != null);
        }
        return result;
    }

    public List<Cheep> GetCheepsFromAuthor(string author, int page)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetCheepCount()
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetCheepCountFromAuthor(string author)
    {
        throw new NotImplementedException();
    }
}