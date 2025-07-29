namespace TransactionsExample.Services.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string item, int id) : base($"{CapitalizeFirstLetter(item)} with id {id} could not be found.")
    {
    }

    public static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "Item";

        return char.ToUpper(input[0]) + input.Substring(1);
    }
}