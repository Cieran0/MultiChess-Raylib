using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChess
{
    public static class Display
    {
        public static void init()
        {
            Raylib.InitWindow(1920, 1080, "MultiChess");
        }

        static Color bg1 = Color.GREEN;
        static Color bg2 = Color.PURPLE;
        static Color bg3 = Color.LIGHTGRAY;

        public static void drawGame()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(bg3);

            for(int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Color bg = ((x+y)%2 == 0)? bg1 : bg2;
                    if(Game.selectedPiece != null)
                    {
                        if (Game.selectedPiece.Value.X == x && Game.selectedPiece.Value.Y == y)
                            bg = Color.BLUE;
                    }
                    Raylib.DrawRectangle(x * 120 + 480, (7-y) * 120 + 60, 120, 120, bg);
                    Piece currentPiece = Game.board.getPiece(x, y);
                    drawPiece(currentPiece, x, 7-y);
                }
            }


            Raylib.EndDrawing();
        }

        private static void drawPiece(Piece currentPiece, int x, int y)
        {
            if (currentPiece.type == Piece.Type.EMPTY)
                return;
            string pieceName = currentPiece.type.ToString();
            int lengthInPixels = Raylib.MeasureText(pieceName, 30);
            Color teamColour =  currentPiece.team == Piece.Team.WHITE? Color.WHITE : Color.BLACK;
            Raylib.DrawText(pieceName, x * 120 + 480 + (60 - lengthInPixels/2), y * 120 + 60 + 60, 30, teamColour);
        }

        public static void drawMenu()
        {

        }

        public static void close()
        {
            Raylib.CloseWindow();
        }

        public static bool shouldClose() => Raylib.WindowShouldClose();
    }
}
