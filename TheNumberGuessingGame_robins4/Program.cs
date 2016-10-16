using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingActivity_TheNumberGuessingGameRobins4
{
    class Program
    {
        /// Declare game variables

        private const int MAX_NUMBER_OF_PLAYER_GUESSES = 4;
        private const int MAX_NUMBER_TO_GUESS = 10;
        private static int playersGuess;
        private static int numberToGuess;
        private static int roundNumber;
        private static int numberOfWins;
        private static int numberOfCurrentPlayerGuess;
        private static int[] numbersPlayerHasGuessed = new int[MAX_NUMBER_OF_PLAYER_GUESSES];
        private static bool playingGame;
        private static bool playingRound;
        private static bool numberGuessedCorrectly;

        /// <summary>
        /// Application's Main method
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Initialize new game to play

            InitializeGame();

            // Display the Welcome Screen with application Quit option

            DisplayWelcomeScreen();

            // Display the game rules

            DisplayRulesScreen();

            // Game loop for playing game

            while (playingGame)
            {
                // Initialize new game round

                InitializeRound();

                // Round loop

                while (playingRound)
                {
                    // Display the player guess screen and return the player's guess

                    playersGuess = DisplayGetPlayersGuessScreen();

                    // Evaluate the player's guess and provide the player feedback

                    DisplayPlayerGuessFeedback();

                    // Update round variables, process the results and provide player feedback

                    UpdateAndDisplayRoundStatus();
                }

                // Round complete, display player stats

                DisplayPlayerStats();
            }

            //Prompt closing screen

            DisplayClosingScreen();
        }

        /// <summary>
        /// Initialize all game variables
        /// </summary>
        public static void InitializeGame()
        {
            numberToGuess = 0;
            numberOfWins = 0;
            playingGame = true;
            roundNumber = 0;
        }

        /// <summary>
        /// Initialize all round variables and get number to guess
        /// </summary>
        public static void InitializeRound()
        {
            numberOfCurrentPlayerGuess = 1;
            numberGuessedCorrectly = false;
            playingRound = true;
            ++roundNumber;
            for (int index = 0; index < 4; ++index)
                numbersPlayerHasGuessed[index] = 0;
            roundNumber = roundNumber++;
            numberToGuess = GetNumberToGuess();
        }

        /// <summary>
        /// Display the opening screen and prompt to Continue/Quit
        /// </summary>
        public static void DisplayWelcomeScreen()
        {
            ConsoleKeyInfo playerKeyResponse;
            Console.Clear();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\n");
            Console.WriteLine("     Guess the Number Game");
            Console.WriteLine("\n\n");
            Console.WriteLine("      Press the (Enter) key to Play.");
            Console.WriteLine("      Press the (Esc) key to Quit.");

            playerKeyResponse = Console.ReadKey();

            // Console window closes immediately without displaying the closing screen.

            if (playerKeyResponse.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Display the game rules
        /// </summary>
        public static void DisplayRulesScreen()
        {
            Console.Clear();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("                Guess the Number Game");
            Console.WriteLine("\n\n");
            Console.WriteLine("   We will randomly select a number between 1 and 10.");
            Console.WriteLine("   You will have four attempts to guess the number. After each");
            Console.WriteLine("   guess the computer will indicate if you have guessed correctly");
            Console.WriteLine("   or whether your guess is high or low.");
            Console.WriteLine("\n\n");
            Console.WriteLine("\n\n");
            Console.WriteLine("   Press the any key to continue.");
            Console.ReadLine();
        }

        /// <summary>
        /// Prompt for and return the player's guess
        /// </summary>
        /// <returns></returns>
        public static int DisplayGetPlayersGuessScreen()
        {
            Console.Clear();
            bool validResponse = false;

            // Validation for player's guess

            while (!validResponse)
            {
                // Clear screen, header set

                DisplayReset();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("   Enter a number between 1 and 10");
                if (Int32.TryParse(Console.ReadLine(), out playersGuess))
                {
                    if (playersGuess >= 1 && playersGuess <= 10)
                    {
                        validResponse = true;
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine();
                        Console.WriteLine("   You did not enter a number within the proper range.");
                        Console.WriteLine("   Guess again.");
                        DisplayContinueQuitPrompt();
                    }
                }
                else
                {
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine();
                    Console.WriteLine("   You did not enter a valid number.");
                    Console.WriteLine("   Guess again.");
                    DisplayContinueQuitPrompt();
                }
            }
            return playersGuess;
        }

        /// <summary>
        /// Evaluate the player's guess and provide the player feedback
        /// </summary>
        public static void DisplayPlayerGuessFeedback()
        {
            if (playersGuess == numberToGuess)
            {
                numberGuessedCorrectly = true;
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("   Good Job! you guessed correctly with " + playersGuess + "!");
            }
            else if (playersGuess > numberToGuess)
                Console.WriteLine("   Sorry!, " + playersGuess + " is too high!");
            else
                Console.WriteLine("   Sorry!, " + playersGuess + " is too low!");
        }

        /// <summary>
        /// Update round status, process the results and provide player feedback
        /// </summary>
        public static void UpdateAndDisplayRoundStatus()
        {
            // Player guessed correctly and Player stats are displayed

            if (numberGuessedCorrectly)
            {
                ++numberOfWins;
                playingRound = false;
                DisplayPlayerStats();
            }
            // Player guessed incorrectly and has more guesses left

            else if (numberOfCurrentPlayerGuess < MAX_NUMBER_OF_PLAYER_GUESSES)
            {
                numbersPlayerHasGuessed[numberOfCurrentPlayerGuess - 1] = playersGuess;
                DisplayNumbersGuessed();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("   Try Again?");
            }
            // Player guessed incorrectly and has no more guesses left

            else
            {
                playingRound = false;
                Console.WriteLine("   Sorry!, you have used all of your guesses.");
                DisplayPlayerStats();
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Display the player's current stats and prompt to Continue/Quit
        /// </summary>
        public static void DisplayPlayerStats()
        {
            int losses = roundNumber - numberOfWins;
            int percentofwins = (int)Math.Round(numberOfWins / roundNumber * 100.0);
            DisplayReset();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("   You current stats:\n");
            Console.WriteLine("   Number of Rounds Played : " + roundNumber);
            Console.WriteLine("   Number of Wins          : " + numberOfWins);
            Console.WriteLine("   Number of Losses        : " + losses);
            Console.WriteLine("   Percentage of Wins      : " + percentofwins + "%");
            Console.WriteLine("\n\n");
            Console.WriteLine("   Press the (Enter) key to play another round.");
            Console.WriteLine("   Press the (Esc) key to Quit.");
            if (Console.ReadKey().Key != ConsoleKey.Escape)
                return;
            playingGame = false;
        }

        /// <summary>
        /// Display the numbers already guessed
        /// </summary>
        public static void DisplayNumbersGuessed()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("   The numbers you have currently guessed include:");
            Console.WriteLine();
            for (int index = 0; index < 4; ++index)
            {
                if ((ushort)numbersPlayerHasGuessed[index] > 0)
                    Console.WriteLine("   Guess :    {1}", index + 1, numbersPlayerHasGuessed[index]);
            }
            Console.WriteLine();

        }

        /// <summary>
        /// Display a closing screen
        /// </summary>
        public static void DisplayClosingScreen()
        {
            // Clear screen and set heading

            DisplayReset();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("   Thank you for playing Guess the Number Game.\n");
            Console.ReadLine();
            Environment.Exit(0);
        }

        /// <summary>
        /// Generate and return a random number 
        /// </summary>
        /// <returns>random integer in desired range</returns>
        public static int GetNumberToGuess()
        {
            Random round = new Random();
            int numberToGuess = round.Next(1, 10);
            return numberToGuess;

        }

        /// <summary>
        /// reset display to default size and colors including the header
        /// </summary>
        public static void DisplayReset()
        {
            Console.Clear();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.SetCursorPosition(15, 1);
            Console.WriteLine("   Guess the Number Game   ");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// display the Continue/Quit screen
        /// </summary>
        public static void DisplayContinueQuitPrompt()
        {
            Console.CursorVisible = false;
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine("   Press any key to continue or press the ESC key to quit.");
            Console.WriteLine();
            ConsoleKeyInfo response = Console.ReadKey();

            // Set flag if player chooses to quit

            if (response.Key == ConsoleKey.Escape)
            {
                DisplayClosingScreen();
            }

            Console.CursorVisible = true;
        }
    }
}
