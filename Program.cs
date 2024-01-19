namespace MultiChess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Display.init();
            Game.init(Game.State.PLAYING_PLAYER);
            while(!Display.shouldClose())
            {
                Display.drawGame();
                Controller.handleMouseInput();
            }
            Display.close();
        }
    }
}