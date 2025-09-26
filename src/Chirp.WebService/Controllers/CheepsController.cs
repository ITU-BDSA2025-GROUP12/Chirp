using Chirp.Core;
using Microsoft.AspNetCore.Mvc;
using SimpleDB;
using System.Collections.Generic;

namespace Chirp.WebService.Controllers;

[ApiController]
[Route("")]
public class CheepsController : ControllerBase
{
    private readonly IDatabaseRepository<Cheep> _database;

    public CheepsController(IDatabaseRepository<Cheep> database)
    {
        _database = database;
    }

    [HttpGet("cheeps")]
    public ActionResult<List<Cheep>> GetCheeps()
    {
        Cheep c = new("Nancy", "Davolio",0);
        //var cheeps = _database.Read();
        return Ok(new List<Cheep> { c });
    }

    [HttpPost("cheep")]
    public ActionResult PostCheep([FromBody] Cheep newCheep)
    {
        //_database.Store(newCheep);
        return Ok();
    }
}