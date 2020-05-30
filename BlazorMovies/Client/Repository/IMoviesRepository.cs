using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    public interface IMoviesRepository
    {
        Task<int> CreateMovie(Movie movie);
        Task DeleteMovie(int Id);
        Task<DetailsMovieDTO> GetDetailsMovieDTO(int id);
        Task<IndexPageDTO> GetIndexPageDTO();
        Task<PaginatedResponse<List<Movie>>> GetMovieFiltered(FilterMoviesDTO filterMoviesDTO);
        Task<MovieUpdateDTO> GetMovieForUpdate(int id);
        Task<List<Movie>> GetMovies();
        Task UpdateMovie(Movie movie);
    }
}
 