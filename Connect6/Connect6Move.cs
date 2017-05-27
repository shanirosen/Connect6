using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect6
{
    class Connect6Move
    {
        public readonly BoardPosition Pos1;
        public readonly BoardPosition Pos2;

        public Connect6Move(BoardPosition pos1, BoardPosition pos2)
        {
            this.Pos1 = pos1;
            this.Pos2 = pos2;
        }

       
        public Connect6Move OppositeMove
        {
            get
            {
                return new Connect6Move(Pos2, Pos1);
            }
        }

        public bool IsEqual(Connect6Move m2)
        {
			return Pos1.Row == m2.Pos1.Row && Pos1.Column == m2.Pos1.Column && Pos2.Row == m2.Pos2.Row && Pos2.Column == m2.Pos2.Column;
        }


        public override string ToString()
        {
            return string.Format("[Connect6Move] {0}, {1}", Pos1, Pos2);
        }



    }

}