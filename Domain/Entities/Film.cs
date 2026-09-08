using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
namespace Domain.Entities
{
    public class Film
    {
        public int Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public int Year { get; private set; }
        public string Author {  get; private set; } = string.Empty ;
        private DateTime _createdAt;
        private DateTime _updatedAt;
        public static Result<Film> Create(int Id,string Title,string Description,int Year,string Author)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return Result<Film>.Failure("Film.Title cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                return Result<Film>.Failure("Film.Description cannot be empty.");
            }

            if (Year < 0 || Year > DateTime.UtcNow.Year)
            {
                return Result<Film>.Failure("Film.Year cannot be negative or in the future.");
            }

            if (string.IsNullOrWhiteSpace(Author))
            {
                return Result<Film>.Failure("Film.Author cannot be empty.");
            }

            var film = new Film(Id, Title, Description, Year, Author);
            return Result<Film>.Success(film);
        }
        private Film() { }
        private Film(int Id,string Title,string Description,int Year,string Author)
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.Year = Year;
            this.Author = Author;
            _createdAt = DateTime.UtcNow;
        }
        public void FilmUpdated() => _updatedAt = DateTime.UtcNow;
        public Result SetTitle(string title)
        {
            if(string.IsNullOrWhiteSpace(title) )
            {
                return Result.Failure("Film.Title, title can not be empty");
            }
            Title = title;
            FilmUpdated();
            return Result.Success();
        }
        public Result SetDescription(string description)
        {
            if( string.IsNullOrWhiteSpace(description) )
            {
                return Result.Failure("Film Description can not be empty");

            }
            Description = description;
            FilmUpdated();
            return Result.Success();
        }
        public Result SetYear(int year)
        {
            if(year < 0 || year > DateTime.UtcNow.Year)
            {
                return Result.Failure("Given parametr year can not be negative or more than Year now...");
            }
            Year = year;
            FilmUpdated();
            return Result.Success();
        }
        public Result SetAuthor(string author)
        {
            if(string.IsNullOrWhiteSpace(author) )
            {
                return Result.Failure("Author can not be empty..");
            }
            Author = author;
            FilmUpdated();
            return Result.Success();
        }
        public DateTime GetFilmCreatedDate() => _createdAt;
        public DateTime GetFilmUpdatedDate() => _updatedAt;
    }
}
