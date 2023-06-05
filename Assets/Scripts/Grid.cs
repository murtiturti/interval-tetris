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

    public int Width()
    {
        return width;
    }

    public int Height()
    {
        return height;
    }
}
