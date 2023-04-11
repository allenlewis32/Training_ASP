using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Books
{
    public class IndexBookModel : PageModel
    {
        public List<Books> bookList = new();
        public void OnGet()
        {
            SqlConnection connection = new("Data Source=DESKTOP-IQRSRP8;initial Catalog=LMS;Integrated Security=True;Encrypt=False;");
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT BOOK_CODE, BOOK_TITLE, AUTHOR, PUBLICATION, PRICE from LMS_BOOK_DETAILS";
            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Books book = new();
                    book.Id = reader.GetString(0);
                    book.BookName = reader.GetString(1);
                    book.Author = reader.GetString(2);
                    book.Publication = reader.GetString(3);
                    book.Price = reader.GetInt32(4);
                    bookList.Add(book);
                }
            }
        }
    }
    public class Books
    {
        public string Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Publication { get; set; }
        public int Price { get; set; }
    }
}
