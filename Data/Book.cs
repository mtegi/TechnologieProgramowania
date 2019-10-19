using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum LiteraryGenre
{
    Fantasy, SciFi, Romance, Drama, Thriller, Comedy, Biography
}

namespace Data
{
    /** Katalog (ksiazka) */
    public class Book
    {
        public Book(int id, string title, Author author, List<LiteraryGenre> genres)
        {
            this.Id = id; /** klucz dostepu do danych */
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Author = author ?? throw new ArgumentNullException(nameof(author));
            this.Genres = genres ?? throw new ArgumentNullException(nameof(author));
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public List<LiteraryGenre> Genres { get; }
    }
}
