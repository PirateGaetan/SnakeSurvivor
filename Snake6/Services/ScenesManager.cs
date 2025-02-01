public interface IScenesManager
{
    void Load<T>(object[] args) where T : Scene, new();
}

public class ScenesManager : IScenesManager
{
    private Scene? _currentScene;
    public ScenesManager()
    {
        Services.Register<IScenesManager>(this);
    }
    public void Load<T>(object[]? args) where T : Scene, new()
    {
        if(_currentScene !=null)_currentScene.Unload();
        //_currentScene?.Unload(); c'est pareil que celle du dessus
        _currentScene = new T();
        _currentScene.Load(args);
    }
    public void Update()
    {
        _currentScene?.Update();
    }
    public void Draw()
    {
        _currentScene?.Draw();
    }
}
