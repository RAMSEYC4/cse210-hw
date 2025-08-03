using System;
using System.Threading;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        MindfulnessProgram program = new MindfulnessProgram();
        program.Run();
    }
}

class MindfulnessProgram
{
    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Mindfulness Program!");
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Start breathing activity");
            Console.WriteLine("2. Start reflecting activity");
            Console.WriteLine("3. Start listing activity");
            Console.WriteLine("4. Quit");
            Console.WriteLine();
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BreathingActivity breathing = new BreathingActivity();
                    breathing.StartActivity();
                    break;
                case "2":
                    ReflectionActivity reflection = new ReflectionActivity();
                    reflection.StartActivity();
                    break;
                case "3":
                    ListingActivity listing = new ListingActivity();
                    listing.StartActivity();
                    break;
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}

class BreathingActivity
{
    public void StartActivity()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Breathing Activity.");
        Console.WriteLine();
        Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like for your session? ");
        
        int duration = int.Parse(Console.ReadLine());
        
        Console.WriteLine();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);

        DateTime startTime = DateTime.Now;
        bool breatheIn = true;

        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.Clear();
            if (breatheIn)
            {
                Console.WriteLine("Breathe in...");
                ShowCountdown(4);
            }
            else
            {
                Console.WriteLine("Breathe out...");
                ShowCountdown(4);
            }
            breatheIn = !breatheIn;
        }

        FinishActivity("Breathing Activity", duration);
    }

    private void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i}");
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    private void ShowSpinner(int seconds)
    {
        string[] spinner = { "|", "/", "-", "\\" };
        int spinnerIndex = 0;
        
        for (int i = 0; i < seconds * 4; i++)
        {
            Console.Write($"\r{spinner[spinnerIndex]}");
            spinnerIndex = (spinnerIndex + 1) % spinner.Length;
            Thread.Sleep(250);
        }
        Console.WriteLine();
    }

    private void FinishActivity(string activityName, int duration)
    {
        Console.Clear();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine($"You have completed another {duration} seconds of the {activityName}.");
        ShowSpinner(3);
        Console.WriteLine("Press any key to return to menu...");
        Console.ReadKey();
    }
}

class ReflectionActivity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public void StartActivity()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Reflection Activity.");
        Console.WriteLine();
        Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like for your session? ");
        
        int duration = int.Parse(Console.ReadLine());
        
        Console.WriteLine();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);

        Random random = new Random();
        string selectedPrompt = prompts[random.Next(prompts.Count)];
        
        Console.Clear();
        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {selectedPrompt} ---");
        Console.WriteLine();
        Console.WriteLine("When you have something in mind, press enter to continue.");
        Console.ReadLine();

        Console.WriteLine("Now ponder on each of the following questions as they related to this experience.");
        Console.WriteLine("You may begin in...");
        ShowCountdown(5);

        DateTime startTime = DateTime.Now;

        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            string question = questions[random.Next(questions.Count)];
            Console.Clear();
            Console.WriteLine($"> {question}");
            ShowSpinner(10);
            
            if ((DateTime.Now - startTime).TotalSeconds >= duration)
                break;
        }

        FinishActivity("Reflection Activity", duration);
    }

    private void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i}");
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    private void ShowSpinner(int seconds)
    {
        string[] spinner = { "|", "/", "-", "\\" };
        int spinnerIndex = 0;
        
        for (int i = 0; i < seconds * 4; i++)
        {
            Console.Write($"\r{spinner[spinnerIndex]}");
            spinnerIndex = (spinnerIndex + 1) % spinner.Length;
            Thread.Sleep(250);
        }
        Console.WriteLine();
    }

    private void FinishActivity(string activityName, int duration)
    {
        Console.Clear();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine($"You have completed another {duration} seconds of the {activityName}.");
        ShowSpinner(3);
        Console.WriteLine("Press any key to return to menu...");
        Console.ReadKey();
    }
}

class ListingActivity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public void StartActivity()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Listing Activity.");
        Console.WriteLine();
        Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like for your session? ");
        
        int duration = int.Parse(Console.ReadLine());
        
        Console.WriteLine();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);

        Random random = new Random();
        string selectedPrompt = prompts[random.Next(prompts.Count)];
        
        Console.Clear();
        Console.WriteLine("List as many responses you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {selectedPrompt} ---");
        Console.WriteLine();
        Console.WriteLine("You may begin in...");
        ShowCountdown(5);

        DateTime startTime = DateTime.Now;
        int itemCount = 0;

        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            }
            Console.Write("> ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                itemCount++;
        }

        Console.WriteLine();
        Console.WriteLine($"You listed {itemCount} items!");
        
        FinishActivity("Listing Activity", duration);
    }

    private void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i}");
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    private void ShowSpinner(int seconds)
    {
        string[] spinner = { "|", "/", "-", "\\" };
        int spinnerIndex = 0;
        
        for (int i = 0; i < seconds * 4; i++)
        {
            Console.Write($"\r{spinner[spinnerIndex]}");
            spinnerIndex = (spinnerIndex + 1) % spinner.Length;
            Thread.Sleep(250);
        }
        Console.WriteLine();
    }

    private void FinishActivity(string activityName, int duration)
    {
        Console.Clear();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine($"You have completed another {duration} seconds of the {activityName}.");
        ShowSpinner(3);
        Console.WriteLine("Press any key to return to menu...");
        Console.ReadKey();
    }
}