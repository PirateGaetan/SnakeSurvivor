using System.Numerics;
using Raylib_cs;

class Game
{
    static ScenesManager scenesManager = new ScenesManager();
    static void Main()
    {
        int screenWidth = 1400;
        int screenHeight = 800;

        Raylib.InitWindow(screenWidth, screenHeight, "Snake5");
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();

        scenesManager.Load<HomeScreen>(null);

        while(!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            scenesManager.Update();
            scenesManager.Draw();
            Raylib.EndDrawing();
        }
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }
}  