using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;
using Google.Cloud.Firestore;
namespace Infrastructure.Repository
{
    public class FirebaseFilmRepository:IFilmRepository
    {
        private readonly FirestoreDb _client;
        public FirebaseFilmRepository(FirestoreDb client)
        {
            _client = client;
        }
        public Task<Film?>GetFilmByTitle(string title)
        {
            
        }
    }
}
