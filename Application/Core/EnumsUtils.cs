namespace Application.Core;

public class EnumsUtils
{
    public static List<T> EnumToList<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }
}