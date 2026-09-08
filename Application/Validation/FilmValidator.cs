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
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description can not be empty").MaximumLength(250).WithMessage("Description is tooo long sir...");
            RuleFor(x => x.Year)
            .InclusiveBetween(1888, 2100).WithMessage("Incorrect year");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author can not be empty man...");
        }
    }
}
