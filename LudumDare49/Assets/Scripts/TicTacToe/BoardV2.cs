using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class BoardV2
{

    static readonly int BOARD_WIDTH = 3;

    public enum State { Blank, X, O }
    private State[,] board;
    private State playersTurn;
    private State winner;
    private HashSet<int> movesAvailable;

    private int moveCount;
    private bool gameOver;

    public BoardV2()
    {
        board = new State[BOARD_WIDTH, BOARD_WIDTH];
        movesAvailable = new HashSet<int>();
        Reset();
    }

    private void initialize()
    {
        for (int row = 0; row < BOARD_WIDTH; row++)
        {
            for (int col = 0; col < BOARD_WIDTH; col++)
            {
                board[row, col] = State.Blank;
            }
        }

        movesAvailable.Clear();

        for (int i = 0; i < BOARD_WIDTH * BOARD_WIDTH; i++)
        {
            movesAvailable.Add(i);
        }
    }

    public void Reset()
    {
        moveCount = 0;
        gameOver = false;
        playersTurn = State.X;
        winner = State.Blank;
        initialize();
    }

    public bool move(int index)
    {
        return move(index % BOARD_WIDTH, index / BOARD_WIDTH);
    }

    private bool move(int x, int y)
    {

        if (gameOver)
        {
            return false;
        }

        if (board[y, x] == State.Blank)
        {
            board[y, x] = playersTurn;
        }
        else
        {
            return false;
        }

        moveCount++;
        movesAvailable.Remove(y * BOARD_WIDTH + x);

        // The game is a draw.
        if (moveCount == BOARD_WIDTH * BOARD_WIDTH)
        {
            winner = State.Blank;
            gameOver = true;
        }

        // Check for a winner.
        checkRow(y);
        checkColumn(x);
        checkDiagonalFromTopLeft(x, y);
        checkDiagonalFromTopRight(x, y);

        playersTurn = (playersTurn == State.X) ? State.O : State.X;
        return true;
    }

    public bool isGameOver()
    {
        return gameOver;
    }

    public State getTurn()
    {
        return playersTurn;
    }

    public State getWinner()
    {
        if (!gameOver)
        {
            return State.Blank;
        }
        return winner;
    }

    public HashSet<int> getAvailableMoves()
    {
        return movesAvailable;
    }

    private void checkRow(int row)
    {
        for (int i = 1; i < BOARD_WIDTH; i++)
        {
            if (board[row, i] != board[row, i - 1])
            {
                break;
            }
            if (i == BOARD_WIDTH - 1)
            {
                winner = playersTurn;
                gameOver = true;
            }
        }
    }

    private void checkColumn(int column)
    {
        for (int i = 1; i < BOARD_WIDTH; i++)
        {
            if (board[i, column] != board[i - 1, column])
            {
                break;
            }
            if (i == BOARD_WIDTH - 1)
            {
                winner = playersTurn;
                gameOver = true;
            }
        }
    }

    private void checkDiagonalFromTopLeft(int x, int y)
    {
        if (x == y)
        {
            for (int i = 1; i < BOARD_WIDTH; i++)
            {
                if (board[i, i] != board[i - 1, i - 1])
                {
                    break;
                }
                if (i == BOARD_WIDTH - 1)
                {
                    winner = playersTurn;
                    gameOver = true;
                }
            }
        }
    }

    private void checkDiagonalFromTopRight(int x, int y)
    {
        if (BOARD_WIDTH - 1 - x == y)
        {
            for (int i = 1; i < BOARD_WIDTH; i++)
            {
                if (board[BOARD_WIDTH - 1 - i, i] != board[BOARD_WIDTH - i, i - 1])
                {
                    break;
                }
                if (i == BOARD_WIDTH - 1)
                {
                    winner = playersTurn;
                    gameOver = true;
                }
            }
        }
    }

    public BoardV2 getDeepCopy()
    {
        using (MemoryStream ms = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, this);
            ms.Position = 0;

            return (BoardV2)formatter.Deserialize(ms);
        }
    }

    public List<int> GetEnemyIndices()
    {
        List<int> EnemyIndices = new List<int>();

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (board[x, y] == State.O)
                {
                    EnemyIndices.Add(x * 3 + y);
                }
            }
        }

        return EnemyIndices;
    }
}

