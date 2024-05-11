using System.Text;

namespace BinLab;

internal static class Calculator
{
    private const string YesLabel = "Yes (1)";
    private const string NoLabel = "No  (0)";
    private const int BinaryNumberLength = 8;
    private const int TableWidth = 40;

    private static string IntToBinary(int number)
    {
        return Convert.ToString(number, 2).PadLeft(BinaryNumberLength, '0');
    }

    public static void DisplayPowerTable()
    {
        var tableBuilder = new StringBuilder();

        AppendTableHeader(tableBuilder, "Power of 2 Table");
        AppendTableColumnHeaders(tableBuilder, "Exponent", "Result");

        for (int i = BinaryNumberLength - 1; i >= 0; i--)
        {
            double powerOf2 = Math.Pow(2, i);
            AppendTableRow(tableBuilder, $"2^{i}", powerOf2.ToString("N0"));
        }

        AppendTableFooter(tableBuilder);
        Console.WriteLine(tableBuilder.ToString());
    }

    public static void DisplayBinaryTableFor(double number)
    {
        Console.WriteLine($"\nCalculating binary representation of {number}...\n");

        var binaryResult = IntToBinary((int)number);
        var remainingValue = number;
        var tableBuilder = new StringBuilder();

        AppendTableHeader(tableBuilder, "Can be taken away?");
        AppendTableColumnHeaders(tableBuilder, "i", "2^i", "Can be taken away?");

        for (var i = BinaryNumberLength - 1; i >= 0; i--)
        {
            var value = GetPowerOfTwo(i);
            var canBeTakenAway = remainingValue >= value ? YesLabel : NoLabel;

            if (canBeTakenAway == YesLabel)
                remainingValue -= value;

            AppendTableRow(tableBuilder, i.ToString(), value.ToString(), canBeTakenAway);
        }

        AppendTableFooter(tableBuilder);
        Console.WriteLine(tableBuilder.ToString());
        Console.WriteLine($"\nBinary result: {binaryResult}");
    }

    public static void PrintBinaryConversion(string binaryNumber)
    {
        ValidateBinaryNumber(binaryNumber);

        Console.WriteLine($"\nCalculating decimal representation of {binaryNumber}...\n");

        var decimalValue = 0;
        var tableBuilder = new StringBuilder();

        AppendTableHeader(tableBuilder, "Is 1 present?");
        AppendTableColumnHeaders(tableBuilder, "i", "2^i", "Is 1 present?");

        var binDigits = binaryNumber.ToCharArray();
        for (var i = 0; i < BinaryNumberLength; i++)
        {
            var power = Math.Pow(2, BinaryNumberLength - 1 - i);
            var isOnePresent = binDigits[i] == '1' ? YesLabel : NoLabel;

            decimalValue += isOnePresent == YesLabel ? (int)power : 0;

            AppendTableRow(tableBuilder, i.ToString(), ((int)power).ToString(), isOnePresent);
        }

        AppendTableFooter(tableBuilder);
        tableBuilder.AppendLine($"\nDecimal result: {binaryNumber} = {decimalValue}");

        Console.WriteLine(tableBuilder.ToString());
    }

    public static string GetBinaryNumberFromUser()
    {
        string binaryNumber;
        bool isValidInput;
        do
        {
            Console.Write("Enter a binary number (8 1s or 0s): ");
            binaryNumber = Console.ReadLine() ?? throw new InvalidOperationException();

            isValidInput = IsBinaryNumberValid(binaryNumber);

            if (!isValidInput)
            {
                Console.WriteLine("Invalid input. Please enter a binary number with exactly 8 characters (0s or 1s).");
            }

        } while (!isValidInput);

        return binaryNumber;
    }

    private static double GetPowerOfTwo(int exponent)
    {
        return Math.Pow(2, exponent);
    }

    public static double GetNumberFromUser()
    {
        double number;
        bool isValidInput;
        do
        {
            Console.Write("Enter a number: ");
            var input = Console.ReadLine();

            isValidInput = double.TryParse(input, out number);

            if (!isValidInput)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

        } while (!isValidInput);

        return number;
    }

    private static void ValidateBinaryNumber(string binaryNumber)
    {
        if (!IsBinaryNumberValid(binaryNumber))
            throw new ArgumentException("Invalid binary string.");
    }

    private static bool IsBinaryNumberValid(string binaryNumber)
    {
        return !string.IsNullOrWhiteSpace(binaryNumber) &&
               binaryNumber.Length == BinaryNumberLength &&
               binaryNumber.All(c => c == '0' || c == '1');
    }

    private static void AppendTableHeader(StringBuilder tableBuilder, string title)
    {
        tableBuilder.AppendLine(new string('═', Console.WindowWidth));
        tableBuilder.AppendLine($"{title,TableWidth}");
        tableBuilder.AppendLine(new string('═', Console.WindowWidth));
    }

    private static void AppendTableColumnHeaders(StringBuilder tableBuilder, params string[] headers)
    {
        tableBuilder.AppendLine($"| {string.Join(" | ", headers.Select(h => h.PadRight(TableWidth / headers.Length - 3))),-TableWidth} |");
        tableBuilder.AppendLine(new string('═', Console.WindowWidth));
    }

    private static void AppendTableRow(StringBuilder tableBuilder, params string[] values)
    {
        tableBuilder.AppendLine($"| {string.Join(" | ", values.Select(v => v.PadRight(TableWidth / values.Length - 3))),-TableWidth} |");
    }

    private static void AppendTableFooter(StringBuilder tableBuilder)
    {
        tableBuilder.AppendLine(new string('═', Console.WindowWidth));
    }
}