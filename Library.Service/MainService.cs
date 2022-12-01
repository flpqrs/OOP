using FluentValidation;
using Library.DataAccess;
using Library.Entities;
namespace Library.Service
{
    public class MainService
    {
        private readonly Repository<BookEntity> _books;
        private readonly Repository<LibraryEntity> _libraries;

        public MainService()
        {
            _books = new Repository<BookEntity>();
            _libraries = new Repository<LibraryEntity>();
        }
        public void BookCreate()
        {
            Console.Clear();
            var library = ChooseLibrary();
            CreateBook(library);
        }
        public void BookList()
        {
            Console.Clear();
            var library = ChooseLibrary();
            Console.Clear();
            PrintBooks(library);
        }

        public LibraryEntity ChooseLibrary()
        {
            var allLibraries = _libraries.GetAll();
   
             if (!allLibraries.Any()) 
                throw new Exception("There are no libraries.");

            Console.WriteLine("Choose lib id:\n");

            foreach (var library in allLibraries)
            {
                Console.WriteLine($"Id: {library.Id}, Name: {library.LibraryName}");
            }
            var input = Console.ReadLine();

            try
            {
                var isValid = int.TryParse(input, out int a);

                if (isValid == false || int.Parse(input) > allLibraries.Last().Id || int.Parse(input) <= 0)

                    throw new Exception("You entered wrong ID.");
            }
            catch (Exception ex)
            {
                PrintException(ex);
                return ChooseLibrary();
            }

            return _libraries.Get(int.Parse(input));
        }
        public void PrintLibraries()
        {
            Console.Clear();

            var allLibraries = _libraries.GetAll();

            if (!allLibraries.Any()) 

                throw new Exception("There are no libraries.");

                Console.WriteLine("There are all Libraries:\n");

                foreach (var library in allLibraries)
                {
                    Console.WriteLine($"Id: {library.Id}, Name: {library.LibraryName}");
                }

                PressAnyKey();
        }
        public void CreateLibrary()
        {
            Console.Clear();

            Console.WriteLine("Enter name for new library: ");

            var newLibrary = new LibraryEntity
            {
                LibraryName = Console.ReadLine()
            };

            try
            {
                LibraryValidation(newLibrary);
            }
            catch (Exception ex)
            {
                PrintException(ex);
                return;
            }

            _libraries.Insert(newLibrary);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou created Library: '{newLibrary.LibraryName}' with ID: {newLibrary.Id}.");
            Console.ResetColor();

            PressAnyKey();
        }
        private void PrintBooks(LibraryEntity library)
        {
            {
                var allBooks = _books.GetAll().Where(book => book.LibraryId == library.Id).ToList();
                
                try
                {
                    if (!allBooks.Any()) throw new Exception("There are no books in this library.");
                }
                catch (Exception ex)
                {
                    PrintException(ex);
                    return;
                }
                
                Console.WriteLine($"Library {library.LibraryName} with ID[{library.Id}] has books:\n");

                foreach (var book in allBooks)
                {
                    Console.WriteLine($"BookID: [{book.Id}], Title: {book.Title}, Author: {book.AuthorsFirstName} {book.AuthorsLastName}, Year: {book.Year}");
                }

                PressAnyKey();
            }

        }
        private void CreateBook(LibraryEntity library)
        {
            Console.WriteLine("Enter name of book: ");
            string title = Console.ReadLine();

            Console.WriteLine("Enter authors First name");
            string authorFirstName = Console.ReadLine();

            Console.WriteLine("Enter authors Last name");
            string authorLastName = Console.ReadLine();

            Console.WriteLine("Enter year");
            var input = Console.ReadLine();

            var isValid = int.TryParse(input, out int result);

            try
            {
                if (isValid == false)
                    throw new Exception("Wrong input.");
            }
            catch (Exception ex)
            {
                PrintException(ex);
                return;
            }

            int year = int.Parse(input);

            var newBook = new BookEntity
            {
                Title = title,
                AuthorsFirstName = authorFirstName,
                AuthorsLastName = authorLastName,
                Year = year,
                LibraryId = library.Id,
            };

            try
            {
                BookValidation(newBook);
            }
            catch (Exception ex)
            {
                PrintException(ex);
                return;
            }

            _books.Insert(newBook);

            Console.Clear();

            Console.ForegroundColor= ConsoleColor.Green;

            Console.WriteLine($"You created book!\n" +
                $"\n BookID: {newBook.Id}" +
                $"\n Author: {authorFirstName} {authorLastName}" +
                $"\n Year: {year}" +
                $"\n Library: [{library.Id}] {library.LibraryName} ");

            Console.ResetColor();

            PressAnyKey();
        }
        private static void LibraryValidation(LibraryEntity newLibrary)
        {
            LibraryInputValidatorService validator = new();
            validator.ValidateAndThrow(newLibrary);
        }
        private static void BookValidation(BookEntity newBook)
        {
            BookValidatorService validator = new();
            validator.ValidateAndThrow(newBook);
        }

        private static void PrintException(Exception ex)
        {
            Console.ForegroundColor = System.ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();

            PressAnyKey();
        }
        private static void PressAnyKey()
        {
            Console.WriteLine("\nPress Any Key to Continue...");

            Console.ReadKey();

            Console.Clear();
        }
    }
}



