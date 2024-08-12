using System.Text.Json;

namespace DropdownTest
{
    public static class TypeExtensions
    {

        static JsonSerializerOptions serializerOptions = new JsonSerializerOptions { WriteIndented = true };


        // Methods

        /// <summary>
        /// Performs a case insensitive string comparison
        /// </summary>
        public static bool Like(this string? str, string? stringToCompare)
        {
            //handle nulls
            if (str == null)
                return (stringToCompare == null);
            if (stringToCompare == null) //first string is not null if execution reaches this point
                return false;
            
            //compare
            return str.Equals(stringToCompare, System.StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines if a specified substring occurs within this string, ignoring differences in case
        /// </summary>
        public static bool ContainsLike(this string str, string stringToCompare)
        {
            return (str.IndexOf(stringToCompare, StringComparison.OrdinalIgnoreCase) != -1);
        }


        public static string Serialize(this object? obj, bool indent = false)
        {
            if (obj == null)
                return string.Empty;

            var options = new JsonSerializerOptions { WriteIndented = indent };

            try
            {
                return JsonSerializer.Serialize(obj, options);
            }
            catch (JsonException)
            {
                return "(Unable to serialize)";
            }
        }

    }
}

