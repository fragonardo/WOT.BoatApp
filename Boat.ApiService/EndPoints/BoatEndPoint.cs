using Azure.Identity;
using Boat.ApiService.Requests;
using Boat.ApiService.Services;
using BoatApp.ApiService.Requests;
using BoatApp.Application.Commands;
using BoatApp.Application.Queries;
using BoatApp.Application.Queries.Result;
using BoatApp.Application.Queries.ViewModels;
using BoatApp.Domain.Exceptions;
using BoatApp.Domain.Models;
using BoatApp.Shared.Application;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using static BoatApp.ApiService.EndPoints.BoatEndPoint;

namespace BoatApp.ApiService.EndPoints;

public static class BoatEndPoint
{
    public static void MapBoatsApi(this WebApplication app)
    {
        var api = app.MapGroup("api/boats");

        api.MapPost("/", CreateBoatAsync);
        api.MapPut("{id:Guid}", UpdateBoatAsync);
        api.MapGet("{id:Guid}", GetBoatByIdAsync);
        api.MapDelete("{id:Guid}", DeleteBoatAsync);
        api.MapGet("/types", GetTypes);

        api.MapGet("/",
            async Task<Results<Ok<ApiCollectionResult<BoatViewModel>>, UnauthorizedHttpResult, ProblemHttpResult>>
            ([FromQuery] int PageIndex, 
            [FromQuery] int ItemPerPage, 
            [FromQuery] string Filter, 
            [AsParameters] ApiBoatServices services,
            CancellationToken cancellationToken = default) => 
        {
            try
            {
                var query = new GetAllBoatsQuery
                {
                    Filter = Filter,
                    PageIndex = PageIndex,
                    ItemPerPage = ItemPerPage
                }; 

                var result = await services.Mediator.Send(query, cancellationToken);
                return TypedResults.Ok(result);
               
            }
            catch (UnauthorizedAccessException)
            {
                return TypedResults.Unauthorized();
            }
            catch (Exception ex)
            {
                services.Logger.LogError(ex, "An unexpected error occured ");
                return TypedResults.Problem(detail: "An unexpected error occurs when fetching boats, please contact your administrator", title: "Unexpected error");
            }
        } );

        

    }

    public static async Task<Results<Ok<Guid>, Conflict<string>, UnauthorizedHttpResult, ProblemHttpResult>> CreateBoatAsync(
        CreateBoatRequest request,
        [AsParameters] ApiBoatServices services,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var createBoatCommand = new CreateBoatCommand(
             Guid.NewGuid(),
             request.SerialNumber,
             (Domain.Models.BoatType)request.Type,
             request.LaunchingDate,
             request.Owner,
             request.Name);

            var result = await services.Mediator.Send(createBoatCommand, cancellationToken);
            return TypedResults.Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return TypedResults.Unauthorized();
        }
        catch (BoatConflictException ex)
        {
            return TypedResults.Conflict<string>(ex.Message);
        }
        catch (Exception ex)
        {
            services.Logger.LogError(ex, "An unexpected error occured ");
            return TypedResults.Problem(detail: "An unexpected error occurs when fetching boats, please contact your administrator");
        }
    }

    public static async 
        Task<Results<Ok<ApiCollectionResult<BoatViewModel>>, UnauthorizedHttpResult, ProblemHttpResult>> 
        GetBoatsAsync(
        [AsParameters] ApiBoatServices services,
        //[AsParameters] FilterBoatRequest? filter,
        [AsParameters] int? PageIndex,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await services.Mediator.Send(new GetAllBoatsQuery()
            //{
            //    SerialNumberFilter = filter.SerialNumberFilter,
            //    NameFilter = filter.NameFilter,
            //    OwnerFilter = filter.OwnerFilter,
            //    PageIndex = filter.PageIndex,
            //    ItemPerPage = filter.ItemPerPage
            //}
            , cancellationToken);
            return TypedResults.Ok(result);
        }
        catch(UnauthorizedAccessException)
        {
            return TypedResults.Unauthorized();
        }        
        catch(Exception ex)
        {
            services.Logger.LogError(ex, "An unexpected error occured ");
            return TypedResults.Problem(detail: "An unexpected error occurs when fetching boats, please contact your administrator", title: "Unexpected error");
        }
    }

    public static async Task<Results<Ok<Guid>, NotFound<string>, UnauthorizedHttpResult, ProblemHttpResult>> UpdateBoatAsync(
    UpdateBoatRequest request,
    [FromRoute] Guid id,
    [AsParameters] ApiBoatServices services,
    CancellationToken cancellationToken)
    {
        try
        {
            var updateBoatCommand = new UpdateBoatCommand(
             id,
             //request.SerialNumber,
             //(BoatType)request.Type,
             //request.LaunchingDate,
             request.Owner,
             request.Name);

            var result = await services.Mediator.Send(updateBoatCommand, cancellationToken);
            return TypedResults.Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return TypedResults.Unauthorized();
        }
        catch (BoatNotFoundException ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            services.Logger.LogError(ex, "An unexpected error occured ");
            return TypedResults.Problem(detail: "An unexpected error occurs when fetching boats, please contact your administrator");
        }
    }

    public static async Task<Results<Ok<BoatViewModel>, UnauthorizedHttpResult, ProblemHttpResult>> GetBoatByIdAsync(
        [FromRoute] Guid id,
        [AsParameters] ApiBoatServices services,
        CancellationToken cancellationToken)
    {
        try
        {            
            var result = await services.Mediator.Send(new GetBoatByIdQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return TypedResults.Unauthorized();
        }
        catch (Exception ex)
        {
            services.Logger.LogError(ex, "An unexpected error occured ");
            return TypedResults.Problem(detail: "An unexpected error occurs when fetching boats, please contact your administrator", title: "Unexpected error");
        }
    }

    public static async Task<Results<Ok<bool>, NotFound<string>, UnauthorizedHttpResult, ProblemHttpResult>> DeleteBoatAsync(    
    [FromRoute] Guid id,
    [AsParameters] ApiBoatServices services,
    CancellationToken cancellationToken)
    {
        try
        {
            var deleteBoatCommand = new DeleteBoatCommand(id);

            var result = await services.Mediator.Send(deleteBoatCommand, cancellationToken);
            return TypedResults.Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return TypedResults.Unauthorized();
        }
        catch (BoatNotFoundException ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            services.Logger.LogError(ex, "An unexpected error occured ");
            return TypedResults.Problem(detail: "An unexpected error occurs when fetching boats, please contact your administrator");
        }
    }

    public static IList<BoatType> GetTypes(CancellationToken cancellationToken)
    {
        IList<BoatType> types = [];
        foreach(var value in Enum.GetValues(typeof(Domain.Models.BoatType)))
        {
            types.Add(new BoatType((int)value, Enum.GetName(typeof(Domain.Models.BoatType), value)));
        }

        return types;
    }

    public record BoatType(int Id, string Name);
}
