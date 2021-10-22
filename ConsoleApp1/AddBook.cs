using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    public class AddBook
    {
        Dictionary<string, string> booksList = new Dictionary<string, string>();
        Dictionary<string, string> bookListForUser = new Dictionary<string, string>();
        public static TakeBook listbook = new TakeBook();

        List<string> booksdatalist = new List<string>
        {
           "Name","Author","Category","Language","Publication date","ISBN","BorrowedforMonths","Bookborrower"
         };
        string path = @"C:\path.json";
        public void AppStart()
        {
            Console.Write(
         "\nPLEASE CHOICE A COMMAND :            *return - to return borrowed book\n" +
         "*delete - to delete book             *take - to borrow a book\n*all - to see all awailable books    *add - to add new book\n" +
         "FILTER COMMANDS : *filter by author  *filter by category     *filter by language  \n*filter by ISBN " +
         "  *filter by name    *filter by taken        *filter by available\n");
        }
        public string CheckforAddDetails(string readLine,string item,string InputThatIsInt,int len)
        {
            if (item == InputThatIsInt)
            {
                if (len == 4)
                {
                    while (!(readLine.All(char.IsDigit) && readLine.Length == len))
                    {
                        Console.Write($"Book {item.ToLower()} consists of {len} digits, please enter {item.ToLower()} again : ");
                        readLine = Console.ReadLine();

                    }
                }
                if (len == 10)
                {
                    while (listbook.listAllBooks("IfISBNExist", item, readLine)||!(readLine.All(char.IsDigit) && readLine.Length == len))
                    {
                        Console.Write($"{item} have to consist of 10 digits and has to be unique, please enter again : ");
                        readLine = Console.ReadLine();
                    }
                }

            }
            return readLine;
        }
        public void AddBookInfoToList()
        {
            int index = 0;
            foreach (var item in booksdatalist)
            {
                string readline;
                if (index < 6)
                {
                    Console.Write($"Enter book {item} : ");
                    readline = Console.ReadLine();
                    while (readline.Length < 2)
                    {
                        Console.Write($"Enter book {item} : ");
                        readline = Console.ReadLine();
                    }
                    readline = CheckforAddDetails(readline, item, "Publication date",4);
                    readline = CheckforAddDetails(readline, item, "ISBN",10);
                    booksList.Add(item, readline);
                    bookListForUser.Add(item, readline);
                }
                if (index == 6)
                {
                    booksList.Add(item, "Nope");
                }
                if (index == 7)
                {
                    booksList.Add(item, "No");
                }
                index++;
            }
        }

        public void AddBookDataCheck()
        {
            var bookliststring = string.Join(":", bookListForUser);
            bookliststring = bookliststring.Replace("[", "");
            bookliststring = bookliststring.Replace("]", "");
            bookliststring = bookliststring.Replace(",", " -");
            bookliststring = bookliststring.Replace(":", ", ");
            Console.WriteLine($"You have entered : {bookliststring}");
            Console.WriteLine($"Does entered data is correct? (yes/no)");
            string WriteToFile = Console.ReadLine();
                  while (WriteToFile != "yes" && WriteToFile != "no")
            {
                WriteToFile = Console.ReadLine();
            }
            IfAddBookToFile(bookliststring, WriteToFile);
        }


        public bool IfAddBookToFile(string bookliststring,string WriteToFile)
        {
            if (WriteToFile == "yes")
            {
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                }
                if (new FileInfo(path).Length > 2)
                {
                    var json = JsonConvert.SerializeObject(booksList);
                    string fileContent = File.ReadAllText(path);
                    fileContent = fileContent.Remove(fileContent.Length - 1) + "," + json + "]";
                    System.IO.File.WriteAllText(path, fileContent);
                }
                if (new FileInfo(path).Length == 0)
                {
                    var fileContent = File.ReadAllText(path);
                    var json = JsonConvert.SerializeObject(booksList);
                    fileContent = "[" + json + "]";
                    System.IO.File.WriteAllText(path, fileContent);
                }

                Console.WriteLine("***Book added***");
                BookListsClear();
                AppStart();
                return true;
            }
            if (WriteToFile == "no")
            {
                BookListsClear();
                AppStart();
                return false;
            }
            return true;
        }
        private void BookListsClear()
        {
            booksList.Clear();
            bookListForUser.Clear();
        }
        public void WriteBookInfo()
        {
            Console.Write("\nPlease enter book data: name, author, category, language, publication date, ISBN\n");

            AddBookInfoToList();

        }
    }
}
