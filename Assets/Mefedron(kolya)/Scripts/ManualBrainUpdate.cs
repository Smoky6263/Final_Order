using UnityEngine;
using Cinemachine;

public class ManualBrainUpdate : MonoBehaviour
{
    public CinemachineBrain cineBrain;

    private void Start()
    {
        cineBrain = GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        cineBrain.ManualUpdate();
    }
}