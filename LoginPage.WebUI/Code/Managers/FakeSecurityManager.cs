using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;


namespace LoginPage.WebUI.Managers
{
	public class FakeSecurityManager : ISecurityManager
	{


		public bool Authenticate( string username, string password )
		{
			if ( username == "qwerty" && password == "1qaz2wsx" )
			{
				FormsAuthentication.SetAuthCookie( username, false );
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
