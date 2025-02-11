using FMODUnity;
using UnityEngine;

public class FMODParameterChanger : MonoBehaviour
{
    private string _parametrName;
    private float _parametrValue;

    private EventBus _eventBus;

    private void Awake()
    {
        _eventBus = GetComponent<GameManager>().EventBus;
        _eventBus.Subscribe<FMODParameterChangeSignal>(ChangeParametrValue);
    }

    public void ChangeParametrValue()
    {
        RuntimeManager.StudioSystem.setParameterByName(_parametrName, _parametrValue);
    }
    private void ChangeParametrValue(FMODParameterChangeSignal signal)
    {
        RuntimeManager.StudioSystem.setParameterByName(signal.ParametrName, signal.ParametrValue);
    }

    public void SetParametrName(string name) => _parametrName = name;
    public void SetParametrValue(float value) => _parametrValue = value;


}

