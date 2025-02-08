using UnityEngine;


public class BackgroundController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    private EventBus _eventBus;

    [SerializeField] private GameObject defaultBackground;
    [SerializeField] private GameObject level1Background;
    [SerializeField] private GameObject level2Background;
    [SerializeField] private GameObject level3Background;
    [SerializeField] private GameObject level4Background;
    [SerializeField] private GameObject level5Background;
    [SerializeField] private GameObject level6Background;


    private void Start ()
    {
        SetDefault();
        _eventBus = _gameManager.EventBus;
        _eventBus.Subscribe<RangEnableSignal>(OnRangEnabled);
    }


    private void OnRangEnabled (RangEnableSignal signal)
    {
        LevelIndex levelIndex = signal.SceneIndex;

        switch (levelIndex) {
            case LevelIndex.level1:
                if (level1Background != null) {
                    SetBg1();
                }
                else {
                    SetDefault();
                }
                break;
            case LevelIndex.level2:
                if (level2Background != null) {
                    SetBg2();
                }
                else {
                    SetDefault();
                }
                break;
            case LevelIndex.level3:
                if (level3Background != null) {
                    SetBg3();
                }
                else {
                    SetDefault();
                }
                break;
            case LevelIndex.level4:
                if (level4Background != null) {
                    SetBg4();
                }
                else {
                    SetDefault();
                }
                break;
            case LevelIndex.level5:
                if (level5Background != null) {
                    SetBg5();
                }
                else {
                    SetDefault();
                }
                break;
            case LevelIndex.level6:
                if (level6Background != null) {
                    SetBg6();
                }
                else {
                    SetDefault();
                }
                break;
            default:
                SetDefault();
                break;
        }
    }


    public void SetDefault ()
    {
        if (defaultBackground != null) {
            defaultBackground.SetActive(true);
        }
        if (level1Background != null) {
            level1Background.SetActive(false);
        }
        if (level2Background != null) {
            level2Background.SetActive(false);
        }
        if (level3Background != null) {
            level3Background.SetActive(false);
        }
        if (level4Background != null) {
            level4Background.SetActive(false);
        }
        if (level5Background != null) {
            level5Background.SetActive(false);
        }
        if (level6Background != null) {
            level6Background.SetActive(false);
        }
    }

    private void SetBg1 ()
    {
        if (defaultBackground != null) {
            defaultBackground.SetActive(false);
        }
        if (level1Background != null) {
            level1Background.SetActive(true);
        }
        if (level2Background != null) {
            level2Background.SetActive(false);
        }
        if (level3Background != null) {
            level3Background.SetActive(false);
        }
        if (level4Background != null) {
            level4Background.SetActive(false);
        }
        if (level5Background != null) {
            level5Background.SetActive(false);
        }
        if (level6Background != null) {
            level6Background.SetActive(false);
        }
    }

    private void SetBg2 ()
    {
        if (defaultBackground != null) {
            defaultBackground.SetActive(false);
        }
        if (level1Background != null) {
            level1Background.SetActive(false);
        }
        if (level2Background != null) {
            level2Background.SetActive(true);
        }
        if (level3Background != null) {
            level3Background.SetActive(false);
        }
        if (level4Background != null) {
            level4Background.SetActive(false);
        }
        if (level5Background != null) {
            level5Background.SetActive(false);
        }
        if (level6Background != null) {
            level6Background.SetActive(false);
        }
    }

    private void SetBg3 ()
    {
        if (defaultBackground != null) {
            defaultBackground.SetActive(false);
        }
        if (level1Background != null) {
            level1Background.SetActive(false);
        }
        if (level2Background != null) {
            level2Background.SetActive(false);
        }
        if (level3Background != null) {
            level3Background.SetActive(true);
        }
        if (level4Background != null) {
            level4Background.SetActive(false);
        }
        if (level5Background != null) {
            level5Background.SetActive(false);
        }
        if (level6Background != null) {
            level6Background.SetActive(false);
        }
    }

    private void SetBg4 ()
    {
        if (defaultBackground != null) {
            defaultBackground.SetActive(false);
        }
        if (level1Background != null) {
            level1Background.SetActive(false);
        }
        if (level2Background != null) {
            level2Background.SetActive(false);
        }
        if (level3Background != null) {
            level3Background.SetActive(false);
        }
        if (level4Background != null) {
            level4Background.SetActive(true);
        }
        if (level5Background != null) {
            level5Background.SetActive(false);
        }
        if (level6Background != null) {
            level6Background.SetActive(false);
        }
    }

    private void SetBg5 ()
    {
        if (defaultBackground != null) {
            defaultBackground.SetActive(false);
        }
        if (level1Background != null) {
            level1Background.SetActive(false);
        }
        if (level2Background != null) {
            level2Background.SetActive(false);
        }
        if (level3Background != null) {
            level3Background.SetActive(false);
        }
        if (level4Background != null) {
            level4Background.SetActive(false);
        }
        if (level5Background != null) {
            level5Background.SetActive(false);
        }
        if (level5Background != null) {
            level5Background.SetActive(true);
        }
        if (level6Background != null) {
            level6Background.SetActive(false);
        }
    }

    private void SetBg6 ()
    {
        if (defaultBackground != null) {
            defaultBackground.SetActive(false);
        }
        if (level1Background != null) {
            level1Background.SetActive(false);
        }
        if (level2Background != null) {
            level2Background.SetActive(false);
        }
        if (level3Background != null) {
            level3Background.SetActive(false);
        }
        if (level4Background != null) {
            level4Background.SetActive(false);
        }
        if (level5Background != null) {
            level5Background.SetActive(false);
        }
        if (level5Background != null) {
            level5Background.SetActive(false);
        }
        if (level6Background != null) {
            level6Background.SetActive(true);
        }
    }
}
