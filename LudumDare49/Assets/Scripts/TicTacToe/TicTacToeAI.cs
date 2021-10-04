using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeAI
{
    private static double MaxSearchDepth;

    private TicTacToeAI() { }

    public static void Run(BoardV2.State player, BoardV2 board, double MaxSearchDepth)
    {

        if (MaxSearchDepth < 1)
        {
            Debug.LogError("Maximum depth must be greater than 0.");
        }

        TicTacToeAI.MaxSearchDepth = MaxSearchDepth;
        AlphaBetaPruning(player, board, double.MinValue, double.MaxValue, 0);
    }

    private static int AlphaBetaPruning(BoardV2.State player, BoardV2 board, double alpha, double beta, int currentPly)
    {
        if (currentPly++ == MaxSearchDepth || board.isGameOver())
        {
            return Score(player, board, currentPly);
        }

        if (board.getTurn() == player)
        {
            return GetMax(player, board, alpha, beta, currentPly);
        }
        else
        {
            return GetMin(player, board, alpha, beta, currentPly);
        }
    }

    private static int GetMax(BoardV2.State player, BoardV2 board, double alpha, double beta, int currentPly)
    {
        int indexOfBestMove = -1;

        foreach(int Move in board.getAvailableMoves())
        {

            BoardV2 modifiedBoard = board.getDeepCopy();
            modifiedBoard.move(Move);
            int score = AlphaBetaPruning(player, modifiedBoard, alpha, beta, currentPly);

            if (score > alpha)
            {
                alpha = score;
                indexOfBestMove = Move;
            }

            if (alpha >= beta)
            {
                break;
            }
        }

        if (indexOfBestMove != -1)
        {
            board.move(indexOfBestMove);
        }
        return (int)alpha;
    }

    private static int GetMin(BoardV2.State player, BoardV2 board, double alpha, double beta, int currentPly)
    {
        int indexOfBestMove = -1;

        foreach(int Move in board.getAvailableMoves())
        {

            BoardV2 modifiedBoard = board.getDeepCopy();
            modifiedBoard.move(Move);

            int score = AlphaBetaPruning(player, modifiedBoard, alpha, beta, currentPly);

            if (score < beta)
            {
                beta = score;
                indexOfBestMove = Move;
            }

            if (alpha >= beta)
            {
                break;
            }
        }

        if (indexOfBestMove != -1)
        {
            board.move(indexOfBestMove);
        }
        return (int)beta;
    }

    private static int Score(BoardV2.State player, BoardV2 board, int currentPly)
    {

        if (player == BoardV2.State.Blank)
        {
            Debug.LogError("Player must be X or O.");
        }

        BoardV2.State opponent = (player == BoardV2.State.X) ? BoardV2.State.O : BoardV2.State.X;

        if (board.isGameOver() && board.getWinner() == player)
        {
            return 10 - currentPly;
        }
        else if (board.isGameOver() && board.getWinner() == opponent)
        {
            return -10 + currentPly;
        }
        else
        {
            return 0;
        }
    }

}
