using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Books
{
	public class EditBookModel : PageModel
	{
		public Book book = new();
		public string message = "", messageType = "";
		public void OnGet()
		{
			try
			{
				book.BookCode = Request.Query["bookCode"];
				SqlConnection connection = new("Data Source=DESKTOP-IQRSRP8;initial Catalog=LMS;Integrated Security=True;Encrypt=False;");
				connection.Open();

				SqlCommand command = connection.CreateCommand();
				command.CommandText = $"select * from lms_book_details where book_code='{book.BookCode}'";
				using (var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						book.BookCode = reader.GetString(0);
						book.BookTitle = reader.GetString(1);
						book.Category = reader.GetString(2);
						book.Author = reader.GetString(3);
						book.Publication = reader.GetString(4);
						book.PublishDate = DateOnly.FromDateTime(reader.GetDateTime(5));
						book.BookEdition = reader.GetInt32(6);
						book.Price = reader.GetInt32(7);
						book.RackNum = reader.GetString(8);
						book.DateArrival = DateOnly.FromDateTime(reader.GetDateTime(9));
						book.SupplierID = reader.GetString(10);
					}
					else
					{
						throw new Exception("Invalid book code");
					}
				}

				message = "";

				connection.Close();
			}
			catch (Exception ex)
			{
				message = ex.Message;
				messageType = "alert-danger";
			}
		}
		public void OnPost()
		{
			try
			{
				SqlConnection connection = new("Data Source=DESKTOP-IQRSRP8;initial Catalog=LMS;Integrated Security=True;Encrypt=False;");
				connection.Open();

				book.BookCode = Request.Form["bookCode"];
				book.BookTitle = Request.Form["bookTitle"];
				book.Category = Request.Form["category"];
				book.Author = Request.Form["author"];
				book.Publication = Request.Form["publication"];
				book.PublishDate = DateOnly.FromDateTime(Convert.ToDateTime(Request.Form["publishDate"]));
				book.BookEdition = Convert.ToInt32(Request.Form["bookEdition"]);
				book.Price = Convert.ToInt32(Request.Form["price"]);
				book.RackNum = Request.Form["rackNum"];
				book.DateArrival = DateOnly.FromDateTime(Convert.ToDateTime(Request.Form["dateArrival"]));
				book.SupplierID = Request.Form["supplierID"];

				SqlCommand command = connection.CreateCommand();
				command.CommandText = $"UPDATE LMS_BOOK_DETAILS SET BOOK_CODE='{book.BookCode}', BOOK_TITLE='{book.BookTitle}', " +
					$"CATEGORY='{book.Category}', AUTHOR='{book.Author}', PUBLICATION='{book.Publication}', " +
					$"PUBLISH_DATE='{book.PublishDate}', BOOK_EDITION={book.BookEdition}, PRICE={book.Price}, " +
					$"RACK_NUM='{book.RackNum}', DATE_ARRIVAL='{book.DateArrival}', SUPPLIER_ID='{book.SupplierID}' " +
					$"WHERE BOOK_CODE='{Request.Form["originalBookCode"]}'";
				command.ExecuteNonQuery();

				message = "Book updated successfully";
				messageType = "alert-success";
				connection.Close();
			}
			catch (Exception ex)
			{
				message = ex.Message;
				messageType = "alert-danger";
			}
		}
	}
}
