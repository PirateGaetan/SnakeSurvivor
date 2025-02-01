using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Security.Cryptography;
using Raylib_cs;

public class Cell
{
    public Coordinates coordinates;
    // public bool IsWalkable = true;

    public Grid grid;
    public Cell(Coordinates coordinates, Grid grid)
    {
        this.coordinates = coordinates;
        this.grid = grid;
    }
    public void Draw()
    {
        var cellPosInWorld = grid.GridToWorld(coordinates);
        // Color color = IsWalkable ? Color.White : Color.Red;
        Raylib.DrawRectangle((int)cellPosInWorld.X, (int)cellPosInWorld.Y, grid.cellSize, grid.cellSize, Color.Blue);
    }
}

public class Grid
{
    #region Properties
    public Vector2 position = Vector2.Zero;
    public int columns {get; private set;}
    public int rows {get; private set;}
    public int cellSize {get; private set;}
    private Cell[,] grid;
    #endregion

    public Grid(int columns = 10, int rows = 10, int cellSize = 32)
    {
        this.columns = columns;
        this.rows = rows;
        this.cellSize = cellSize;

        grid = new Cell[columns, rows];
    }

    public void Draw()
    {
        for (int column = 0; column < columns; column++)
            for (int row = 0; row < rows; row++)
            {
                Cell cell = grid[column, row];
                cell?.Draw();
            }
        for (int column = 0; column <= columns; column++)
            Raylib.DrawLineV(GridToWorld(new(column, 0)), GridToWorld(new(column, rows)), Color.Gray);
        for (int row = 0; row <= rows; row++)
            Raylib.DrawLineV(GridToWorld(new(0, row)), GridToWorld(new(columns, row)), Color.Gray);
    }

    public void SetCell(Coordinates coordinates)
    // obj: assigner a la grid (a la colunne et au rang) la cellule que l'on créer
    {
        if (!IsInBounds(coordinates)) return;
        Cell cell = new Cell(coordinates, this);
        grid[coordinates.column, coordinates.row] = cell;
    }

    public Cell? GetCell(Coordinates coordinates)
    {
        if (!IsInBounds(coordinates)) return null;
        return grid[coordinates.column, coordinates.row];
    }

    #region Coordinateconversion
    // public Vector2 WorldToGrid(Vector2 pos)
    // {
    //     return new Vector2((int)((pos.X - position.X) / cellSize), (int)((pos.Y - position.Y) / cellSize));
    // }

    public Coordinates WorldToGrid(Vector2 pos)
    {
        pos -= position;
        pos /=cellSize;
        return new Coordinates((int)pos.X, (int)pos.Y);
    }

    // public Vector2 GridToWorld(Vector2 pos)
    // {
    //     return new Vector2(pos.X * cellSize + position.X, pos.Y * cellSize + position.Y);
    // }
    public Vector2 GridToWorld(Coordinates coordinates)
    {
        coordinates *= cellSize;
        // return coordinates + position;
        return coordinates.ToVector2 + position;
    }
    #endregion


    #region  Helper Methods
    public bool IsInBounds(Coordinates coordinates)
    {
        return coordinates.column >= 0 && coordinates.column < columns && coordinates.row >= 0 && coordinates.row < rows;
    }
    #endregion

}