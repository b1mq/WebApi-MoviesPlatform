using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;
using Google.Cloud.Firestore;
using Infrastructure.Dtos;
namespace Infrastructure.Repository
{
    public class FirebaseFilmRepository:IFilmRepository
    {
        private readonly FirestoreDb _client;
        private const string CollectionName = "films";
        public FirebaseFilmRepository(FirestoreDb client)
        {
            _client = client;
        }
        public async Task<bool> AddNewFilmAsync(Film film)
        {
            var document = new FilmDto
            {
                Id = film.Id,
                Title = film.Title,
                Description = film.Description,
                Year = film.Year,
                Author = film.Author,
            };
            await _client.Collection(CollectionName).Document(film.Id.ToString()).SetAsync(document);
            return true;

        }
        public async Task<Film?> GetFilmByTitleAsync(string title)
        {
            var query = _client.Collection(CollectionName).WhereEqualTo("Title", title);
            var snapshot = await query.GetSnapshotAsync();
            var doc = snapshot.Documents.FirstOrDefault();
            if (doc == null || !doc.Exists)
            {
                return null;
            }
            var dto = doc.ConvertTo<FilmDto>();
            var result = Film.Create(dto.Id, dto.Title, dto.Description, dto.Year, dto.Author);
            return result.isSuccess ? result.Value : null;
        }
        public async Task<Film?> GetFilmByIdAsync(int id)
        {
            var snapshot = await _client.Collection(CollectionName)
                .Document(id.ToString())
                .GetSnapshotAsync();

            if (!snapshot.Exists) return null;

            
            var doc = snapshot.ConvertTo<FilmDto>();

          
            var result = Film.Create(doc.Id, doc.Title, doc.Description, doc.Year, doc.Author);

            return result.isSuccess ? result.Value : null;
        }
        public async Task<bool> DeleteFilmByIdAsync(int id)
        {
            await _client.Collection(CollectionName).Document(id.ToString()).DeleteAsync();
            return true;
        }
        public async Task<IEnumerable<Film>> GetAllFilmsAsync()
        {
            var snapshot = await _client.Collection(CollectionName).GetSnapshotAsync();
            var films = new List<Film>();

            foreach (var document in snapshot.Documents)
            {
                if (!document.Exists) continue;

                var doc = document.ConvertTo<FilmDto>();
                var result = Film.Create(doc.Id, doc.Title, doc.Description, doc.Year, doc.Author);

                if (result.isSuccess)
                {
                    films.Add(result.Value);
                }
            }

            return films;
        }
        public async Task<bool> UpdateFilmAsync(Film film)
        {
            var dto = new FilmDto
            {
                Id = film.Id,
                Title = film.Title,
                Description = film.Description,
                Year = film.Year,
                Author = film.Author
            };

            await _client.Collection(CollectionName).Document(film.Id.ToString()).SetAsync(dto);
            return true;
        }

    }
}
