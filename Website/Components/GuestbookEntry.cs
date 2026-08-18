// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Codon.Binary;
using Codon.Optionals;
using Nocturne.Database.API;

namespace Website.Components;

public class GuestbookEntry(string name, string? url, string text, long time)
{
    public string Name { get; set; } = name;

    public string? Url { get; set; } = url;

    public string Text { get; set; } = text;
    public long Time { get; set; } = time;

    public static readonly IBinaryCodec<GuestbookEntry> BINARY_CODEC = BinaryCodecs.For<GuestbookEntry>()
        .Field(BinaryCodecs.STRING, g => g.Name)
        .Field(BinaryCodecs.STRING.Optional(), g => ToOptional(g.Url))
        .Field(BinaryCodecs.STRING, g => g.Text)
        .Field(BinaryCodecs.LONG, g => g.Time)
        .Build((name, url, text, time) => new GuestbookEntry(name, url.Value, text, time));

    public static readonly NocturneCollection<Guid, GuestbookEntry> DATABASE_COLLECTION = Program.NOCTURNE_DATABASE.For
    (
        collectionKey: "guestbook_entries",
        schemaVersion: 0,
        keySerializer: KeySerializers.GUID,
        valueSerializer: NocturneSerializer.FromCodec(BINARY_CODEC)
    );

    public static Optional<string> ToOptional(string? text)
    {
        return text is not null
            ? new Optional<string>(text)
            : new Optional<string>(null);
    }

    public string? Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) return "Name cannot be empty";
        if (string.IsNullOrWhiteSpace(Text)) return "Message cannot be empty";

        if (Name.Length > 24) return "Name is too long (max 24 chars)";
        if (!string.IsNullOrEmpty(Url) && Url.Length > 64) return "Website is too long (max 64 chars)";
        if (Text.Length > 128) return "Message is too long (max 128 chars)";

        if (string.IsNullOrEmpty(Url)) return null;

        if (!Uri.TryCreate(Url, UriKind.Absolute, out var uriResult) ||
            (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
        {
            return "Invalid website URL format or must use http:// or https://";
        }

        return null;
    }
}
