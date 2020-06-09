namespace MultiGry
{
    public interface INumberGenerator
    {
        byte GetNumberBetween1And100();
        int Next(int min, int max);
    }
}
