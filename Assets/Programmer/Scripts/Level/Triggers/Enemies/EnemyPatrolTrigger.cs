using System;
using UnityEngine;

public class EnemyPatrolTrigger : MonoBehaviour, IEnemyTrigger
{
    public string UniqueID { get; private set; } = Guid.NewGuid().ToString().Substring(0, 8);
}
