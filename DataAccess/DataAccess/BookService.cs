using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections.Generic;

namespace DataAccess.DataAccess
{
    public class BookService : IRepositoryService<Book>
    {
        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        //new Category(category_id: 1, category_name: "Programing"),
        //new Category(category_id: 2, category_name: "Math"),
        //new Category(category_id: 3, category_name: "Physics"),
        //new Category(category_id: 4, category_name: "Chemistry"),
        //new Category(category_id: 5, category_name: "Biology"),
        //new Category(category_id: 6, category_name: "Social"), };
        public IEnumerable<Book> GetAllItems()
        {
            List<Book> books = new()
            {
                //new Book(1, "1234567891231", "ASP.NET4 StepByStep", 1, 1, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Programing\HigherRes\ASP.NET4 StepByStep.png"),
                //new Book(2, "3455656334534", "Beginning OOP With CSharp", 1, 2, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Programing\HigherRes\Beginning OOP With CSharp.png"),
                //new Book(3, "2356443345556", "Head First HTML and CSS", 1, 3, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Programing\HigherRes\Head First HTML and CSS.png"),
                //new Book(4, "9780080496467", "A Mathematical Introduction to Logic", 2, 5, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Maths\A Mathematical Introduction to Logic.png"),
                //new Book(5, "9781108836654", "Abstract Algebra", 2, 6, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Maths\Abstract Algebra.png"),
                //new Book(6, "9781108836654", "An Introduction to Mathematical Logic", 2, 7, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Maths\An Introduction to Mathematical Logic.png"),
                //new Book(7, "9780071412124", "Physics Demystified", 3, 9, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Physics\Physics Demystified.png"),
                //new Book(8, "9781315359144", "Quantum Physics for Beginners", 3, 10, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Physics\Quantum Physics for Beginners.png"),
                //new Book(9, "9780241450048", "The Physics Book", 3, 11, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Physics\The Physics Book.png"),
                //new Book(10, "9789391184995", "CBSE Chapterwise Worksheets for Class 10", 4, 13, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Chemistry\CBSE Chapterwise Worksheets for Class 10.png"),
                //new Book(11, "9780081026915", "Comprehensive Natural Products III", 4, 14, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Chemistry\Comprehensive Natural Products III.png"),
                //new Book(12, "9781119444251", "Organic Chemistry", 4, 15, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Chemistry\Organic Chemistry.png"),
                //new Book(13, "9781420077643", "Biology for Engineers", 5, 17, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Biology\Biology for Engineers.png"),
                //new Book(14, "9781617317569", "Exploring Biology in the Laboratory, 3e", 5, 18, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Biology\Exploring Biology in the Laboratory, 3e.png"),
                //new Book(15, "9781284104493", "Lewin's GENES XII", 5, 19, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Biology\Lewin's GENES XII.png"),
                //new Book(16, "9780595434978", "Mindsight", 6, 21, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Social\Mindsight.png"),
                //new Book(17, "9781576750919", "People Smart", 6, 22, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Social\People Smart.png"),
                //new Book(18, "9780674417809", "Whiteness of a Different Color", 6, 23, @"C:\Users\Noxio\Documents\MEGAsync\ProjectTest\Books\Social\Whiteness of a Different Color.png")
            };   
            return books;
        
        }

        public Book GetById(int id)
        {
            return null;
        }

        public Book GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Book GetByUniqueID(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public Book GetItemByNameAndParentID(string name, int ID)
        {
            throw new NotImplementedException();
        }

        public int GetItemIDByName(string name)
        {
            throw new NotImplementedException();
        }

        public int GetItemIDByName(string name, int ID)
        {
            throw new NotImplementedException();
        }

        public void InsertItem(Book item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Book item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(int id, string DBProperty, object value)
        {
            throw new NotImplementedException();
        }
    }
}
