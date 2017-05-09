using System;
using System.IO;
using System.Collections.Generic;

namespace Ignatenko.Nsudotnet.NumberGuesser
{
    class GuessNumberGame
    {
        private const int MinNumber = 0;
        private const int MaxNumber = 100;

        private readonly List<int> answers = new List<int>();
        private readonly TextReader input;
        private readonly TextWriter output;
        private string userName;
        private int rightAnswer;

        private readonly string[] insults =
        {
            "You have no chance to win, {0}.",
            "Laugh to tears from your fail, {0}.",
            "Tears. I'm in tears. Stop it, {0}, please.",
            "{0} is stupid bitch.",
            "You suck, {0}"
        };

        public GuessNumberGame(TextReader input, TextWriter output)
        {
            this.input = input;
            this.output = output;
            AskUserName();
        }

        private void AskUserName()
        {
            output.WriteLine("Please, enter your name:");
            userName = input.ReadLine();
        }

        private void ShowHistory()
        {
            for (int i = 0; i < answers.Count; ++i)
            {
                output.WriteLine("Attempt №{0}: you've chosen {1} that {2} than right answer.",
                                  i + 1, answers[i], answers[i] > rightAnswer ? "greater" : "less");
            }
            output.WriteLine("Attempt №{0}: you've chosen {1} that equals to the right answer.",
                              answers.Count, rightAnswer);
        }

        public void Run()
        {
            var random = new Random();
            rightAnswer = random.Next(MinNumber, MaxNumber + 1);

            output.WriteLine("Hi, {0}! Let's play the Guess number game.\n" +
                            "I made up a number from {1} to {2}. You should guess it.\n" +
                            "Write your answer here or type 'q' to exit.",
                            userName, MinNumber, MaxNumber);

            var startTime = DateTime.Now;
            int answerNumber;
            int attempt = 1;
            while (true)
            {
                string answer = input.ReadLine();
                bool isOk = int.TryParse(answer, out answerNumber);

                if (isOk)
                {
                    if (answerNumber == rightAnswer)
                    {
                        var spentTime = DateTime.Now - startTime;
                        output.WriteLine("Congratulations! You've guessed number for {0} attempt(s), {1} minutes.\n" +
                                        "History of your answers:", attempt, spentTime.TotalMinutes);
                        ShowHistory();
                        break;
                    }
                    else
                    {
                        answers.Add(answerNumber);
                        output.WriteLine("Your answer is {0} than right answer. Try again!",
                                          answerNumber > rightAnswer ? "greater" : "less");
                    }
                    if (attempt % 4 == 0)
                    {
                        int insultIdx = random.Next(insults.Length);
                        output.WriteLine(insults[insultIdx], userName);
                    }
                    ++attempt;
                }
                else if (answer == "q")
                {
                    output.WriteLine("See you soon.");
                    break;
                }
                else
                {
                    output.WriteLine("I don't get it. Try again!");
                }
            }
            input.ReadLine();
        }
    }
}