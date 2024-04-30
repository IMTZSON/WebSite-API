namespace TEST_API.Helpers.API
{
    public static class ServerResponseHelpers
    {
        public static string GetErrorMessage(int code)
        {
            return code switch
            {
                200 => "SUCCESS",
                201 => "CREATED",
                400 => "BAD_REQUEST",
                401 => "UNAUTHORIZED",
                403 => "FORBIDDEN",
                404 => "NOT_FOUND",
                409 => "CONFLICT",
                425 => "TOO_EARLY",
                500 => "INTERNAL_SERVER_ERROR",
                _ => "",
            };
        }
    }
}
