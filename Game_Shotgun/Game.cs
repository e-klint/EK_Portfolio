using System.Formats.Asn1;

class Game //Klass som ansvarar för hela "spelflödet".
{
    private bool isShowingEndMenu = false;
    private bool isRunningGame = true;

    //Fälten kommer tilldelas ett värde i SetPlayersName()
    private Player _player1 = null!;
    private Bot _bot = null!;

    public void RunGame()
    {
        while (isRunningGame)
        {
            SetUpGame();
            RunGameLoop();
            ShowEndMenu();
        }
    }

    // Metoder för att sätta upp spelet
    private void SetUpGame()
    {
        Console.Clear();
        Console.WriteLine("===Welcome to Shotgun!===\n");
        SetPlayersName();
        Console.WriteLine($"Hello {_player1.Name}. I'm {_bot.Name}. Let's play!\n");
    }

    private void SetPlayersName()
    {
        string? inputName; //Felhantering som förhindrar null-värde kommer i loopen nedan.

        do
        {
            Console.WriteLine("Enter your name: ");
            inputName = Console.ReadLine()?.Trim();
            Console.Clear();

            if (string.IsNullOrWhiteSpace(inputName))
            {
                Console.WriteLine("You didn't enter a valid name. Please try again. ");
            }
        } while (string.IsNullOrWhiteSpace(inputName));

        _player1 = new Player(inputName);
        _bot = new Bot("SHOT-BOT");

        Console.Clear();
    }

    //Pågående spelflöde
    private void RunGameLoop()
    {
        while (isRunningGame)
        {
            _player1.ChooseAction();
            _bot.ChooseAction();
            ControlResult();
        }
    }

    private void ControlResult()
    {
        string p1Wins = $"{_player1.Name} wins.";
        string p1ShotGun = $"{_player1.Name} has three shots. Shotgun!";
        string botWins = $"{_bot.Name} wins.";
        string botShotGun = $"{_bot.Name} has three shots. Shotgun!";

        if (_player1.Shots == 3 && _bot.Shots == 3)
        {
            Console.WriteLine("The game was drawn.");
            isRunningGame = false;
        }
        else if (_player1.Shots == 3)
        {
            Console.WriteLine(p1ShotGun + " " + p1Wins);
            isRunningGame = false;
            isShowingEndMenu = true;
        }
        else if (_bot.Shots == 3)
        {
            Console.WriteLine(botShotGun + " " + botWins);
            isRunningGame = false;
            isShowingEndMenu = true;
        }
        else if (_player1.PlayersAction == Action.Shoot && _bot.PlayersAction == Action.Reload)
        {
            Console.WriteLine(p1Wins);
            isRunningGame = false;
            isShowingEndMenu = true;
        }
        else if (_player1.PlayersAction == Action.Reload && _bot.PlayersAction == Action.Shoot)
        {
            Console.WriteLine(botWins);
            isRunningGame = false;
            isShowingEndMenu = true;
        }

        Console.ReadKey();
        Console.Clear();
    }

    // Meny för att avsluta eller spela igen.
    private void ShowEndMenu()
    {
        while (isShowingEndMenu)
        {
            Console.WriteLine("===Game Meny===\n");
            Console.WriteLine(
                "What do you want to do? (Choose a number.) \n1. Reset game \n2. End game"
            );
            string playersChoice = Console.ReadLine()?.Trim() ?? string.Empty; //Om användaren ej skriver in något blir det en tom sträng. Hanteras i switchen.
            switch (playersChoice)
            {
                case "1":
                    isShowingEndMenu = false;
                    isRunningGame = true;
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("The game has been closed.");
                    isRunningGame = false;
                    isShowingEndMenu = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("You have made an invalid choice. Try again.");
                    continue;
            }
        }
    }
}
