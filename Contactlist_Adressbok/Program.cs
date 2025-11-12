class Program 
{
    static void Main(string[] args)
    {   
        bool isProgramRunning = true;
        AddressBook addressBook = new AddressBook();

        while (isProgramRunning)
        {
            Console.Clear();
            Console.WriteLine(InfoMessage.InfoHeadTitle);
            Console.WriteLine(InfoMessage.InfoHeadMeny);
            Console.WriteLine(PromptMessage.PromptChooseService);

            int.TryParse(Console.ReadLine()?.Trim(), out int userChoice);
            HeadMenuOptions menuOption = (HeadMenuOptions)userChoice;

            switch (menuOption)
            {
                case HeadMenuOptions.AddContact:
                    addressBook.AddContact();
                    break;

                case HeadMenuOptions.UpdateContact:
                    addressBook.UpdateContact();
                    break;

                case HeadMenuOptions.RemoveContact:
                    addressBook.RemoveContact();
                    break;

                case HeadMenuOptions.SearchContact:
                    addressBook.SearchContact();
                    break;

                case HeadMenuOptions.ShowContacts:
                    addressBook.ShowContactList();
                    break;

                case HeadMenuOptions.Exit:
                    Console.Clear();
                    Console.WriteLine(InfoMessage.InfoProgramClosed);
                    isProgramRunning = false;
                    break;

                default:
                    Console.WriteLine(ErrorMessage.ErrorInvalidChoice);
                    Console.ReadKey();
                    break;
            }
        }
    }
}
