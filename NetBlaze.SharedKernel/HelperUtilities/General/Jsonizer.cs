// Ignore Spelling: Jsonizer Jsonize Dejsonize

using System.Text.Json;

namespace NetBlaze.SharedKernel.HelperUtilities.General
{
    public static class Jsonizer
    {
        public static string Jsonize<T>(T objectToSerialize)
        {
            ArgumentNullException.ThrowIfNull(objectToSerialize);

            return JsonSerializer.Serialize(objectToSerialize, CustomJsonSerializerOptions._jsonSerializerOptions);
        }

        public static T? Dejsonize<T>(string stringToDeserialize)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(stringToDeserialize);

            return JsonSerializer.Deserialize<T>(stringToDeserialize, CustomJsonSerializerOptions._jsonSerializerOptions);
        }
    }
}
