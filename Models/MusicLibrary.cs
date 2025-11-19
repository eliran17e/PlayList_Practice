using System;
using System.Collections.Generic;
using System.Linq;

namespace Telhai.DotNet.Classes.HW1.EliranElisha.Models
{
    /// <summary>
    /// MusicLibrary class representing a collection of songs and playlists.
    /// </summary>
    public class MusicLibrary
    {
        public List<Playlist> Playlists { get; private set; } = new List<Playlist>();
        public Dictionary<int, Song> Songs { get; private set; } = new Dictionary<int, Song>();
        /// <summary>
        /// Adds a song to the music library.
        /// </summary>
        /// <param name="s"></param>
        public void AddSong(Song s)
        {
            Songs[s.Id] = s;
        }
        /// <summary>
        /// Gets a song by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Song GetSong(int id)
        {
            return Songs.ContainsKey(id) ? Songs[id] : null;
        }
        /// <summary>
        /// Gets a list of song names in the format "Id: Title by Artist".
        /// </summary>
        /// <returns></returns>
        public List<string> GetSongNames()
        {
            return Songs.Values
                        .Select(s => $"{s.Id}: {s.Title} by {s.Artist}")
                        .ToList();
        }
        /// <summary>
        /// Adds a playlist to the music library.
        /// </summary>
        /// <param name="p"></param>
        public void AddPlaylist(Playlist p)
        {
            Playlists.Add(p);
        }
        /// <summary>
        /// Gets a playlist by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Playlist GetPlaylist(int id)
        {
            return Playlists.FirstOrDefault(x => x.Id == id);
        }
        /// <summary>
        /// Removes a playlist by its ID.
        /// </summary>
        /// <param name="id"></param>
        public void RemovePlaylist(int id)
        {
            var p = GetPlaylist(id);
            if (p != null)
                Playlists.Remove(p);
        }
    }
}
