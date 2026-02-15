namespace ApiBankApp.Logger
{
    public static class Logger
    {
        //Loggar fel till separat fil
        public static void LogErrorToFile(Exception ex)
        {
            string directoryPath = Directory.GetCurrentDirectory();
            string logPath = Path.Combine(
                directoryPath,
                "Logger",
                "ErrorLog.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(logPath, append: true))
                {
                    writer.WriteLine($"[{DateTime.Now}] Error: {ex}");
                    writer.WriteLine($"Inner Exception: {ex.InnerException}");
                    writer.WriteLine("-------------------------------------------------\n");
                }
            }
            catch
            {
                // Om loggningen misslyckas, skriv till konsolen som sista utväg
                Console.WriteLine("Failed to log error.");
            }
        }
    }
}
