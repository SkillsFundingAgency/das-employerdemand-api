using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SFA.DAS.EmployerDemand.Api.Infrastructure
{
    public class JsonPatchDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var schemas = swaggerDoc.Components.Schemas.ToList();

            var patchOperation = swaggerDoc.Components.Schemas.ToList()
                .FirstOrDefault(s => s.Key.ToLower() == "operation");

            if (patchOperation.Key != default)
                patchOperation.Value.Properties.Remove("operationType");

        }
    }
}