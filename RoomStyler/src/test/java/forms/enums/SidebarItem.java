package forms.enums;

public enum SidebarItem {

    BUILD_ROOM_LAYOUT("Build room layout"),
    FURNISH_YOUR_ROOM("Furnish your room"),
    DECORATE_YOUR_ROOM("Decorate your room"),
    MANAGE_LIGHTS("Manage lights"),
    POPULATE_YOUR_LIST("Populate your list"),
    PROPERTIES("Properties"),
    HIDE_PANEL("Hide panel");

    private final String text;

    SidebarItem(final String text) {
        this.text = text;
    }

    @Override
    public String toString() {
        return text;
    }

    public static SidebarItem getMenu(String menuName) {
        for (SidebarItem item :values()) {
            if(item.text.equals(menuName)) {
                return item;
            }
        }
        return null;
    }
}
