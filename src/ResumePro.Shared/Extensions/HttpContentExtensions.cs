using Newtonsoft.Json.Serialization;

namespace ResumePro.Shared.Extensions;

public static class HttpContentExtensions
{
    public static T DeserializeObject<T>(this HttpContent content)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Include
        };

        var jsonContent = content.ReadAsStringAsync().Result;

        var obj = JsonConvert.DeserializeObject<T>(jsonContent);
        return obj!;
    }
}