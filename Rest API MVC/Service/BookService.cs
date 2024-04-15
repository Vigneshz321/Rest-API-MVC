using Microsoft.EntityFrameworkCore;
using Rest_API_MVC.Data_Access_Layer;
using Rest_API_MVC.Interfaces;
using Rest_API_MVC.Models;

namespace Rest_API_MVC.Service
{
    public class BookService : IBooks
    {
        private readonly DatabaseContext _context;

        public BookService(DatabaseContext context)
        {
            _context = context;
        }       
        public async Task<IEnumerable<Book>> GetSortedBooks()
        {
            return await _context.Books
               .OrderBy(b => b.Publisher)
               .ThenBy(b => b.AuthorLastName)
               .ThenBy(b => b.AuthorFirstName)
               .ThenBy(b => b.Title)
               .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksSortedByAuthor()
        {
            return await _context.Books.OrderBy(b => b.AuthorLastName)
                .ThenBy(b => b.AuthorFirstName)
                .ThenBy(b => b.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetSortedBooksBySP()
        {
            return await _context.Books.FromSqlRaw("EXEC GetSortedBooks").ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksSortedByAuthorBySP()
        {
            return await _context.Books.FromSqlRaw("EXEC GetBooksSortedByAuthor").ToListAsync();
        }

        public async Task<decimal> GetTotalPriceOfBooks()
        {
            return await _context.Books.SumAsync(b => b.Price);
        }


    }
}
