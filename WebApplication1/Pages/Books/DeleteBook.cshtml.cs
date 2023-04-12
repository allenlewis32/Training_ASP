using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Books
{
    public class DeleteBookModel : PageModel
    {
		public string bookCode;
		public Book book = new();
		public string message = "", messageType = "";
        public void OnGet()
        {
			bookCode = Request.Query["bookCode"];
			try
			{
				SqlConnection connection = new("Data Source=DESKTOP-IQRSRP8;initial Catalog=LMS;Integrated Security=True;Encrypt=False;");
				connection.Open();
				SqlCommand command = connection.CreateCommand();
				command.CommandText = $"SELECT BOOK_CODE, BOOK_TITLE, AUTHOR, PUBLICATION, PRICE from LMS_BOOK_DETAILS WHERE BOOK_CODE={bookCode}";
				using(var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						book = new Book();
						book.BookCode = reader.GetString(0);
						book.BookTitle = reader.GetString(1);
						book.Author = reader.GetString(2);
						book.Publication = reader.GetString(3);
						book.Price = reader.GetInt32(4);
					}
				}

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
				SqlCommand command = connection.CreateCommand();
				command.CommandText = $"DELETE FROM LMS_BOOK_DETAILS WHERE BOOK_CODE='{Request.Form["bookCode"]}'";
				if(command.ExecuteNonQuery() > 0)
				{
					Response.Redirect("/Books/IndexBook");
				} else
				{
					message = "Unable to delete";
					messageType = "alert-danger";
				}
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
