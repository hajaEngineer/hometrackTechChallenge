using Newtonsoft.Json;
using RestSharp;
using PurgomalumAPIAutomation.Models;
using PurgomalumAPIAutomation.Utilities;

namespace PurgomalumAPIAutomation.Services
{
    public class PurgomalumService : IPurgomalumService
    {
        private readonly string _baseUrl;

        public PurgomalumService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public ProfanityCheckJsonResponse? CheckProfanityJson(string text, List<string>? add = null, string? fillText = null, char? fillChar = null)
        {
            var request = new RestRequest("service/json", Method.Get);
            request.AddQueryParameter("text", text);

            // Add optional parameters if provided
            if (add != null && add.Any())
            {
                request.AddQueryParameter("add", string.Join(",", add));
            }

            if (!string.IsNullOrWhiteSpace(fillText))
            {
                request.AddQueryParameter("fill_text", fillText);
            }

            if (fillChar.HasValue)
            {
                request.AddQueryParameter("fill_char", fillChar.Value.ToString());
            }

            IList<Parameter> parameters = new List<Parameter>();

            foreach (var parameter in request.Parameters)
            {
                parameters.Add(parameter);
            }

            // Using HttpClientHelper to execute the request
            var response = HttpClientHelper.ExecuteRequestAsync(_baseUrl, "service/json", Method.Get, parameters).Result;

            Logger.Log($"Request to CheckProfanityJson: {response.Content}");

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    var apiResponse = JsonConvert.DeserializeObject<ProfanityCheckJsonResponse>(response.Content);
                    return apiResponse;
                }
                catch (JsonException)
                {
                    Logger.Log("Error parsing JSON response in CheckProfanityJson.");
                    return new ProfanityCheckJsonResponse
                    {
                        Result = string.Empty,
                        Error = "Unexpected response format."
                    };
                }
            }
            else
            {
                Logger.Log($"Error in CheckProfanityJson: {response.ErrorMessage}");
                return new ProfanityCheckJsonResponse
                {
                    Result = string.Empty,
                    Error = response.ErrorMessage ?? "Unknown error occurred."
                };
            }
        }

        public ContainsProfanityResponse? CheckContainsProfanity(string text)
        {
            var request = new RestRequest("service/containsprofanity", Method.Get);
            request.AddHeader("Accept", "text/plain");
            request.AddQueryParameter("text", text);
            IList<Parameter> parameters = new List<Parameter>();

            foreach (var parameter in request.Parameters) { 
                parameters.Add(parameter);
            }

            // Using HttpClientHelper to execute the request
            var response = HttpClientHelper.ExecuteRequestAsync(_baseUrl, "service/containsprofanity", Method.Get, parameters).Result;

            Logger.Log($"Request to CheckContainsProfanity: {response.Content}");

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    var containsProfanity = bool.Parse(response.Content.Trim());
                    return new ContainsProfanityResponse
                    {
                        ContainsProfanity = containsProfanity
                    };
                }
                catch (FormatException)
                {
                    Logger.Log("Error parsing boolean response in CheckContainsProfanity.");
                    return new ContainsProfanityResponse
                    {
                        ContainsProfanity = false,
                        Error = "Unexpected response format."
                    };
                }
            }
            else
            {
                Logger.Log($"Error in CheckContainsProfanity: {response.ErrorMessage}");
                return new ContainsProfanityResponse
                {
                    ContainsProfanity = false,
                    Error = response.ErrorMessage ?? "Unknown error occurred."
                };
            }
        }
    }
}