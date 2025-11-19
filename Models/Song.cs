using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telhai.DotNet.Classes.HW1.EliranElisha.Models
{
    public enum Genre
    {
        Pop, Rock, Jazz, Classical, HipHop, Electronic, Other
    }
    /// <summary>
    /// Song class representing a music track with properties and validation.
    /// </summary>
    public class Song
    {
        private static int _autoId = 1;

        public int Id { get; private set; }
        public Guid UniqueId { get; private set; } = Guid.NewGuid();
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public int Year { get; private set; }
        public double Duration { get; private set; }
        public Genre Genre { get; private set; }

        // Empty constructor
        public Song() { }

        // Full constructor with validation
        public Song(string title, string artist, int year, double duration, Genre genre)
        {
            Id = _autoId++;
            SetTitle(title);
            SetArtist(artist);
            SetYear(year);
            SetDuration(duration);
            Genre = genre;
        }
        /// <summary>
        /// Sets the title of the object.
        /// </summary>
        /// <param name="title">The new title to set. Must contain at least 2 characters and cannot be null, empty, or consist only of
        /// whitespace.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="title"/> is null, empty, consists only of whitespace, or has fewer than 2
        /// characters.</exception>

        private void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Length < 2)
                throw new ArgumentException("Title must contain at least 2 characters.");
            Title = title;
        }
        /// <summary>
        ///    sets the artist of the object.
        /// </summary>
        /// <param name="artist"></param>
        /// <exception cref="ArgumentException"></exception>
        private void SetArtist(string artist)
        {
            if (string.IsNullOrWhiteSpace(artist))
                throw new ArgumentException("Artist name is required.");
            Artist = artist;
        }
        /// <summary>
        /// sets the year of the object.
        /// </summary>
        /// <param name="year"></param>
        /// <exception cref="ArgumentException"></exception>
        private void SetYear(int year)
        {
            if (year < 1900 || year > 2025)
                throw new ArgumentException("Year must be between 1900 and 2025.");
            Year = year;
        }
        /// <summary>
        /// sets the duration of the object.
        /// </summary>
        /// <param name="duration"></param>
        /// <exception cref="ArgumentException"></exception>
        private void SetDuration(double duration)
        {
            if (duration <= 0 || duration > 20)
                throw new ArgumentException("Duration must be greater than 0 and max 20 minutes.");
            Duration = duration;
        }
        /// <summary>
        /// prints the object details to the console.
        /// </summary>
        public void Print()
        {
            Console.WriteLine(ToString());
        }
        /// <summary>
        /// Returns a string representation of the song, including its ID, unique identifier, title, artist, year,
        /// duration, and genre.
        /// </summary>

        public override string ToString()
        {
            return $"[Song] Id={Id}, Guid={UniqueId}, Title={Title}, Artist={Artist}, Year={Year}, Duration={Duration}, Genre={Genre}";
        }
    }
}