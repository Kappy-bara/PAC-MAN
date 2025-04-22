using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class Testing : MonoBehaviour
{
    //The testing script has actually become the Grid :>

    public PathFinding pathfinding;
    //public EnemyAI enemyAI;
    public GameObject player;
    public GameObject smallOrbPrefab;

    private int[,] wallGrid = new int[17,17]
    {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,2,0,0,0,0,0,0,1,0,0,0,0,0,0,2,1},
        {1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1},
        {1,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,1},
        {1,1,1,0,1,0,0,0,0,0,0,0,1,0,1,1,1},
        {2,2,1,0,1,1,1,1,0,1,1,1,1,0,1,2,2},
        {2,2,1,0,1,0,0,0,0,0,0,0,1,0,1,2,2},
        {1,1,1,0,0,0,1,1,0,1,1,0,0,0,1,1,1},
        {0,0,0,0,1,0,1,0,0,0,1,0,1,0,0,0,0},
        {1,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,1},
        {2,2,1,0,0,0,0,0,0,0,0,0,0,0,1,2,2},
        {1,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,1},
        {1,0,0,0,1,0,0,1,1,1,0,0,1,0,0,0,1},
        {1,0,1,0,1,1,0,0,0,0,0,1,1,0,1,0,1},
        {1,0,1,0,0,0,0,1,1,1,0,0,0,0,1,0,1},
        {1,2,0,0,1,1,0,0,0,0,0,1,1,0,0,2,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
    };
    void Start()
    {
        pathfinding = new PathFinding(17, 17);

        for (int x = 0; x < wallGrid.GetLength(0); x++)
        {
            for (int y = 0; y < wallGrid.GetLength(1); y++)
            {
                if (wallGrid[x,y] == 1)
                {
                    pathfinding.GetNode(y,16-x).ToggleWalkable();
                }
                else if (wallGrid[x,y] == 0)
                {
                    Instantiate(smallOrbPrefab,pathfinding.GetGrid().GetWorldPosition(y,16-x) + new Vector3(5f,5f,0f),Quaternion.identity);
                }
            }
        }
    }
    public void Chase(EnemyAI enemy, Color color)
    {
        pathfinding.GetGrid().GetXY(enemy.transform.position, out int enemyx, out int enemyy);
        pathfinding.GetGrid().GetXY(player.transform.position, out int x, out int y);
        List<PathNode> path = pathfinding.FindPath(enemyx, enemyy, x, y);
        enemy.SetPath(path);
        DrawPath(path,color);
    }
    public void ChooseCorner(EnemyAI enemy, Color color)
    {
        int vertical = Random.Range(0, 2);
        int horizontal = Random.Range(0, 2);
        pathfinding.GetGrid().GetXY(enemy.transform.position, out int enemyx, out int enemyy);
        List<PathNode> path = pathfinding.FindPath(enemyx,enemyy,1 + horizontal * 14,1 + vertical * 14);
        enemy.SetPath(path);
        DrawPath(path, color);
    }
    public void GoToCenter(EnemyAI enemy, Color color)
    {
        pathfinding.GetGrid().GetXY(enemy.transform.position,out int enemyx,out int enemyy);
        List<PathNode> path = pathfinding.FindPath(enemyx, enemyy, 8, 8);
        enemy.SetPath(path);
        DrawPath(path, color);
    }
    private void DrawPath(List<PathNode> path, Color color)
    {
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                //Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, color, 2f);
            }
        }
    }
}
