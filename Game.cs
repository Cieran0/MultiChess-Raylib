using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MultiChess
{
    public static class Game
    {
        public enum State
        {
            PLAYING_PLAYER,
            PLAYING_CPU,
            MENU,
        }

        public static State state = State.MENU;
        public static Board board = new Board();
        public static Vector2? selectedPiece = null;

        public static void init(State state)
        {
            Game.state = state;
            Game.board = new Board();
        }

        public static void selectPiece(int x, int y)
        {
            if (board.getPiece(x,y).type == Piece.Type.EMPTY)
            {
                return;
            }
            //TODO: only select player pieces
            selectedPiece = new Vector2(x, y);
        }

        public static void moveSelectedPieceTo(int x, int y)
        {
            if (selectedPiece == null)
            {
                Console.WriteLine("This should not have happened in \"moveSelectedPieceTo()\"");
                return;
            }
            int selectedX = (int)selectedPiece.Value.X;
            int selectedY = (int)selectedPiece.Value.Y;
            board.movePiece(selectedX,selectedY,x,y);
        }
    }
}
