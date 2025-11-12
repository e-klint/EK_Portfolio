using static System.Runtime.InteropServices.JavaScript.JSType;

class FileHandler //Sköter sparning och läsning till/från textfil.
{
    //Filväg till kontaktlistan
    string contactsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\ContactList.txt");


    // Sparar listan av kontakter till fil
    public void SaveContactsToFile(List<Contact> contacts)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(contactsFilePath))
            {
                foreach (Contact c in contacts)
                    writer.WriteLine(c.ToString());
                Console.WriteLine(InfoMessage.InfoContactsSavedtoList);
            }
        }
        catch (Exception ex) //Exceptionklassen i .NET- den hittar fel och skriver felmeddelanden.
        {
            Console.WriteLine(ErrorMessage.ErrorCouldNotWriteToFile);
            LogErrorToFile(ex);
        }
    }

    //Läser kontaktlistan från fil och returnerar den
    public List<Contact> ReadContactsFromFile()
    {
        List<Contact> fileContacts = new List<Contact>();

        try
        {
            using (StreamReader reader = new StreamReader(contactsFilePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] cDetails = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    Contact contact = new Contact(
                        cDetails[0],
                        cDetails[1],
                        cDetails[2],
                        cDetails[3],
                        cDetails[4],
                        cDetails[5],
                        cDetails[6]
                    );
                    fileContacts.Add(contact);
                }
                return fileContacts;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ErrorMessage.ErrorCouldNotReadFile);
            LogErrorToFile(ex);
            return fileContacts;
        }
    }

    //Loggar fel till separat fil
    private void LogErrorToFile(Exception ex)
    {
        string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\ErrorLog.txt");
        
        try
        {
            using (StreamWriter writer = new StreamWriter(logPath, append: true)) 
            {
                writer.WriteLine($"[{DateTime.Now}] Error: {ex.Message}");
                writer.WriteLine($"Type: {ex.GetType()}");
                writer.WriteLine($"StackTrace: {ex.StackTrace}");
                writer.WriteLine("-------------------------------------------------\n");
            }
        }
        catch
        {
            // Om loggningen misslyckas, skriv till konsolen som sista utväg
            Console.WriteLine(ErrorMessage.ErrorFailedToLogError);
        }
    }
}
