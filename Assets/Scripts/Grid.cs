using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPos;
    private Cell[,] gridArray;

    public Grid(int width, int height, float cellSize, Vector3 originPos)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;

        gridArray = new Cell[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                var cell = new Cell(x, y, cellSize, originPos);
                gridArray[x, y] = cell;
                Debug.DrawLine(GetPosition(cell), GetPosition(x, y+1), Color.white, 100f);
                Debug.DrawLine(GetPosition(cell), GetPosition(x+1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetPosition(0, height), GetPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetPosition(width, 0), GetPosition(width, height), Color.white, 100f);
    }
    
    public Cell[,] GetGridArray()
    {
        return gridArray;
    }

    public Cell GetCell(int x, int y)
    {
        return gridArray[x, y];
    }

    public Vector3 GetPosition(Cell cell)
    {
        var x = cell.GetX() * cellSize + originPos.x;
        var y = cell.GetY() * cellSize + originPos.y;
        return new Vector3(x, y);
    }

    public Vector3 GetPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPos;
    }

    public Vector3 GetCellWorldPosition(int x, int y)
    {
        return GetCell(x, y).GetCenterWorldPosition();
    }

    public int Width()
    {
        return width;
    }

    public int Height()
    {
        return height;
    }

    public bool CheckGrid(int x, int y, Block.FallDirection fallDirection)
    {
        if (fallDirection == Block.FallDirection.Down)
        {
            return GetCell(x, y - 1).IsOccupied();
        }
        else
        {
            return GetCell(x, y + 1).IsOccupied();
        }
    }
    
    public void SetOccupied(int x, int y, bool occupied)
    {
        GetCell(x, y).SetOccupied(occupied);
    }

    public bool CheckOutOfBounds(int newY)
    {
        // Returns true if out of bounds
        return newY < 0 || newY >= height;
    }
    
    public bool CheckHorizontalBounds(int newX)
    {
        // Returns true if out of bounds
        return newX < 0 || newX >= width;
    }
}
