namespace UI.MySession
{
    public interface IMySessionStorage
    {
        ISession Create();
        ISession Get(string sessionId);
    }
}
