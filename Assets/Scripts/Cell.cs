using UnityEngine;

public class Cell
{
    private int x, y;
    private float size;
    private bool occupied;
    private Vector3 originPos;

    public Cell(int x, int y, float size, Vector3 originPos)
    {
        this.x = x;
        this.y = y;
        this.size = size;
        this.originPos = originPos;
    }

    public Vector3 GetCenterWorldPosition()
    {
        var pos = new Vector3();
        pos.x = originPos.x + x * size + size / 2f;
        pos.y = originPos.y + y * size + size / 2f;
        return pos;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public bool IsOccupied()
    {
        return occupied;
    }

    public void SetOccupied(bool occupied)
    {
        this.occupied = occupied;
    }
    
    public float GetSize()
    {
        return size;
    }

    
}
