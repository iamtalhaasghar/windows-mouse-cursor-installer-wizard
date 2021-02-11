using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CursorInstallerWizard
{
	class CursorFile
	{
		public string FName { get; set; }
		public string FPath { get; set; }
		public string CursorName { get; set; }
		public System.Drawing.Icon Icon { get; set; }
		public override string ToString()
		{
			return "[FName: " + FName + ", CursorName: " + CursorName + ", FPath: " + FPath + "]";
		}
	}
}
