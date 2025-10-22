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
        var query = _context.Cheeps
        .Join(_context.Authors,
              cheep => cheep.AuthorId,
              author => author.AuthorId,
              (cheep, author) =>
              new Cheep()
              {
                  AuthorId=cheep.AuthorId,
                  CheepId=cheep.CheepId,
                  Text=cheep.Text,
                  TimeStamp=cheep.TimeStamp,
                  Author = author

              }
        );
        var result = query.ToList();
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