using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

//KRÄVS FÖR ATT INLOGG MED JWT SKA FUNGERA I SCALAR.
internal sealed class BearerSecuritySchemeTransformer(
    IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var schemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        
        // Only proceed if Bearer authentication is configured
        if (schemes.Any(s => s.Name == "Bearer"))
        {
            // Define the bearer secuyrity scheme
            var bearerScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // "bearer" refers to the header name here
                In = ParameterLocation.Header,
                BearerFormat = "JWT"
            };

            // Ensure components are initialized
            document.Components ??= new OpenApiComponents();

            // Add the scheme to the document components
            document.AddComponent("bearer", bearerScheme);

            // Create a security requirement referencing the scheme
            var securityRequirement = new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            };

            // Apply the requirement to all operations
            foreach (var operation in document.Paths.Values
                .Where(x => x.Operations != null)
                .SelectMany(p => p.Operations!))
            {
                operation.Value.Security ??= new List<OpenApiSecurityRequirement>();
                operation.Value.Security.Add(securityRequirement);
            }
        }
    }
}
