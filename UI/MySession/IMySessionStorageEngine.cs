namespace UI.MySession
{
    public interface IMySessionStorageEngine
    {
        public Task CommitAsync(string id, Dictionary<string, byte[]> store, CancellationToken cancellationToken = default);
        public Task<Dictionary<string, byte[]>> LoadAsync(string id, CancellationToken cancellationToken = default);
    }
}
