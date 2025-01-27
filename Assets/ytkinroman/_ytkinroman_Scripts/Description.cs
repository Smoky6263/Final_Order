using UnityEngine;
using TMPro;


public class Description : MonoBehaviour
{
    public StringListDescruption _description;
    private TextMeshProUGUI _textMeshPro;


    private void Start ()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();

        string randomString = GetRandomString();
        _textMeshPro.text = randomString;
    }

    private string GetRandomString () 
    {
        int randomIndex = Random.Range(0, _description.stringList.Count);
        return _description.stringList[randomIndex];
    }
}
