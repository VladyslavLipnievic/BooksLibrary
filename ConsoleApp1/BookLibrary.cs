using System;
namespace ConsoleApp1
{
    class BookLibrary
    {
        public static AddBook addbook = new AddBook();
        public static TakeBook takebook = new TakeBook();
        public static ListAllBooks listAll = new ListAllBooks();
        static void Main()
        {
            string variable;
            Console.WriteLine("Welcome to library ");
            addbook.AppStart();
            while (true)
            {
                string input = Console.ReadLine();
                switch (input)
                {
                    case "add":
                        addbook.WriteBookInfo();
                        addbook.AddBookDataCheck();//appstart method added in another class
                        break;
                    case "take":
                        takebook.TakeInfo();//appstart method added in another class
                        break;
                    case "return":
                        Console.WriteLine("\n Enter your name : ");
                        string name = Console.ReadLine();
                        if (takebook.listAllBooks(name,"0","Empty"))
                        {
                            Console.WriteLine("\n Enter books ISBN number to return book : ");
                            string ISBNum = Console.ReadLine();
                            listAll.DeleteOrReturnBook(ISBNum, "return");
                        }
                        else
                        {
                            Console.WriteLine("\n You made a mistake entering your name or you haven't borrowed book from our library ");
                        }
                        addbook.AppStart();
                        break;
                    case "filter by author":
                        Console.WriteLine("\nWrite author name that you want find in books list \n");
                        variable = Console.ReadLine();
                        takebook.listAllBooks("filter", variable,"Author");
                        addbook.AppStart();
                        break;
                    case "filter by category":
                        Console.WriteLine("\nWrite category name that you want find in books list \n");
                        variable = Console.ReadLine();
                        takebook.listAllBooks("filter", variable,"Category");
                        addbook.AppStart();
                        break;
                    case "filter by language":
                        Console.WriteLine("\nWrite language that you want find books \n");
                        variable = Console.ReadLine();
                        takebook.listAllBooks("filter", variable,"Language");
                        addbook.AppStart();
                        break;
                    case "filter by ISBN":
                        Console.WriteLine("\nWrite book ISBN code that you want to find \n");
                        variable = Console.ReadLine();
                        takebook.listAllBooks("filter", variable,"ISBN");
                        addbook.AppStart();
                        break;
                    case "filter by name":
                        Console.WriteLine("\nWrite book name that you want to find \n");
                        variable = Console.ReadLine();
                        takebook.listAllBooks("filter", variable,"Name");
                        addbook.AppStart();
                        break;
                    case "filter by taken":
                        takebook.listAllBooks("taken", "No", "Empty");
                        addbook.AppStart();
                        break;
                    case "filter by available":
                        Console.WriteLine("\nHere are available books : \n");
                        takebook.listAllBooks("No","0","Empty");
                        addbook.AppStart();
                        break;
                    case "all":
                        Console.WriteLine("\n");
                        listAll.TakeInfo();
                        addbook.AppStart();
                        break;
                    case "delete":
                        Console.WriteLine("\n");
                        if (listAll.TakeInfo())
                        {
                            Console.WriteLine("\n Enter books ISBN number to delete it : ");
                            string ISBN = Console.ReadLine();
                            listAll.DeleteOrReturnBook(ISBN,"delete");
                        }
                        addbook.AppStart();
                        break;
                    default:
                        addbook.AppStart();
                        break;
                }
            }
        }
    }
}
