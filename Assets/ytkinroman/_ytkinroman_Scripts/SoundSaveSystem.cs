using UnityEngine;
using System.IO;


public class SoundSaveSystem : ISaveable
{
    private const string _fileName = "SaveData/soundData.json";
    private string _filePath;

    public SoundData _soundData { get; private set; } = new SoundData();


    public void Initialization ()
    {
        _filePath = Path.Combine(Application.persistentDataPath, _fileName);
        LoadData();
    }


    public void LoadData ()
    {
        if (File.Exists(_filePath)) {
            string jsonData = File.ReadAllText(_filePath);
            _soundData = JsonUtility.FromJson<SoundData>(jsonData);
        }
        else {
            SaveDefaultData();
        }
    }


    public void SaveData ()
    {
        string jsonData = JsonUtility.ToJson(_soundData);

        string folderPath = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }

        File.WriteAllText(_filePath, jsonData);
    }

    private void SaveDefaultData ()
    {
        _soundData = new SoundData();
        SaveData();
    }
}
