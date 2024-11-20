using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsController : MonoBehaviour
{
    [SerializeField]
    private EventReference dodgeEvent;

    [SerializeField]
    private EventReference jumpStartEvent;

    [SerializeField]
    private EventReference groundEvent;

    [SerializeField]
    private EventReference footstepsEvent;

    public void Dodge()
    {
        if (dodgeEvent.IsNull == false)
        {
            RuntimeManager.PlayOneShot(dodgeEvent);
        }
    }

    public void Land()
    {
        if (groundEvent.IsNull == false)
        {
            RuntimeManager.PlayOneShot(groundEvent);
        }
    }

    public void jumpStart()
    {
        if (jumpStartEvent.IsNull == false)
        {
            RuntimeManager.PlayOneShot(jumpStartEvent);
        }
    }

    public void FootSteps()
    {
        if (footstepsEvent.IsNull == false)
        {
            RuntimeManager.PlayOneShot(footstepsEvent);
        }
    }
}
