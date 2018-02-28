Feature: RoomStylerFeature
  In the feature drag and drop an item from the sidebar
  Furnish Your Room -> Dining Room, checks the correctly move it,
  that dimensions of item in description not null. Then the item
  is deleted and checks correctly it deleting.

  Scenario: DiningRoomItemFeature
    When I click the close button on the welcome dialog
    Then The welcome dialog closed
    When I click the button 'Furnish your room' on sidebar
    And I click the link 'Dining room' in the opened menu
    And I drag an item from sidebar to the work field
    Then The dragged item is displayed correctly on the work field
    When I click the dragged item on the work field
    Then The image on the sidebar is same with the image of the dragged item
    And The dimensions of the item on the sidebar not equal null
    When I delete the dragged item from the work field
    Then The work field is not contains the dragged item
    And The sidebar not contains the image of the dragged item
    And The scene information contains '0' in the all fields
