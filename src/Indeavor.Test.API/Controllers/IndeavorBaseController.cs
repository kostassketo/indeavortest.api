using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Indeavor.Test.API.Controllers
{
    public abstract class IndeavorBaseController : ControllerBase
    {
        protected Task<BadRequestObjectResult> HandleInvalidModelAsync()
        {
            return Task.FromResult(BadRequest(ModelState.Values.SelectMany(modelState => modelState.Errors)));
        }
    }
}