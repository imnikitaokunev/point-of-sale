namespace PointOfSale.Common;

public static class Require
{
    public static void NotNull<T>(T argument, string argumentName = null) where T : class
    {
        if (argument is not null)
        {
            return;
        }

        if (string.IsNullOrEmpty(argumentName))
        {
            throw new ArgumentNullException();
        }

        throw new ArgumentNullException(argumentName);
    }

    public static void NotNullOrEmpty(string argument, string argumentName = null)
    {
        if (!string.IsNullOrEmpty(argument))
        {
            return;
        }

        if (string.IsNullOrEmpty(argumentName))
        {
            throw new ArgumentNullException();
        }

        throw new ArgumentNullException(argumentName);
    }

    public static void GreaterThanZero(double argument, string argumentName = null)
    {
        if (argument > 0)
        {
            return;
        }

        if (string.IsNullOrEmpty(argumentName))
        {
            throw new ArgumentException("Must be greater than zero");
        }

        throw new ArgumentException("Must be greater than zero", argumentName);
    }

    public static void NonNullElements<T>(IEnumerable<T> collection, string argumentName = null)
    {
        if (!collection.Any(x => x is null))
        {
            return;
        }

        if (string.IsNullOrEmpty(argumentName))
        {
            throw new ArgumentException("Should not contain nulls");
        }

        throw new ArgumentException("Should not contain nulls", argumentName);
    }
}
