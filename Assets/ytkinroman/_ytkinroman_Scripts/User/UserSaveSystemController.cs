using UnityEngine;


public class UserSaveSystemController : MonoBehaviour
{
    private GameManager _gameManager;
    private UserSaveSystem _userSaveSystem;
    private EventBus _eventBus;


    public void Initialization (GameManager gameManager)
    {
        _gameManager = gameManager;
        _eventBus = _gameManager.EventBus;
        _eventBus.Subscribe<RangEnableSignal>(OnRangEnabled);

        _userSaveSystem = new UserSaveSystem();
        _userSaveSystem.Initialization();
    }


    public LevelData GetLevelData (int levelNumber)
    {
        switch (levelNumber) {
            case 1: return _userSaveSystem._userData.level1;
            case 2: return _userSaveSystem._userData.level2;
            case 3: return _userSaveSystem._userData.level3;
            case 4: return _userSaveSystem._userData.level4;
            default:
                return null;
        }
    }

    public void SetLevelData (int levelNumber, LevelData newData)
    {
        switch (levelNumber) {
            case 1: _userSaveSystem._userData.level1 = newData; break;
            case 2: _userSaveSystem._userData.level2 = newData; break;
            case 3: _userSaveSystem._userData.level3 = newData; break;
            case 4: _userSaveSystem._userData.level4 = newData; break;
            case 5: _userSaveSystem._userData.level4 = newData; break;
            default:
                break;
        }

        /////////////////////// [ ј  —ќ’–јЌя“№ –≈«”Ћ№“ј“џ ”–ќ¬Ќя?] ////////////////////////////////
        //
        //  LevelData newLevelData = new LevelData
        //  {
        //      totalScore = 500,      // —колько очков.  
        //      totalRang = "Silver",  //  акой ранг ему. 
        //      isCompleted = true     // ‘лаг что уровень пройден.
        //  };
        //
        //
        //  int activeLevel = 1;
        //  _userSaveSystemController.SerLevelData(activeLevel, newLevelData);
        //
        ///////////////////////////////////////////////////////////////////////////////////////////

        _userSaveSystem.SaveData();
    }


    private void OnRangEnabled (RangEnableSignal signal)
    {
        LevelIndex levelIndex = signal.SceneIndex;

        switch (levelIndex) {
            case LevelIndex.level1:
                _eventBus.Invoke(new RangValueChangeSignal(GetLevelData(1)));
                break;
            case LevelIndex.level2:
                _eventBus.Invoke(new RangValueChangeSignal(GetLevelData(2)));
                break;
            case LevelIndex.level3:
                _eventBus.Invoke(new RangValueChangeSignal(GetLevelData(3)));
                break;
            case LevelIndex.level4:
                _eventBus.Invoke(new RangValueChangeSignal(GetLevelData(4)));
                break;
            case LevelIndex.level5:
                _eventBus.Invoke(new RangValueChangeSignal(GetLevelData(4)));
                break;
            default:
                break;
        }
    }
}
