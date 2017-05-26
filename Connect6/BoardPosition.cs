using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect6
{
    class BoardPosition
    {
        public readonly int Row;
        public readonly int Column;

        public BoardPosition(int row, int column)
        {
            // TODO: Complete member initialization
            this.Row = row;
            this.Column = column;
        }

        public BoardPosition Right
        {
            get
            {
                return new BoardPosition(Row + 1, Column);
            }
        }

        public BoardPosition Down
        {
            get
            {
                return new BoardPosition(Row, Column + 1);
            }

        }

        public BoardPosition Diagonal
        {
            get
            {
                return new BoardPosition(Row + 1, Column + 1);
            }

        }

        public BoardPosition Opposite
        {
            get
            {
                return new BoardPosition(Column, Row);
            }
        }

        public override string ToString()
        {
            return string.Format("[BoardPosition: Row={0}, Col={1}", Row, Column);
        }
    }
}
