using System;
using UnityEngine;

public class MobWithShieldTrigger : MonoBehaviour, IMobWithShieldTrigger
{
    public string UniqueID { get; private set; } = Guid.NewGuid().ToString().Substring(0, 8);
}
