using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> entries = new List<Entry>();
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What made me smile today?",
        "What did I learn about myself today?"
    };

    public void AddEntry()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];
        
        Console.WriteLine(prompt);
        Console.Write("Your response: ");
        string response = Console.ReadLine();
        
        Entry entry = new Entry(prompt, response);
        entries.Add(entry);
        
        Console.WriteLine("Entry added!\n");
    }

    public void DisplayEntries()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("No entries found.\n");
            return;
        }

        Console.WriteLine("=== Journal Entries ===");
        foreach (Entry entry in entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile()
    {
        Console.Write("Enter filename to save: ");
        string filename = Console.ReadLine();
        
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Entry entry in entries)
                {
                    writer.WriteLine(entry.ToFileString());
                }
            }
            Console.WriteLine("Journal saved successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}\n");
        }
    }

    public void LoadFromFile()
    {
        Console.Write("Enter filename to load: ");
        string filename = Console.ReadLine();
        
        try
        {
            entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    Entry entry = new Entry(parts[1], parts[2]);
                    entry.Date = parts[0];
                    entries.Add(entry);
                }
            }
            Console.WriteLine("Journal loaded successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}\n");
        }
    }
}