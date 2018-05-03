namespace smart.framework.Elements
{
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        protected bool Equals(Cell other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public Cell GetCopy()
        {
            return new Cell(Row, Column);
        }
    }
}
