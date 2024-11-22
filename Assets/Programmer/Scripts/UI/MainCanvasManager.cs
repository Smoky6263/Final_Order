using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    [SerializeField] private VFXPrefabs _prefabs;

    public void SpawnUIElement(GameObject gameObject)
    {
        Instantiate(gameObject, transform);
    }
}
