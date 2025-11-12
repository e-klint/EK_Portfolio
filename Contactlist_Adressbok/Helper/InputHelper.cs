public static class InputHelper //Små hjälpfunktioner för att hantera input
{
    public static string GetValidInput(string prompt, string errormessage) 
    {
        Console.WriteLine(prompt);
        string? input = Console.ReadLine()?.Trim(); //Ogiltig input hanteras i loopen. 

        while (string.IsNullOrWhiteSpace(input)) 
        {
            Console.Clear();
            Console.WriteLine(errormessage);
            Console.WriteLine(prompt);
            input = Console.ReadLine()?.Trim();
        }
        return input;
    }

    public static string GetOptionalInput(string prompt, string currentValue)
    {
        Console.Write($"{prompt} ({currentValue}): ");
        string input = Console.ReadLine() ?? "";

        // Om användaren bara trycker Enter — behåll gamla värdet
        return string.IsNullOrWhiteSpace(input) ? currentValue : input;
    }


    public static (string chosenName, string chosenSurname) GetFullNameFromUser() //Tuple för att skicka tillbaka två värden.
    {
        string chosenName = GetValidInput(PromptMessage.PromptName, ErrorMessage.ErrorInvalidName);
        string chosenSurname = GetValidInput(
            PromptMessage.PromptSurname,
            ErrorMessage.ErrorInvalidSurname
        );
        return (chosenName, chosenSurname);
    }
}
