using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnockModel;

namespace KnockoutData
{
    public class BookManager
    {
        public List<BookModel> GetBooksList()
        {
            List<BookModel> bookList = new List<BookModel>();
            BookModel objBook = new BookModel();
            try
            {
                using (TestKnockoutMVCEntities entities = new TestKnockoutMVCEntities())
                {
                    var books = entities.Books.ToList();

                    foreach (var item in books)
                    {
                        objBook.ID = item.ID;
                        objBook.BookName = item.BookName;
                        bookList.Add(objBook);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return bookList;
        }

        public BookModel GetBookById(string id)
        {
            BookModel objBook = new BookModel();
            try
            {
                int bookId = Convert.ToInt32(id);

                using (TestKnockoutMVCEntities entities = new TestKnockoutMVCEntities())
                {
                    var bookObj= entities.Books.SingleOrDefault(book => book.ID == bookId);
                    objBook.ID = bookObj.ID;
                    objBook.BookName = bookObj.BookName;
                }
            }
            catch
            {
                //throw new FaultException("Something went wrong");
            }
            return objBook;
        }

        //public void AddBook(string name)
        //{
        //    using (TestKnockoutMVCEntities entities = new TestKnockoutMVCEntities())
        //    {
        //        Book book = new Book { BookName = name };
        //        entities.Books.Add(book);
        //        entities.SaveChanges();
        //    }
        //}

        //public void UpdateBook(string id, string name)
        //{
        //    try
        //    {
        //        int bookId = Convert.ToInt32(id);

        //        using (TestKnockoutMVCEntities entities = new TestKnockoutMVCEntities())
        //        {
        //            Book book = entities.Books.SingleOrDefault(b => b.ID == bookId);
        //            book.BookName = name;
        //            entities.SaveChanges();
        //        }
        //    }
        //    catch
        //    {
        //        //throw new FaultException("Something went wrong");
        //    }
        //}

        //public void DeleteBook(string id)
        //{
        //    try
        //    {
        //        int bookId = Convert.ToInt32(id);

        //        using (TestKnockoutMVCEntities entities = new TestKnockoutMVCEntities())
        //        {
        //            Book book = entities.Books.SingleOrDefault(b => b.ID == bookId);
        //            entities.Books.Remove(book);
        //            entities.SaveChanges();
        //        }
        //    }
        //    catch
        //    {
        //        //throw new FaultException("Something went wrong");
        //    }
        //}

        //public List<string> GetBooksNames()
        //{
        //    using (TestKnockoutMVCEntities entities = new TestKnockoutMVCEntities())
        //    {
        //        return entities.Books.Select(book => book.BookName).ToList();
        //    }
        //}
    }
}
