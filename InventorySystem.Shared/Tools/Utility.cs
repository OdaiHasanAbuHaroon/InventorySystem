using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace InventorySystem.Shared.Tools
{
    public static class Utility
    {
        private static readonly string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string NumericChars = "0123456789";
        private static readonly string SpecialChars = "!@#$%^&*()_-+=<>?";

        public static string GenerateRandomPassword(int length = 12, bool includeLowercase = true, bool includeUppercase = true, bool includeNumeric = true, bool includeSpecial = true)
        {
            string charSet = string.Empty;

            if (includeLowercase)
                charSet += LowercaseChars;

            if (includeUppercase)
                charSet += UppercaseChars;

            if (includeNumeric)
                charSet += NumericChars;

            if (includeSpecial)
                charSet += SpecialChars;

            if (string.IsNullOrEmpty(charSet))
                throw new ArgumentException("At least one character set must be selected.");

            Random random = new();
            StringBuilder password = new();

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(0, charSet.Length);
                password.Append(charSet[randomIndex]);
            }

            return password.ToString();
        }

        public static string GenerateRandomPassword()
        {
            return GenerateRandomPassword(10, true, true, true, false);
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            List<string> columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();

            PropertyInfo[] properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                T? objT = Activator.CreateInstance<T>();

                foreach (PropertyInfo property in properties)
                {
                    if (columnNames.Contains(property.Name))
                    {
                        PropertyInfo? propertyInfo = objT?.GetType().GetProperty(property.Name);
                        if (propertyInfo != null)
                        {
                            // Check if property is nullable
                            Type? underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                            if (underlyingType != null)
                            {
                                property.SetValue(objT,
                                    row[property.Name] == DBNull.Value ? null : Convert.ChangeType(row[property.Name], underlyingType));
                            }
                            else
                            {
                                property.SetValue(objT,
                                    row[property.Name] == DBNull.Value
                                        ? null
                                        : Convert.ChangeType(row[property.Name], propertyInfo.PropertyType));
                            }
                        }
                    }
                }

                return objT;
            }).ToList();
        }

        public static T ObjectCopy<T, U>(U source, T target) where T : class where U : class
        {
            // Get the type of source object and fetch its properties
            Type typeSource = source.GetType();
            PropertyInfo[] propertyInfoSource = typeSource.GetProperties();

            // Get the type of target object and fetch its properties
            Type typeTarget = target.GetType();
            PropertyInfo[] propertyInfoTarget = typeTarget.GetProperties();

            foreach (PropertyInfo property in propertyInfoSource)
            {
                // Check if the source property exists in target
                var prop = propertyInfoTarget.FirstOrDefault(p => p.Name == property.Name);

                // If the property exists in target, then copy
                prop?.SetValue(target, property.GetValue(source));
            }

            return target;
        }

        public static string GenerateMD5String(string inputString)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static string GenerateMD5String(string inputString, string key)
        {
            using (MD5 md5 = MD5.Create())
            {
                string inputWithKey = key + inputString.Replace(".", "#");  // Concatenating key with input
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputWithKey);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static string GenerateSha512Hash(string input)
        {
            // Convert the input string to a byte array and compute the hash.
            using (var sha512 = SHA512.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha512.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string.
                StringBuilder builder = new();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static DateTime ConvertToUserTimezone(DateTime dateTime, string userTimezoneId)
        {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimezoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, userTimeZone);
        }
    }
}
