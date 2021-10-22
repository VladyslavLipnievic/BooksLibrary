using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace ConsoleApp1
{
    public class ListAllBooks
    {
        List<string> booksdatalist = new List<string>
        {
           "Name","Author","Category","Language","Publication date","ISBN","Bookborrower","BorrowedforMonths"
        };
        string path = @"C:\path.json";
        public static TakeBook allbooks = new TakeBook();
        public bool TakeInfo()
        {


            if (allbooks.listAllBooks("All","0","Empty") == false)
            {
                Console.Write("Seems there's no more books left\n");
                return false;
            }
            else
            {
                return true;
            }
        }
        public void DeleteOrReturnBook(string ISBN, string action)
        {
            var json = File.ReadAllText(path);
            JArray jsonArray = JArray.Parse(json);
            var jsonObjects = jsonArray.OfType<JObject>().ToList();
            int count = jsonObjects.Count;
            var Object = jsonObjects.ElementAt(0);
            int wasRemovedorReturned = 0;
            int jsonlocation = 0;


            for (int i = 0; i < count; i++)
            {
                Object = jsonObjects.ElementAt(i);
                foreach (var type in booksdatalist)
                {
                    if (Object[type].ToString() == ISBN)
                    {
                        wasRemovedorReturned++;
                        jsonlocation = i;
                    }
                }
            }
            if (action == "return")
            {
                if (wasRemovedorReturned == 1)
                {
                    Object = jsonObjects.ElementAt(jsonlocation);
                    Object["Bookborrower"] = "No";
                    Object["BorrowedforMonths"] = "Nope";
                    var jsonString = JsonConvert.SerializeObject(jsonObjects);
                    System.IO.File.WriteAllText(path, jsonString);
                    Console.WriteLine("***You have returned book succesfully ***\n");
                }
                else
                {
                    Console.WriteLine("Seems you have entered wrong ISBN number \n");
                }
            }
            if (action == "delete")
            {
                if (wasRemovedorReturned == 1)
                {
                    jsonObjects.Remove(jsonObjects.ElementAt(jsonlocation));
                    Console.WriteLine("\n***Book was removed***\n");
                }
                else
                {
                    Console.WriteLine("\n Seems you enteres wrong ISBN number \n");
                }
                var jsonString = JsonConvert.SerializeObject(jsonObjects);
                System.IO.File.WriteAllText(path, jsonString);
                if (new FileInfo(path).Length == 2)
                {
                    System.IO.File.WriteAllText(path, "");
                }
            }
        }
    }
}
