class Bot : Player //Klass för boten, ärver från Player.
{
    private bool _botsFirstAction;

    public Bot(string name)
        : base(name) //Anropar basklassens konstruktor, så Shots och PlayersAction får sina startvärden automatiskt.
    {
        _botsFirstAction = false;
    }

    //Metod från basklassen som ändras och som avgör vad boten väljer; skjuta, ladda eller blocka.
    public override void ChooseAction()
    {
        bool isValidChoice = false;
        if (!_botsFirstAction) //När spelet börjar ska boten alltid välja att ladda.
        {
            Reload();
            PlayersAction = Action.Reload;
            _botsFirstAction = true;
            return;
        }

        while (!isValidChoice)
        {
            switch (RandomUtils.BotsChoice())
            {
                case 1:
                    Reload();
                    PlayersAction = Action.Reload;
                    isValidChoice = true;
                    break;
                case 2:
                    if (!Shoot())
                    {
                        isValidChoice = false;
                    }
                    else
                    {
                        PlayersAction = Action.Shoot;
                        isValidChoice = true;
                    }
                    break;
                case 3:
                    Block();
                    PlayersAction = Action.Block;
                    isValidChoice = true;
                    break;
                default:
                    isValidChoice = false;
                    continue;
            }
        }
    }
}
