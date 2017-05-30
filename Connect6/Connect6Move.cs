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

        /// <summary>
        /// current  (0,1) (1,2) 
        ///  m2 (1,2) (0,1)
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:Connect6.Connect6Move"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:Connect6.Connect6Move"/>; otherwise, <c>false</c>.</returns>

        public override bool Equals(object obj)
        {
            if (obj is Connect6Move)
            {
                Connect6Move m2 = obj as Connect6Move;
                return Pos1 == m2.Pos1 && Pos2 == m2.Pos2 || Pos1 == m2.Pos2 && Pos2 == m2.Pos1;
            }
            else
                return false;
        }


        public override string ToString()
        {
            return string.Format("[Connect6Move] {0}, {1}", Pos1, Pos2);
        }



    }

}