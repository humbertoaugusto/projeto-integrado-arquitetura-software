using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Gcm.Info.Cadastrais.Application.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System;

namespace Gcm.Info.Cadastrais.Api.Filters
{
    [ExcludeFromCodeCoverage]
    public class DefaultExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private const string DEFAULT_EXCEPTION = "Ocorreu um erro inesperado.";

        public override void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception, context.Exception.Message);

            var returnDefaltException = Environment.GetEnvironmentVariable("RETURN_DEFAULT_EXCEPTION") == bool.TrueString;

            if (returnDefaltException)
            {
                context.Result = new ObjectResult(new ErrorModel(DEFAULT_EXCEPTION))
                {
                    StatusCode = HttpStatusCode.InternalServerError.GetHashCode()
                };
            }
            else
            {
                context.Result = new ObjectResult(new ErrorModel(context.Exception.ToString()))
                {
                    StatusCode = HttpStatusCode.InternalServerError.GetHashCode()
                };
            }
        }
    }
}
