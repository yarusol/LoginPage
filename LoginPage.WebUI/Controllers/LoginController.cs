using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

using LoginPage.WebUI.Managers;
using LoginPage.WebUI.Models;

namespace LoginPage.WebUI.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

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
			return View();
		}


    }
}
