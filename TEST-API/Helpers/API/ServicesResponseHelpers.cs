using TEST_API.Models.API;

namespace TEST_API.Helpers.API
{
    public static class ServicesResponseHelpers<T>
    {
        public static ServiceResults<T> Success(int code, string message, T? data)
           => new ServiceResults<T>(true, code, message, data);

        public static ServiceResults<T> Error(int code, string message, T? data)
            => new ServiceResults<T>(false, code, message, data);
    }
}
