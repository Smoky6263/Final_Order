using UnityEngine;


public class SaveSystemController : MonoBehaviour
{
    [SerializeField] private SoundSaveSystemController _soudSaveSystemController;
    [SerializeField] private GameManager _gameManager;


    private void Awake ()
    {
        if (_soudSaveSystemController != null) {
            _soudSaveSystemController.Initialization(_gameManager);
        }
        /// vjycnhf
        /// buhjrf
        ///
    }
}
