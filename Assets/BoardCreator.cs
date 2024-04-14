using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    public int width = 15;  // �������̵Ŀ��
    public int depth = 15;  // �������̵����
    public GameObject cubePrefab; // ��Inspector�з��������ȷ����ָ�����̷����Ԥ�Ƽ�

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
                cube.transform.parent = this.transform;  // �������̷���ĸ�����Ϊ��ǰ����
                cube.transform.localScale = new Vector3(1, 0.1f, 1);  // ������������ţ�ʹ�����ƽ
            }
        }
    }
}
