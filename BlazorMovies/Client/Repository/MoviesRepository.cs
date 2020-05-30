﻿using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly IHttpService httpService;

        //endpoint in witch Controller located
        private string url = "api/movies";

        public MoviesRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<IndexPageDTO> GetIndexPageDTO()
        {
            return await httpService.GetHelper<IndexPageDTO>(url);
        }
        public async Task<MovieUpdateDTO> GetMovieForUpdate(int id)
        {
            return await httpService.GetHelper<MovieUpdateDTO>($"{url}/update/{id}");
        }

        public async Task<DetailsMovieDTO> GetDetailsMovieDTO(int id)
        {

            return await httpService.GetHelper<DetailsMovieDTO>($"{url}/{id}");
        }

        public async Task<PaginatedResponse<List<Movie>>> GetMovieFiltered(FilterMoviesDTO filterMoviesDTO)
        {
            var responseHTTP = await httpService.Post<FilterMoviesDTO, List<Movie>>($"{url}/filter", filterMoviesDTO);
            var totalAmountPages = int.Parse(responseHTTP.HttpResponseMessage.Headers.GetValues("totalAmountPages").FirstOrDefault());
            var paginatedResponse = new PaginatedResponse<List<Movie>>()
            { 
                Response = responseHTTP.Response,
                TotalAmountPages = totalAmountPages
            };
            return paginatedResponse;
        }

        public async Task<int> CreateMovie(Movie movie)
        {uigk
            var respose = await httpService.Post<Movie, int>(url, movie);
            if (!respose.Success)
            {
                throw new ApplicationException(await respose.GetBody());
            }
            return respose.Response;
        }

        public async Task<List<Movie>> GetMovies()
        {
            var response = await httpService.Get<List<Movie>>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task UpdateMovie(Movie movie)
        {
            var respose = await httpService.Put(url, movie);
            if (!respose.Success)
            {
                throw new ApplicationException(await respose.GetBody());
            }
        }

        public async Task DeleteMovie(int Id)
        {
            var respose = await httpService.Delete($"{url}/{Id}");
            if (!respose.Success)
            {
                throw new ApplicationException(await respose.GetBody());
            }
        }
    }
}
