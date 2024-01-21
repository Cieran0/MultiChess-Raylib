using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MultiChess
{
    public class Board
    {
        private Piece[,] board;
        private int? playerInCheck;
        private bool isGameOver;

        //TODO: Non Passant

        public Board()
        {
            this.board = new Piece[8, 8];
            this.playerInCheck = null;
            this.isGameOver = false;
            clear();
            placeStartingPieces();
        }


        public void movePiece(int oldX, int oldY, int newX, int newY) {
            board[newX, newY] = board[oldX, oldY];
            board[newX, newY].hasMoved = true;
            board[oldX, oldY] = Piece.emptySquare;
        }

        private void placeStartingPieces()
        {
            //TODO: implement rules;
            Piece whitePawn = new Piece(Piece.Type.PAWN, Piece.Team.WHITE);
            Piece blackPawn = new Piece(Piece.Type.PAWN, Piece.Team.BLACK);
            for (int x = 0; x < 8; x++)
            {
                board[x, 1] = whitePawn;
                board[x, 6] = blackPawn;
            }

            Piece whiteRook = new Piece(Piece.Type.ROOK, Piece.Team.WHITE);
            Piece blackRook = new Piece(Piece.Type.ROOK, Piece.Team.BLACK);
            Piece whiteKnight = new Piece(Piece.Type.KNIGHT, Piece.Team.WHITE);
            Piece blackKnight = new Piece(Piece.Type.KNIGHT, Piece.Team.BLACK);
            Piece whiteBishop = new Piece(Piece.Type.BISHOP, Piece.Team.WHITE);
            Piece blackBishop = new Piece(Piece.Type.BISHOP, Piece.Team.BLACK);
            Piece whiteQueen = new Piece(Piece.Type.QUEEN, Piece.Team.WHITE);
            Piece blackQueen = new Piece(Piece.Type.QUEEN, Piece.Team.BLACK);
            Piece whiteKing = new Piece(Piece.Type.KING, Piece.Team.WHITE);
            Piece blackKing = new Piece(Piece.Type.KING, Piece.Team.BLACK);

            board[0,0] = whiteRook;
            board[1,0] = whiteKnight;
            board[2,0] = whiteBishop;
            board[3,0] = whiteQueen;
            board[4,0] = whiteKing;
            board[5,0] = whiteBishop;
            board[6,0] = whiteKnight;
            board[7,0] = whiteRook;

            board[0, 7] = blackRook;
            board[1, 7] = blackKnight;
            board[2, 7] = blackBishop;
            board[3, 7] = blackQueen;
            board[4, 7] = blackKing;
            board[5, 7] = blackBishop;
            board[6, 7] = blackKnight;
            board[7, 7] = blackRook;

        }

        public void clear()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    board[x, y] = Piece.emptySquare;
                }
            }
        }

        public Piece getPiece(int x, int y)
        {
            return board[x, y];
        }

        public Piece getPiece(Vector2 vector)
        {
            return board[(int)vector.X, (int)vector.Y];
        }

        public List<Move> getPossibleMoves(int x, int y)
        {
            Piece piece = board[x, y];
            Move[] allMoves = (Move[])(Game.currentRuleSet[piece.type].Clone());
            List<Move> possibleMoves = new List<Move>();  
            for(int i = 0; i < allMoves.Length; i++)
            {
                if (piece.team == Piece.Team.WHITE)
                {
                    allMoves[i].relativeX *= -1;
                    allMoves[i].relativeY *= -1;
                }
                int realX = x + allMoves[i].relativeX;
                int realY = y + allMoves[i].relativeY;

                if (realX < 0 || realY < 0 || realX > 7 || realY > 7)
                    continue;

                if (piece.team != board[realX,realY].team)
                {
                    if (allMoves[i].first && piece.hasMoved)
                    {
                        continue;
                    }
                    if ((!allMoves[i].moving) && allMoves[i].taking && board[realX,realY].team == null) 
                    {
                        continue;
                    }

                    possibleMoves.Add(allMoves[i]);
                }
            }

            possibleMoves = pathFind(possibleMoves, x, y);

            return possibleMoves;
        }

        private List<Move> pathFind(List<Move> possibleMoves, int x, int y)
        {
            possibleMoves = sortMoves(possibleMoves);
            bool[,] validMoveGrid = new bool[8, 8];
            validMoveGrid[x, y] = true;

            List<Move> validMoves = new List<Move>();

            foreach(Move move in possibleMoves)
            {
                int moveX = x + move.relativeX;
                int moveY = y + move.relativeY;
                if(move.jumping)
                {
                    validMoveGrid[moveX, moveY] = true;
                    validMoves.Add(move);
                    continue;
                }

                bool hasValidNeighbour = false;
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++) 
                    {
                        if(moveX + i < 0 || moveY + j < 0 || moveX + i > 7 || moveY+j > 7)
                        {
                            continue;
                        }
                        if (validMoveGrid[moveX+i,moveY+j]) 
                        {
                            if (board[moveX,moveY].team == null)
                                validMoveGrid[moveX, moveY] = true;
                            hasValidNeighbour = true;
                            break;
                        }
                    }
                }
                if(hasValidNeighbour)
                {
                    validMoves.Add(move);
                }
                //Console.WriteLine($"X:{move.relativeX}, Y:{move.relativeY}, Valid:{hasValidNeighbour}");
            }
            //for(int j = 0; j < 8; j++)
            //{
            //    for (int i = 0; i < 8; i++)
            //    {
            //        Console.Write(validMoveGrid[i, j] ? "1" : "0");
            //    }
            //    Console.WriteLine();
            //}

            //Console.WriteLine("---------------------");
            return validMoves;
        }

        private List<Move> sortMoves(List<Move> possibleMoves)
        {
            double[] totalDistance = new double[possibleMoves.Count];

            for(int i = 0; i < possibleMoves.Count; i++) {
                totalDistance[i] = Math.Sqrt(Math.Pow(possibleMoves[i].relativeX, 2) + Math.Pow(possibleMoves[i].relativeY, 2));          }

            //Insertion sort
            for (int i = 1; i < possibleMoves.Count; i++)
            {
                int j = i;
                while (j > 0 && totalDistance[j] < totalDistance[j - 1])
                {
                    double temp = totalDistance[j];
                    Move buff = possibleMoves[j];
                    totalDistance[j] = totalDistance[j - 1];
                    possibleMoves[j] = possibleMoves[j - 1];
                    totalDistance[j - 1] = temp;
                    possibleMoves[j - 1] = buff;
                    j--;
                }
            }

            return possibleMoves;
        }
    }
}
