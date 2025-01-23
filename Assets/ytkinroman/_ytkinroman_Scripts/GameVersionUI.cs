using UnityEngine;
using TMPro;

public class GameVersionUI : MonoBehaviour
{
    private TextMeshProUGUI versionText;

    private void Start () {
        versionText = GetComponent<TextMeshProUGUI>();

        if (versionText != null) {
            string version = Application.version;
            versionText.text = $"ver. {version}";
        }
    }
}
