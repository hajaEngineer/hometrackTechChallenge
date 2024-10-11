using System;
using System.Collections.Generic;
using PurgomalumAPIAutomation.Models;
using PurgomalumAPIAutomation.Services;
using TechTalk.SpecFlow;
using Xunit;

namespace PurgomalumAPIAutomation.Steps
{
    [Binding]
    public class PurgomalumSteps
    {
        private readonly IPurgomalumService _purgomalumService;
        private string? _inputText; // Nullable
        private List<string>? _additionalWords;
        private string? _fillText;
        private char? _fillChar;
        private ProfanityCheckJsonResponse? _jsonResponse; // Nullable
        private ContainsProfanityResponse? _containsProfanityResponse; // Nullable
        private string _selectedEndpoint = "json"; // Default endpoint

        public PurgomalumSteps()
        {
            _purgomalumService = new PurgomalumService("https://www.purgomalum.com/");
        }

        [Given(@"I have the text ""(.*)""")]
        public void GivenIHaveTheText(string text)
        {
            _inputText = text;
        }

        [Given(@"I have added the following words to the profanity list:")]
        public void GivenIHaveAddedTheFollowingWordsToTheProfanityList(Table table)
        {
            _additionalWords = new List<string>();
            foreach (var row in table.Rows)
            {
                _additionalWords.Add(row["Word"]);
            }
        }

        [Given(@"I have set the fill text to ""(.*)""")]
        public void GivenIHaveSetTheFillTextTo(string fillText)
        {
            _fillText = fillText;
        }

        [Given(@"I have set the fill character to ""(.)""")]
        public void GivenIHaveSetTheFillCharacterTo(char fillChar)
        {
            _fillChar = fillChar;
        }

        [Given(@"I want to use the ""(.*)"" endpoint")]
        public void GivenIWantToUseTheEndpoint(string endpoint)
        {
            if (endpoint.ToLower() == "json")
            {
                _selectedEndpoint = "json";
            }
            else if (endpoint.ToLower() == "containsprofanity")
            {
                _selectedEndpoint = "containsprofanity";
            }
            else
            {
                throw new ArgumentException($"Unsupported endpoint: {endpoint}");
            }
        }

        [When(@"I check the text")]
        public void WhenICheckTheText()
        {
            if (string.IsNullOrWhiteSpace(_inputText))
            {
                throw new Exception("Input text is null or empty. Make sure to set it before checking.");
            }

            if (_selectedEndpoint == "json")
            {
                _jsonResponse = _purgomalumService.CheckProfanityJson(_inputText, _additionalWords, _fillText, _fillChar);
            }
            else if (_selectedEndpoint == "containsprofanity")
            {
                _containsProfanityResponse = _purgomalumService.CheckContainsProfanity(_inputText);
            }
            else
            {
                throw new ArgumentException($"Unsupported endpoint: {_selectedEndpoint}");
            }
        }

        [Then(@"the response should indicate profanity is found")]
        public void ThenTheResponseShouldIndicateProfanityIsFound()
        {
            if (_selectedEndpoint == "json")
            {
                Assert.NotNull(_jsonResponse); // xUnit's Assert
                if (_jsonResponse.Error != null)
                {
                    Assert.False(false, $"API Error: {_jsonResponse.Error}");
                }

                // Determine if profanity was found by comparing input and result
                Assert.NotNull(_inputText);
                Assert.NotNull(_jsonResponse.Result);
                Assert.NotEqual(_inputText, _jsonResponse.Result); // If different, profanity was found
            }
            else if (_selectedEndpoint == "containsprofanity")
            {
                Assert.NotNull(_containsProfanityResponse);
                if (_containsProfanityResponse.Error != null)
                {
                    Assert.True(_containsProfanityResponse.ContainsProfanity, $"API Error: {_containsProfanityResponse.Error}");
                }

                Assert.True(_containsProfanityResponse.ContainsProfanity, "Expected profanity to be found, but it was not.");
            }
            else
            {
                throw new ArgumentException($"Unsupported endpoint: {_selectedEndpoint}");
            }
        }

        [Then(@"the response should indicate profanity is not found")]
        public void ThenTheResponseShouldIndicateProfanityIsNotFound()
        {
            if (_selectedEndpoint == "json")
            {
                Assert.NotNull(_jsonResponse);
                if (_jsonResponse.Error != null)
                {
                    Assert.True(false, $"API Error: {_jsonResponse.Error}");
                }

                // Determine if profanity was not found by comparing input and result
                Assert.NotNull(_inputText);
                Assert.NotNull(_jsonResponse.Result);
                Assert.Equal(_inputText, _jsonResponse.Result); // If same, no profanity was found
            }
            else if (_selectedEndpoint == "containsprofanity")
            {
                Assert.NotNull(_containsProfanityResponse);
                if (_containsProfanityResponse.Error != null)
                {
                    Assert.False(false, $"API Error: {_containsProfanityResponse.Error}");
                }

                Assert.False(_containsProfanityResponse.ContainsProfanity, "Expected no profanity to be found, but it was.");
            }
            else
            {
                throw new ArgumentException($"Unsupported endpoint: {_selectedEndpoint}");
            }
        }
    }
}