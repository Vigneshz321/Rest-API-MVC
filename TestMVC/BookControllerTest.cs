using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rest_API_MVC.Controllers;
using Rest_API_MVC.Interfaces;
using Rest_API_MVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC
{
    [TestClass]
    public class BookControllerTest
    {
        private Mock<IBooks> _booksMock;
        private BookController _controller;

        [TestInitialize]
        public void Setup()
        {
            _booksMock = new Mock<IBooks>();
            _controller = new BookController(_booksMock.Object);
        }

        [TestMethod]
        public async Task Index()
        {
            var expectedBooks = new List<Book>
            {
                new Book { Id = 1, AuthorLastName = "Doe", AuthorFirstName = "John", Title = "Sample Book" },
                new Book { Id = 2, AuthorLastName = "Smith", AuthorFirstName = "Jane", Title = "Another Book" }
            };
            _booksMock.Setup(x => x.GetSortedBooks()).ReturnsAsync(expectedBooks);
            _booksMock.Setup(x => x.GetTotalPriceOfBooks()).ReturnsAsync(100);

            var result = await _controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as IEnumerable<Book>;
            Assert.IsNotNull(model);
            Assert.AreEqual(expectedBooks.Count, model.Count());            
            Assert.AreEqual(100, viewResult.ViewBag.TotalPrice);
        }

        [TestMethod]
        public async Task GetBooksByAuthor()
        {
            var expectedBooks = new List<Book>
            {
                new Book { Id = 1, AuthorLastName = "Doe", AuthorFirstName = "John", Title = "Sample Book" },
                new Book { Id = 2, AuthorLastName = "Smith", AuthorFirstName = "Jane", Title = "Another Book" }
            };
            _booksMock.Setup(x => x.GetBooksSortedByAuthor()).ReturnsAsync(expectedBooks);

            var result = await _controller.GetBooksByAuthor();

            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Book>>));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var books = okResult.Value as IEnumerable<Book>;
            Assert.IsNotNull(books);
            Assert.AreEqual(expectedBooks.Count, books.Count());
        }

        [TestMethod]
        public async Task GetSortedBooksBySP()
        { 
            var expectedBooks = new List<Book>
            {
                new Book { Id = 1, AuthorLastName = "Doe", AuthorFirstName = "John", Title = "Sample Book" },
                new Book { Id = 2, AuthorLastName = "Smith", AuthorFirstName = "Jane", Title = "Another Book" }
            };
            _booksMock.Setup(x => x.GetSortedBooksBySP()).ReturnsAsync(expectedBooks);

            var result = await _controller.GetSortedBooksBySP();

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var model = okResult.Value as IEnumerable<Book>;
            Assert.IsNotNull(model);
            Assert.AreEqual(expectedBooks.Count, model.Count());
        }

        [TestMethod]
        public async Task GetBooksSortedByAuthorBySP()
        {
            var expectedBooks = new List<Book>
        {
            new Book { Id = 1, AuthorLastName = "Doe", AuthorFirstName = "John", Title = "Sample Book" },
            new Book { Id = 2, AuthorLastName = "Smith", AuthorFirstName = "Jane", Title = "Another Book" }
        };
            _booksMock.Setup(x => x.GetBooksSortedByAuthorBySP()).ReturnsAsync(expectedBooks);

            var result = await _controller.GetBooksSortedByAuthorBySP();

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var model = okResult.Value as IEnumerable<Book>;
            Assert.IsNotNull(model);
            Assert.AreEqual(expectedBooks.Count, model.Count());
        }

    }
}
