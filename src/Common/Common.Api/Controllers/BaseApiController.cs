using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Common.Api.Extensions;
using Common.Application.Models;
using Microsoft.AspNetCore.Http;

namespace Common.Api.Controllers;

public abstract class BaseApiController : ControllerBase
{
    protected const string Id = "{id}";
    protected const string PathSeparator = "/";

    private IMediator? mediator;

    protected IMediator Mediator
        => mediator ??= HttpContext
            .RequestServices
            .GetService<IMediator>()!;

    protected Task<ActionResult<TResult>> Send<TResult>(
        IRequest<TResult> request)
        => Mediator.Send(request).ToActionResult();

    protected Task<ActionResult<TResult>> Send<TResult>(
        IRequest<Result<TResult>> request)
        => Mediator.Send(request).ToActionResult();

    protected Task<ActionResult> Send(
        IRequest<Result> request)
        => Mediator.Send(request).ToActionResult();

    protected Task<ActionResult> Send(
        IRequest<Stream> request)
    {
        var headers = Response.GetTypedHeaders();

        headers.CacheControl = new CacheControlHeaderValue
        {
            Public = true,
            MaxAge = TimeSpan.FromDays(30)
        };

        headers.Expires = new DateTimeOffset(DateTime.UtcNow.AddDays(30));

        return Mediator.Send(request).ToActionResult();
    }
}