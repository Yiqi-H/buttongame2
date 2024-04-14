using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public GameObject[,] boardObjects;
    public char[,] board;
    public GameObject blackPiecePrefab;  // ���ں�ɫ���ӵ�Ԥ�Ƽ�
    public GameObject whitePiecePrefab;  // ���ڰ�ɫ���ӵ�Ԥ�Ƽ�
    public GameObject cubePrefab;        // �������̷����Ԥ�Ƽ�
    public Minimax minimaxScript;

    void Start()
    {
        SetupBoard(15);
        minimaxScript = GetComponent<Minimax>(); // ȷ�� Minimax ����Ѿ�������ͬһ������ʵ��Ķ�����
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
                tile.AddComponent<BoxCollider>(); // �����ײ���Ա���꽻��
                boardObjects[i, j] = tile;
                board[i, j] = '.';
            }
        }
    }


    public void UpdateTile(int x, int y, char player)
    {
        if (board[x, y] == '.') // ��������Ϊ��ʱ�ŷ���������
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
        if (board[x, y] == '.') // ȷ����λ���ǿյ�
        {
            UpdateTile(x, y, player);
            CheckGameEnd(); // �����Ϸ�Ƿ����
            if (player == 'X') // ���������ƶ�������AI�ƶ�
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
