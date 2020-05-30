using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext Dbcontext;
        public GenresController(ApplicationDbContext context)
        {
            Dbcontext = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            return await Dbcontext.Genres.ToListAsync();
        }

        // Endpoint which is going to respond to HTTPPOST
        [HttpPost]
        public async Task<ActionResult<int>> Post(Genre genre)
        {
            Dbcontext.Add(genre);
            await Dbcontext.SaveChangesAsync();
            return genre.Id;
        }
        //Get a specific data from db
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            var genre = await Dbcontext.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null) { return NotFound(); }
            return genre;
        }
        [HttpPut]
        public async Task<ActionResult> Put(Genre genre)
        {
            Dbcontext.Attach(genre).State = EntityState.Modified;
            await Dbcontext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult>Delete(int Id)
        {
            var genre = await Dbcontext.Genres.FirstOrDefaultAsync(x => x.Id == Id);
            if(genre == null)
            {
                return NotFound();
            }

            Dbcontext.Remove(genre);
            await Dbcontext.SaveChangesAsync();
            return NoContent();
        }
    }
}
