using System;

namespace Example1_BlankWindow
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
