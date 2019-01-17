using System;
using System.Collections.Generic;
using System.Text;

namespace Tibos.Common
{
    /// <summary>Contains the values of status codes defined for HTTP.</summary>
    public enum HttpStatusCode
    {
        Continue = 100, // 0x00000064                    （继续） 请求者应当继续提出请求。 服务器返回此代码表示已收到请求的第一部分，正在等待其余部分。  
        SwitchingProtocols = 101, // 0x00000065          （切换协议） 请求者已要求服务器切换协议，服务器已确认并准备切换。
        Processing = 102, // 0x00000066                  （处理中）
        EarlyHints = 103, // 0x00000067                  （预提示）
                          //                              2xx （成功）表示成功处理了请求的状态代码。
        OK = 200, // 0x000000C8                          （成功）  服务器已成功处理了请求。 通常，这表示服务器提供了请求的网页。
        Created = 201, // 0x000000C9                     （已创建）  请求成功并且服务器创建了新的资源。
        Accepted = 202, // 0x000000CA                    （已接受）  服务器已接受请求，但尚未处理。 
        NonAuthoritativeInformation = 203, // 0x000000CB （非授权信息）  服务器已成功处理了请求，但返回的信息可能来自另一来源。
        NoContent = 204, // 0x000000CC                   （无内容）  服务器成功处理了请求，但没有返回任何内容。 
        ResetContent = 205, // 0x000000CD                （重置内容） 服务器成功处理了请求，但没有返回任何内容。 
        PartialContent = 206, // 0x000000CE              （部分内容）  服务器成功处理了部分 GET 请求。
        MultiStatus = 207, // 0x000000CF
        AlreadyReported = 208, // 0x000000D0
        IMUsed = 226, // 0x000000E2
                      //                                  3xx （重定向）表示要完成请求，需要进一步操作。 通常，这些状态代码用来重定向。
        Ambiguous = 300, // 0x0000012C                   
        MultipleChoices = 300, // 0x0000012C             （多种选择）针对请求，服务器可执行多种操作。 服务器可根据请求者 (user agent) 选择一项操作，或提供操作列表供请求者选择。
        Moved = 301, // 0x0000012D                       （永久移动） 请求的网页已永久移动到新位置。 服务器返回此响应（对 GET 或 HEAD 请求的响应）时，会自动将请求者转到新位置。 
        MovedPermanently = 301, // 0x0000012D            （临时移动） 服务器目前从不同位置的网页响应请求，但请求者应继续使用原有位置来进行以后的请求。 
        Found = 302, // 0x0000012E                       （已找到）
        Redirect = 302, // 0x0000012E                    （重定向）
        RedirectMethod = 303, // 0x0000012F              （重定向到方法）
        SeeOther = 303, // 0x0000012F                    （查看其他位置） 请求者应当对不同的位置使用单独的 GET 请求来检索响应时，服务器返回此代码。 
        NotModified = 304, // 0x00000130                 （未修改） 自从上次请求后，请求的网页未修改过。 服务器返回此响应时，不会返回网页内容。 
        UseProxy = 305, // 0x00000131                    （使用代理） 请求者只能使用代理访问请求的网页。 如果服务器返回此响应，还表示请求者应使用代理。
        Unused = 306, // 0x00000132                      （未使用）
        RedirectKeepVerb = 307, // 0x00000133            （保持动作的重定向）
        TemporaryRedirect = 307, // 0x00000133           （临时重定向）  服务器目前从不同位置的网页响应请求，但请求者应继续使用原有位置来进行以后的请求。
        PermanentRedirect = 308, // 0x00000134           （永久性重定向）
                                 //                                   4xx（请求错误）这些状态代码表示请求可能出错，妨碍了服务器的处理。
        BadRequest = 400, // 0x00000190                  （错误请求） 服务器不理解请求的语法。 
        Unauthorized = 401, // 0x00000191                （未授权） 请求要求身份验证。 对于需要登录的网页，服务器可能返回此响应。 
        PaymentRequired = 402, // 0x00000192             （支付请求）
        Forbidden = 403, // 0x00000193                   （禁止） 服务器拒绝请求。 
        NotFound = 404, // 0x00000194                    （未找到） 服务器找不到请求的网页。 
        MethodNotAllowed = 405, // 0x00000195            （方法禁用） 禁用请求中指定的方法。
        NotAcceptable = 406, // 0x00000196               （不接受） 无法使用请求的内容特性响应请求的网页。 
        ProxyAuthenticationRequired = 407, // 0x00000197 （需要代理授权） 此状态代码与 401（未授权）类似，但指定请求者应当授权使用代理。
        RequestTimeout = 408, // 0x00000198              （请求超时）  服务器等候请求时发生超时。 
        Conflict = 409, // 0x00000199                    （冲突）  服务器在完成请求时发生冲突。 服务器必须在响应中包含有关冲突的信息。 
        Gone = 410, // 0x0000019A                        （已删除）  如果请求的资源已永久删除，服务器就会返回此响应。 
        LengthRequired = 411, // 0x0000019B              （需要有效长度） 服务器不接受不含有效内容长度标头字段的请求。 
        PreconditionFailed = 412, // 0x0000019C          （未满足前提条件） 服务器未满足请求者在请求中设置的其中一个前提条件。 
        RequestEntityTooLarge = 413, // 0x0000019D       （请求实体过大） 服务器无法处理请求，因为请求实体过大，超出服务器的处理能力。 
        RequestUriTooLong = 414, // 0x0000019E           （请求的 URI 过长） 请求的 URI（通常为网址）过长，服务器无法处理。 
        UnsupportedMediaType = 415, // 0x0000019F        （不支持的媒体类型） 请求的格式不受请求页面的支持。 
        RequestedRangeNotSatisfiable = 416, // 0x000001A0（请求范围不符合要求） 如果页面无法提供请求的范围，则服务器会返回此状态代码。 
        ExpectationFailed = 417, // 0x000001A1           （未满足期望值） 服务器未满足”期望”请求标头字段的要求。
        MisdirectedRequest = 421, // 0x000001A5          （方向错误的请求）
        UnprocessableEntity = 422, // 0x000001A6         （不可获得的实体）
        Locked = 423, // 0x000001A7                      （被锁定）
        FailedDependency = 424, // 0x000001A8            （依赖注入失败）
        UpgradeRequired = 426, // 0x000001AA             （需要升级）
        PreconditionRequired = 428, // 0x000001AC        （必须满足的先决条件）
        TooManyRequests = 429, // 0x000001AD             （过多的请求）
        RequestHeaderFieldsTooLarge = 431, // 0x000001AF （请求头字段太大）
        UnavailableForLegalReasons = 451, // 0x000001C3  （由于法律原因不可用）
                                          //                                      5xx（服务器错误）这些状态代码表示服务器在尝试处理请求时发生内部错误。 这些错误可能是服务器本身的错误，而不是请求出错。
        InternalServerError = 500, // 0x000001F4         （服务器内部错误）  服务器遇到错误，无法完成请求。 
        NotImplemented = 501, // 0x000001F5              （尚未实施） 服务器不具备完成请求的功能。 例如，服务器无法识别请求方法时可能会返回此代码。 
        BadGateway = 502, // 0x000001F6                  （错误网关） 服务器作为网关或代理，从上游服务器收到无效响应。 
        ServiceUnavailable = 503, // 0x000001F7          （服务不可用） 服务器目前无法使用（由于超载或停机维护）。 通常，这只是暂时状态。 
        GatewayTimeout = 504, // 0x000001F8              （网关超时）  服务器作为网关或代理，但是没有及时从上游服务器收到请求。 
        HttpVersionNotSupported = 505, // 0x000001F9     （HTTP 版本不受支持） 服务器不支持请求中所用的 HTTP 协议版本。
        VariantAlsoNegotiates = 506, // 0x000001FA
        InsufficientStorage = 507, // 0x000001FB         （存储不足）
        LoopDetected = 508, // 0x000001FC                （循环检测）
        NotExtended = 510, // 0x000001FE                 （无法扩展）
        NetworkAuthenticationRequired = 511, //0x000001FF（必须的网络授权）
    }
}
