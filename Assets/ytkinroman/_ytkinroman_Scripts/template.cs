using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class template : MonoBehaviour
{
    [SerializeField] UserSaveSystemController _userSaveSystemController;


    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            int activeLevel = 1;

            LevelData newLevelData = new LevelData
            {
                totalScore = 9999,  
                totalRang = "DungeonMaster",
                isCompleted = true
            };
            
            _userSaveSystemController.SetLevelData(activeLevel, newLevelData);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            int activeLevel = 1;
            LevelData _levelData = _userSaveSystemController.GetLevelData(activeLevel);
            Debug.Log(_levelData.totalScore);  // 9999
            Debug.Log(_levelData.totalRang);   // DungeonMaster
        }
    }
}
