using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    public int width = 15;  // 定义棋盘的宽度
    public int depth = 15;  // 定义棋盘的深度
    public GameObject cubePrefab; // 在Inspector中分配这个，确保它指向棋盘方块的预制件

    void Start()
    {
        CreateBoard();
    }

    void CreateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                GameObject cube = Instantiate(cubePrefab, new Vector3(x, 0, z), Quaternion.identity);
                cube.transform.parent = this.transform;  // 设置棋盘方块的父对象为当前对象
                cube.transform.localScale = new Vector3(1, 0.1f, 1);  // 调整方块的缩放，使其更扁平
            }
        }
    }
}
