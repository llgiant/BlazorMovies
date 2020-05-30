using BlazorMovies.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Helpers
{
    public static class QueryableExtensions
    {
        //in this method we implementing pagination using EFcore
        public static IQueryable<T>Paginate<T>(this IQueryable<T>querable, PaginationDTO paginationDTO)
        {
            return querable
                .Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPage)//skiping certain ammount of records
                .Take(paginationDTO.RecordsPerPage);//only want to take certain ammount of records
        }
    }
}
