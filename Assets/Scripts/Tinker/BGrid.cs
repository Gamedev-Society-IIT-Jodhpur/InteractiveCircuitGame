using UnityEngine;

public class BGrid : MonoBehaviour
{
    [SerializeField] public int width;
    [SerializeField] public int height;
    private int[,] gridArray;
    [SerializeField] private float cellSize;
    [SerializeField] GameObject node;
    [SerializeField] float nodeScale;
    //public List<List<GameObject>> nodes;
    //[SerializeField] Transform parent;

    // Start is called before the first frame update
    void Awake()
    {
        //nodes = new List<List<GameObject>>();
        cellSize = (nodeScale + 0.1f);
        CreateGrid(width, height, cellSize);
    }



    public void CreateGrid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];

        //Debug.Log(width + height);
        //GameObject node1 = Instantiate(node, gameObject.transform.position + GetWorldposition(0, 0), Quaternion.identity);

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            //nodes.Add(new List<GameObject>());
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                GameObject node1 = Instantiate(node, gameObject.transform.position + GetWorldposition(x, y), Quaternion.identity);
                node1.transform.SetParent(gameObject.transform);
                node1.transform.localScale = new Vector3(nodeScale, nodeScale, 1);
                //nodes[x].Add(node1);

            }
        }
    }

    private Vector3 GetWorldposition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }
}
