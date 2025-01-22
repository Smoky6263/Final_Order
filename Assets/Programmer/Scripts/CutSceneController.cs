using UnityEngine;
using UnityEngine.Playables;

public class CutSceneController : MonoBehaviour, IPauseHandler
{
    private PlayableDirector _cutscene;
    private PauseManager _pauseManager;

    public void Init(PauseManager pauseManager)
    {
        
    }
    private void Start()
    {
        _cutscene = GetComponent<PlayableDirector>();
        _pauseManager = GameManager.Instance.GetComponent<PauseManager>();
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
