Feature: SecretClosetFeature
  In the feature checks the android application "Secret Closet".
  First checks correctness choosing the region in menu in top right of the main app activity.
  After which checks city of found good. It must equal the selected region.

  Scenario: Select region in app and check the region of a good
    When I click the label to select region on the main activity
      And I click ok on the please do not forget activity
      And I input into the search field 'Toronto' on the select region activity
      And I click on the found label 'Toronto' on the select region activity
    Then The label on the main activity equal 'Toronto'
    When I click the category 'Special' on the main activity
      And I click 'Gifts for her' on the panel with the good type on the special search activity
      And I click the search button on the special search activity
      And I click any of the found goods on the goods list activity
    Then The label 'City' of the good is 'Toronto' on the good info activity
