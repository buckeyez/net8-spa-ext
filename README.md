# .NET 8 SPA Extension

Reproduction of .NET 8 Spa Extension Bug

## Setup

Run `npm i` within `./ClientApp`.

## First Use Case

1. Serve Frontend: `npm run dev` within `./ClientApp`
2. Serve .NET: `dotnet watch` at root
3. Browser should open to the backend SPA proxy: `localhost:9001`
4. Page will display loading, after some time the server will log:

```text
fail: Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware[1]
      An unhandled exception has occurred while executing the request.
      System.Net.Http.HttpRequestException: Failed to proxy the request to http://localhost:8080/api/weather, because the request to the proxy target failed. Check that the proxy target server is running and accepting requests to http://localhost:8080/.

The underlying exception message was 'No connection could be made because the target machine actively refused it. (localhost:8080)'.Check the InnerException for more details.
       ---> System.Net.Http.HttpRequestException: No connection could be made because the target machine actively refused it. (localhost:8080)
       ---> System.Net.Sockets.SocketException (10061): No connection could be made because the target machine actively refused it.
         at System.Net.Sockets.Socket.AwaitableSocketAsyncEventArgs.ThrowException(SocketError error, CancellationToken cancellationToken)
         at System.Net.Sockets.Socket.AwaitableSocketAsyncEventArgs.System.Threading.Tasks.Sources.IValueTaskSource.GetResult(Int16 token)
         at System.Net.Sockets.Socket.<ConnectAsync>g__WaitForConnectWithCancellation|285_0(AwaitableSocketAsyncEventArgs saea, ValueTask connectTask, CancellationToken cancellationToken)
         at System.Net.Http.HttpConnectionPool.ConnectToTcpHostAsync(String host, Int32 port, HttpRequestMessage initialRequest, Boolean async, CancellationToken cancellationToken)
         --- End of inner exception stack trace ---
         at System.Net.Http.HttpConnectionPool.ConnectToTcpHostAsync(String host, Int32 port, HttpRequestMessage initialRequest, Boolean async, CancellationToken cancellationToken)
         at System.Net.Http.HttpConnectionPool.ConnectAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
         at System.Net.Http.HttpConnectionPool.CreateHttp11ConnectionAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
         at System.Net.Http.HttpConnectionPool.AddHttp11ConnectionAsync(QueueItem queueItem)
         at System.Threading.Tasks.TaskCompletionSourceWithCancellation`1.WaitWithCancellationAsync(CancellationToken cancellationToken)
         at System.Net.Http.HttpConnectionPool.SendWithVersionDetectionAndRetryAsync(HttpRequestMessage request, Boolean async, Boolean doRequestAuth, CancellationToken cancellationToken)
         at System.Net.Http.DiagnosticsHandler.SendAsyncCore(HttpRequestMessage request, Boolean async, CancellationToken cancellationTcontext, HttpClient httpClient, Task`1 baseUriTask, CancellationToken applicationStoppingToken, Boolean proxy404s)
         --- End of inner exception stack trace ---
         at Microsoft.AspNetCore.SpaServices.Extensions.Proxy.SpaProxy.PerformProxyRequest(HttpContext context, HttpClient httpClient, Task`1 baseUriTask, CancellationToken applicationStoppingToken, Boolean proxy404s)
         at Microsoft.AspNetCore.Builder.SpaProxyingExtensions.<>c__DisplayClass2_0.<<UseProxyToSpaDevelopmentServer>b__0>d.MoveNext() 
      --- End of stack trace from previous location ---
         at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
         at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
         at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
         at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
         at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)
```

## Second Use Case

1. Do NOT serve the frontend
2. Serve .NET: `dotnet watch` at root
3. Browser should open to the backend SPA proxy: `localhost:9001`
4. Open swagger `/swagger` and try to execute the weather API endpoint, this is the result:

```text
System.Net.Http.HttpRequestException: Failed to proxy the request to http://localhost:8080/api/Weather, because the request to the proxy target failed. Check that the proxy target server is running and accepting requests to http://localhost:8080/.

The underlying exception message was 'No connection could be made because the target machine actively refused it. (localhost:8080)'.Check the InnerException for more details.
 ---> System.Net.Http.HttpRequestException: No connection could be made because the target machine actively refused it. (localhost:8080)
 ---> System.Net.Sockets.SocketException (10061): No connection could be made because the target machine actively refused it.
   at System.Net.Sockets.Socket.AwaitableSocketAsyncEventArgs.ThrowException(SocketError error, CancellationToken cancellationToken)
   at System.Net.Sockets.Socket.AwaitableSocketAsyncEventArgs.System.Threading.Tasks.Sources.IValueTaskSource.GetResult(Int16 token)
   at System.Net.Sockets.Socket.<ConnectAsync>g__WaitForConnectWithCancellation|285_0(AwaitableSocketAsyncEventArgs saea, ValueTask connectTask, CancellationToken cancellationToken)
   at System.Net.Http.HttpConnectionPool.ConnectToTcpHostAsync(String host, Int32 port, HttpRequestMessage initialRequest, Boolean async, CancellationToken cancellationToken)
   --- End of inner exception stack trace ---
   at System.Net.Http.HttpConnectionPool.ConnectToTcpHostAsync(String host, Int32 port, HttpRequestMessage initialRequest, Boolean async, CancellationToken cancellationToken)
   at System.Net.Http.HttpConnectionPool.ConnectAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
   at System.Net.Http.HttpConnectionPool.CreateHttp11ConnectionAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
   at System.Net.Http.HttpConnectionPool.AddHttp11ConnectionAsync(QueueItem queueItem)
   at System.Threading.Tasks.TaskCompletionSourceWithCancellation`1.WaitWithCancellationAsync(CancellationToken cancellationToken)
   at System.Net.Http.HttpConnectionPool.SendWithVersionDetectionAndRetryAsync(HttpRequestMessage request, Boolean async, Boolean doRequestAuth, CancellationToken cancellationToken)
   at System.Net.Http.DiagnosticsHandler.SendAsyncCore(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
   at System.Net.Http.HttpClient.<SendAsync>g__Core|83_0(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationTokenSource cts, Boolean disposeCts, CancellationTokenSource pendingRequestsCts, CancellationToken originalCancellationToken)
   at Microsoft.AspNetCore.SpaServices.Extensions.Proxy.SpaProxy.PerformProxyRequest(HttpContext context, HttpClient httpClient, Task`1 baseUriTask, CancellationToken applicationStoppingToken, Boolean proxy404s)
   --- End of inner exception stack trace ---
   at Microsoft.AspNetCore.SpaServices.Extensions.Proxy.SpaProxy.PerformProxyRequest(HttpContext context, HttpClient httpClient, Task`1 baseUriTask, CancellationToken applicationStoppingToken, Boolean proxy404s)
   at Microsoft.AspNetCore.Builder.SpaProxyingExtensions.<>c__DisplayClass2_0.<<UseProxyToSpaDevelopmentServer>b__0>d.MoveNext()
--- End of stack trace from previous location ---
   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)

HEADERS
=======
Accept: text/plain
Connection: keep-alive
Host: localhost:9001
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36 Edg/121.0.0.0
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9
Referer: http://localhost:9001/swagger/index.html
sec-ch-ua: "Not A(Brand";v="99", "Microsoft Edge";v="121", "Chromium";v="121"
sec-ch-ua-mobile: ?0
sec-ch-ua-platform: "Windows"
Sec-Fetch-Site: same-origin
Sec-Fetch-Mode: cors
Sec-Fetch-Dest: empty
```

## Third Use Case

1. Comment out `app.UseProxyToSpaDevelopmentServer` in `Program.cs`
2. Serve Frontend: `npm run dev` within `./ClientApp`
3. Serve .NET: `dotnet watch` at root
4. Browser should open to the backend SPA proxy: `localhost:9001`
5. Open swagger `/swagger` and try to execute the weather API endpoint, it will work.
6. Navigate to `localhost:8080` it will also load and proxy correctly to the backend.
