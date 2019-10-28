﻿using System;
using DefinitionLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    /** Katalog (ksiazka) */
    public class Book
    {
        public Book(int id, string title, string author, LiteraryGenre[] genres)
        {
            this.Id = id; /** klucz dostepu do danych */
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Author = author ?? throw new ArgumentNullException(nameof(author));
            this.Genres = new List<LiteraryGenre>(genres);
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public List<LiteraryGenre> Genres { get; set; }
    }
}
