using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Common;
using FluentValidation;
using Application.Dtos;
namespace Application.Services
{
    public class FilmService:IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IValidator<CreateFilmDto> _validator;
        public FilmService(IFilmRepository filmRepository,IValidator<CreateFilmDto> validator)
        {
            _validator = validator;
            _filmRepository = filmRepository;
        }
        public async Task<Result> AddFilmAsync(CreateFilmDto dto)
        {
            var validationres = await _validator.ValidateAsync(dto);
            if(!validationres.IsValid)
            {
                var errors = string.Join("; ", validationres.Errors.Select(e => e.ErrorMessage));
                return Result.Failure(errors);
            }
            var filmResult = Film.Create(dto.Id, dto.Title, dto.Description, dto.Year, dto.Author);
            if(!filmResult.isSuccess)
            {
                return Result.Failure(filmResult.Error);
            }
            var success = await _filmRepository.AddNewFilmAsync(filmResult.Value);
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
        public async Task<Film?> GetFilmByIdAsync(int id)
        {
            var succes = await _filmRepository.GetFilmByIdAsync(id);
            if(succes != null)
            {
                return succes;
            }
            return null;
        }
    }
}
