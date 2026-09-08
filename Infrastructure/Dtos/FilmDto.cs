using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Infrastructure.Dtos
{
    [FirestoreData]
    public  class FilmDto
    {
        [FirestoreProperty]
        public int Id { get;  set; }
        [FirestoreProperty]
        public string Title { get;  set; } = string.Empty;
        [FirestoreProperty]
        public string Description { get; set; } = string.Empty;
        [FirestoreProperty]
        public int Year { get;  set; }
        [FirestoreProperty]
        public string Author { get;  set; } = string.Empty;
    }
}
