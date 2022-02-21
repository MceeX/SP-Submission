namespace Sindile.InternUI.Settings
{

  /// <summary>
  /// Represents ui authorisations
  /// </summary>
  public class Security
  {

    /// <summary>
    /// Gets or sets the OAuth authority
    /// </summary>
    public static string STSAuthority { get; set; }

    /// <summary>
    /// Gets or sets the audience
    /// </summary>
    public static string STSAudience { get; set; }

    /// <summary>
    /// Gets or sets the client id used by swagger
    /// </summary>
    public static string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the client secret
    /// </summary>
    public static string ClientSecret { get; set; }

    /// <summary>
    /// Gets or sets the STS authorize endpoint
    /// </summary>
    public static string SwaggerAuthorizationUrl { get; set; }
  }

}
