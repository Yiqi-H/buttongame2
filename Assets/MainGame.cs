using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public GameObject[,] boardObjects;
    public char[,] board;
    public GameObject blackPiecePrefab;  // 用于黑色棋子的预制件
    public GameObject whitePiecePrefab;  // 用于白色棋子的预制件
    public GameObject cubePrefab;        // 用于棋盘方块的预制件
    public Minimax minimaxScript;

    void Start()
    {
        SetupBoard(15);
        minimaxScript = GetComponent<Minimax>(); // 确保 Minimax 组件已经挂载在同一对象或适当的对象上
    }

    public void SetupBoard(int size)
    {
        board = new char[size, size];
        boardObjects = new GameObject[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject tile = Instantiate(cubePrefab, new Vector3(i, 0, j), Quaternion.identity);
                tile.AddComponent<BoxCollider>(); // 添加碰撞器以便鼠标交互
                boardObjects[i, j] = tile;
                board[i, j] = '.';
            }
        }
    }


    public void UpdateTile(int x, int y, char player)
    {
        if (board[x, y] == '.') // 仅当格子为空时才放置新棋子
        {
            GameObject prefab = (player == 'X') ? blackPiecePrefab : whitePiecePrefab;
            GameObject piece = Instantiate(prefab, new Vector3(x, 1, y), Quaternion.identity);
            boardObjects[x, y] = piece;
            board[x, y] = player;
        }
    }


    public void MakeMove(string move, char player)
    {
        var parts = move.Split(',');
        int x = int.Parse(parts[0]);
        int y = int.Parse(parts[1]);
        if (board[x, y] == '.') // 确保该位置是空的
        {
            UpdateTile(x, y, player);
            CheckGameEnd(); // 检查游戏是否结束
            if (player == 'X') // 如果是玩家移动，触发AI移动
            {
                minimaxScript.PerformMove();
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 point = hit.collider.gameObject.transform.position;
                MakeMove($"{(int)point.x},{(int)point.z}", 'X');
            }
        }
    }

    private void CheckGameEnd()
    {
        if (minimaxScript != null)
        {
            if (minimaxScript.IsTerminal(board, out int result))
            {
                Debug.Log(result == 1 ? "AI Wins!" : result == -1 ? "Player Wins!" : "Draw!");
            }
        }
        else
        {
            Debug.LogError("Minimax script is not attached or failed to load.");
        }
    }

}
