using System.Text.RegularExpressions;

namespace CustomerRecords.Validation
{
    public static class NameInputValidation
    {
        private static readonly Regex template = new Regex(@"[a-zA-Z]");
        public static bool Validate(string name)
        {
            return template.IsMatch(name);
        }
    }
}
