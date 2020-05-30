using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IHttpService httpService;

        //endpoint in witch genresController located
        private string url = "api/genres";

        public GenreRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }
        public async Task<List<Genre>> GetGenres()
        {
            var response = await httpService.Get<List<Genre>>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<Genre> GetGenre(int Id)
        {
            var response = await httpService.Get<Genre>($"{url}/{Id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
        public async Task CreateGenre(Genre genre)
        {
            var respose =await httpService.Post(url, genre);
            if (!respose.Success)
            {
                throw new ApplicationException(await respose.GetBody());
            }
        }
        public async Task UpdateGenre(Genre genre)
        {
            var respose = await httpService.Put(url, genre);
            if (!respose.Success)
            {
                throw new ApplicationException(await respose.GetBody());
            }
        }

        public async Task DeleteGenre(int Id)
        {
            var respose = await httpService.Delete($"{url}/{Id}");
            if (!respose.Success)
            {
                throw new ApplicationException(await respose.GetBody());
            }
        }
    }
}
