using System;
using System.Linq;
using Telhai.DotNet.Classes.HW1.EliranElisha.Models;

namespace Telhai.DotNet.Classes.HW1.EliranElisha.Menu
{
    /// <summary>
    /// MusicMenu class providing a console-based menu for managing a music library.
    /// </summary>
    public class MusicMenu
    {
        private readonly MusicLibrary _library;

        public MusicMenu(MusicLibrary library)
        {
            _library = library;
        }

        // COLOR HELPERS
        private void PrintError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        private void PrintSuccess(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        private void PrintTitle(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        // INPUT HELPERS
        private int ReadInt(string message)
        {
            int number;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out number))
                    return number;
                PrintError("Invalid number. Try again.");
            }
        }

        private double ReadDouble(string message)
        {
            double number;
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out number))
                    return number;
                PrintError("Invalid number. Try again.");
            }
        }

        private string ReadNonEmpty(string message, int minLen = 1)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && input.Length >= minLen)
                    return input;

                PrintError($"Input must contain at least {minLen} characters.");
            }
        }

        private Genre ReadGenre()
        {
            PrintTitle("Select Genre:");
            foreach (var g in Enum.GetValues(typeof(Genre)))
                Console.WriteLine($"{(int)g} - {g}");

            while (true)
            {
                Console.Write("Enter genre number: ");
                if (int.TryParse(Console.ReadLine(), out int g) &&
                    Enum.IsDefined(typeof(Genre), g))
                {
                    return (Genre)g;
                }

                PrintError("Invalid genre selection.");
            }
        }

        // MENU ACTIONS
        /// <summary>
        /// Creates a new playlist by prompting the user for a name and adding it to the music library.
        /// </summary>
        public void CreatePlaylist()
        {
            Console.Clear();
            PrintTitle("=== Create New Playlist ===");
            string name = ReadNonEmpty("Playlist name: ", 3);

            try
            {
                var p = new Playlist(name);
                _library.AddPlaylist(p);

                PrintSuccess("Playlist created successfully!");
                PrintSuccess($"Playlist ID: {p.Id}");
                PrintSuccess($"Name: {p.Name}");
            }
            catch (Exception ex)
            {
                PrintError("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a new song by prompting the user for song details and adding it to the music library.
        /// </summary>
        public void CreateSong()
        {
            Console.Clear();
            PrintTitle("=== Create New Song ===");

            string title = ReadNonEmpty("Title: ", 2);
            string artist = ReadNonEmpty("Artist: ", 1);
            int year = ReadInt("Year (1900–2025): ");
            double duration = ReadDouble("Duration (minutes): ");
            Genre genre = ReadGenre();

            try
            {
                var s = new Song(title, artist, year, duration, genre);
                PrintSuccess("Song created successfully!");
                PrintSuccess(s.ToString());
                _library.AddSong(s);

            }
            catch (Exception ex)
            {
                PrintError("Error: " + ex.Message);
            }
            

        }
        /// <summary>
        /// Adds an existing song to a selected playlist in the music library.
        /// </summary>
        public void AddSongToPlaylist()
        {
            Console.Clear();
            PrintTitle("=== Add Existing Song to Playlist ===");

            // 🔥 Show playlists first
            if (_library.Playlists.Count == 0)
            {
                PrintError("No playlists available. Create a playlist first.");
                return;
            }

            PrintTitle("=== Available Playlists ===");
            foreach (var p in _library.Playlists)
                Console.WriteLine($"ID: {p.Id} - {p.Name}");

            Console.WriteLine();
            int playlistId = ReadInt("Enter playlist ID: ");
            var playlist = _library.GetPlaylist(playlistId);

            if (playlist == null)
            {
                PrintError("Playlist not found.");
                return;
            }

            // 🔥 Now check that there are songs
            if (_library.Songs.Count == 0)
            {
                PrintError("No songs available. Create songs first.");
                return;
            }

            PrintTitle("=== All Available Songs ===");
            foreach (var s in _library.Songs.Values)
                Console.WriteLine($"{s.Id} - {s.Title} by {s.Artist}");

            int songId = ReadInt("Enter song ID to add: ");

            var song = _library.GetSong(songId);
            if (song == null)
            {
                PrintError("Song not found.");
                return;
            }

            playlist.AddSong(song);
            PrintSuccess("Song added to playlist successfully!");
        }


        /// <summary>
        /// Shows all songs in a selected playlist.
        /// </summary>
        public void ShowSongsInPlaylist()
        {
            Console.Clear();
            PrintTitle("=== Show Songs in Playlist ===");

            int id = ReadInt("Enter playlist ID: ");
            var playlist = _library.GetPlaylist(id);

            if (playlist == null)
            {
                PrintError("Playlist not found.");
                return;
            }

            PrintTitle($"Playlist: {playlist.Name}");
            if (playlist.Songs.Count == 0)
            {
                PrintError("No songs in this playlist.");
                return;
            }

            playlist.Print();
        }
        /// <summary>
        /// Removes a song from a selected playlist in the music library.
        /// </summary>
        public void RemoveSongFromPlaylist()
        {
            Console.Clear();
            PrintTitle("=== Remove Song from Playlist ===");

            int playlistId = ReadInt("Playlist ID: ");
            var playlist = _library.GetPlaylist(playlistId);

            if (playlist == null)
            {
                PrintError("Playlist not found.");
                return;
            }

            int songId = ReadInt("Song ID to remove: ");

            var song = playlist.FindSong(songId);
            if (song == null)
            {
                PrintError("Song not found.");
                return;
            }

            playlist.RemoveSong(songId);
            PrintSuccess("Song removed.");
        }
        /// <summary>
        /// Shows all playlists in the music library.
        /// </summary>
        public void ShowAllPlaylists()
        {
            Console.Clear();
            PrintTitle("=== All Playlists ===");

            if (_library.Playlists.Count == 0)
            {
                PrintError("No playlists in library.");
                return;
            }

            foreach (var p in _library.Playlists)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"ID: {p.Id} | Name: {p.Name} | Songs: {p.Songs.Count}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Shows songs of a specific genre in a selected playlist.
        /// </summary>
        public void ShowSongsByGenre()
        {
            Console.Clear();
            PrintTitle("=== Show Songs by Genre ===");

            int playlistId = ReadInt("Playlist ID: ");
            var playlist = _library.GetPlaylist(playlistId);

            if (playlist == null)
            {
                PrintError("Playlist not found.");
                return;
            }

            Genre g = ReadGenre();
            var filtered = playlist.Songs.Where(s => s.Genre == g).ToList();

            if (filtered.Count == 0)
            {
                PrintError("No songs with this genre.");
                return;
            }

            PrintTitle($"Songs in {playlist.Name} with genre {g}:");
            foreach (var s in filtered)
                Console.WriteLine(s);
        }

        /// <summary>
        /// Shuffles the songs in a selected playlist.
        /// </summary>
        public void ShufflePlaylist()
        {
            Console.Clear();
            PrintTitle("=== Shuffle Playlist ===");

            if (_library.Playlists.Count == 0)
            {
                PrintError("No playlists available.");
                return;
            }

            // Show playlists
            foreach (var p in _library.Playlists)
                Console.WriteLine($"ID: {p.Id} - {p.Name}");

            int id = ReadInt("\nEnter playlist ID: ");
            var playlist = _library.GetPlaylist(id);

            if (playlist == null)
            {
                PrintError("Playlist not found.");
                return;
            }

            if (playlist.Songs.Count == 0)
            {
                PrintError("Playlist is empty.");
                return;
            }

            playlist.Shuffle();
            PrintSuccess("Playlist shuffled successfully!");

            Console.WriteLine("\nNew order:");
            foreach (var s in playlist.Songs)
                Console.WriteLine($"{s.Id} - {s.Title}");
        }

        /// <summary>
        /// Deletes a playlist from the music library.
        /// </summary>
        public void DeletePlaylist()
        {
            Console.Clear();
            PrintTitle("=== Delete Playlist ===");

            int id = ReadInt("Playlist ID: ");
            var playlist = _library.GetPlaylist(id);

            if (playlist == null)
            {
                PrintError("Playlist not found.");
                return;
            }

            _library.RemovePlaylist(id);
            PrintSuccess("Playlist removed successfully.");
        }

        /// <summary>
        /// Searches for songs by a specific artist in the music library.
        /// </summary>
        public void SearchSongsByArtist()
        {
            Console.Clear();
            PrintTitle("=== Search Songs by Artist ===");

            if (_library.Songs.Count == 0)
            {
                PrintError("No songs in the library.");
                return;
            }

            string artist = ReadNonEmpty("Enter artist name: ", 1).ToLower();

            var matches = _library.Songs.Values
                .Where(s => s.Artist.ToLower().Contains(artist))
                .ToList();

            if (matches.Count == 0)
            {
                PrintError("No songs found for this artist.");
                return;
            }

            PrintSuccess($"Found {matches.Count} song(s):");
            foreach (var s in matches)
                Console.WriteLine($"{s.Id} - {s.Title} ({s.Artist})");
        }


        // MAIN MENU LOOP
        public void Start()
        {
            while (true)
            {
                Console.Clear();
                PrintTitle("=== MUSIC LIBRARY MENU — By Eliran Elisha ===");
                Console.WriteLine("1. Create new playlist");
                Console.WriteLine("2. Create new song");
                Console.WriteLine("3. Add song to playlist");
                Console.WriteLine("4. Show songs in playlist");
                Console.WriteLine("5. Remove song from playlist");
                Console.WriteLine("6. Show all playlists");
                Console.WriteLine("7. Show songs by genre in playlist");
                Console.WriteLine("8. Delete playlist");
                Console.WriteLine("9. Search songs by artist");
                Console.WriteLine("10. Shuffle playlist");
                Console.WriteLine("0. Exit");
                Console.WriteLine();

                int choice = ReadInt("Select option: ");
                switch (choice)
                {
                    case 1: CreatePlaylist(); break;
                    case 2: CreateSong(); break;
                    case 3: AddSongToPlaylist(); break;
                    case 4: ShowSongsInPlaylist(); break;
                    case 5: RemoveSongFromPlaylist(); break;
                    case 6: ShowAllPlaylists(); break;
                    case 7: ShowSongsByGenre(); break;
                    case 8: DeletePlaylist(); break;
                    case 9: SearchSongsByArtist(); break;
                    case 10: ShufflePlaylist(); break;
                    case 0: return;
                    default: PrintError("Invalid option."); break;
                }

                Console.WriteLine();
                Console.WriteLine("Press ENTER to continue...");
                Console.ReadLine();
            }
        }
    }
}
