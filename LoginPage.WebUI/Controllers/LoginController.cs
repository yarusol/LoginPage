using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

using LoginPage.WebUI.Managers;

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
		public ActionResult Login( string message = "" )
		{
			ViewBag.TitleCyan = "Repotring";
			ViewBag.TitleWhite = "Tool";
			ViewBag.Message = message;
			return View();
		}

		[HttpPost]
		public ActionResult Login( string username, string password )
		{
			//System.Windows.Forms.MessageBox.Show( String.Format( "username = '{0}'\npassword = '{1}'", username, password ) );

			ISecurityManager security = new FakeSecurityManager();
			if ( security.Authenticate( username, password ) )
			{
				HttpContext.User = new GenericPrincipal( new GenericIdentity( username), new string[]{"user"} );
				return RedirectToAction( "Welcome" );
			}
			else
			{
				return Login( "Login failed !" );
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
