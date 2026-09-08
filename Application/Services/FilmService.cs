using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Common;
namespace Application.Services
{
    public class FilmService:IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }
        public async Task<Result> AddFilmAsync(Film film)
        {
            var success = await _filmRepository.AddNewFilmAsync(film);
            if(!success)
            {
                return Result.Failure("Failed to add film to DB");
            }
            return Result.Success();
        }
        public async Task<IEnumerable<Film>> GetAllFilmsAsync()
        {
            return await _filmRepository.GetAllFilmsAsync();
        }
        public async Task<Result> DeleteFilmAsync (int id)
        {
            var success = await _filmRepository.DeleteFilmByIdAsync(id);
            if(!success)
            {
                return Result.Failure($"Failed to delete film with id:{id}");
            }
            return Result.Success();
        }
    }
}
