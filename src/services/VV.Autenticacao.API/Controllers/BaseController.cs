using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace VV.Autenticacao.API.Controllers
{
    [ApiController]
    public abstract class BaseController : Controller
    {
        private readonly ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
                return Ok(result);

            return BadRequest(
                new ValidationProblemDetails(
                    new Dictionary<string, string[]>() { { "Mensagens", Errors.ToArray() } }
                )
            );
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            IEnumerable<ModelError> errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (ModelError error in errors)
                AddErrors(error.ErrorMessage);

            return CustomResponse();
        }

        protected bool ValidOperation() => !Errors.Any();

        protected void AddErrors(string errorMessage) => Errors.Add(errorMessage);

        protected void ClearErrors() => Errors.Clear();
    }
}
