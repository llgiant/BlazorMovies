using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovies.Shared.Entities
{
    //Класс объединяет эти свойства
    // Если например фильм имеет 2 жанра Боевик и Комедия
    // будет 2 записи в MoviesGenres таблице
    public class MoviesGenres
    {

        public int MovieId { get; set; }
        public int GenreId { get; set; }
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
