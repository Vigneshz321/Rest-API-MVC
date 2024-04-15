using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest_API_MVC.Data_Access_Layer;
using Rest_API_MVC.Models;
using Rest_API_MVC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class BookServiceTests
    {
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new DatabaseContext(_options))
            {
                context.Database.EnsureCreated();
                context.Books.AddRange(new List<Book>
                {
                    new Book { Id = 1, Publisher = "Publisher1", AuthorLastName = "LastName1", AuthorFirstName = "FirstName1", Title = "Title1", Price = 10 },
                    new Book { Id = 2, Publisher = "Publisher2", AuthorLastName = "LastName2", AuthorFirstName = "FirstName2", Title = "Title2", Price = 20 },
                    new Book { Id = 3, Publisher = "Publisher3", AuthorLastName = "LastName3", AuthorFirstName = "FirstName3", Title = "Title3", Price = 30 },
                });
                context.SaveChanges();
            }
        }

        [TestMethod]
        public async Task GetSortedBooks()
        {
            using (var context = new DatabaseContext(_options))
            {
                var service = new BookService(context);
                var result = await service.GetSortedBooks();

                Assert.AreEqual(3, result.Count());
            }
        }

        [TestMethod]
        public async Task GetBooksSortedByAuthor()
        {
            using (var context = new DatabaseContext(_options))
            {
                var service = new BookService(context);

                var result = await service.GetBooksSortedByAuthor();

                Assert.AreEqual(3, result.Count());
                Assert.AreEqual("Doe", result.ElementAt(0).AuthorLastName);
                Assert.AreEqual("Jane", result.ElementAt(0).AuthorFirstName);
                Assert.AreEqual("Doe", result.ElementAt(1).AuthorLastName);
                Assert.AreEqual("John", result.ElementAt(1).AuthorFirstName);
                Assert.AreEqual("Smith", result.ElementAt(2).AuthorLastName);
                Assert.AreEqual("Alice", result.ElementAt(2).AuthorFirstName);
            }
        }

        [TestMethod]
        public async Task GetSortedBooksBySP()
        {
            var service = new BookService(_context);

            var result = await service.GetSortedBooksBySP();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Doe", result.ElementAt(0).AuthorLastName);
            Assert.AreEqual("Jane", result.ElementAt(0).AuthorFirstName);
            Assert.AreEqual("Doe", result.ElementAt(1).AuthorLastName);
            Assert.AreEqual("John", result.ElementAt(1).AuthorFirstName);
            Assert.AreEqual("Smith", result.ElementAt(2).AuthorLastName);
            Assert.AreEqual("Alice", result.ElementAt(2).AuthorFirstName);
        }

        [TestMethod]
        public async Task GetBooksSortedByAuthorBySP()
        {
            var service = new BookService(_context);

            var result = await service.GetBooksSortedByAuthorBySP();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Smith", result.ElementAt(0).AuthorLastName);
            Assert.AreEqual("Alice", result.ElementAt(0).AuthorFirstName);
            Assert.AreEqual("Doe", result.ElementAt(1).AuthorLastName);
            Assert.AreEqual("Jane", result.ElementAt(1).AuthorFirstName);
            Assert.AreEqual("Doe", result.ElementAt(2).AuthorLastName);
            Assert.AreEqual("John", result.ElementAt(2).AuthorFirstName);
        }

        [TestMethod]
        public async Task GetTotalPriceOfBooks()
        {
            var service = new BookService(_context);

            var result = await service.GetTotalPriceOfBooks();

            Assert.AreEqual(60, result);
        }
    }
}
