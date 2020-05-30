using AutoMapper;
using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.DTOs;
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
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext Dbcontext;
        private readonly IFileStorageService fileStorageService;
        private readonly IMapper mapper;

        public PeopleController(ApplicationDbContext context, IFileStorageService fileStorageService,IMapper mapper)
        {
            Dbcontext = context;
            this.fileStorageService = fileStorageService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get([FromQuery]PaginationDTO paginationDTO)
        {
            var queryable = Dbcontext.People.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, paginationDTO.RecordsPerPage);
            return await queryable.Paginate(paginationDTO).ToListAsync();
        }

        //[HttpGet]
        //public async Task<ActionResult<List<Person>>> Get()
        //{
        //    return await Dbcontext.People.ToListAsync();
        //}

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<Person>>> FilterByName(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText)) { return new List<Person>(); }
            return await Dbcontext.People.Where(x => x.Name.Contains(searchText)).Take(5).ToListAsync();
        }






        [HttpPost]
        public async Task<ActionResult<int>> Post(Person person)
        {
            if (!string.IsNullOrWhiteSpace(person.Picture))
            {
                var personPicture = Convert.FromBase64String(person.Picture);
                person.Picture = await fileStorageService.SaveFile(personPicture, "jpg", "people");
            }

            Dbcontext.Add(person);
            await Dbcontext.SaveChangesAsync();
            return person.Id;
        }


        //Get a specific data from db
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = await Dbcontext.People.FirstOrDefaultAsync(x => x.Id == id);
            if (person == null) { return NotFound(); }
            return person;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Person person)
        {
            var personDB = await Dbcontext.People.FirstOrDefaultAsync(x => x.Id == person.Id);

            if (personDB == null) { return NotFound(); }

            personDB = mapper.Map(person, personDB);

            if (!string.IsNullOrWhiteSpace(person.Picture))//null means that person does't want to update the picture of the person
            {
                var personPicture = Convert.FromBase64String(person.Picture);
                personDB.Picture = await fileStorageService.EditFile(personPicture, "jpeg", "people", personDB.Picture);
            }
            await Dbcontext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var person = await Dbcontext.People.FirstOrDefaultAsync(x => x.Id == Id);
            if (person == null)
            {
                return NotFound();
            }

            Dbcontext.Remove(person);
            await Dbcontext.SaveChangesAsync();
            return NoContent();
        }
    }
}
