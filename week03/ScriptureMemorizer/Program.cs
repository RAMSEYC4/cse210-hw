using System;
using System.Collections.Generic;
using System.Linq;

// Program.cs
// This program helps users memorize scriptures by gradually hiding words
// 
// EXCEEDING REQUIREMENTS:
// - Added multiple scriptures library (user can choose from different scriptures)
// - Added difficulty levels (easy hides 1 word, medium hides 2, hard hides 3)
// - Added progress indicator showing how many words are left
// - Added option to restart with same scripture or choose a new one
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Scripture Memorizer!");
        Console.WriteLine();
        
        // Create a library of scriptures
        List<Scripture> scriptureLibrary = new List<Scripture>
        {
            new Scripture(new Reference("John", 3, 16), "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),
            new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight."),
            new Scripture(new Reference("Philippians", 4, 13), "I can do all this through him who gives me strength."),
            new Scripture(new Reference("Psalm", 23, 1, 2), "The Lord is my shepherd, I lack nothing. He makes me lie down in green pastures, he leads me beside quiet waters.")
        };
        
        while (true)
        {
            // Let user choose a scripture
            Console.WriteLine("Choose a scripture:");
            for (int i = 0; i < scriptureLibrary.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {scriptureLibrary[i].GetReference()}");
            }
            Console.WriteLine($"{scriptureLibrary.Count + 1}. Random scripture");
            Console.WriteLine("0. Quit");
            Console.Write("Enter your choice: ");
            
            string choice = Console.ReadLine();
            
            if (choice == "0")
                break;
                
            Scripture selectedScripture;
            if (choice == (scriptureLibrary.Count + 1).ToString())
            {
                Random random = new Random();
                selectedScripture = scriptureLibrary[random.Next(scriptureLibrary.Count)];
            }
            else if (int.TryParse(choice, out int index) && index >= 1 && index <= scriptureLibrary.Count)
            {
                selectedScripture = scriptureLibrary[index - 1];
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                continue;
            }
            
            // Choose difficulty level
            Console.WriteLine("\nChoose difficulty:");
            Console.WriteLine("1. Easy (hide 1 word at a time)");
            Console.WriteLine("2. Medium (hide 2 words at a time)");
            Console.WriteLine("3. Hard (hide 3 words at a time)");
            Console.Write("Enter your choice (1-3): ");
            
            int wordsToHide = 1;
            string difficultyChoice = Console.ReadLine();
            if (difficultyChoice == "2")
                wordsToHide = 2;
            else if (difficultyChoice == "3")
                wordsToHide = 3;
            
            // Start the memorization process
            RunMemorizationSession(selectedScripture, wordsToHide);
            
            Console.WriteLine("\nWould you like to try another scripture? (y/n): ");
            string continueChoice = Console.ReadLine().ToLower();
            if (continueChoice != "y" && continueChoice != "yes")
                break;
        }
        
        Console.WriteLine("Thank you for using Scripture Memorizer!");
    }
    
    static void RunMemorizationSession(Scripture scripture, int wordsToHide)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine($"\nProgress: {scripture.GetHiddenWordsCount()}/{scripture.GetTotalWordsCount()} words hidden");
            
            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("\nCongratulations! You've hidden all the words!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                break;
            }
            
            Console.WriteLine("\nPress Enter to hide more words, or type 'quit' to exit:");
            string input = Console.ReadLine();
            
            if (input.ToLower() == "quit")
                break;
                
            scripture.HideRandomWords(wordsToHide);
        }
    }
}

// Reference.cs
// This class handles scripture references like "John 3:16" or "Proverbs 3:5-6"
public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;
    
    // Constructor for single verse (like "John 3:16")
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = verse;
    }
    
    // Constructor for verse range (like "Proverbs 3:5-6")
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = startVerse;
        _endVerse = endVerse;
    }
    
    // Get the reference as a string
    public string GetDisplayText()
    {
        if (_verse == _endVerse)
        {
            return $"{_book} {_chapter}:{_verse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_verse}-{_endVerse}";
        }
    }
}

// Word.cs
// This class represents a single word in the scripture
public class Word
{
    private string _text;
    private bool _isHidden;
    
    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }
    
    // Hide this word
    public void Hide()
    {
        _isHidden = true;
    }
    
    // Check if word is hidden
    public bool IsHidden()
    {
        return _isHidden;
    }
    
    // Get the display text (either the word or underscores)
    public string GetDisplayText()
    {
        if (_isHidden)
        {
            return new string('_', _text.Length);
        }
        else
        {
            return _text;
        }
    }
    
    // Get the original text
    public string GetText()
    {
        return _text;
    }
}

// Scripture.cs
// This class represents a complete scripture with reference and text
public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    
    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        
        // Split the text into words and create Word objects
        string[] wordArray = text.Split(' ');
        foreach (string word in wordArray)
        {
            _words.Add(new Word(word));
        }
    }
    
    // Hide a specified number of random words
    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        
        // Get all visible words
        List<Word> visibleWords = new List<Word>();
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                visibleWords.Add(word);
            }
        }
        
        // Hide random words from the visible ones
        int wordsToHide = Math.Min(numberToHide, visibleWords.Count);
        for (int i = 0; i < wordsToHide; i++)
        {
            if (visibleWords.Count > 0)
            {
                int randomIndex = random.Next(visibleWords.Count);
                visibleWords[randomIndex].Hide();
                visibleWords.RemoveAt(randomIndex);
            }
        }
    }
    
    // Get the complete display text (reference + scripture text)
    public string GetDisplayText()
    {
        string displayText = _reference.GetDisplayText() + " ";
        
        foreach (Word word in _words)
        {
            displayText += word.GetDisplayText() + " ";
        }
        
        return displayText.Trim();
    }
    
    // Check if all words are hidden
    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }
    
    // Get the reference as a string
    public string GetReference()
    {
        return _reference.GetDisplayText();
    }
    
    // Get count of hidden words
    public int GetHiddenWordsCount()
    {
        int count = 0;
        foreach (Word word in _words)
        {
            if (word.IsHidden())
                count++;
        }
        return count;
    }
    
    // Get total word count
    public int GetTotalWordsCount()
    {
        return _words.Count;
    }
}