namespace github.api;

public static class UsersEndpoint
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        // Example: https://api.github.com/users/hugonguyen
        // Doc: https://docs.github.com/en/rest/users/users?apiVersion=2022-11-28#get-a-user
        app.MapGet("api/users/{username}", async (
            string username,
            GitHubService gitHubService) =>
        {
            var content = await gitHubService.GetByUsernameAsync(username);

            return Results.Ok(content);
        });
    }
}
