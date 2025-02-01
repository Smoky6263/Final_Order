using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public static ComboSystem Instance;

    [SerializeField] private float comboTimeWindow = 1f;


    private int comboCount = 0; // current hit combo counter
    private int comboMultiplyer = 0;
    private float comboTimer = 0f;
    private int totalPoints = 0; // current lvl points
    private List<int> scoreList = new List<int>(); // list to store score board ? 

    public delegate void ComboUpdated(int comboCount, int comboMultiplier);
    public event ComboUpdated OnComboUpdated; // update combo info
    
    public delegate void ComboEnded(int score);
    public event ComboEnded OnComboEnded; // combo ended


    public float ComboWindowProgress => Mathf.Clamp01(comboTimer / comboTimeWindow); // combo progress bar (slider)

    private void Awake()
    {
        Instance = this;
    }

    
    private void Update()
    {
        // reset if time is out
        if (comboCount > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
                EndCombo();
        }
    }

    public void RegisterAttack()
    {
        if (comboCount == 0)
            comboTimer = comboTimeWindow;

        comboCount++;
        comboTimer = comboTimeWindow;

        int currentScore = comboCount;
        totalPoints += currentScore;

        OnComboUpdated?.Invoke(comboCount, comboCount);
        Debug.Log($"Combo counter is {comboCount}");
        Debug.Log($"Combo multy is {comboMultiplyer}");
    }

    public void TakeDamage()
    {
        EndCombo(); // If take damage then drop combo, SHOULD BE IN ANOTHER PLACE
    }

    private void EndCombo()
    {
        if (comboCount > 0)
        {
            int comboScore = comboCount * (comboCount + 1) / 2;
            scoreList.Add(comboScore);
            CalculateFinalScore();

            OnComboEnded?.Invoke(comboScore);
        }

        comboCount = 0;
        comboTimer = 0;
    }

    public List<int> GetScoreList()
    {
        return scoreList;
    }

    public void ResetComboData()
    {
        scoreList.Clear();
        totalPoints = 0;
    }

    
    public int CalculateFinalScore()
    {
        int finalScore = 0;
        foreach (var score in scoreList)
            finalScore += score;
        Debug.Log("Final score is " +  finalScore);
        return finalScore;
    }
}
