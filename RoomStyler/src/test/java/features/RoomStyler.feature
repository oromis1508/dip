Feature: RoomStylerFeature
  In the feature drag and drop an item from the sidebar
  Furnish Your Room -> Dining Room, checks the correctly move it,
  that dimensions of item in description not null. Then the item
  is deleted and checks correctly it deleting. Images to compare are in the resources

  Scenario: DiningRoomItemFeature
    When I click the close button on the welcome dialog
    Then The welcome dialog closed
    When I click the button 'Furnish your room' on sidebar
      And I click the link 'Dining room' in the opened menu
      And I drag the item 'dragItem' from sidebar to the work field 'dropItem'
    Then The item 'placedItem' is displayed correctly on the work field
    When I click the item 'placedItem' on the work field
    Then The image on the sidebar is same with the image 'dragItem'
      And The dimensions of the item on the sidebar not equal '0'
    When I click 'deleteItemIcon' on the item from the work field
    Then The work field is not contains the item 'placedItem'
      And The sidebar not contains the item 'dragItem'
      And The scene information contains '0' in the all fields
