using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Description : MonoBehaviour
{
    public StringListDescruption _descruption;
    private TextMeshProUGUI _textMeshPro;


    private void Start ()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();

        string randomString = GetRandomString();
        _textMeshPro.text = randomString;
    }

    private string GetRandomString () 
    {
        int randomIndex = Random.Range(0, _descruption.stringList.Count);
        return _descruption.stringList[randomIndex];
    }
}
