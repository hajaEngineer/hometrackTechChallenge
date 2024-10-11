using RestSharp;

namespace PurgomalumAPIAutomation.Utilities
{
    public static class HttpClientHelper
    {
        public static async Task<RestResponse> ExecuteRequestAsync(string url, string endpoint, Method method, IList<Parameter> parameters)
        {
            var client = new RestClient(url);
            var request = new RestRequest(endpoint, method);

            // Ensure parameters are added
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.AddParameter(parameter.Name, parameter.Value, parameter.Type);
                }
            }

            // Execute the request asynchronously
            RestResponse response = await client.ExecuteAsync(request);
            return response;
        }
    }
}