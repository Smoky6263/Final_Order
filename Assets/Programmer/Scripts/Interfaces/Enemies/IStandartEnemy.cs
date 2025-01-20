
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IStandartEnemy
{
    public float MaxHealth { get; }
    public float Health { get; set; }
    public bool OnPause { get; set; }
    public VFXManager VFXManager { get; }
    public Rigidbody2D RigidBody2D { get; }
    public PauseManager PauseManager { get; }
    public SoundsController SoundsController { get; }
    public LayerMask PlayerLayer { get; }
    public EventBus EventBus { get; }


    public Vector2 PlayerDetectionArea { get; }
    public Vector3 PlayerDetectionAreaOffset { get; }

    public Vector2 PlayerFollowArea { get;}
    public Vector3 PlayerFollowAreaOffset { get; }
    public bool PlayerDetected { get; set; }


    public Vector3 GetPosition();
    public UniTask ChangeMaterial();
    public void Die();
}
