using Library.Service;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new MainService();

            do
            {
                Console.WriteLine("Choose operation (-lib_c) - create library, (-lib_list) - print list of libraries, (-book_c) - create book, (-book_list) - print list of books, (-out) - exit: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "-lib_c":

                        service.CreateLibrary();

                        break;

                    case "-lib_list":

                        service.PrintLibraries();

                        break;

                    case "-book_c":

                        service.BookCreate();

                        break;
                    case "-book_list":

                        service.BookList();

                        break;

                    case "-out":
                        goto Exit;
                    default:
                        continue;
                }
            } while (true);

        Exit:;
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}