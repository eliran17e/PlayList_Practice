using Telhai.DotNet.Classes.HW1.EliranElisha.Models;
using Telhai.DotNet.Classes.HW1.EliranElisha.Menu;

namespace Telhai.DotNet.Classes.HW1.EliranElisha
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MusicLibrary library = new MusicLibrary();
            MusicMenu menu = new MusicMenu(library);

            menu.Start();
        }
    }
}
