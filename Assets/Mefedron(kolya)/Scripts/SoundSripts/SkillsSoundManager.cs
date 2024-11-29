using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsSoundManager : MonoBehaviour
{
    [SerializeField]
    private EventReference throwEvent;

    [SerializeField]
    private EventReference endEvent;

    public void Throw()
    {
        RuntimeManager.PlayOneShot(throwEvent);
    }

    public void End()
    {
        RuntimeManager.PlayOneShot(endEvent);
    }
}
