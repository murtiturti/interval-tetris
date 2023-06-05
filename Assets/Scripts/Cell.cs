using UnityEngine;

public class Cell
{
    private int x, y;
    private float size;
    private bool occupied;
    private Vector3 originPos;

    //TODO: See if I need this
    //public Subject subject;
    
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
        pos.x = x + (size/2f) + originPos.x;
        pos.y = y + (size / 2f) + originPos.y;
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

    
}
