using FMODUnity;
using UnityEngine;

public class ParameterChanger : MonoBehaviour
{
    [SerializeField] private string _parametrName = string.Empty;
    [SerializeField] private float _parametrValue = 0f;

    public void ChangeParametrValue()
    {
        RuntimeManager.StudioSystem.setParameterByName(_parametrName, _parametrValue);
    }
}
