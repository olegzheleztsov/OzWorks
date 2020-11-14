using LinksShare.Models;
using LinksShare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LinksShare.Controllers
{
    [Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get() => _bookService.Get();

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("user")]
        public IActionResult GetUser()
        {
            if(User.Identity != null)
            {
                _logger.LogInformation("Identity:");
                _logger.LogInformation($"Name: {User.Identity.Name}, Is Authenticated: {User.Identity.IsAuthenticated}, Authentication type: {User.Identity.AuthenticationType}");
               
            }
            if(User.Claims != null)
            {
                _logger.LogInformation("Claims:");
                foreach(var claim in User.Claims)
                {
                    _logger.LogInformation($"Value: {claim.Value}, Type: {claim.Type}, Issuer: {claim.Issuer}");
                }
            }
            return NoContent();
        }



        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);
            if(book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);
            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookService.Get(id);
            if(book == null)
            {
                return NotFound();
            }
            _bookService.Update(id, bookIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);
            if(book == null)
            {
                return NotFound();
            }
            _bookService.Remove(book.Id);
            return NoContent();
        }
    }
}
