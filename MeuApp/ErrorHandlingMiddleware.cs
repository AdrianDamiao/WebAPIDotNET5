using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate Next;
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.Next = next;
    }

    public async Task Invoke(HttpContext context) //Método que passa o contexto para o dotnet
    {
        try
        {
            //var endpoint = context.GetEndpoint();
            await Next(context);   //Encapsula todas as informações individuais do request 
        }
        catch (Exception exception)
        {
            //throw new Exception("Unexpected Error! \nError: "+e.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; //Status code que será retornado ao usuário
        if (exception is Exception)
        {
            code = HttpStatusCode.NotFound;
        }
        if (exception is ArgumentNullException)
        {
            code = HttpStatusCode.BadRequest;
        }

        context.Response.ContentType = "application/json"; //Confirma que a api sempre retornará um JSON
        context.Response.StatusCode = (int)code;
        // return context.Response.WriteAsJsonAsync(code); <- PESQUISAR
        return context.Response.WriteAsync(JsonSerializer.Serialize(new { error = exception.Message })); //Padrao com o front-end
        //JSON Serialize -> converte um objeto c# em JSON
    }
}