using System;
using System.Collections.Generic;
using System.Linq;

namespace Telhai.DotNet.Classes.HW1.EliranElisha.Models
{
    /// <summary>
    /// Playlist class representing a collection of songs.
    /// </summary>
    public class Playlist
    {
        private static int _autoId = 1;

        public int Id { get; private set; }
        public Guid UniqueId { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public List<Song> Songs { get; private set; }
        /// <summary>
        /// Empty constructor for Playlist.
        /// </summary>
        public Playlist()
        {
            Id = _autoId++;
            Songs = new List<Song>();
        }

        public Playlist(string name) : this()
        {
            SetName(name);
        }

        public Playlist(string name, List<Song> songs) : this(name)
        {
            Songs = songs ?? new List<Song>();
        }
        /// <summary>
        /// Sets the name of the playlist.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="ArgumentException"></exception>
        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
                throw new ArgumentException("Playlist name must contain at least 3 characters.");
            Name = name;
        }
        /// <summary>
        /// Adds a song to the playlist.
        /// </summary>
        /// <param name="song"></param>
        public void AddSong(Song song)
        {
            Songs.Add(song);
        }
        /// <summary>
        /// Removes a song from the playlist by its ID.
        /// </summary>
        /// <param name="songId"></param>
        public void RemoveSong(int songId)
        {
            var s = Songs.FirstOrDefault(x => x.Id == songId);
            if (s != null)
                Songs.Remove(s);
        }
        /// <summary>
        /// Finds a song in the playlist by its ID.
        /// </summary>
        /// <param name="songId"></param>
        /// <returns></returns>
        public Song FindSong(int songId)
        {
            return Songs.FirstOrDefault(x => x.Id == songId);
        }
        /// <summary>
        /// Gets the total duration of all songs in the playlist.
        /// </summary>
        /// <returns></returns>
        public double GetTotalDuration()
        {
            return Songs.Sum(s => s.Duration);
        }
        /// <summary>
        /// Shuffles the songs in the playlist randomly.
        /// </summary>
        public void Shuffle()
        {
            Random rand = new Random();
            Songs = Songs.OrderBy(s => rand.Next()).ToList();
        }
        /// <summary>
        /// Writes the string representation of the current object to the console.
        /// </summary>
        /// <remarks>This method calls the <see cref="ToString"/> method of the current object and writes
        /// the result to the standard output.</remarks>
        public void Print()
        {
            Console.WriteLine(ToString());
        }


        public override string ToString()
        {
            string list = string.Join("\n", Songs.Select(s => "   " + s.ToString()));
            return $"[Playlist] Id={Id}, Guid={UniqueId}, Name={Name}, SongsCount={Songs.Count}\n{list}";
        }
    }
}
