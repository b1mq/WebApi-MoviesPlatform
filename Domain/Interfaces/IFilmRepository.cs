using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
namespace Domain.Interfaces
{
    public  interface IFilmRepository
    {
        Task<Film?> GetFilmByTitleAsync(string title);
        Task<Film?> GetFilmByIdAsync(int id);
        Task<bool> DeleteFilmByIdAsync(int id);
        Task<bool> AddNewFilmAsync(Film film);
        Task<bool> UpdateFilmAsync(Film film);
        Task<IEnumerable<Film>> GetAllFilmsAsync();

    }
}
