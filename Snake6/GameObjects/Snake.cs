using System.Threading.Channels;
using Raylib_cs;
public class Snake
{
    #region Properties
    Grid grid;
    Queue<Coordinates> body = new Queue<Coordinates>();
    Coordinates currentDirection = Coordinates.right;
    Coordinates nextDirection = Coordinates.right;
    #endregion

    private bool growing = false;
    public Coordinates head => body.Last();

    public Snake(Coordinates coordinates,Grid grid, int startSize = 3)
    {
        this.grid = grid;
        for(int i = startSize - 1; i >=0; i--)
            // body.Enqueue(coordinates - Coordinates.left * i);
            body.Enqueue(coordinates - currentDirection * i);
    }

    public void changeDirection(Coordinates newDirection)
    {
        if (newDirection == -currentDirection || newDirection == Coordinates.zero) return;
        nextDirection = newDirection;
    }

    public void Growth()
    {
        growing = true;
    }

    public void Draw()
    {
        foreach(var segment in body)
        {
            var posInWorld = grid.GridToWorld(segment);
            Raylib.DrawRectangle((int)posInWorld.X, (int)posInWorld.Y, grid.cellSize, grid.cellSize, Color.White);
        }
    }

    public void Move()
    {
        currentDirection = nextDirection;
        body.Enqueue(head + currentDirection);
        if (!growing) body.Dequeue();
        else growing = false;
    }

    public bool IsCollindingWith(Coordinates coordinates)
    {
        return body.Contains(coordinates);
    }


    public bool IsOutOfBound()
    {
        return !grid.IsInBounds(head);
    }

    public bool IsOverLapping()
    {
        return body.Count != body.Distinct().Count();
        // body.Count => le nombre d'élément présent dans le snake
        // body.Distinct() => créer une liste l'élément distinct a partir de body
        // donc si on a 2 élément l'un au dessus de l'autre, alors le nombre d'élément sera différent
    }

}