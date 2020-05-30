using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IHttpService httpService;

        //endpoint in witch Controller located
        private string url = "api/people";

        public PersonRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<PaginatedResponse<List<Person>>> GetPeople(PaginationDTO paginationDTO)
        {
            return await httpService.GetHelper<List<Person>>(url, paginationDTO);
        }

        public async Task<List<Person>> GetPeopleByName(string name)
        {
            var response = await httpService.Get<List<Person>>($"{url}/search/{name}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task CreatePerson(Person person)
        {
            var respose = await httpService.Post(url, person);
            if (!respose.Success)
            {
                throw new ApplicationException(await respose.GetBody());
            }
        }

        public async Task UpdatePerson(Person person)
        {
            var respose = await httpService.Put(url, person);
            if (!respose.Success)
            {
                throw new ApplicationException(await respose.GetBody());
            }
        }
        //  GetPeopleById
        public async Task<Person> GetPeopleById(int id)
        {
            return await httpService.GetHelper<Person>($"{url}/{id}");
        }

        public async Task DeletePerson(int Id)
        {
            var respose = await httpService.Delete($"{url}/{Id}");
            if (!respose.Success)
            {
                throw new ApplicationException(await respose.GetBody());
            }
        }
    }
}
