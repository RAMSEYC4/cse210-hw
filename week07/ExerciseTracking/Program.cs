using System;
using System.Collections.Generic;

// Base Activity class
public abstract class Activity
{
    private string _date;
    private int _minutes;

    public Activity(string date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    // Protected properties to allow derived classes to access these values
    protected string Date { get { return _date; } }
    protected int Minutes { get { return _minutes; } }

    // Abstract methods that must be implemented by derived classes
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Virtual method for activity name - can be overridden if needed
    public virtual string GetActivityName()
    {
        return this.GetType().Name;
    }

    // Summary method that uses the overridden methods
    public virtual string GetSummary()
    {
        return $"{_date} {GetActivityName()} ({_minutes} min) - Distance {GetDistance():F1} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}

// Running class
public class Running : Activity
{
    private double _distance;

    public Running(string date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return (_distance / Minutes) * 60;
    }

    public override double GetPace()
    {
        return Minutes / _distance;
    }
}

// Cycling class
public class Cycling : Activity
{
    private double _speed;

    public Cycling(string date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return (_speed * Minutes) / 60;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetPace()
    {
        return 60 / _speed;
    }
}

// Swimming class
public class Swimming : Activity
{
    private int _laps;

    public Swimming(string date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        // Distance in miles = laps * 50 / 1000 * 0.62
        return _laps * 50.0 / 1000.0 * 0.62;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Minutes) * 60;
    }

    public override double GetPace()
    {
        return Minutes / GetDistance();
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
        // Create activities of each type
        List<Activity> activities = new List<Activity>();

        // Add running activity
        Running running = new Running("03 Nov 2022", 30, 3.0);
        activities.Add(running);

        // Add cycling activity  
        Cycling cycling = new Cycling("03 Nov 2022", 30, 12.0);
        activities.Add(cycling);

        // Add swimming activity
        Swimming swimming = new Swimming("03 Nov 2022", 30, 20);
        activities.Add(swimming);

        // Add more activities to demonstrate variety
        activities.Add(new Running("04 Nov 2022", 45, 5.2));
        activities.Add(new Cycling("05 Nov 2022", 60, 15.5));
        activities.Add(new Swimming("06 Nov 2022", 25, 15));

        // Iterate through the list and display summaries
        Console.WriteLine("Exercise Activity Summary:");
        Console.WriteLine("========================");
        
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }

        Console.WriteLine("\nDetailed Information:");
        Console.WriteLine("====================");
        
        // Demonstrate polymorphism - same method calls work for all types
        foreach (Activity activity in activities)
        {
            Console.WriteLine($"{activity.GetActivityName()}:");
            Console.WriteLine($"  Distance: {activity.GetDistance():F2} miles");
            Console.WriteLine($"  Speed: {activity.GetSpeed():F2} mph"); 
            Console.WriteLine($"  Pace: {activity.GetPace():F2} min per mile");
            Console.WriteLine();
        }
    }
}