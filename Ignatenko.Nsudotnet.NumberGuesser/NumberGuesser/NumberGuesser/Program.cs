using System;

namespace Ignatenko.Nsudotnet.NumberGuesser
{
    class Program
    { 
        static void Main(string[] args)
        {
            GuessNumberGame game = new GuessNumberGame(Console.In, Console.Out);
            game.Run();
        }
    }
}