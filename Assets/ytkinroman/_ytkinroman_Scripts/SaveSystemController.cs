using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveSystemController : MonoBehaviour
{
    [SerializeField] private SoundSaveSystemController _soudSaveSystemController;


    private void Awake ()
    {
        if (_soudSaveSystemController != null) {
            _soudSaveSystemController.Initialization();
        }
    }
}
