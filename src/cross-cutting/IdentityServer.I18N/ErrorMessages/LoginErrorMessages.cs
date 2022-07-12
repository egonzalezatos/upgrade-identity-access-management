namespace IdentityServer.I18N.ErrorMessages
{
    public static class LoginErrorMessages
    {
        public static string PARAM_REQUIRED(string param) => $"The {param} param is required";
        public static string INVALID_CREDENTIALS() => "Invalid username or password";
        public static string USER_NOT_FOUND() => "This username does not exist.";

    }
}