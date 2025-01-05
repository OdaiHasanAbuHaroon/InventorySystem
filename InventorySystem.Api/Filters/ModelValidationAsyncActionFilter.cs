using InventorySystem.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InventorySystem.Api.Filters
{
    /// <summary>
    /// Action filter to perform model validation asynchronously. If the model state is invalid, it stops the execution pipeline and returns a standardized JSON error response.
    /// </summary>
    public class ModelValidationAsyncActionFilter : IAsyncActionFilter
    {
        /// <summary>
        /// Executes the model validation before proceeding with the action. 
        /// If the model state is valid, the action continues; otherwise, an error response is returned.
        /// </summary>
        /// <param name="context">The context for action execution, containing the model state and other information.</param>
        /// <param name="next">Delegate to execute the next action filter or the action itself.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                // Proceed to the next action in the pipeline
                await next();
            }
            else
            {
                // Create a generic response object for invalid model state
                GenericResponse<bool> result = new()
                {
                    Success = false,
                };

                // Extract error messages from the model state
                var modelStateValues = context.ModelState?.Values.SelectMany(v => v.Errors);

                if (modelStateValues != null)
                {
                    foreach (var value in modelStateValues)
                    {
                        // Add error messages to the response
                        result.Messages.Add(new ResponseMessage(ResponseMessageType.Error, value.ErrorMessage));
                    }
                }

                // Set the result as a JSON response with the error details
                result.Success = false;
                context.Result = new JsonResult(result);
            }
        }
    }
}
