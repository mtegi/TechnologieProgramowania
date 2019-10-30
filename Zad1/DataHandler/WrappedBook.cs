using Data;
using DefinitionLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public class WrappedBook
    {
        private readonly Book book;


        public string Title { get; }
        public int Id { get; }
        public string Author { get; }
        public IEnumerable<LiteraryGenre> Genres { get; }


        public WrappedBook (Book book)
        {
            this.book = book;
            this.Title = book.Title;
            this.Id = book.Id;
            this.Author = book.Author;
            this.Genres = new List<LiteraryGenre> (book.Genres);

        }

        internal Book GetBook()
        {
            return this.book;
        }
    }
}
