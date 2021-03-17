using System.Data.SqlClient;

namespace VietUSA.Repository.Common.Functions
{
    public static class RepositoryCommonFunctions
    {
        public static int ConvertSqlParameterToInt(SqlParameter input)
        {
            try
            {
                if (input.Value == null) return -1;
                int output;
                var isConverted = int.TryParse(input.Value.ToString(), out output);
                {
                    if (isConverted)
                    {
                        return output;
                    }
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }
        public static bool ConvertSqlParameterToBoolean(SqlParameter input)
        {
            try
            {
                if (input.Value == null) return false;
                bool output;
                var isConverted = bool.TryParse(input.Value.ToString(), out output);
                {
                    if (isConverted)
                    {
                        return output;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static string ConvertSqlParameterToString(SqlParameter input)
        {
            try
            {
                if (input == null) return string.Empty;
                return input.Value.ToString();
            }
            catch
            {

                return string.Empty;
            }
        }

    }
}
