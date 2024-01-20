using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChess
{
    public struct Piece
    {
        public enum Type {
            QUEEN,
            KING,
            ROOK,
            KNIGHT,
            BISHOP,
            PAWN,
            EMPTY,
        }

        public enum Team
        {
            WHITE,
            BLACK
        }

        public Type type;
        public bool hasMoved;
        public Team? team;

        public Piece(Type type, Team? team)
        {
            this.type = type;
            this.team = team;
            this.hasMoved= false;
        }

        public static Piece emptySquare = new Piece(Type.EMPTY, null);
    }
}
