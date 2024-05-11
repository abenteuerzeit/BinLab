using BinLab;

Console.WriteLine("Welcome to Binary Lab!");

while (true)
{
    DisplayMenu();

    var choice = GetUserChoice();

    Console.WriteLine();

    switch (choice)
    {
        case "1":
            Calculator.DisplayPowerTable();
            break;
        case "2":
            var number = Calculator.GetNumberFromUser();
            Calculator.DisplayBinaryTableFor(number);
            break;
        case "3":
            var binaryNumber = Calculator.GetBinaryNumberFromUser();
            Calculator.PrintBinaryConversion(binaryNumber);
            break;
        case "4":
            Console.WriteLine("Thank you for using Binary Lab!");
            return;
        default:
            Console.WriteLine("Invalid choice. Please enter a valid option.");
            break;
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey(true);
    Console.Clear();
}

static void DisplayMenu()
{
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║             Main Menu                ║");
    Console.WriteLine("╠══════════════════════════════════════╣");
    Console.WriteLine("║ Options:                             ║");
    Console.WriteLine("║ 1. Display Power Table               ║");
    Console.WriteLine("║ 2. Convert Decimal to Binary         ║");
    Console.WriteLine("║ 3. Convert Binary to Decimal         ║");
    Console.WriteLine("║ 4. Exit                              ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.Write("Enter your choice: ");
}

static string GetUserChoice()
{
    string choice;
    bool isValidChoice;

    do
    {
        choice = Console.ReadLine() ?? throw new InvalidOperationException();
        isValidChoice = !string.IsNullOrWhiteSpace(choice) && choice is "1" or "2" or "3" or "4";

        if (isValidChoice) continue;
        Console.WriteLine("Invalid choice. Please enter a valid option.");
        Console.Write("Enter your choice: ");
    } while (!isValidChoice);

    return choice;
}