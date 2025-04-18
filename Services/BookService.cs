using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Services;

public class BookService
{
    private readonly LibraryContext _context;

    public BookService()
    {
        _context = new LibraryContext();
    }
    private string RemoveNullChars(string input) => string.IsNullOrEmpty(input) ? "" : input.Replace("\0", "").Trim();


    public void ShowAllBooks()
    {
        var books = _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .ToList();

        foreach (var book in books)
        {
            Console.WriteLine($"{book.Id}: {book.Title} — {book.Author.Name}, {book.Genre.Name} [{book.ReadingStatus}]");
        }
    }

    public void AddBook()
    {
        Console.Clear();
        Console.WriteLine("=== Добавление книги ===");

        Console.Write("Название: ");
        var title = RemoveNullChars(Console.ReadLine());
        //var title = Console.ReadLine();

        Console.Write("Дата публикации (yyyy-mm-dd, можно оставить пустым): ");
        var dateInput = RemoveNullChars(Console.ReadLine());
        //var dateInput = Console.ReadLine();
        DateTime? publishedDate = null;
        if (DateTime.TryParse(dateInput, out var parsedDate))
            publishedDate = parsedDate;

        Console.Write("Автор (если новый - будет добавлен): ");
        var authorName = RemoveNullChars(Console.ReadLine());
        //var authorName = Console.ReadLine();
        var author = _context.Authors.FirstOrDefault(a => a.Name == authorName);
        if (author == null)
        {
            author = new Author { Name = authorName };
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        Console.Write("Жанр (если новый - будет добавлен): ");
        var genreName = RemoveNullChars(Console.ReadLine());
        //var genreName = Console.ReadLine();
        var genre = _context.Genres.FirstOrDefault(a => a.Name == genreName);
        if (genre == null)
        {
            genre = new Genre { Name = genreName };
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        Console.Write("Статус чтения (Прочитано / Читаю / Хочу прочитать): ");
        var status = RemoveNullChars(Console.ReadLine());
        //var status = Console.ReadLine();

        var book = new Book
        {
            Title = title,
            AuthorId = author.Id,
            GenreId = genre.Id,
            PublishedDate = publishedDate,
            ReadingStatus = status
        };

        Console.WriteLine($"[DEBUG] Название: {title}, Автор: {authorName}, Жанр: {genreName}, Статус: {status}");

        _context.Books.Add(book);
        _context.SaveChanges();

        Console.WriteLine("Книга добавлена!");
        Console.WriteLine("Нажмите Enter для продолжения...");
        Console.ReadLine();
    }

    // TODO: Добавить методы: AddBook, EditBook, DeleteBook, FindBook и т.п.
}
