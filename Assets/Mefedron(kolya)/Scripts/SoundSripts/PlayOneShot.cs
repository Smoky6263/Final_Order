using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayOneShot : MonoBehaviour
{
    [SerializeField]
    private EventReference soundEventReference;

    public void PlaySoundEvent()
    {
        if (soundEventReference.IsNull == false)
        {
            RuntimeManager.PlayOneShot(soundEventReference);
        }
    }
}
