using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MultiChess
{
    public static class Display
    {
        public const double SCALE_FACTOR = 0.5;

        public static int scale(int num)
        {
            return (int)(num * SCALE_FACTOR);
        }

        public static void init()
        {
            Raylib.InitWindow(scale(1920), scale(1080), "MultiChess");
        }

        static Color bg = Color.LIGHTGRAY;
        static readonly Color[] squareColors = { Color.BLANK, Color.BLUE, Color.YELLOW };

        public static void drawGame()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(bg);


            //TODO: OPTOMIZE THIS IT SHOULDNT RUN EVERY FRAME JUST ONE WHEN PIECE IS SELECTED 

            int[,] squareType = new int[8, 8];

            if(Game.selectedPiece != null)
            {
                int x = (int)Game.selectedPiece.Value.X;
                int y = (int)Game.selectedPiece.Value.Y;

                squareType[x, y] = 1;

                foreach(Move move in Game.board.getPossibleMoves(x,y))
                {
                    int newX = x + move.relativeX;
                    int newY = y + move.relativeY;
                    squareType[x + move.relativeX, y + move.relativeY] = 2;
                }
            }

            for(int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Color bg = ((x+y)%2 == 0)? Color.GREEN : Color.PURPLE;
                    if (squareType[x,y] > 0)
                    {
                        bg = squareColors[squareType[x, y]];
                    }
                    Raylib.DrawRectangle(scale(x * 120 + 480), scale((7-y) * 120 + 60), scale(120), scale(120), bg);
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
            Raylib.DrawText(pieceName, scale(x * 120 + 480 + (60 - lengthInPixels/2)), scale(y * 120 + 60 + 60), scale(30), teamColour);
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
