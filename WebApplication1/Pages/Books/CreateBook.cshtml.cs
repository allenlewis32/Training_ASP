using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Books
{
    public class CreateBookModel : PageModel
    {
		Book book = new();
		public string message = "", messageType="";
        public void OnGet()
        {
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
				
				SqlCommand command = new SqlCommand("insert_book", connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@bookCode", book.BookCode);
				command.Parameters.AddWithValue("@bookTitle", book.BookTitle);
				command.Parameters.AddWithValue("@category", book.Category);
				command.Parameters.AddWithValue("@author", book.Author);
				command.Parameters.AddWithValue("@publication", book.Publication);
				command.Parameters.AddWithValue("@publishDate", book.PublishDate);
				command.Parameters.AddWithValue("@bookEdition", book.BookEdition);
				command.Parameters.AddWithValue("@price", book.Price);
				command.Parameters.AddWithValue("@rackNum", book.RackNum);
				command.Parameters.AddWithValue("@dateArrival", book.DateArrival);
				command.Parameters.AddWithValue("@supplierID", book.SupplierID);
				command.ExecuteNonQuery();

				message = "Book added successfully";
				messageType = "alert-success";

				TempData["message"] = message;
				TempData["messageType"] = messageType;
				Response.Redirect("/Books/IndexBook");
				
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
