package forms.algorithm;

public enum CellStatus {

    HIT("cell__hit"), KILL("cell__done"), EMPTY("cell__empty");

    private final String text;

    CellStatus(final String text) {
        this.text = text;
    }

    @Override
    public String toString() {
        return text;
    }
}
