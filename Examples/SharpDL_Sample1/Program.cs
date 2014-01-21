using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL_Sample1
{
	class Program
	{
		[STAThread()]
		static void Main(string[] args)
		{
			MainGame game = new MainGame();
			game.Run();
		}
	}
}
