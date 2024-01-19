using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
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
    }
}
