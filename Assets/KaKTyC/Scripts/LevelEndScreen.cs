using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelEndScreen : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject levelEndPanel;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text rankText;

    [Header("Ranking Settings")]
    [SerializeField] private int rankF = 0;
    [SerializeField] private int rankC = 500;
    [SerializeField] private int rankB = 1000;
    [SerializeField] private int rankA = 1500;
    [SerializeField] private int rankS = 2000;

    //NEED TO SOLVE IN FUTURE
    
    [Space]
    [Space]
    [Space]
    public int activeLevel = 1; // WARNING !!!!!!! ACTIVE LVL == ACTIVE GAME SCENE;
    [SerializeField] private UserSaveSystemController _userSaveSystemController;

    private float levelStartTime;
    private List<int> scoreList;

    private void Start()
    {
        levelStartTime = Time.time;
    }

    private void OnEnable()
    {
        EndLevel();
    }

    public void EndLevel()
    {
        finalScoreText.text = "";
        timeText.text = "";
        rankText.text = "";

        levelEndPanel.SetActive(true);

        float levelTime = Time.time - levelStartTime;
        int finalScore = ComboSystem.Instance.CalculateFinalScore();
        scoreList = ComboSystem.Instance.GetScoreList();

        string timeFormatted = $"{(int)(levelTime / 60)}:{(levelTime % 60):00.00}";
        string finalRank = GetRank(finalScore);
        string avgRank = GetAverageRank(scoreList);

        finalScoreText.ForceMeshUpdate();
        timeText.ForceMeshUpdate();
        rankText.ForceMeshUpdate();

        timeText.text = $"Âðåìÿ: {timeFormatted}";
        finalScoreText.text = $"Î÷êè ñòèëÿ: {finalScore}";
        rankText.text = $"Ôèíàëüíàÿ îöåíêà: {avgRank}";

        Debug.Log($"Level finished in {timeFormatted}, Style Points: {finalScore}, Rank: {finalRank}");

        /////////////////////// [ÊÀÊ ÑÎÕÐÀÍßÒÜ ÐÅÇÓËÜÒÀÒÛ ÓÐÎÂÍß?] ////////////////////////////////

        LevelData newLevelData = new LevelData
        {
            totalScore = finalScore,      // Ñêîëüêî î÷êîâ.  
            totalRang = finalRank,  // Êàêîé ðàíã åìó. 
            isCompleted = true     // Ôëàã ÷òî óðîâåíü ïðîéäåí.
        };

        _userSaveSystemController.SetLevelData(activeLevel, newLevelData);

        ///////////////////////////////////////////////////////////////////////////////////////////
    }

    private string GetRank(int score)
    {
        if (score >= rankS) return "S";
        if (score >= rankA) return "A";
        if (score >= rankB) return "B";
        if (score >= rankC) return "C";
        return "F";
    }

    private string GetAverageRank(List<int> scores)
    {
        if (scores.Count == 0) return "F";

        int sum = 0;
        foreach (var score in scores)
            sum += GetRankValue(score);

        int avg = Mathf.RoundToInt((float)sum / scores.Count);
        return RankValueToLetter(avg);
    }

    private int GetRankValue(int score)
    {
        if (score >= rankS) return 4;
        if (score >= rankA) return 3;
        if (score >= rankB) return 2;
        if (score >= rankC) return 1;
        return 0;
    }

    private string RankValueToLetter(int value)
    {
        switch (value)
        {
            case 4: return "S";
            case 3: return "A";
            case 2: return "B";
            case 1: return "C";
            default: return "F";
        }
    }
}
