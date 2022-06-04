namespace Meetup
{
    public class ExeptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExeptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (DAOExeption ex)
            {
                switch(ex.Type)
                {
                    case DAOErrorType.WrongEntity:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("Wrong entity");
                        break;
                    case DAOErrorType.InternalServerError:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsync("Internal Server Error");
                        break;
                }
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Bad Request");
            }
        }
    }
}
