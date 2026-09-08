using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using FluentValidation;

namespace Application.Validation
{
    public class FilmValidator:AbstractValidator<CreateFilmDto>
    {
        public FilmValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title can not be empty").MaximumLength(100).WithMessage("Title can not be more than 100");

        }
    }
}
