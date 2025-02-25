using Serilog.Context;
using Serilog;
using System.Text;

namespace FoodieBlog.MVCCoreUI.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = Log.ForContext<RequestResponseLoggingMiddleware>();
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Request body'i güvenli şekilde oku
                var requestBody = await ReadRequestBody(context);

                // Orijinal response body stream'i sakla
                var originalResponseBodyStream = context.Response.Body;

                using (var responseBodyStream = new MemoryStream())
                {
                    // Response body'yi geçici stream'e yönlendir
                    context.Response.Body = responseBodyStream;

                    try
                    {
                        // Middleware zincirine devam et
                        await _next(context);


                    }
                    catch (Exception ex)
                    {
                        // Pipeline'daki diğer middleware'lerde oluşan hataları yakala
                        _logger.Error(ex, "Pipeline içinde hata: {ErrorMessage}", ex.Message);
                        throw; // Hata yönetiminin üst katmanlara ulaşmasını sağla
                    }

                    // Response body'yi oku (hata ayıklama için daha detaylı try-catch)
                    string responseBody;
                    try
                    {
                        responseBody = await ReadResponseBody(responseBodyStream);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Response body okunurken hata: {ErrorMessage}", ex.Message);
                        responseBody = "[Response body okunamadı]";
                    }

                    // Log işlemini yap
                    using (LogContext.PushProperty("RequestPath", context.Request.Path))
                    using (LogContext.PushProperty("RequestMethod", context.Request.Method))
                    using (LogContext.PushProperty("RequestBody", requestBody))
                    using (LogContext.PushProperty("ResponseBody", responseBody))
                    using (LogContext.PushProperty("StatusCode", context.Response.StatusCode))
                    using (LogContext.PushProperty("Timestamp", DateTime.UtcNow))
                    {

                        // Buradaki log seviyesi mutlaka program.cs deki log seviyesi ile örtüşmeli. BU seviyeden daha yüksek bir minumumlevel belirlenirse. Yukarıdaki customize kolonlara bilgi gönderilmemiş oluyor. Serilog bu noktada bize hata verdi

                        _logger.Warning("HTTP Request & Response Log");
                    }

                    // Response'u geri yükle - daha güvenli bir yaklaşım
                    try
                    {
                        responseBodyStream.Seek(0, SeekOrigin.Begin);
                        await responseBodyStream.CopyToAsync(originalResponseBodyStream);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Response body kopyalanırken hata: {ErrorMessage}", ex.Message);
                        // Minimal hata mesajı yaz
                        context.Response.StatusCode = 500;
                        if (originalResponseBodyStream.CanWrite)
                        {
                            var errorBytes = Encoding.UTF8.GetBytes("Internal Server Error");
                            await originalResponseBodyStream.WriteAsync(errorBytes, 0, errorBytes.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Middleware'de kritik hata: {ErrorMessage}", ex.Message);

                // Response daha önce değiştirilmediyse
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Internal Server Error");
                }
            }
            finally
            {
                // Ek temizlik işlemleri burada yapılabilir
            }
        }

        private async Task<string> ReadRequestBody(HttpContext context)
        {
            try
            {
                // Request body okuma büyüklük sınırı ekleyin (örn: 10KB)
                const int maxRequestBodySize = 10240;

                context.Request.EnableBuffering();

                using (var reader = new StreamReader(
                    context.Request.Body,
                    Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: true,
                    bufferSize: 1024,
                    leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;

                    // Çok büyükse kırp
                    if (body.Length > maxRequestBodySize)
                    {
                        return body.Substring(0, maxRequestBodySize) + "... [kırpıldı]";
                    }

                    return body;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Request body okunurken hata: {ErrorMessage}", ex.Message);
                return "[Request body okunamadı]";
            }
        }

        private async Task<string> ReadResponseBody(MemoryStream responseBodyStream)
        {
            try
            {
                // Response body okuma büyüklük sınırı ekleyin
                const int maxResponseBodySize = 10240;

                responseBodyStream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(responseBodyStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();

                    // Çok büyükse kırp
                    if (body.Length > maxResponseBodySize)
                    {
                        return body.Substring(0, maxResponseBodySize) + "... [kırpıldı]";
                    }

                    return body;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Response body okuma hatası", ex);
            }
        }
    }

    // Extension method
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}

