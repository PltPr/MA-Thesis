using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Exceptions.Handler
{
	public class CustomExceptionHandler : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
		{
			(string Detail, string Title, int StatusCode) details = exception switch
			{
				InternalServerException => (
				exception.Message,
				exception.GetType().Name,
				StatusCodes.Status500InternalServerError
				),
				BadRequestException => (
				exception.Message,
				exception.GetType().Name,
				StatusCodes.Status400BadRequest
				),
				NotFoundException => (
				exception.Message,
				exception.GetType().Name,
				StatusCodes.Status404NotFound
				),
				_=>(
				exception.Message,
				exception.GetType().Name,
				StatusCodes.Status500InternalServerError
				)
			};
			context.Response.StatusCode = details.StatusCode;

			var problemDetails = new ProblemDetails
			{
				Title = details.Title,
				Detail = details.Detail,
				Status = details.StatusCode,
				Instance = context.Request.Path
			};
			
			if(exception is ValidationException validationException)
			{
				problemDetails.Extensions.Add("validationErrors", validationException.Errors);
			}
			else if (exception is InternalServerException internalServerException && internalServerException.Description is not null)
			{
				problemDetails.Extensions.Add("description", internalServerException.Description);
			}
			else if (exception is BadRequestException badRequestException && badRequestException.Description is not null)
			{
				problemDetails.Extensions.Add("description", badRequestException.Description);
			}

			await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

			return true;

		}
	}
}
