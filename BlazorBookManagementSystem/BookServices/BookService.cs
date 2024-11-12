using BlazorBookManagementSystem.BookServices.Interfaces;
using BlazorBookManagementSystem.Data;
using BlazorBookManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookManagementSystem.BookServices
{
    public class BookService : IBookService
    {
        private AppDbContext _context;

        public BookService(AppDbContext context) 
        { 
            _context = context;
        }
        public async Task<(bool Success, string Message)> DeleteBookAsync(int id)
        {
            if (id <= 0) return ErrorMessage();
            
            var bookToDelete = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (bookToDelete is null) return ErrorMessage();
            _context.Books.Remove(bookToDelete!);
            await _context.SaveChangesAsync();
            return SuccessMessage();
        }

        public async Task<List<Book>> GetAllBooksAsync() => await _context.Books.ToListAsync();
        

        public async Task<(bool Success, string Message)> ManageBookAsync(Book book)
        {
            if (book == null) return ErrorMessage();

            if(book.Id == 0)
            {
                if(!await IsBookAlreadyAdded(book.Title!))
                {
                    _context.Books.Add(book);
                    await _context.SaveChangesAsync();
                    return SuccessMessage();
                }
            }

            var bookToUpdate = await _context.Books.FirstOrDefaultAsync(x => x.Id == book.Id);
            if (bookToUpdate is null) return ErrorMessage();

            bookToUpdate.Title = book.Title;
            bookToUpdate.Description = book.Description;
            bookToUpdate.Image = book.Image;
            await _context.SaveChangesAsync();
            return SuccessMessage();
        }

        private static (bool, string) SuccessMessage() => (true, "Process successfully completed");
        private static (bool, string) ErrorMessage() => (false, "Error occurred while processing");

        private async Task<bool> IsBookAlreadyAdded(string title)
        {
            var book = await _context.Books.Where(_ => _.Title!.ToLower().Equals(title)).FirstOrDefaultAsync();
            return book != null;
        }
    }
}
