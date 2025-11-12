using System.Reflection;

class Player //Klass med spelarna och de metoder de kan utföra.
{
    //Properties för spelarna (Värdena ska kunna hämtas i game, men bara sättas i basklassen och subklassen.)
    public string Name { get; protected set; }
    public int Shots { get; protected set; }
    public Action PlayersAction { get; protected set; }

    public Player(string name)
    {
        Name = name;
        Shots = 0;
        PlayersAction = Action.None;
    }

    //Metod som låter spelaren välja, skjuta, ladda eller blocka. ("virtual" för att kunna ändra metoden för boten).
    public virtual void ChooseAction()
    {
        bool isValidChoice = false;
        while (!isValidChoice)
        {
            Console.WriteLine(
                "What do you want to do? (Choose a number): \n1. Reload \n2. Shoot \n3. Block"
            );
            string? playersChoice = Console.ReadLine()?.Trim();
            Console.Clear();

            switch (playersChoice)
            {
                case "1":
                    Reload();
                    isValidChoice = true;
                    PlayersAction = Action.Reload;

                    break;
                case "2":
                    if (!Shoot())
                    {
                        Console.WriteLine(
                            $"{Name} doesn't have any shots left. Make another choice."
                        );
                        isValidChoice = false;
                    }
                    else
                    {
                        isValidChoice = true;
                        PlayersAction = Action.Shoot;
                    }
                    break;
                case "3":
                    Block();
                    PlayersAction = Action.Block;
                    isValidChoice = true;
                    break;
                default:
                    Console.WriteLine("You have made an invalid choice. Try again.");
                    isValidChoice = false;
                    continue;
            }
        }
    }

    // Metoderna shoot, reload, block bör vara protected så subklassen kommer åt dem, men de används inte direkt i game utand via public-metoden "ChooseAction".
    protected void Reload()
    {
        Console.WriteLine($"{Name} reloads.");
        Shots++;
    }

    protected bool Shoot()
    {
        if (Shots > 0)
        {
            Console.WriteLine($"{Name} shoots.");
            Shots--;
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void Block()
    {
        Console.WriteLine($"{Name} blocks.");
    }
}
