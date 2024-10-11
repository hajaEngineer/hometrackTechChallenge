using PurgomalumAPIAutomation.Models;

namespace PurgomalumAPIAutomation.Services
{
    public interface IPurgomalumService
    {
        ProfanityCheckJsonResponse? CheckProfanityJson(string text, List<string>? add = null, string? fillText = null, char? fillChar = null);
        ContainsProfanityResponse? CheckContainsProfanity(string text);
    }
}