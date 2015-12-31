using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPage.WebUI.Managers
{
	public interface ISecurityManager
	{
		bool Authenticate( string username, string password );
	}
}
