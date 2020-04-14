namespace MultiGry
{
    public interface IMenuOption
    {
        OptionsCategory OptionExecuting();
        string NameOption { get; }
    }
}