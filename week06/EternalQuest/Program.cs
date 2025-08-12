using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Base class for all goals
    public abstract class Goal
    {
        protected string _name;
        protected string _description;
        protected int _points;

        public Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
        }

        public abstract void RecordEvent();
        public abstract bool IsComplete();
        public abstract string GetDetailsString();
        public abstract string GetStringRepresentation();

        public virtual int GetPoints()
        {
            return _points;
        }

        public string GetName()
        {
            return _name;
        }
    }

    // Simple goal that can be completed once
    public class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, string description, int points) 
            : base(name, description, points)
        {
            _isComplete = false;
        }

        public SimpleGoal(string name, string description, int points, bool isComplete) 
            : base(name, description, points)
        {
            _isComplete = isComplete;
        }

        public override void RecordEvent()
        {
            _isComplete = true;
        }

        public override bool IsComplete()
        {
            return _isComplete;
        }

        public override string GetDetailsString()
        {
            return $"[{(_isComplete ? "X" : " ")}] {_name} ({_description})";
        }

        public override string GetStringRepresentation()
        {
            return $"SimpleGoal:{_name},{_description},{_points},{_isComplete}";
        }
    }

    // Eternal goal that is never complete
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points) 
            : base(name, description, points)
        {
        }

        public override void RecordEvent()
        {
            // Nothing special happens, just awards points
        }

        public override bool IsComplete()
        {
            return false; // Never complete
        }

        public override string GetDetailsString()
        {
            return $"[ ] {_name} ({_description})";
        }

        public override string GetStringRepresentation()
        {
            return $"EternalGoal:{_name},{_description},{_points}";
        }
    }

    // Checklist goal that must be completed a certain number of times
    public class ChecklistGoal : Goal
    {
        private int _amountCompleted;
        private int _target;
        private int _bonus;

        public ChecklistGoal(string name, string description, int points, int target, int bonus) 
            : base(name, description, points)
        {
            _amountCompleted = 0;
            _target = target;
            _bonus = bonus;
        }

        public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted) 
            : base(name, description, points)
        {
            _amountCompleted = amountCompleted;
            _target = target;
            _bonus = bonus;
        }

        public override void RecordEvent()
        {
            _amountCompleted++;
        }

        public override bool IsComplete()
        {
            return _amountCompleted >= _target;
        }

        public override string GetDetailsString()
        {
            return $"[{(IsComplete() ? "X" : " ")}] {_name} ({_description}) -- Currently completed: {_amountCompleted}/{_target}";
        }

        public override string GetStringRepresentation()
        {
            return $"ChecklistGoal:{_name},{_description},{_points},{_target},{_bonus},{_amountCompleted}";
        }

        public override int GetPoints()
        {
            if (_amountCompleted == _target)
            {
                return _points + _bonus;
            }
            return _points;
        }
    }

    // Main program class
    public class GoalManager
    {
        private List<Goal> _goals;
        private int _score;

        public GoalManager()
        {
            _goals = new List<Goal>();
            _score = 0;
        }

        public void Start()
        {
            int choice = -1;
            while (choice != 6)
            {
                DisplayPlayerInfo();
                Console.WriteLine();
                Console.WriteLine("Menu Options:");
                Console.WriteLine("  1. Create New Goal");
                Console.WriteLine("  2. List Goals");
                Console.WriteLine("  3. Save Goals");
                Console.WriteLine("  4. Load Goals");
                Console.WriteLine("  5. Record Event");
                Console.WriteLine("  6. Quit");
                Console.Write("Select a choice from the menu: ");

                string input = Console.ReadLine();
                if (int.TryParse(input, out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            CreateGoal();
                            break;
                        case 2:
                            ListGoalDetails();
                            break;
                        case 3:
                            SaveGoals();
                            break;
                        case 4:
                            LoadGoals();
                            break;
                        case 5:
                            RecordEvent();
                            break;
                        case 6:
                            Console.WriteLine("Thank you for using Eternal Quest!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
                Console.WriteLine();
            }
        }

        public void DisplayPlayerInfo()
        {
            Console.WriteLine($"You have {_score} points.");
        }

        public void ListGoalNames()
        {
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
            }
        }

        public void ListGoalDetails()
        {
            Console.WriteLine("The goals are:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
            }
        }

        public void CreateGoal()
        {
            Console.WriteLine("The types of Goals are:");
            Console.WriteLine("  1. Simple Goal");
            Console.WriteLine("  2. Eternal Goal");
            Console.WriteLine("  3. Checklist Goal");
            Console.Write("Which type of goal would you like to create? ");
            
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                Console.Write("What is the name of your goal? ");
                string name = Console.ReadLine();
                Console.Write("What is a short description of it? ");
                string description = Console.ReadLine();
                Console.Write("What is the amount of points associated with this goal? ");
                
                if (int.TryParse(Console.ReadLine(), out int points))
                {
                    switch (choice)
                    {
                        case 1:
                            _goals.Add(new SimpleGoal(name, description, points));
                            break;
                        case 2:
                            _goals.Add(new EternalGoal(name, description, points));
                            break;
                        case 3:
                            Console.Write("How many times does this goal need to be accomplished for completion? ");
                            if (int.TryParse(Console.ReadLine(), out int target))
                            {
                                Console.Write("What is the bonus for accomplishing it that many times? ");
                                if (int.TryParse(Console.ReadLine(), out int bonus))
                                {
                                    _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                                }
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid goal type.");
                            break;
                    }
                }
            }
        }

        public void RecordEvent()
        {
            Console.WriteLine("The goals are:");
            ListGoalNames();
            Console.Write("Which goal did you accomplish? ");
            
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= _goals.Count)
            {
                Goal selectedGoal = _goals[choice - 1];
                selectedGoal.RecordEvent();
                int pointsEarned = selectedGoal.GetPoints();
                _score += pointsEarned;
                
                Console.WriteLine($"Congratulations! You have earned {pointsEarned} points!");
                Console.WriteLine($"You now have {_score} points.");
            }
            else
            {
                Console.WriteLine("Invalid goal selection.");
            }
        }

        public void SaveGoals()
        {
            Console.Write("What is the filename for the goal file? ");
            string filename = Console.ReadLine();

            try
            {
                using (StreamWriter outputFile = new StreamWriter(filename))
                {
                    outputFile.WriteLine(_score);
                    foreach (Goal goal in _goals)
                    {
                        outputFile.WriteLine(goal.GetStringRepresentation());
                    }
                }
                Console.WriteLine("Goals saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }

        public void LoadGoals()
        {
            Console.Write("What is the filename for the goal file? ");
            string filename = Console.ReadLine();

            try
            {
                string[] lines = File.ReadAllLines(filename);
                _goals.Clear();
                
                _score = int.Parse(lines[0]);
                
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(':');
                    string goalType = parts[0];
                    string[] goalParts = parts[1].Split(',');

                    switch (goalType)
                    {
                        case "SimpleGoal":
                            string name = goalParts[0];
                            string description = goalParts[1];
                            int points = int.Parse(goalParts[2]);
                            bool isComplete = bool.Parse(goalParts[3]);
                            _goals.Add(new SimpleGoal(name, description, points, isComplete));
                            break;
                        case "EternalGoal":
                            name = goalParts[0];
                            description = goalParts[1];
                            points = int.Parse(goalParts[2]);
                            _goals.Add(new EternalGoal(name, description, points));
                            break;
                        case "ChecklistGoal":
                            name = goalParts[0];
                            description = goalParts[1];
                            points = int.Parse(goalParts[2]);
                            int target = int.Parse(goalParts[3]);
                            int bonus = int.Parse(goalParts[4]);
                            int amountCompleted = int.Parse(goalParts[5]);
                            _goals.Add(new ChecklistGoal(name, description, points, target, bonus, amountCompleted));
                            break;
                    }
                }
                Console.WriteLine("Goals loaded successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GoalManager manager = new GoalManager();
            manager.Start();
        }
    }
}