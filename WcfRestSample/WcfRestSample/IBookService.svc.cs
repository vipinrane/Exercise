using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfRestSample
{   
    public class BookService : IBookService
    {
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBooksList")]
        public List<Book> GetBooksList()
        {
            using (TestKnockoutMVCEntities1 entities = new TestKnockoutMVCEntities1())
            {
                return entities.Books.ToList();
            }
        }

        public Book GetBookById(string id)
        {
            try
            {
                int bookId = Convert.ToInt32(id);

                using (TestKnockoutMVCEntities1 entities = new TestKnockoutMVCEntities1())
                {
                    return entities.Books.SingleOrDefault(book => book.ID == bookId);
                }
            }
            catch
            {
                throw new FaultException("Something went wrong");
            }
        }

        public void AddBook(string name)
        {
            using (TestKnockoutMVCEntities1 entities = new TestKnockoutMVCEntities1())
            {
                Book book = new Book { BookName = name };
                entities.Books.Add(book);
                entities.SaveChanges();
            }
        }

        public void UpdateBook(string id, string name)
        {
            try
            {
                int bookId = Convert.ToInt32(id);

                using (TestKnockoutMVCEntities1 entities = new TestKnockoutMVCEntities1())
                {
                    Book book = entities.Books.SingleOrDefault(b => b.ID == bookId);
                    book.BookName = name;
                    entities.SaveChanges();
                }
            }
            catch
            {
                throw new FaultException("Something went wrong");
            }
        }

        public void DeleteBook(string id)
        {
            try
            {
                int bookId = Convert.ToInt32(id);

                using (TestKnockoutMVCEntities1 entities = new TestKnockoutMVCEntities1())
                {
                    Book book = entities.Books.SingleOrDefault(b => b.ID == bookId);
                    entities.Books.Remove(book);
                    entities.SaveChanges();
                }
            }
            catch
            {
                throw new FaultException("Something went wrong");
            }
        }

        public List<string> GetBooksNames()
        {
            using (TestKnockoutMVCEntities1 entities = new TestKnockoutMVCEntities1())
            {
                return entities.Books.Select(book => book.BookName).ToList();
            }
        }
    }
}
