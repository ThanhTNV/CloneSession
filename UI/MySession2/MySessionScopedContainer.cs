namespace UI.MySession2
{
    // Khi cần sử dụng 1 session trong cùng 1 request-scoped, việc liên tục load session từ storage sẽ gây ra low-performance
    // => có thể sử dụng DI để lưu 1 temp session cho mỗi Request_Scoped(HttpContext)
    // giúp hạn chế việc phải CommitAsync mỗi lần set key-value, và LoadAsync cho những lần truy cập session sau(Trong cùng 1 HttpContext)
    public class MySessionScopedContainer
    {
        public ISession? Session { get; set; }
    }
}
