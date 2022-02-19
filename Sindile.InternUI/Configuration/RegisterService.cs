using Sindile.InternUI.APIHelper;

namespace Sindile.InternUI.Configuration
{
  public static class RegisterService
  {
    public static void AddService(this IServiceCollection services)
    {
      services.AddScoped(typeof(IAPIService), typeof(APIService));
    }
  }
}
