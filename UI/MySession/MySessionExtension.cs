
using UI.MySession2;

namespace UI.MySession
{
    public static class MySessionExtension
    {
        public const string SessionIdCookieName = "MY_SESSION_ID";
        public static ISession GetSession(this HttpContext context)
        {
            // SessionContainer: Một biến request-long-lifetime, chứa session đã được load ra từ sessionStorage
            var sessionContainer = context.RequestServices.GetRequiredService<MySessionScopedContainer>();
            // Nếu SessionContainer.Session != null => Session đã được load từ sessionStorage rồi, không cần load lại
            if (sessionContainer.Session != null)
            {
                return sessionContainer.Session;
            }
            else // Nếu SessionContainer.Session == null => Session chưa được load từ sessionStorage
            {
                // lấy sessionId từ Cookie của Request
                string? sessionId = context.Request.Cookies[SessionIdCookieName];
                if (IsSessionIdFormValid(sessionId))
                {
                    // Nếu sessionId Valid thì dùng id đó để load/fetch session từ storage
                    var session = context.RequestServices.GetRequiredService<IMySessionStorage>().Get(sessionId!);
                    context.Response.Cookies.Append(SessionIdCookieName, session.Id, new CookieOptions
                    {
                        HttpOnly = true//Chặn code js đọc được cookie này(HttpOnly cookie)
                    });
                    //Sau đó, lưu(cache) lại session này trong request-scoped để các tác vụ tiếp theo có thể dùng nếu cần
                    sessionContainer.Session = session;
                    // Session được trả ra
                    return session;
                }
                else
                {
                    // Nếu sessionId Invalid(Muốn hash, decrypt/encrypt gì thì tùy) => id này không xài được
                    // => tạo 1 Session mới vào trong SessionStorage
                    var session = context.RequestServices.GetRequiredService<IMySessionStorage>().Create();
                    context.Response.Cookies.Append(SessionIdCookieName, session.Id, new CookieOptions
                    {
                        HttpOnly = true//Chặn code js đọc được cookie này(HttpOnly cookie)
                    });
                    //Sau đó, lưu(cache) lại session này trong request-scoped để các tác vụ tiếp theo có thể dùng nếu cần
                    sessionContainer.Session = session;
                    // Session được trả ra
                    return session;
                }
            }
        }
        /*
         If one cookie is HttpOnly, it cannot be accessed by client JavaScript, 
        which means hackers cannot read the cookie value and send it to his own server, 
        not even know whether this cookie exist.
         */
        /*
         Ngay khi cookie được tạo và trả về cho client, khi cờ httponly được bật là true. 
        Cả client, browser đều sẽ biết cookie này chỉ được phép truy cập ở máy chủ, 
        mọi phương thức khác cố gắng truy cập thông tin cookie đều bị từ chối.
         */
        private static bool IsSessionIdFormValid(string? sessionId)
        {
            return !string.IsNullOrEmpty(sessionId) && Guid.TryParse(sessionId, out var _);
        }
    }
}
