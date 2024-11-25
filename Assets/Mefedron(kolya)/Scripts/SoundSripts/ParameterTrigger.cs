using UnityEngine;
using FMODUnity;

public class ParameterTrigger : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter emitter;
    [SerializeField] private string parameterName;
    [SerializeField] private float enterValue = 1f;
    [SerializeField] private float exitValue = 0f;

    private string _player = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _player)
        {
            Debug.Log(parameterName);
            emitter.EventInstance.setParameterByName(parameterName, enterValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            emitter.EventInstance.setParameterByName(parameterName, exitValue);
        }
    }
}
