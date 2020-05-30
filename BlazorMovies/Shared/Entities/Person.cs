using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlazorMovies.Shared.Entities
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Длина имени до 50 символов")]
        public string Name { get; set; }
        [Required]
        [StringLength(3000, ErrorMessage = "Длина биографии до 3000 символов")]
        public string Biography { get; set; }
        public string Picture { get; set; }
        // Знак "?" перед типом DateTime дает возможность испольлзовать нулевое значение       
        //в inputdate при создании Person, будет выглядеть так mm/dd//yyyy
        //[Required] - проверка на ввод даты, если пустая запросит ввести дату 
        [Required(ErrorMessage = "Это поле необходимо заполнить")]
        public DateTime? DateOfBirth { get; set; }
        public List<MoviesActors> MoviesActors { get; set; } = new List<MoviesActors>();

        [NotMapped]//because we don't want to map Character property into the Person database 
        public string Character { get; set; }
    }
}
