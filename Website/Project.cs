namespace Website;

public record Project(string Name, string Url, string Description, List<string> Tags)
{
    public static readonly List<Project> Projects =
    [
        new Project(
            "🧪 Synesthesia",
            "https://github.com/SynesthesiaDev/Synesthesia",
            "C# Game Engine made with SDL3 and OpenGL inspired by osu!framework",
            ["C#", ".NET 10", "SDL3", "OpenGL", "BASS", "Zero-alloc"]
        ),
        new Project(
            "🌙 Nocturne",
            "https://github.com/SynesthesiaDev/Nocturne",
            "Lightweight and easy-to-use local database for C#",
            ["C#", ".NET 10", "Data Structures", "Persistence"]
        ),
        new Project(
            "🌿 Canopy",
            "https://github.com/SynesthesiaDev/Canopy",
            "Lightweight wallpaper engine that automatically switches your desktop based on real-world conditions",
            ["C#", ".NET 10"]
        ),
        new Project(
            "🌸 Chibi",
            "https://github.com/SynesthesiaDev/Chibi",
            "Tiny and type-safe managed Windows library for lifecycle management and window creation",
            ["C#", ".NET 10", "Win32 API"]
        ),
        new Project(
            "🧬 Codon",
            "https://github.com/SynesthesiaDev/Codon",
            "Explicit, version-aware serialization library for .NET with zero reflection or source generation",
            ["C#", ".NET 10", "DotNetty", "Binary Protocols", "Serialization"]
        ),
    ];
}