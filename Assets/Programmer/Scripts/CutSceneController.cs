using UnityEngine;
using UnityEngine.Playables;
using VContainer;

public class CutSceneController : MonoBehaviour, IPauseHandler
{
    [Inject] GameManager _gameManager;
    [Inject] private PauseManager _pauseManager;
    private PlayableDirector _cutscene;

    public void Init(PauseManager pauseManager)
    {
        
    }
    private void Start()
    {
        _cutscene = GetComponent<PlayableDirector>();
        _pauseManager.Register(this);
    }

    public void OnDestroy()
    {
        Unregister();
    }
    public void SetPlay()
    {
        if( _cutscene.state == PlayState.Paused)
            _cutscene.Play();
    }

    public void SetPause()
    {
        if (_cutscene.state == PlayState.Playing)
            _cutscene.Pause();
    }


    public void Unregister() => _pauseManager.Unregister(this);



}
