using System;

namespace DomainLayer.Models
{
    public class Book
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private string isbn_10;

        public string ISBN_10
        {
            get { return isbn_10; }
            set { isbn_10 = value; }
        }
        private string isbn_13;

        public string ISBN_13
        {
            get { return isbn_13; }
            set { isbn_13 = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string imagePath;

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }
        private int pageCount;

        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = value; }
        }
        private string publisher;

        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; }
        }
        private DateTime published;

        public DateTime Published
        {
            get { return published; }
            set { published = value; }
        }
        private string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        private string language;

        public string Language
        {
            get { return language; }
            set { language = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int categoryID;

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        private int shelfID;

        public int ShelfID
        {
            get { return shelfID; }
            set { shelfID = value; }
        }

        public Book(int iD, string iSBN_10, string iSBN_13, string title, string imagePath, int pageCount, string publisher, DateTime published, string author, string language, string description, int categoryID, int shelfID)
        {
            ID = iD;
            ISBN_10 = iSBN_10;
            ISBN_13 = iSBN_13;
            Title = title;
            ImagePath = imagePath;
            PageCount = pageCount;
            Publisher = publisher;
            Published = published;
            Author = author;
            Language = language;
            Description = description;
            CategoryID = categoryID;
            ShelfID = shelfID;
        }
    }
}
