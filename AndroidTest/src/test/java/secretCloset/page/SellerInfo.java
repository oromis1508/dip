package secretCloset.page;

import frame.Helpers;

public enum SellerInfo {

    NAME("Name", "%sName"),
    RESPONSE_RATE("Response rate", "%sRate"),
    RESPONSE_TIME("Response time", "%sTime"),
    RATING("Rating", "%sRating"),
    CITY("City", "%sCity");

    private String name;
    private String id;
    private String sellerInfoIdPrefix = "tvItemSeller";

    SellerInfo(String name, String id) {
        this.name = name;
        this.id = String.format(id, sellerInfoIdPrefix);
    }

    @Override
    public String toString() {
        return id;
    }

    public static String getByName(String name) {
        for (SellerInfo sellerInfo : values()) {
            if (sellerInfo.name.equals(name)) {
                return sellerInfo.toString();
            }
        }
        Helpers.logger.warn(String.format("Seller info with name %s not found", name));
        return null;
    }
}
