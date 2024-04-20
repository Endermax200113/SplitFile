using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SplitFile.Exceptions
{
	internal interface IException
	{
		void SendMessage(string file, string method);
	}
}
