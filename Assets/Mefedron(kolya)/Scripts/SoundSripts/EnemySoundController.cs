using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    [SerializeField]
    private EventReference hitEvent;

    [SerializeField]
    private EventReference diedEvent;

    [SerializeField]
    private EventReference detectEvent;

    public void Hit()
    {
        if (hitEvent.IsNull == false)
        {
            RuntimeManager.PlayOneShot(hitEvent);
        }
    }

    public void Died()
    {
        if (diedEvent.IsNull == false)
        {
            RuntimeManager.PlayOneShot(diedEvent);
        }
    }

    public void detect()
    {
        if (detectEvent.IsNull == false)
        {
            RuntimeManager.PlayOneShot(detectEvent);
        }
    }

}
