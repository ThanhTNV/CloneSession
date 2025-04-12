using System.Diagnostics.CodeAnalysis;

namespace UI.MySession
{
    public class MySession(string id, IMySessionStorageEngine engine) : ISession
    {
        private readonly Dictionary<string, byte[]> _store = new();
        public bool IsAvailable { 
            get
            {
                LoadAsync(CancellationToken.None).Wait();
                return true;
            } }

        public string Id { get => id; }

        public IEnumerable<string> Keys => _store.Keys;

        public void Clear()
        {
            _store.Clear();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await engine.CommitAsync(id, _store, cancellationToken);
        }

        public async Task LoadAsync(CancellationToken cancellationToken = default)
        {
            _store.Clear();
            var loadedStorage = await engine.LoadAsync(id, cancellationToken);
            foreach (var pair in loadedStorage)
            {
                _store[pair.Key] = pair.Value;
            }
        }

        public void Remove(string key)
        {
            _store.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            _store[key] = value;
        }

        public bool TryGetValue(string key, [NotNullWhen(true)] out byte[]? value)
        {
            return _store.TryGetValue(key, out value);
        }
    }
}
