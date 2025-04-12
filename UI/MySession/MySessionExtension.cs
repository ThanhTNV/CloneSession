
namespace UI.MySession
{
    public static class MySessionExtension
    {
        public const string SessionIdCookieName = "MY_SESSION_ID";
        public static ISession GetSession(this HttpContext context)
        {
            string? sessionId = context.Request.Cookies[SessionIdCookieName];
            if (IsSessionIdFormValid(sessionId))
            {
                var session = context.RequestServices.GetRequiredService<IMySessionStorage>().Get(sessionId!);
                context.Response.Cookies.Append(SessionIdCookieName, session.Id);
                return session;
            }
            else
            {
                var session = context.RequestServices.GetRequiredService<IMySessionStorage>().Create();
                context.Response.Cookies.Append(SessionIdCookieName, session.Id);
                return session;
            }
        }

        private static bool IsSessionIdFormValid(string? sessionId)
        {
            return !string.IsNullOrEmpty(sessionId) && Guid.TryParse(sessionId, out var _);
        }
    }
}
