using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimax : MonoBehaviour
{
    public MainGame game;
    const int size = 15;
    const int maxDepth = 3;
    const int winLength = 5; // 连续5个棋子算赢
    char aiPlayer = 'O';
    char huPlayer = 'X';

    void Start()
    {
        game = GameObject.Find("MainGame").GetComponent<MainGame>();
        game.SetupBoard(size);
    }

    public void PerformMove()
    {
        string nextMove = FindBestMove(game.board);
        game.MakeMove(nextMove, aiPlayer);
    }

    string FindBestMove(char[,] board)
    {
        int bestScore = int.MinValue;
        string bestMove = null;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] == '.')
                {
                    board[i, j] = aiPlayer;
                    int score = MinimaxAlg(board, 0, false);
                    board[i, j] = '.';
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = $"{i},{j}";
                    }
                }
            }
        }
        return bestMove;
    }

    int MinimaxAlg(char[,] board, int depth, bool isMaximizing)
    {
        bool isTerminal = IsTerminal(board, out int result);
        if (depth == maxDepth || isTerminal)
        {
            return Evaluate(board, result);
        }

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] == '.')
                    {
                        board[i, j] = aiPlayer;
                        int score = MinimaxAlg(board, depth + 1, false);
                        board[i, j] = '.';
                        bestScore = Mathf.Max(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] == '.')
                    {
                        board[i, j] = huPlayer;
                        int score = MinimaxAlg(board, depth + 1, true);
                        board[i, j] = '.';
                        bestScore = Mathf.Min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
    }

    int Evaluate(char[,] board, int result)
    {
        if (result == 1) return 1000;  // AI wins
        if (result == -1) return -1000; // Human wins
        return 0; // Draw or no result yet
    }

    public bool IsTerminal(char[,] board, out int result)
    {
        result = 0;
        bool hasEmpty = false;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] == '.')
                {
                    hasEmpty = true;
                    continue;
                }
                char currentPlayer = board[i, j];
                if (CheckDirection(board, i, j, 1, 0, currentPlayer) ||
                    CheckDirection(board, i, j, 0, 1, currentPlayer) ||
                    CheckDirection(board, i, j, 1, 1, currentPlayer) ||
                    CheckDirection(board, i, j, 1, -1, currentPlayer))
                {
                    result = (currentPlayer == aiPlayer) ? 1 : -1;
                    return true;
                }
            }
        }
        if (!hasEmpty) return true; // Draw
        return false; // Game continues
    }

    // Helper method to check for win condition in a given direction
    bool CheckDirection(char[,] board, int x, int y, int dx, int dy, char player)
    {
        for (int k = 1; k < winLength; k++)
        {
            int nx = x + k * dx;
            int ny = y + k * dy;
            if (nx < 0 || nx >= size || ny < 0 || ny >= size || board[nx, ny] != player)
                return false;
        }
        return true;
    }
}
