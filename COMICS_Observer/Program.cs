namespace COMICS_Observer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Game game = 
                new Game(new AchievementHandler());
            
            game.StartGame();
        }
    }
}