public interface IPauseHandler
{
    public void Init(PauseManager pauseManager);
    public void SetPlay();
    public void SetPause();

    public void Unregister();

    public void OnDestroy();
}
