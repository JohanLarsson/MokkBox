namespace MokkBox
{
    public interface IServer
    {
        int SomeValue { get; }

        string Login(string token);
    }
}
