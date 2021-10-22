using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    public class TakeBook
    {
        List<string> booksdatalist = new List<string>
        {
           "Name","Author","Category","Language","Publication date","ISBN","Bookborrower","BorrowedforMonths"
        };
        List<string> BooklistUserSee = new List<string>();
        public static AddBook addbook = new AddBook();
        string path = @"C:\path.json";
        public bool listAllBooks(string Bookstatus, string FilterUserPasses, string FilterName)
        {
            string json = File.ReadAllText(path);
            if (new FileInfo(path).Length != 0)
            {
                JArray jsonArray = JArray.Parse(json);
                var jsonObjects = jsonArray.OfType<JObject>().ToList();
                int count = jsonObjects.Count;
                int index = 0;
                if (Bookstatus == "IfISBNExist")
                {
                    for (int i = 0; i < count; i++)
                    {
                        var Object = jsonObjects.ElementAt(i);
                        if (Object[FilterUserPasses].ToString() == FilterName)
                        {
                            return true;
                        }
                    }
                    BooklistUserSee.Clear();
                }
                if (Bookstatus == "filter")
                {
                    for (int i = 0; i < count; i++)
                    {
                        var Object = jsonObjects.ElementAt(i);

                        if (Object[FilterName].ToString().ToUpper().Contains(FilterUserPasses.ToUpper()))
                        {
                            BooklistUserSee.Add(i.ToString());
                        }
                    }
                    foreach (var item in BooklistUserSee)
                    {
                        index = 0;
                        var Object = jsonObjects.ElementAt(Convert.ToInt32(item));
                        foreach (var type in booksdatalist)
                        {
                            if (index < 6)
                            {
                                Console.WriteLine("Book " + type + " : " + Object[type]);
                            }
                            index++;
                        }
                        Console.WriteLine("\n");
                    }
                    if (BooklistUserSee.Any())
                    {
                        BooklistUserSee.Clear();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("\n Can't find a book, try another filter \n");
                        return false;
                    }
                }
                if (Bookstatus == "taken")
                {
                    for (int i = 0; i < count; i++)
                    {
                        var Object = jsonObjects.ElementAt(i);
                        if (Object["Bookborrower"].ToString().ToUpper() != FilterUserPasses.ToUpper())
                        {
                            BooklistUserSee.Add(i.ToString());
                        }
                    }
                    if (BooklistUserSee.Any())
                    {
                        Console.WriteLine("\nBooks that have been borrowed: \n");
                    }
                    else
                    {
                        Console.WriteLine("\nNo books have been borrowed, maybe you will be first? \n");
                    }
                    foreach (var item in BooklistUserSee)
                    {
                        index = 0;
                        var Object = jsonObjects.ElementAt(Convert.ToInt32(item));
                        foreach (var type in booksdatalist)
                        {
                            if (index < 6)
                            {
                                Console.WriteLine("Book " + type + " : " + Object[type]);
                            }
                            index++;
                        }
                        Console.WriteLine("\n");
                    }
                    if (BooklistUserSee.Any())
                    {
                        BooklistUserSee.Clear();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (Bookstatus != "IfISBNExist")
                {
                    for (int i = 0; i < count; i++)
                    {
                        var Object = jsonObjects.ElementAt(i);
                        foreach (var type in booksdatalist)
                        {
                            if (Bookstatus != "All")
                            {
                                if (Object[type].ToString().ToUpper() == Bookstatus.ToUpper())
                                {
                                    BooklistUserSee.Add(i.ToString());
                                    index++;
                                }
                            }
                        }
                        if (Bookstatus == "All")
                        {
                            BooklistUserSee.Add(i.ToString());
                            index++;
                        }
                    }
                    if (index > 0)
                    {
                        foreach (var item in BooklistUserSee)
                        {
                            index = 0;
                            var Object = jsonObjects.ElementAt(Convert.ToInt32(item));
                            foreach (var type in booksdatalist)
                            {
                                if (index < 6)
                                {
                                    Console.WriteLine("Book " + type + " : " + Object[type]);
                                }
                                index++;
                            }
                            Console.WriteLine("\n");
                        }
                        BooklistUserSee.Clear();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            BooklistUserSee.Clear();
            return false;
        }
        public bool FindBookInJson(string borrowtime, string ISBN, string bookkeeperName)
        {
            var json = File.ReadAllText(path);
            JArray jsonArray = JArray.Parse(json);
            var jsonObjects = jsonArray.OfType<JObject>().ToList();
            int count = jsonObjects.Count;
            var Object = jsonObjects.ElementAt(0);
            bool TrueFalse = false;
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                foreach (string type in booksdatalist)
                {
                    Object = jsonObjects.ElementAt(i);
                    if (Object[type].ToString().ToUpper() == bookkeeperName.ToUpper())
                    {
                        if (index >= 3)
                        {
                            TrueFalse = false;
                        }
                        index++;
                    }
                }
            }
            if (index < 3)
            {
                for (int i = 0; i < count; i++)
                {
                    foreach (string type in booksdatalist)
                    {
                        Object = jsonObjects.ElementAt(i);
                        if (ISBN == Object[type].ToString())
                        {
                            jsonArray[i]["Bookborrower"] = bookkeeperName;
                            jsonArray[i]["BorrowedforMonths"] = borrowtime;
                            TrueFalse = true;
                        }
                    }
                }
            }
            if (TrueFalse == true)
            {
                System.IO.File.WriteAllText(path, jsonArray.ToString());
            }
            return TrueFalse;
        }
        public void TakeInfo()
        {
            Console.Write("Please enter your name : ");
            string bookkeperName = Console.ReadLine();
            string bookStatus = "No";
            if (listAllBooks(bookStatus, "0", "Empty") == true)
            {
                Console.Write(bookkeperName + " choice a book from a list, enter books ISBN code to borrow it : \n");
                string bookIsbnCode;
                bookIsbnCode = Console.ReadLine();
                Console.Write("For how long you want to borrow a book? Enter months count(Max is 2 months) : ");
                string borrowtime;
                borrowtime = Console.ReadLine();
                if (Convert.ToInt32(borrowtime) > 2)
                {
                    while (Convert.ToInt32(borrowtime) > 2)
                    {
                        Console.Write("Sorry," + bookkeperName + " you can't borrow book for longer than 2 months\n");
                        Console.Write("Enter months count(Max is 2 months) : ");
                        borrowtime = Console.ReadLine();
                    }
                }
                if (FindBookInJson(borrowtime, bookIsbnCode, bookkeperName))
                {
                    Console.Write("***" + bookkeperName + ", you borrowed book for " + borrowtime + " months***\n");
                }
                else
                {
                    Console.Write("Seems you want to borrow more than 3 books or you entered wrong ISBN number " + bookIsbnCode + "?\n");
                }
            }
            else
            {
                Console.Write("Seems there's no more books left, " + bookkeperName + ", try again later\n");
            }
            addbook.AppStart();
        }
    }
}
