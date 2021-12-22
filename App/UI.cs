using Core.Entities;
using Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace App
{
    internal class UI
    {
        private readonly ILogger<UI> _log;
        private readonly IConfiguration _config;
        private readonly MovieService _movieService;

        public UI(ILogger<UI> log, IConfiguration config, MovieService movieService)
        {
            _log = log;
            _config = config;
            _movieService = movieService;
        }

        public void Menu()
        {
            Console.WriteLine("Welcome to Movie");
            Console.WriteLine("CRUD Administrator of Movie Information.");
            Console.WriteLine("[1].Create");
            Console.WriteLine("[2].Read");
            Console.WriteLine("[3].Update");
            Console.WriteLine("[4].Delete");
            Console.WriteLine("[5].Exit");
            Console.WriteLine("");
            int optionSelected = ChooseMenu(1, 5);
            switch (optionSelected)
            {
                case 1:
                    CreateMovie();
                    break;
            }
        }

        private async void CreateMovie()
        {
            Console.WriteLine("");
            _log.LogInformation("Create Movie");
            Console.WriteLine("");

            Console.WriteLine("Set the following data to create a new movie file:");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Yellow;

            var movie = new Movie();

            Console.Write("Title: ");
            movie.Title = CannotBeEmpty().ToUpper();
            Console.Write("Released: ");
            movie.Released = CannotBeEmptyAndString();
            Console.Write("Runtime: ");
            movie.Runtime = CannotBeEmptyAndString();
            Console.Write("Genre: ");
            movie.Genre = CannotBeEmpty();
            Console.Write("Director: ");
            movie.Director = CannotBeEmpty();
            Console.Write("Plot: ");
            movie.Plot = CannotBeEmpty();

            await _movieService.CreateMovie(movie);

            Console.WriteLine("");
            if (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The movie was created succesfully.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The movie was not created succesfully.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine("");
        }

        private int ChooseMenu(int min, int max)
        {
            int optionMenu = -1;
            bool validateMenu = true;
            while (validateMenu)
            {
                Console.Write("Type an option: ");
                while (!int.TryParse(Console.ReadLine(), out optionMenu))
                {
                    Console.Write("This is not a valid input. Please enter an integer value: ");
                }
                if (optionMenu >= min && optionMenu <= max)
                {
                    validateMenu = false;
                }
                else
                {
                    Console.Write($"Please enter an integer value between {min}-{max}: ");
                    validateMenu = true;
                }
            }
            return optionMenu;
        }

        private string CannotBeEmpty()
        {
            string? value = string.Empty;
            while (string.IsNullOrEmpty(value))
            {
                value = Console.ReadLine();
                if (string.IsNullOrEmpty(value))
                {
                    Console.Write("You must type something: ");
                }
            }

            return value;
        }

        private int CannotBeEmptyAndString()
        {
            int value = 0;
            while (value == 0 && !int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("You must type a year: ");
            }
            return value;
        }
    }
}
