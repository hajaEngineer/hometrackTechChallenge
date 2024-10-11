namespace PurgomalumAPIAutomation.Models
{
    public class ContainsProfanityResponse
    {
        public bool ContainsProfanity { get; set; }
        public string? Error { get; set; } // Error message, if any
    }
}