using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is the magic number: ");
        string magic = Console.ReadLine();
        int magicNumber = int.Parse(magic);

        int magicGuessNumber = 0;

        for (int i = 0; i < 100; i++)
        {
            Console.Write("What is your guess: ");
            string magicGuess = Console.ReadLine();
            magicGuessNumber = int.Parse(magicGuess); // Remove 'int' - just assign the value

            if (magicNumber > magicGuessNumber)
            {
                Console.WriteLine("Higher");
            }

            else if (magicNumber == magicGuessNumber)
            {
                Console.WriteLine("You guessed right");
                break;
            }

            else
            {
                Console.WriteLine("Lower");
            }
        }
    }
}