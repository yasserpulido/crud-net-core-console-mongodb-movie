using System.Runtime.CompilerServices;
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

        /// <summary>
        /// Display the main menu with options to CRUD.
        /// </summary>
        /// <returns></returns>
        public async Task Menu()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("");
            _log.LogInformation("Menu");
            Console.WriteLine("");

            Console.WriteLine("Welcome to Movie.");
            Console.WriteLine("CRUD Administrator of Movie Information.");
            Console.WriteLine("[1].Create");
            Console.WriteLine("[2].Read");
            Console.WriteLine("[3].Update");
            Console.WriteLine("[4].Delete");
            Console.WriteLine("[5].Generate JSON");
            Console.WriteLine("[6].Exit");
            Console.WriteLine("");

            int optionSelected = ChooseMenu(1, 6);

            switch (optionSelected)
            {
                case 1:
                    await CreateMovie();
                    break;
                case 2:
                    await ReadMovie();
                    break;
                case 3:
                    await UpdateMovie();
                    break;
                case 4:
                    await DeleteMovie();
                    break;
                case 5:
                    await GenerateJson();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    await Menu();
                    break;
            }
        }

        /// <summary>
        /// Create a movie file.
        /// </summary>
        /// <returns></returns>
        private async Task CreateMovie()
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

            bool result = await _movieService.CreateMovie(movie);

            Console.WriteLine("");
            if (result)
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

            PressAnyKeyMessage();

            await Menu();
        }

        /// <summary>
        /// Read a movie(s) file(s).
        /// </summary>
        /// <returns></returns>
        private async Task ReadMovie()
        {
            Console.WriteLine("");
            _log.LogInformation("Read Method");
            Console.WriteLine("");

            Console.WriteLine("Do you want...?");
            Console.WriteLine("[1].Get a specific movie");
            Console.WriteLine("[2].Get all movie");
            Console.WriteLine("");

            int optionSelected = ChooseMenu(1, 2);

            switch (optionSelected)
            {
                case 1:
                    await GetMovie();
                    break;
                case 2:
                    await GetAllMovie();
                    break;
            }

            PressAnyKeyMessage();

            await Menu();
        }

        /// <summary>
        /// Get a specific movie file.
        /// </summary>
        /// <returns></returns>
        private async Task GetMovie()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("");
            Console.Write("Title: ");
            string title = CannotBeEmpty().ToUpper();
            Console.Write("Released: ");
            int released = CannotBeEmptyAndString();

            var movie = await _movieService.GetMovie(title, released);

            Console.WriteLine("");
            if (movie != null)
            {
                Console.WriteLine($" ({movie.Released}) {movie.Title}");
            }
            else
            {
                Console.WriteLine("There is no registered movie file.");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Get all movies files.
        /// </summary>
        /// <returns></returns>
        private async Task GetAllMovie()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            var movies = await _movieService.GetAllMovies();

            if (movies != null)
            {
                foreach (var x in movies.Select((value, index) => new { value, index }))
                {
                    Console.WriteLine($"{x.index + 1} - ({x.value.Released}) {x.value.Title}");
                }
            }
            else
            {
                Console.WriteLine("There is not movie file registered.");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Update a movie file data.
        /// </summary>
        /// <returns></returns>
        private async Task UpdateMovie()
        {
            Console.WriteLine("");
            _log.LogInformation("Update Method");
            Console.WriteLine("");

            Console.WriteLine("Type the following data to find the movie file:");
            Console.WriteLine("");

            Console.Write("Title: ");
            string title = CannotBeEmpty().ToUpper();
            Console.Write("Released: ");
            int released = CannotBeEmptyAndString();

            var movie = await _movieService.GetMovie(title, released);

            if (movie != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("");
                Console.WriteLine($"Title: {movie.Title}");
                Console.WriteLine($"Released: {movie.Released}");
                Console.WriteLine($"Runtime: {movie.Runtime}");
                Console.WriteLine($"Genre: {movie.Genre}");
                Console.WriteLine($"Plot: {movie.Plot}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("");

                Console.WriteLine("Do you want to edit it?");
                Console.WriteLine("[1].Yes");
                Console.WriteLine("[2].No");
                Console.WriteLine("");

                int optionSelected = ChooseMenu(1, 2);

                if (optionSelected == 1)
                {
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

                    bool result = await _movieService.UpdateMovie(movie);

                    if (result)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The movie was updated succesfully.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The movie was not updated.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }

                PressAnyKeyMessage();

                await Menu();
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("It was not found a movie with that information.");

                PressAnyKeyMessage();

                await Menu();
            }
        }

        /// <summary>
        /// Delete a movie file.
        /// </summary>
        /// <returns></returns>
        private async Task DeleteMovie()
        {
            Console.WriteLine("");
            _log.LogInformation("Delete Method");
            Console.WriteLine("");

            Console.WriteLine("Type the following data to delete the movie file:");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Title: ");
            string title = CannotBeEmpty().ToUpper();
            Console.Write("Released: ");
            int released = CannotBeEmptyAndString();
            Console.ForegroundColor = ConsoleColor.Gray;

            var movie = await _movieService.GetMovie(title, released);

            if (movie != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("");
                Console.WriteLine($"Title: {movie.Title}");
                Console.WriteLine($"Released: {movie.Released}");
                Console.WriteLine($"Runtime: {movie.Runtime}");
                Console.WriteLine($"Genre: {movie.Genre}");
                Console.WriteLine($"Plot: {movie.Plot}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("");

                Console.WriteLine("Do you want to delete it?");
                Console.WriteLine("[1].Yes");
                Console.WriteLine("[2].No");
                Console.WriteLine("");

                int optionSelected = ChooseMenu(1, 2);

                if (optionSelected == 1)
                {
                    bool result = await _movieService.DeleteMovie(movie);

                    if (result)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The movie was deleted succesfully.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The movie was not deleted.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }

                PressAnyKeyMessage();

                await Menu();
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("It was not found a movie with that information.");

                PressAnyKeyMessage();

                await Menu();
            }
        }

        /// <summary>
        /// Generate a json file of movies.
        /// </summary>
        /// <returns></returns>
        private async Task GenerateJson()
        {
            Console.WriteLine("");
            _log.LogInformation("Generate Json File Method");
            Console.WriteLine("");

            Console.WriteLine("Do you want to generate a json file?");
            Console.WriteLine("[1].Yes");
            Console.WriteLine("[2].No");
            Console.WriteLine("");

            int optionSelected = ChooseMenu(1, 2);

            if (optionSelected == 1)
            {
                bool result = await _movieService.GenerateJson();

                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The movie json file was generated succesfully.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The movie json file was not generate.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            PressAnyKeyMessage();

            await Menu();
        }

        /// <summary>
        /// Allow to select an option menu.
        /// </summary>
        /// <param name="min">Minimum</param>
        /// <param name="max">Maximum</param>
        /// <returns>An option selected</returns>
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

        /// <summary>
        /// Allow to not has a data empty.
        /// </summary>
        /// <returns>A value not empty</returns>
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

        /// <summary>
        /// Allow to not has a data empty and as a string type.
        /// </summary>
        /// <returns>Number typed</returns>
        private int CannotBeEmptyAndString()
        {
            int value = 0;

            while (value == 0 && !int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("You must type a year: ");
            }

            return value;
        }

        /// <summary>
        /// Prevents the app progress without press any key.
        /// </summary>
        private void PressAnyKeyMessage()
        {
            Console.WriteLine("");
            Console.Write("Press any key to go to menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
