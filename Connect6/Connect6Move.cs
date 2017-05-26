using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect6
{
    class Connect6Move
    {
        private BoardPosition Pos1;
        private BoardPosition Pos2;

        public Connect6Move(BoardPosition pos1, BoardPosition pos2)
        {
            this.Pos1 = pos1;
            this.Pos2 = pos2;
        }

        public BoardPosition GetPos1()
        {
            return Pos1;
        }
        public BoardPosition GetPos2()
        {
            return Pos2;
        }
        public override string ToString()
        {
            return string.Format("[Connect6Move] Pos1={0}, Pos2={1}", Pos1.ToString(), Pos2.ToString());
        }



    }

}