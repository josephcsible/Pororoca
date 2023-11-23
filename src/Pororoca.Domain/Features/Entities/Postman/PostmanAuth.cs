#nullable disable warnings

using System.Text.Json;
using static Pororoca.Domain.Features.Common.JsonConfiguration;

namespace Pororoca.Domain.Features.Entities.Postman;

internal enum PostmanAuthType
{
    // TODO: Rename enum values according to C# style convention,
    // but preserving JSON serialization and deserialization
    noauth,
    basic,
    oauth1,
    oauth2,
    bearer,
    digest,
    apikey,
    awsv4,
    hawk,
    ntlm
}

internal class PostmanAuth
{
    public PostmanAuthType Type { get; set; }

    public object? Basic { get; set; }

    public object? Bearer { get; set; }

    public object? Ntlm { get; set; }

    public (string basicAuthLogin, string basicAuthPwd) ReadBasicAuthValues()
    {
        static (string, string) ParseFromVariableArray(PostmanVariable[] arr) =>
            (arr.FirstOrDefault(p => p.Key == "username")?.Value ?? string.Empty,
             arr.FirstOrDefault(p => p.Key == "password")?.Value ?? string.Empty);

        if (Basic is JsonElement je)
        {
            if (je.ValueKind == JsonValueKind.Object)
            {
                var basic = je.Deserialize<PostmanAuthBasic>(options: MinifyingOptions);
                return (basic.Username ?? string.Empty, basic.Password ?? string.Empty);
            }
            else if (je.ValueKind == JsonValueKind.Array)
            {
                var basic = je.Deserialize<PostmanVariable[]>();
                return ParseFromVariableArray(basic);
            }
        }
        else if (Basic is PostmanVariable[] arr)
        {
            return ParseFromVariableArray(arr);
        }

        return (string.Empty, string.Empty);
    }

    public string ReadBearerAuthValue()
    {
        static string ParseFromVariableArray(PostmanVariable[] arr) =>
            arr.FirstOrDefault(p => p.Key == "token")?.Value ?? string.Empty;

        if (Bearer is JsonElement je)
        {
            if (je.ValueKind == JsonValueKind.Object)
            {
                var bearer = je.Deserialize<PostmanAuthBearer>(options: MinifyingOptions);
                return bearer.Token ?? string.Empty;
            }
            else if (je.ValueKind == JsonValueKind.Array)
            {
                var bearer = je.Deserialize<PostmanVariable[]>();
                return ParseFromVariableArray(bearer);
            }
        }
        else if (Bearer is PostmanVariable[] arr)
        {
            return ParseFromVariableArray(arr);
        }

        return string.Empty;
    }

    public (string login, string password, string domain, string workstation) ReadNtlmAuthValues()
    {
        static (string, string, string, string) ParseFromVariableArray(PostmanVariable[] arr) =>
            (arr.FirstOrDefault(p => p.Key == "username")?.Value ?? string.Empty,
             arr.FirstOrDefault(p => p.Key == "password")?.Value ?? string.Empty,
             arr.FirstOrDefault(p => p.Key == "domain")?.Value ?? string.Empty,
             arr.FirstOrDefault(p => p.Key == "workstation")?.Value ?? string.Empty);

        if (Ntlm is JsonElement je)
        {
            if (je.ValueKind == JsonValueKind.Object)
            {
                var ntlm = je.Deserialize<PostmanAuthNtlm>(options: MinifyingOptions);
                return (ntlm.Username ?? string.Empty, ntlm.Password ?? string.Empty, ntlm.Domain ?? string.Empty, ntlm.Workstation ?? string.Empty);
            }
            else if (je.ValueKind == JsonValueKind.Array)
            {
                var vars = je.Deserialize<PostmanVariable[]>();
                return ParseFromVariableArray(vars);
            }
        }
        else if (Ntlm is PostmanVariable[] arr)
        {
            return ParseFromVariableArray(arr);
        }

        return (string.Empty, string.Empty, string.Empty, string.Empty);
    }
}

internal class PostmanAuthBasic
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

internal class PostmanAuthBearer
{
    public string? Token { get; set; }
}

internal class PostmanAuthNtlm
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Domain { get; set; }
    public string? Workstation { get; set; }
}

#nullable enable warnings