package forms.algorithm;

public enum Side {

    Left, Right, Top, Bottom;

    private static Side[] values = values();

    public Side next()
    {
        return values[(this.ordinal()+1) % values.length];
    }
}
