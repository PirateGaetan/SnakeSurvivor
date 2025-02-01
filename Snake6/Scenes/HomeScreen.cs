using Raylib_cs;

public class HomeScreen : Scene
{
    public HomeScreen()
    {
        Console.WriteLine("HomeSrceen loaded");
        // throw new NotImplementedException();
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
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
            Services.Get<IScenesManager>().Load<GameScene>(null);

    }
    public override void Draw()
    {
        Raylib.DrawText("Snake Survivor", 10, 10, 20, Color.White);
        Raylib.DrawText("Press SPACE to start", 40, 40, 30, Color.White);
    }

    
}


