using PayPal.Api;

namespace Diplom.MVC;

public  class PaypalConfiguration
{
    //Variables for storing the clientID and clientSecret key
    private readonly IConfiguration _config;
    public readonly string ClientId;  
    public readonly string ClientSecret;  
    //Constructor  
    public PaypalConfiguration(IConfiguration config) {  
        var _config = config;  
        ClientId = _config["clientId"];  
        ClientSecret = _config["clientSecret"];  
    }  
    // getting properties from the web.config  
    public Dictionary < string, string > GetConfig() {  
        return new Dictionary<string, string>
        {
            {"clientId", ClientId},
            {"clientSecret", ClientSecret}
        };  
    }  
    private string GetAccessToken() {  
        // getting accesstocken from paypal  
        string accessToken = new OAuthTokenCredential(ClientId, ClientSecret).GetAccessToken();  
        return accessToken;  
    }  
    public  APIContext GetApiContext() {  
        // return apicontext object by invoking it with the accesstoken  
        APIContext apiContext = new APIContext(GetAccessToken());  
        apiContext.Config = GetConfig();  
        return apiContext;  
    }  
}