using UnityEngine;
using System.IO;


public class UserSaveSystem : ISaveable
{
    private const string _fileName = "SaveData/userData.json";
    private string _filePath;

    public UserData _userData { get; private set; } = new UserData();


    public void Initialization ()
    {
        _filePath = Path.Combine(Application.persistentDataPath, _fileName);
        LoadData();
    }


    public void LoadData ()
    {
        if (File.Exists(_filePath)) {
            string jsonData = File.ReadAllText(_filePath);
            _userData = JsonUtility.FromJson<UserData>(jsonData);
        }
        else {
            SaveDefaultData();
        }
    }


    public void SaveData ()
    {
        string jsonData = JsonUtility.ToJson(_userData);

        string folderPath = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }

        File.WriteAllText(_filePath, jsonData);
    }


    private void SaveDefaultData ()
    {
        _userData = new UserData();
        SaveData();
    }
}

