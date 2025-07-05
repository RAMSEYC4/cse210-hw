using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Input your grade:  ");
        string grade = Console.ReadLine();
        int gradeNumber = int.Parse(grade);
        string letter = "";

        if (gradeNumber >= 90)
        {
            Console.WriteLine("You scored A that's Very Very Good you passed");
            letter = "A";
            Console.WriteLine(letter);
        }

        if (gradeNumber >= 80)
        {
            Console.WriteLine("You scored B that's Very Good you passed");
            letter = "B";
            Console.WriteLine(letter);
        }

        if (gradeNumber >= 70)
        {
            Console.WriteLine("You scored  C that's Good you passed");
            letter = "C";
            Console.WriteLine(letter);
        }

        if (gradeNumber >= 60)
        {
            Console.WriteLine("You scored D work harder next time you did not pass sorry");
            letter = "D";
            Console.WriteLine(letter);
        }

        if (gradeNumber <= 60)
        {
            Console.WriteLine("You scored F You should pull up your socks you did not pass sorry");
            letter = "F";
            Console.WriteLine(letter);
        }
    }
}