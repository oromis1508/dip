Feature: RoomStylerFeature
  In the feature drag and drop an item from the sidebar
  Furnish Your Room -> Dining Room, checks the correctly move it,
  that dimensions of item in description not null. Then the item
  is deleted and checks correctly it deleting.

  Scenario: DiningRoomItemFeature
    Given Page 'https://roomstyler.com/3dplanner' opened
    When The close button clicked in the welcome dialog
    Then The welcome dialog closed
    When Clicked the button 'Furnish your room' on sidebar
    And Clicked the link 'Dining room'
    And Drag an item to the work field
    Then The dragged item is displayed correctly on the work field
    When The item on the work field clicked
    Then The image on the sidebar is same with the image of the dragged item
    And The dimensions of the item on the sidebar not equal null
    When The item deleted from the work field
    Then The work field is empty
    And The sidebar not contains the image of the dragged item
    And The scene information contains '0' in the all fields
