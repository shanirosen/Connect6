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

        public override bool Equals(object obj)
        {
            if(obj is BoardPosition)
            {
                BoardPosition pos2 = obj as BoardPosition;

                return this.Row == pos2.Row && this.Column == pos2.Column;
            }
            return false;
        }

        public override string ToString()
        {
            return string.Format("[{0},{1}]", Row, Column);
        }
    }
}
