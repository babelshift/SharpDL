using System;

namespace Example2_DrawTexture
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
