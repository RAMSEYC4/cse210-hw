using System;
using System.Collections.Generic;

// Comment class to store individual comment information
public class Comment
{
    private string _commenterName;
    private string _commentText;

    // Constructor
    public Comment(string commenterName, string commentText)
    {
        _commenterName = commenterName;
        _commentText = commentText;
    }

    // Properties to access comment data
    public string CommenterName
    {
        get { return _commenterName; }
        set { _commenterName = value; }
    }

    public string CommentText
    {
        get { return _commentText; }
        set { _commentText = value; }
    }

    // Override ToString for easy display
    public override string ToString()
    {
        return $"{_commenterName}: {_commentText}";
    }
}

// Video class to store video information and manage comments
public class Video
{
    private string _title;
    private string _author;
    private int _lengthInSeconds;
    private List<Comment> _comments;

    // Constructor
    public Video(string title, string author, int lengthInSeconds)
    {
        _title = title;
        _author = author;
        _lengthInSeconds = lengthInSeconds;
        _comments = new List<Comment>();
    }

    // Properties to access video data
    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public string Author
    {
        get { return _author; }
        set { _author = value; }
    }

    public int LengthInSeconds
    {
        get { return _lengthInSeconds; }
        set { _lengthInSeconds = value; }
    }

    // Method to add a comment to the video
    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    // Method to return the number of comments
    public int GetCommentCount()
    {
        return _comments.Count;
    }

    // Method to get all comments
    public List<Comment> GetComments()
    {
        return _comments;
    }

    // Helper method to format length as minutes:seconds
    public string GetFormattedLength()
    {
        int minutes = _lengthInSeconds / 60;
        int seconds = _lengthInSeconds % 60;
        return $"{minutes}:{seconds:D2}";
    }
}

// Main program class
public class Program
{
    public static void Main(string[] args)
    {
        // Create a list to store videos
        List<Video> videos = new List<Video>();

        // Create Video 1
        Video video1 = new Video("How to Bake Perfect Chocolate Chip Cookies", "CookingMaster123", 480);
        video1.AddComment(new Comment("BakingFan87", "This recipe is amazing! My cookies turned out perfect."));
        video1.AddComment(new Comment("ChocolateLover", "I added extra chocolate chips and they were incredible!"));
        video1.AddComment(new Comment("MomOf3Kids", "My kids absolutely loved these cookies. Thank you!"));
        video1.AddComment(new Comment("NewbieBaker", "Great step-by-step instructions. Very easy to follow."));
        videos.Add(video1);

        // Create Video 2
        Video video2 = new Video("10 Minute Morning Workout Routine", "FitnessGuru2024", 600);
        video2.AddComment(new Comment("HealthyLife99", "Perfect workout for busy mornings!"));
        video2.AddComment(new Comment("WorkoutWarrior", "I do this every day now. Seeing great results!"));
        video2.AddComment(new Comment("BusyMom", "Finally found a workout that fits my schedule."));
        videos.Add(video2);

        // Create Video 3
        Video video3 = new Video("Learn Python in 15 Minutes", "CodeAcademy101", 900);
        video3.AddComment(new Comment("StudentCoder", "Great introduction to Python! Very clear explanations."));
        video3.AddComment(new Comment("TechEnthusiast", "I wish all programming tutorials were this concise."));
        video3.AddComment(new Comment("CareerChanger", "This helped me decide to learn more about programming."));
        video3.AddComment(new Comment("PythonNewbie", "Bookmarked for future reference. Thanks!"));
        videos.Add(video3);

        // Create Video 4
        Video video4 = new Video("Beautiful Sunset Timelapse - Peaceful Music", "NatureVibes", 180);
        video4.AddComment(new Comment("RelaxationSeeker", "So peaceful and calming. Perfect for meditation."));
        video4.AddComment(new Comment("StressedStudent", "I watch this every night before bed. Very soothing."));
        video4.AddComment(new Comment("NaturePhotographer", "Stunning colors! What camera did you use?"));
        videos.Add(video4);

        // Display information for each video
        Console.WriteLine("=== YouTube Video Tracker ===\n");

        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.GetFormattedLength()} ({video.LengthInSeconds} seconds)");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  - {comment}");
            }

            Console.WriteLine(); // Add blank line between videos
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}