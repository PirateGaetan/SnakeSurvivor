using System.Numerics;
using System.Reflection.Metadata;
using Raylib_cs;



public class GameScene : Scene
{
    enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    Grid grid = new Grid(26, 14);
    Snake snake;
    Apple apple;
    Timer gameOverTimer;
    Timer gamePlayTimer;
    GameState gamestate = GameState.Playing;
    public GameScene()
    {
        grid.position = new Vector2(128, 92);
        snake = new Snake(new Coordinates(10, 5), grid);
        apple = new Apple(grid);

        gameOverTimer = AddTimer(GameOver, 2, false);
        gameOverTimer.Stop();
        gamePlayTimer = AddTimer(OnTimerTriggered, 0.2f);
        

    }

    public override void Load(object[]? args)
    {
        // base.Load(args);
        if(args == null)return;
        foreach(var arg in args)
            Console.WriteLine(arg);
    }
    public override void Update()
    {
        base.Update();
        switch(gamestate)
        {
            case GameState.Playing:
                UpdatePlaying();
                break;
            case GameState.Paused:
                UpdatePaused();
                break;
            // case GameState.GameOver:
            //     UpdateGameOver();
            //     break;
        }
    }
    private void UpdatePlaying()
    {
        snake.changeDirection(GetImputsDirection());
        if (Raylib.IsKeyPressed(KeyboardKey.P))
        {
            gamestate = GameState.Paused;
            gamePlayTimer.Stop();
        }
            
    }
    public void OnTimerTriggered()
    {
        Console.WriteLine("Timer triggered, moving snake...");
        snake.Move();
        if(snake.IsOutOfBound() || snake.IsOverLapping()) 
        {
            gamestate = GameState.GameOver;
            gamePlayTimer.Stop();
            gameOverTimer.Start();
        } 
        if(snake.IsCollindingWith(apple.coordinates)) EatApple();
    }

    private void UpdatePaused()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.P))
        {
            gamestate = GameState.Playing;
            gamePlayTimer.ReStart();
        }
            
    }
    // private void UpdateGameOver()
    // {
    //     Console.WriteLine("Game Over");
    //     GameOver();
    // }
    private void EatApple()
    {
        apple.Respawn();
        snake.Growth();
    }

    private Coordinates GetImputsDirection()
    {
        var direction = Coordinates.zero;

        if(Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown((KeyboardKey.Up))) direction = Coordinates.up;
        if(Raylib.IsKeyDown(KeyboardKey.S) || Raylib.IsKeyDown((KeyboardKey.Down))) direction = Coordinates.down;
        if(Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown((KeyboardKey.Left))) direction = Coordinates.left;
        if(Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown((KeyboardKey.Right))) direction = Coordinates.right;

        return direction;
    }
    public override void Draw()
    {
        switch(gamestate)
        {
            case GameState.Paused:
                Raylib.DrawText("Pause", 10, 10, 20, Color.White);
                break;
            case GameState.GameOver:
                Raylib.DrawText("Game Over", 10, 10, 20, Color.White);
                break;
        }
        grid.Draw();
        apple.Draw();
        snake.Draw();
    }
    public override void Unload()
    {
        Console.WriteLine("GameScene unloaded");
    }

    public void GameOver()
    {
        Services.Get<IScenesManager>().Load<HomeScreen>(null);
    }

}