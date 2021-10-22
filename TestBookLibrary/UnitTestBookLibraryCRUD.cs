using NUnit.Framework;
using ConsoleApp1;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace TestBookLibrary
{
    public class Tests
    {

        List<string> booksdatalist = new List<string>();

        public static ListAllBooks listAll = new ListAllBooks();
        public static TakeBook takebook = new TakeBook();
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void TestIfFilterByAuthorMethodWorks()
        {
            string path = @"C:\path.json";                             //    Those 4 lines of code coulb be somehow declaired globally, a lot of screen space would be  saved
            var json = File.ReadAllText(path);                          //    As see already I'm new at testing :D
            JArray jsonArray = JArray.Parse(json);                      //
            var jsonObjects = jsonArray.OfType<JObject>().ToList();     //
            int count = jsonObjects.Count;
            var Object = jsonObjects.ElementAt(0);
            if(takebook.listAllBooks("filter", Object["Author"].ToString(), "Author"))
            {
                Assert.Pass();
            }
        }


               [Test]
        public void TestIfFilterByISBNMethodWorks()
        {
            string path = @"C:\path.json";
            var json = File.ReadAllText(path);
            JArray jsonArray = JArray.Parse(json);
            var jsonObjects = jsonArray.OfType<JObject>().ToList();
            int count = jsonObjects.Count;
            var Object = jsonObjects.ElementAt(0);
            if (takebook.listAllBooks("filter", Object["ISBN"].ToString(), "ISBN"))
            {
                Assert.Pass();
            }
        }




        [Test]
        public void TestIfFilterByLanguageMethodWorks()
        {
            string path = @"C:\path.json";
            var json = File.ReadAllText(path);
            JArray jsonArray = JArray.Parse(json);
            var jsonObjects = jsonArray.OfType<JObject>().ToList();
            int count = jsonObjects.Count;
            var Object = jsonObjects.ElementAt(0);
            if (takebook.listAllBooks("filter", Object["Language"].ToString(), "Language"))
            {
                Assert.Pass();
            }
        }
        [Test]
        public void TestIfFilterByCategoryMethodWorks()
        {
            string path = @"C:\path.json";
            var json = File.ReadAllText(path);
            JArray jsonArray = JArray.Parse(json);
            var jsonObjects = jsonArray.OfType<JObject>().ToList();
            int count = jsonObjects.Count;
            var Object = jsonObjects.ElementAt(0);
            if (takebook.listAllBooks("filter", Object["Category"].ToString(), "Category"))
            {
                Assert.Pass();
            }
        }
        [Test]
        public void TestIfFilterByNameMethodWorks()
        {
            string path = @"C:\path.json";
            var json = File.ReadAllText(path);
            JArray jsonArray = JArray.Parse(json);
            var jsonObjects = jsonArray.OfType<JObject>().ToList();
            int count = jsonObjects.Count;
            var Object = jsonObjects.ElementAt(0);
            if (takebook.listAllBooks("filter", Object["Name"].ToString(), "Name"))
            {
                Assert.Pass();
            }
        }
        [Test]
        public void TakeBookTestChangesInJson()
        {
            string path = @"C:\path.json";
            var json = File.ReadAllText(path);
            JArray jsonArray = JArray.Parse(json);
            var jsonObjects = jsonArray.OfType<JObject>().ToList();
            int count = jsonObjects.Count;
            var Object = jsonObjects.ElementAt(0);
           
            takebook.FindBookInJson("2", Object["ISBN"].ToString(), "UnitTestUser");
            json = File.ReadAllText(path);
            jsonArray = JArray.Parse(json);
            jsonObjects = jsonArray.OfType<JObject>().ToList();
            Object = jsonObjects.ElementAt(0);
            int index = 0;
            if (Object["Bookborrower"].ToString() == "UnitTestUser")
            {
                index++;
            }
            jsonArray[0]["Bookborrower"] = "No";

            System.IO.File.WriteAllText(path, jsonArray.ToString());
            booksdatalist.Clear();
            if (index > 0)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void ReturnBookTestChangesInJsonFile()
        {
            string path = @"C:\path.json";
            var json = File.ReadAllText(path);
            JArray jsonArray = JArray.Parse(json);
            var jsonObjects = jsonArray.OfType<JObject>().ToList();
            int count = jsonObjects.Count;
            var Object = jsonObjects.ElementAt(5);
            string ISBN = (Object["ISBN"].ToString());
            Object["Bookborrower"] = "UnitTest";
            Object["BorrowedforMonths"] = "UnitTest";
            listAll.DeleteOrReturnBook(ISBN, "return");
            json = File.ReadAllText(path);
            jsonArray = JArray.Parse(json);
            jsonObjects = jsonArray.OfType<JObject>().ToList();
            Object = jsonObjects.ElementAt(5);
            int index = 0;
            if (Object["Bookborrower"].ToString() == "No" && Object["BorrowedforMonths"].ToString() == "Nope")
            {
                index++;
            }
            if (index > 0)
            {
                Assert.Pass();
            }
        }














        [Test]
        public void DeleteBookInfoFromJsonFile()
        {
            string path = @"C:\path.json";
            var json = File.ReadAllText(path);
            JArray jsonArray = JArray.Parse(json);
            var jsonObjects = jsonArray.OfType<JObject>().ToList();
            int count = jsonObjects.Count;
          
            var Object = jsonObjects.ElementAt(3);
            string tempISBN = Object["ISBN"].ToString();
            listAll.DeleteOrReturnBook(Object["ISBN"].ToString(), "delete");
            json = File.ReadAllText(path);
            jsonArray = JArray.Parse(json);
            jsonObjects = jsonArray.OfType<JObject>().ToList();
            int index = 0;
            count = jsonObjects.Count;
            for (int i = 0; i < count; i++)
            {
                Object = jsonObjects.ElementAt(i);
                if (Object["ISBN"].ToString() == tempISBN)
                {
                    index++;
                }
            }
            booksdatalist.Clear();
            if (index == 0)
            {
                Assert.Pass();
            }
        }
    }
}