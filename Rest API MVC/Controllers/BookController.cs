using Microsoft.AspNetCore.Mvc;
using Rest_API_MVC.Interfaces;
using Rest_API_MVC.Models;
using Rest_API_MVC.Service;

namespace Rest_API_MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBooks _books;

        public BookController(IBooks books)
        {
            _books = books;
        }
        public async Task<IActionResult> Index()
        {
            var sortedBooks = await _books.GetSortedBooks();
            ViewBag.TotalPrice = await _books.GetTotalPriceOfBooks();
            return View(sortedBooks);
        }

        [HttpGet("sorted-by-author")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor()
        {
            var sortedBooks = await _books.GetBooksSortedByAuthor();
            return Ok(sortedBooks);
        }

        [HttpGet("sortedbooks-sp")]
        public async Task<ActionResult<IEnumerable<Book>>> GetSortedBooksBySP()
        {
            var sortedBooks = await _books.GetSortedBooksBySP();
            return Ok(sortedBooks);
        }

        [HttpGet("sorted-by-author-sp")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksSortedByAuthorBySP()
        {
            var sortedBooks = await _books.GetBooksSortedByAuthorBySP();
            return Ok(sortedBooks);
        }

        [HttpGet("total-price")]
        public async Task<ActionResult<decimal>> GetTotalPriceOfBooks()
        {
            try
            {
                var totalPrice = await _books.GetTotalPriceOfBooks();
                return Ok(totalPrice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
