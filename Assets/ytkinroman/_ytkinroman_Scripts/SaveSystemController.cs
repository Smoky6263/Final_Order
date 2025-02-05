using UnityEngine;


public class SaveSystemController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private SoundSaveSystemController _soudSaveSystemController;
    [SerializeField] private UserSaveSystemController _userSaveSystemController;


    private void Awake ()
    {
        if (_soudSaveSystemController != null) {
            _soudSaveSystemController.Initialization(_gameManager);
        }
        if (_userSaveSystemController != null) {
            _userSaveSystemController.Initialization(_gameManager);
        }
    }
}
