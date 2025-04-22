using UnityEngine;

public class PathNode
{
    private Grid<PathNode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    public PathNode cameFromNode;
    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;

        isWalkable = true;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    public override string ToString()
    {
        return x + "," + y;
    }
    public void ToggleWalkable()
    {
        if (isWalkable)
        {
            isWalkable = false;
        }
    }
    public Vector3 ReturnNodeWorldPosition()
    {
        return grid.GetWorldPosition(x, y)+ new Vector3(5f,5f);
    }
}
