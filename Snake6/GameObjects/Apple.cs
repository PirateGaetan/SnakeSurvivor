using Raylib_cs;

public class Apple
{
    #region Properties
    public Coordinates coordinates {get; private set;}
    Grid grid;
    #endregion

    public Apple(Grid grid)
    {
        this.grid = grid;
        coordinates = Coordinates.Random(grid.columns, grid.rows);
    }

    public void Respawn()
    {
        coordinates = Coordinates.Random(grid.columns, grid.rows);
    }

    public void Draw()
    {
        var posInWorld = grid.GridToWorld(coordinates);
        Raylib.DrawCircle((int)posInWorld.X + grid.cellSize / 2, (int)posInWorld.Y + grid.cellSize / 2, grid.cellSize / 2, Color.Orange);
    }
}