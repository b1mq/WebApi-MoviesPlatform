using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;
namespace Application.Interfaces
{
    public interface IFilmService
    {
        Task<IEnumerable<Film>> GetAllFilmsAsync();
        Task<Result> AddFilmAsync(Film film);
        Task<Result> DeleteFilmAsync(int id);
    }
}
