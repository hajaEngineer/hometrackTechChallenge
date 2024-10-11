Feature: Profanity Check
  In order to ensure content is appropriate
  As a user of the profanity-checking service
  I want to verify that the service correctly identifies profanity in the input text.

  # Basic Scenarios for JSON Endpoint
  Scenario: Check for profanity in text containing bad words (JSON)
    Given I have the text "The word Bastard is an example of profanity."
    And I want to use the "json" endpoint
    When I check the text
    Then the response should indicate profanity is found

  Scenario: Check for no profanity in clean text (JSON)
    Given I have the text "This is a clean test."
    And I want to use the "json" endpoint
    When I check the text
    Then the response should indicate profanity is not found

  # Basic Scenarios for ContainsProfanity Endpoint
  Scenario: Check for profanity in text containing bad words (ContainsProfanity)
    Given I have the text "The word Bastard is an example of profanity."
    And I want to use the "containsprofanity" endpoint
    When I check the text
    Then the response should indicate profanity is found

  Scenario: Check for no profanity in clean text (ContainsProfanity)
    Given I have the text "This is a clean test."
    And I want to use the "containsprofanity" endpoint
    When I check the text
    Then the response should indicate profanity is not found

  # Advanced Scenarios for JSON Endpoint
  Scenario: Check for profanity with additional words added to the list (JSON)
    Given I have the text "This is a custom test with Bastard1 and Bastard2."
    And I have added the following words to the profanity list:
      | Word     |
      | Bastard1 |
      | Bastard2 |
    And I want to use the "json" endpoint
    When I check the text
    Then the response should indicate profanity is found


  Scenario: Check for profanity with custom fill text (JSON)
    Given I have the text "The word Bastard is an example of profanity."
    And I have set the fill text to "[REDACTED]"
    And I want to use the "json" endpoint
    When I check the text
    Then the response should indicate profanity is found

    
  Scenario: Check for error with long fill text
    Given I have the text "this is some test input"
    And I have set the fill text to "this is curiously long replacement text"
    And I want to use the "json" endpoint
    When I check the text
    Then the response should indicate an error for fill text

  Scenario: Check for profanity with custom fill character (JSON)
    Given I have the text "The word Bastard is an example of profanity."
    And I have set the fill character to "*"
    And I want to use the "json" endpoint
    When I check the text
    Then the response should indicate profanity is found

  Scenario: Check for profanity with multiple advanced options (JSON)
    Given I have the text "The word Bastard is an example of profanity with Bastard1."
    And I have added the following words to the profanity list:
      | Word     |
      | Bastard1 |
    And I have set the fill text to "[REDACTED]"
    And I have set the fill character to "#"
    And I want to use the "json" endpoint
    When I check the text
    Then the response should indicate profanity is found

  # Advanced Scenarios for ContainsProfanity Endpoint
  Scenario: Check for profanity with additional words added to the list (ContainsProfanity)
    Given I have the text "This is a custom test with Bastard and Bastard1."
    And I have added the following words to the profanity list:
      | Word    |
      | Bastard |
      | Bastard1|
    And I want to use the "containsprofanity" endpoint
    When I check the text
    Then the response should indicate profanity is found

  Scenario: Check for profanity with custom fill text (ContainsProfanity)
    Given I have the text "The word Bastard is an example of profanity."
    And I have set the fill text to "[REDACTED]"
    And I want to use the "containsprofanity" endpoint
    When I check the text
    Then the response should indicate profanity is found

  Scenario: Check for profanity with custom fill character (ContainsProfanity)
    Given I have the text "The word Bastard is an example of profanity."
    And I have set the fill character to "*"
    And I want to use the "containsprofanity" endpoint
    When I check the text
    Then the response should indicate profanity is found

  Scenario: Check for profanity with multiple advanced options (ContainsProfanity)
    Given I have the text "The word Bastard is an example of profanity with Bastard1."
    And I have added the following words to the profanity list:
      | Word     |
      | Bastard1 |
    And I have set the fill text to "[REDACTED]"
    And I have set the fill character to "#"
    And I want to use the "containsprofanity" endpoint
    When I check the text
    Then the response should indicate profanity is found

