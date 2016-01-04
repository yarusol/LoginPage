using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

using LoginPage.WebUI.Managers;
using LoginPage.WebUI.Models;

using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace LoginPage.WebUI.Controllers
{
    public class LoginController : Controller
    {
       
        public ActionResult Index()
        {
			if ( String.IsNullOrEmpty( HttpContext.User.Identity.Name ) )
			{
				return RedirectToAction( "Login" );
			}
			else
			{
				//System.Windows.Forms.MessageBox.Show( String.Format( "username = '{0}'\npassword = '{1}'", username, password ) );
				return RedirectToAction( "Login" );
			}
        }

		[HttpGet]
		public ActionResult Login( UserCredentials credentials = null , string message = "" )
		{
			ViewBag.TitleCyan = "Repotring";
			ViewBag.TitleWhite = "Tool";
			ViewBag.Message = message;
			if ( credentials == null )
			{
				credentials = new UserCredentials();
			}

			return View( credentials );
		}


		[HttpPost]
		public ActionResult Login( UserCredentials credentials )
		{
			ISecurityManager security = new FakeSecurityManager();
			if ( security.Authenticate( credentials.UserName, credentials.Password ) )
			{
				HttpContext.User = new GenericPrincipal( new GenericIdentity( credentials.UserName ), new string[] { "user" } );
				return RedirectToAction( "Welcome" );
			}
			else
			{
				credentials.Password = "";
				return Login( credentials, "Login failed !" );
			}
		}

		[HttpGet]
		public ActionResult Welcome()
		{
			ViewBag.TitleCyan = "Repotring";
			ViewBag.TitleWhite = "Tool";
			List<string> users = new List<string>();
			ViewBag.Values = users;

			string baseName = @"D:\_Usr\Ruslan\Documents\Projects\SS\LoginPage\users.sqlite";
			//SQLiteConnection.CreateFile(baseName);
			//SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory( "System.Data.SQLite" );
			using ( SQLiteConnection connection = new SQLiteConnection() )
			{
				connection.ConnectionString = "Data Source = " + baseName;
				connection.Open();
				using ( SQLiteCommand command = new SQLiteCommand( connection ) )
				{
					command.CommandText = @"SELECT [Name] From [Users];";
					command.CommandType = CommandType.Text;
					using ( var reader = command.ExecuteReader() )
					{
						foreach ( IDataRecord userName in reader )
						{
							users.Add( userName.GetString(0) );
						}
					}
				}
			}

			return View();
		}


    }
}
