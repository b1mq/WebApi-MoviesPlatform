using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Common;
using Domain.Entities;
namespace Application.Interfaces
{
    public interface IFilmService
    {
        Task<IEnumerable<Film>> GetAllFilmsAsync();
        Task<Result> AddFilmAsync(CreateFilmDto dto);
        Task<Film?> GetFilmByIdAsync(int id);
        Task<Result> DeleteFilmAsync(int id);
    }
}
