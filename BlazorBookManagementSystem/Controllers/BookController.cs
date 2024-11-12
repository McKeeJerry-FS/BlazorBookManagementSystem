using BlazorBookManagementSystem.BookServices.Interfaces;
using BlazorBookManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBookManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooksAsync() => Ok(await _bookService.GetAllBooksAsync());

        [HttpPost]
        public async Task<ActionResult<(bool, string)>> ManageBookAsync(Book book)
        {
            if (book is null) return BadRequest(false);
            var result = await _bookService.ManageBookAsync(book);
            if (result.Success)
                return Ok(result);
            else return BadRequest(result);
            
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<(bool, string)>> DeleteBookAsync(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (result.Success)
                return Ok(result);
            else return BadRequest(result);
        }
    }
}
