package webdriver.elements;

public class Cell {

    private int row;
    private int column;

    public Cell(int row, int column) {
        setRow(row);
        setColumn(column);
    }

    public int getRow() {
        return row;
    }

    public int getColumn() {
        return column;
    }

    public void setRow(int row) {
        this.row = row;
    }

    public void setColumn(int column) {
        this.column = column;
    }

    @Override
    public boolean equals(Object obj) {
        return ((Cell) obj).getColumn() == getColumn() && ((Cell) obj).getRow() == getRow();
    }

    public Cell getCopy() {
        return new Cell(row, column);
    }
}
