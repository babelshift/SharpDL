using System;

namespace SharpDL_Sample2_NetCore
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
