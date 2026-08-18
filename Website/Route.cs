namespace Website;

public record Route(string Name, string Url)
{
    public static readonly List<Route> Routes =
    [
        new Route("Home", "/"),
        new Route("Projects", "/projects"),
        new Route("Guestbook", "/guestbook"),
    ];
}