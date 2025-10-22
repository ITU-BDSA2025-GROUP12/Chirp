using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Chirp.Core;
using Chirp.Razor.Data;

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