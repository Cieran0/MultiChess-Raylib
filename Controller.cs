using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiChess
{
    public static class Controller
    {
        public static void handleMouseInput()
        {
            if(!Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT)) { return; }
            int mouseX = Raylib.GetMouseX();
            int mouseY = Raylib.GetMouseY();

            Vector2? selectedSquare = getSelectedSquare(mouseX, mouseY);
            if(selectedSquare == null) { return; }

            if(Game.selectedPiece == null)
            {
                Game.selectPiece((int)selectedSquare.Value.X, (int)selectedSquare.Value.Y);
            } else
            {
                if(Game.board.getPiece(selectedSquare.Value).team == Game.board.getPiece(Game.selectedPiece.Value).team)  
                {
                    Game.selectPiece((int)selectedSquare.Value.X, (int)selectedSquare.Value.Y);
                } else
                {
                    Game.moveSelectedPieceTo((int)selectedSquare.Value.X, (int)selectedSquare.Value.Y);
                    Game.selectedPiece = null;
                }
            }

        }

        private static Vector2? getSelectedSquare(int mouseX, int mouseY)
        {

            int x = (mouseX - 480) / 120;
            int y = 7 - (mouseY - 60) / 120;

            if (x < 0 || y < 0 || x> 7 || y > 7)
                return null;

            return new Vector2( x, y );
        }
    }
}
