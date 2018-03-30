package secretCloset.page;

import frame.Helpers;

public enum CategoryMenu {

    CLOTHES("Clothes", "%s"),
    CHOES("Shoes", "%s2"),
    BAGS("Bags", "%s3"),
    ACCESSORIES("Accessories", "%s4"),
    BABY_AND_KIDS("Baby and kids", "%s5"),
    SPECIAL("Special", "%s6");

    private String name;
    private String id;
    private String menuIdPrefix = "imageView";

    CategoryMenu(String name, String id) {
        this.name = name;
        this.id = String.format(id, menuIdPrefix);
    }

    @Override
    public String toString() {
        return id;
    }

    public static String getByName(String name) {
        for (CategoryMenu categoryMenu : values()) {
            if (categoryMenu.name.equals(name)) {
                return categoryMenu.toString();
            }
        }
        Helpers.logger.warn(String.format("Category menu with name %s not found", name));
        return null;
    }
}
