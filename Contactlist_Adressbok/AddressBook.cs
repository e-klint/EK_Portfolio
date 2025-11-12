using System.Net;
using System.Numerics;
using System.Reflection;

class AddressBook //Ansvarar för listan av kontakter och logik för att hantera dem.
{
    List<Contact> allContacts = new List<Contact>();
    FileHandler fileHandler = new FileHandler();

    public void AddContact()
    {
        Console.Clear();
        Console.WriteLine(InfoMessage.InfoTitleAddContact);
        Console.WriteLine(PromptMessage.PromptDetails);

        string name = InputHelper.GetValidInput(PromptMessage.PromptName, ErrorMessage.ErrorInvalidName);
        string surname = InputHelper.GetValidInput(
            PromptMessage.PromptSurname,
            ErrorMessage.ErrorInvalidSurname
        );
        string address = InputHelper.GetValidInput(
            PromptMessage.PromptAddress,
            ErrorMessage.ErrorInvalidAddress
        );
        string postcode = InputHelper.GetValidInput(
            PromptMessage.PromptPostcode,
            ErrorMessage.ErrorInvalidPostcode
        );
        string city = InputHelper.GetValidInput(PromptMessage.PromptCity, ErrorMessage.ErrorInvalidCity);
        string phone = InputHelper.GetValidInput(
            PromptMessage.PromptPhone,
            ErrorMessage.ErrorInvalidPhone
        );
        string email = InputHelper.GetValidInput(
            PromptMessage.PromptEmail,
            ErrorMessage.ErrorInvalidEmail
        );

        Console.Clear();
        allContacts = fileHandler.ReadContactsFromFile();
        Contact contact = new Contact(name, surname, address, postcode, city, phone, email);
        allContacts.Add(contact);
        fileHandler.SaveContactsToFile(allContacts);

        Console.WriteLine(InfoMessage.InfoContactAdded + contact.ToDisplay()); 

        Console.WriteLine(PromptMessage.PromptEnter);
        Console.ReadKey();
    }

    public void UpdateContact()
    {
        Console.Clear();
        Console.WriteLine(InfoMessage.InfoTitleUpdateContact);
        Console.WriteLine(PromptMessage.PromptDetailsChooseContact);
        var (name, surname) = InputHelper.GetFullNameFromUser();
        List<Contact> chosenContacts = SearchContactByFullName(name, surname);

        ShowContactsResult(chosenContacts);
        Contact contactToUpdate = chosenContacts[0];

        if (chosenContacts.Count() > 1) //Kontrollerar att rätt kontakt väljs om det finns två med samma för-och efternamn.
        {
            contactToUpdate = ChooseContactFromList(chosenContacts);
            Console.Clear();
            Console.WriteLine(InfoMessage.InfoChosenContact + contactToUpdate.ToDisplay()); 
        }

        //Prompt fyll i eller tryck enter
        contactToUpdate.Name = InputHelper.GetOptionalInput(
            PromptMessage.PromptName,
            contactToUpdate.Name
        );
        contactToUpdate.Surname = InputHelper.GetOptionalInput(
            PromptMessage.PromptSurname,
            contactToUpdate.Surname
        );
        contactToUpdate.Address = InputHelper.GetOptionalInput(
            PromptMessage.PromptAddress,
            contactToUpdate.Address
        );
        contactToUpdate.Postcode = InputHelper.GetOptionalInput(
            PromptMessage.PromptPostcode,
            contactToUpdate.Postcode
        );
        contactToUpdate.City = InputHelper.GetOptionalInput(
            PromptMessage.PromptCity,
            contactToUpdate.City
        );
        contactToUpdate.Phone = InputHelper.GetOptionalInput(
            PromptMessage.PromptPhone,
            contactToUpdate.Phone
        );
        contactToUpdate.Email = InputHelper.GetOptionalInput(
            PromptMessage.PromptEmail,
            contactToUpdate.Email
        );

        Console.Clear();
        fileHandler.SaveContactsToFile(allContacts);
        Console.WriteLine(InfoMessage.InfoContactUpdated + contactToUpdate.ToDisplay());

        Console.WriteLine(PromptMessage.PromptEnter);
        Console.ReadKey();
    }

    public void RemoveContact()
    {  
        Console.Clear();
        Console.WriteLine(InfoMessage.InfoTitleRemoveContact);
        Console.WriteLine(PromptMessage.PromptDetailsChooseContact);

        allContacts = fileHandler.ReadContactsFromFile();

        var (name, surname) = InputHelper.GetFullNameFromUser();
        List<Contact> chosenContacts = SearchContactByFullName(name, surname);

        ShowContactsResult(chosenContacts);
        Contact contactToRemove = chosenContacts[0];

        if (chosenContacts.Count() > 1) //Kontrollerar att rätt kontakt väljs om det finns två med samma för-och efternamn.
        {
            contactToRemove = ChooseContactFromList(chosenContacts);
            Console.Clear();
            Console.WriteLine(InfoMessage.InfoChosenContact + contactToRemove.ToDisplay());
        }

        allContacts.Remove(contactToRemove);
        fileHandler.SaveContactsToFile(allContacts); //Behöver ligga innan console.clear, så användaren inte blir "förvirrad" över att kontakterna sparats till filen igen.

        Console.Clear();
        Console.WriteLine(InfoMessage.InfoContactRemoved + contactToRemove.ToDisplay());

        Console.WriteLine(PromptMessage.PromptEnter);
        Console.ReadKey();
    }

    public void SearchContact()
    {
        Console.Clear();
        Console.WriteLine(InfoMessage.InfoTitleSearchContact);

        string searchTerm = InputHelper.GetValidInput(PromptMessage.PromptSearchContact, ErrorMessage.ErrorInvalidSearchTerm);

        allContacts = fileHandler.ReadContactsFromFile();

        var results = allContacts
        .Where(c =>
            c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            c.Surname.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            c.Address.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            c.Postcode.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            c.City.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            c.Phone.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
        .ToList();

        Console.Clear();
        ShowContactsResult(results);

        Console.WriteLine(PromptMessage.PromptEnter);
        Console.ReadKey();
    }

    public void ShowContactList()
    {
        Console.Clear();
        Console.WriteLine(InfoMessage.InfoTitleShowContactList);

        allContacts = fileHandler.ReadContactsFromFile();

        for (int i = 0; i < allContacts.Count; i++)
        {
            Console.WriteLine(allContacts[i].ToDisplay());

            if (allContacts.Count == 0)
                Console.WriteLine(InfoMessage.InfoNoContactsInList);
        }

        Console.WriteLine(PromptMessage.PromptEnter);
        Console.ReadKey();
    }


    // Hjälpmetod för att välja kontakt när flera matchar
    private Contact ChooseContactFromList(List<Contact> chosenContacts)
    {
        int chosenNumber;

        do
        {
            Console.WriteLine(PromptMessage.PromptChooseContactbyNumber);
            int.TryParse(Console.ReadLine(), out chosenNumber);

            if (chosenNumber < 1 || chosenNumber > chosenContacts.Count)
            {
                Console.WriteLine(ErrorMessage.ErrorInvalidChoice);
            }
        } while (chosenNumber < 1 || chosenNumber > chosenContacts.Count);

        // -1 för att omvandla till 0-baserad indexering
        return chosenContacts[chosenNumber - 1];
    }

    // Visa sökresultat, hantera 0, 1 eller flera träffar
    private void ShowContactsResult(List<Contact> chosenContacts)
    {
        Console.Clear();
        if (chosenContacts.Count == 0)
        {
            Console.WriteLine(ErrorMessage.ErrorNoMatchingContact);
        }
        else if (chosenContacts.Count == 1)
        {
            Console.WriteLine(InfoMessage.InfoOneContactFound);
            Console.WriteLine(chosenContacts[0].ToDisplay()); //ToDisplay() är en metod i Contact-klassen.
        }
        else if (chosenContacts.Count > 1)
        {
            Console.WriteLine(InfoMessage.InfoSeveralContactsFound);
            for (int i = 0; i < chosenContacts.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {chosenContacts[i].ToDisplay()}");
            }
        }
    }

    // Sök kontakt med exakt för- och efternamn
    private List<Contact> SearchContactByFullName(string chosenName, string chosenSurname) 
    {
        allContacts = fileHandler.ReadContactsFromFile();

        List<Contact> chosenContacts = allContacts
            .Where(c =>
                c.Name.Contains(chosenName, StringComparison.OrdinalIgnoreCase)
                && c.Surname.Contains(chosenSurname, StringComparison.OrdinalIgnoreCase)
            )
            .ToList();
        return chosenContacts;
    }
}
