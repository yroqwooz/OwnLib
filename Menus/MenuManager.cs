using BookLibrary.Services;

namespace BookLibrary.Menus;

public static class MenuManager
{
    public static void ShowMainMenu()
    {
        BookService service = new();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Библиотека ===");
            Console.WriteLine("1. Показать все книги");
            Console.WriteLine("2. Добавить новую книгу");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    service.ShowAllBooks();
                    Console.WriteLine("Нажмите Enter для продолжения...");
                    Console.ReadLine();
                    break;
                case "0":
                    return;
                case "2":
                    service.AddBook();
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
        }
    }
}
