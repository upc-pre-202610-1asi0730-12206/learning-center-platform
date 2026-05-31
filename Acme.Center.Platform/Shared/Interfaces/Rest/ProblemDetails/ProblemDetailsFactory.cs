using Acme.Center.Platform.Resources.Errors;
using Acme.Center.Platform.Resources.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
// For base ProblemDetailsFactory
// For ErrorMessages
// For Common

// For StatusCodes

namespace Acme.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;

public class ProblemDetailsFactory(
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<Commons> commonLocalizer,
    Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory aspNetCoreProblemDetailsFactory)
{
    // Corrected type and name
    // Corrected assignment

    // Corrected injected type

    public IActionResult CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        Enum? errorEnum, // The specific error enum (IamError, ProfilesError, etc.)
        string detailMessage) // The localized message from the application service
    {
        // Leverage the base ProblemDetailsFactory for initial creation
        var problemDetails = aspNetCoreProblemDetailsFactory.CreateProblemDetails( // Corrected usage
            controller.HttpContext,
            statusCode,
            errorEnum != null ? errorLocalizer[$"{errorEnum}"] : commonLocalizer["GenericError"],
            detail: detailMessage
        );


        problemDetails.Title =
            errorEnum != null ? errorLocalizer[$"{errorEnum}"] : commonLocalizer["GenericError"];
        problemDetails.Detail = detailMessage;
        problemDetails.Instance = controller.HttpContext.Request.Path;

        return controller.StatusCode(statusCode, problemDetails);
    }

    // Overload for when there's no specific error enum, just a generic message
    public IActionResult CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        string titleKey, // Key for localized title
        string detailKey, // Key for localized detail
        params object[] detailArgs)
    {
        var problemDetails = aspNetCoreProblemDetailsFactory.CreateProblemDetails( // Corrected usage
            controller.HttpContext,
            statusCode,
            commonLocalizer[titleKey],
            detail: errorLocalizer[detailKey, detailArgs],
            instance: controller.HttpContext.Request.Path
        );
        return controller.StatusCode(statusCode, problemDetails);
    }
}