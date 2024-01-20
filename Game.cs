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

        private const string NORMAL_CHESS_RULESET_STRING = "pawnM0,-1,1,0,0,0|0,-2,1,0,0,1|-1,-2,0,0,1,1|1,-2,0,0,1,1|-1,-1,0,0,1,0|1,-1,0,0,1,0|&rookM1,0,1,0,1,0|2,0,1,0,1,0|3,0,1,0,1,0|4,0,1,0,1,0|5,0,1,0,1,0|6,0,1,0,1,0|7,0,1,0,1,0|8,0,1,0,1,0|0,-1,1,0,1,0|0,-2,1,0,1,0|0,-3,1,0,1,0|0,-4,1,0,1,0|0,-5,1,0,1,0|0,-6,1,0,1,0|0,-7,1,0,1,0|0,-8,1,0,1,0|-1,0,1,0,1,0|-2,0,1,0,1,0|-3,0,1,0,1,0|-4,0,1,0,1,0|-5,0,1,0,1,0|-6,0,1,0,1,0|-7,0,1,0,1,0|-8,0,1,0,1,0|0,1,1,0,1,0|0,2,1,0,1,0|0,3,1,0,1,0|0,4,1,0,1,0|0,5,1,0,1,0|0,6,1,0,1,0|0,7,1,0,1,0|0,8,1,0,1,0|&bishopM1,-1,1,0,1,0|2,-2,1,0,1,0|3,-3,1,0,1,0|4,-4,1,0,1,0|5,-5,1,0,1,0|6,-6,1,0,1,0|7,-7,1,0,1,0|8,-8,1,0,1,0|-2,2,1,0,1,0|-3,3,1,0,1,0|-4,4,1,0,1,0|-5,5,1,0,1,0|-6,6,1,0,1,0|-7,7,1,0,1,0|-8,8,1,0,1,0|-1,-1,1,0,1,0|-2,-2,1,0,1,0|-3,-3,1,0,1,0|-4,-4,1,0,1,0|-5,-5,1,0,1,0|-6,-6,1,0,1,0|-7,-7,1,0,1,0|-8,-8,1,0,1,0|-1,1,1,0,1,0|1,1,1,0,1,0|2,2,1,0,1,0|3,3,1,0,1,0|4,4,1,0,1,0|5,5,1,0,1,0|6,6,1,0,1,0|7,7,1,0,1,0|8,8,1,0,1,0|&knightM-1,-2,1,1,1,0|-2,-1,1,1,1,0|1,-2,1,1,1,0|2,-1,1,1,1,0|2,1,1,1,1,0|1,2,1,1,1,0|-2,1,1,1,1,0|-1,2,1,1,1,0|&queenM1,-1,1,0,1,0|0,-1,1,0,1,0|-1,-1,1,0,1,0|-1,0,1,0,1,0|-1,1,1,0,1,0|1,1,1,0,1,0|1,0,1,0,1,0|2,0,1,0,1,0|3,0,1,0,1,0|4,0,1,0,1,0|5,0,1,0,1,0|6,0,1,0,1,0|7,0,1,0,1,0|8,0,1,0,1,0|0,-2,1,0,1,0|0,-3,1,0,1,0|0,-4,1,0,1,0|0,-5,1,0,1,0|0,-6,1,0,1,0|0,-7,1,0,1,0|0,-8,1,0,1,0|0,1,1,0,1,0|0,2,1,0,1,0|0,3,1,0,1,0|0,4,1,0,1,0|0,9,1,0,1,0|0,8,1,0,1,0|0,7,1,0,1,0|0,6,1,0,1,0|0,5,1,0,1,0|-2,2,1,0,1,0|-3,3,1,0,1,0|-4,4,1,0,1,0|-5,5,1,0,1,0|-6,6,1,0,1,0|-7,7,1,0,1,0|-8,8,1,0,1,0|-2,0,1,0,1,0|-3,0,1,0,1,0|-4,0,1,0,1,0|-5,0,1,0,1,0|-6,0,1,0,1,0|-7,0,1,0,1,0|-8,0,1,0,1,0|-2,-2,1,0,1,0|-3,-3,1,0,1,0|-4,-4,1,0,1,0|-5,-5,1,0,1,0|-6,-6,1,0,1,0|-7,-7,1,0,1,0|-8,-8,1,0,1,0|2,-2,1,0,1,0|3,-3,1,0,1,0|4,-4,1,0,1,0|5,-5,1,0,1,0|6,-6,1,0,1,0|7,-7,1,0,1,0|8,-8,1,0,1,0|2,2,1,0,1,0|3,3,1,0,1,0|4,4,1,0,1,0|5,5,1,0,1,0|6,6,1,0,1,0|7,7,1,0,1,0|8,8,1,0,1,0|&kingM1,-1,1,0,1,0|0,-1,1,0,1,0|-1,-1,1,0,1,0|-1,0,1,0,1,0|-1,1,1,0,1,0|0,1,1,0,1,0|1,1,1,0,1,0|1,0,1,0,1,0|";
        public static Dictionary<Piece.Type, Move[]> NORMAL_CHESS_RULESET = loadRuleSetFromString(NORMAL_CHESS_RULESET_STRING);

        public static List<Dictionary<Piece.Type, Move[]>> ruleSets = new List<Dictionary<Piece.Type, Move[]>>() { NORMAL_CHESS_RULESET };

        public static Dictionary<Piece.Type, Move[]> currentRuleSet = ruleSets[0];

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
        private static Dictionary<Piece.Type, Move[]> loadRuleSetFromString(string ruleSetString)
        {
            Dictionary<Piece.Type, Move[]> ruleSet = new Dictionary<Piece.Type, Move[]>();

            string[] allMovesArray = ruleSetString.Split("&");

            foreach (Piece.Type piece in Enum.GetValues(typeof(Piece.Type)))
            {
                if(piece == Piece.Type.EMPTY)
                {
                    ruleSet.Add(piece, new Move[0]);
                    continue;
                }

                int index = -1; 
                string pieceName = piece.ToString().ToLower();
                for (int i = 0; i < allMovesArray.Length; i++)
                {
                    if (allMovesArray[i].Split('M')[0] == pieceName)
                    {
                        index = i;
                        break;
                    }
                }
                if( index == -1 )
                {
                    throw new NotSupportedException();
                }
                string allMoves = allMovesArray[index].Split('M')[1];
                ruleSet.Add(piece,getMoves(allMoves));
            }

            return ruleSet;
        }

        private static Move[] getMoves(string moveString)
        {
            List<Move> moves = new List<Move>();
            string[] moveStringArray = moveString.Split("|");
            Move moveBuff;
            foreach(string move in  moveStringArray)
            {
                if (move == string.Empty)
                    continue;
                string[] moveSplit = move.Split(",");
                moveBuff = new Move {
                    relativeX = int.Parse(moveSplit[0]),
                    relativeY = int.Parse(moveSplit[1]),
                    moving = int.Parse(moveSplit[2]) == 1,
                    jumping = int.Parse(moveSplit[3]) == 1,
                    taking = int.Parse(moveSplit[4]) == 1,
                    first = int.Parse(moveSplit[5]) == 1,
                };
                moves.Add(moveBuff);
            }
            return moves.ToArray();
        }
    }
}
