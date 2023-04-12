using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Books
{
	public class IndexBookModel : PageModel
	{
		public List<Book> bookList = new();
		public string message = "", messageType = "";
		public void OnGet()
		{
			message = ((string?)TempData["message"])??"";
			messageType = ((string?)TempData["messageType"]) ?? "";
			try
			{
				SqlConnection connection = new("Data Source=DESKTOP-IQRSRP8;initial Catalog=LMS;Integrated Security=True;Encrypt=False;");
				connection.Open();
				SqlCommand command = connection.CreateCommand();
				command.CommandText = "SELECT BOOK_CODE, BOOK_TITLE, AUTHOR, PUBLICATION, PRICE from LMS_BOOK_DETAILS";
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Book book = new();
						book.BookCode = reader.GetString(0);
						book.BookTitle = reader.GetString(1);
						book.Author = reader.GetString(2);
						book.Publication = reader.GetString(3);
						book.Price = reader.GetInt32(4);
						bookList.Add(book);
					}
				}
				connection.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
	public class Book
	{
		public string BookCode { get; set; }
		public string BookTitle { get; set; }
		public string Category { get; set; }
		public string Author { get; set; }
		public string Publication { get; set; }
		public DateOnly PublishDate { get; set; }
		public int BookEdition { get; set; }
		public int Price { get; set; }
		public string RackNum { get; set; }
		public DateOnly DateArrival { get; set; }
		public string SupplierID { get; set; }
	}
}
