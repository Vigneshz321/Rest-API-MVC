using Microsoft.AspNetCore.Mvc;
using Rest_API_MVC.Models;

namespace Rest_API_MVC.Interfaces
{
    public interface IBooks
    {
        public Task<IEnumerable<Book>> GetSortedBooks();
        public Task<IEnumerable<Book>> GetBooksSortedByAuthor();
        public Task<IEnumerable<Book>> GetSortedBooksBySP();
        public Task<IEnumerable<Book>> GetBooksSortedByAuthorBySP();
        public Task<decimal> GetTotalPriceOfBooks();

    }


}
