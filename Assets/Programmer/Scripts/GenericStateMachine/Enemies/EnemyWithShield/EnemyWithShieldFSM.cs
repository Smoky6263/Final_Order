using System.Collections.Generic;
using UnityEngine;

public class EnemyWithShieldFSM : StateManager<EnemyWithShieldFSM.EnemyWithShieldStates>
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private EnemyWithShieldAnimatorController _animator;

    private EnemyWithShieldFSM Context;
    private Rigidbody2D _rigidBody2D;

    [Header("Время, которое моб проводит в состоянии Idle\n(например когда игрок убежал от него)")]
    [SerializeField, Range(0f, 10f)] private float _idleTime = 3f;

    [Header("Параметры скорости моба:\n- скорость при патрулировании\n- скорость при преследовании игрока")]
    [SerializeField, Range(0f, 10f)] private float _patrollingSpeed = 2f;
    [SerializeField, Range(0f, 10f)] private float _palyerFollowSpeed = 4f;


    public Rigidbody2D RigidBody2D { get { return _rigidBody2D; } }
    public EnemyWithShieldAnimatorController AnimatorController { get { return _animator; } }
    public float IdleTime { get { return _idleTime; } } 
    public float PatrollingSpeed { get {return _patrollingSpeed;} }
    public float PlayerFollowSpeed { get {return _palyerFollowSpeed; } }

    public enum EnemyWithShieldStates
    {
        Idle,
        Walk,
        FollowPlayer
    }

    private void Awake()
    {
        Context = this;

        _rigidBody2D = GetComponent<Rigidbody2D>();

        States = new Dictionary<EnemyWithShieldStates, BaseState<EnemyWithShieldStates>>
        {
            { EnemyWithShieldStates.Idle, new EnemyWithShieldIdle(EnemyWithShieldStates.Idle, Context) },
            { EnemyWithShieldStates.Walk, new EnemyWithShieldWalk(EnemyWithShieldStates.Walk, Context) },
            { EnemyWithShieldStates.FollowPlayer, new EnemyWithShieldFollowPlayer(EnemyWithShieldStates.FollowPlayer, Context) }
        };

        CurrentState = States[EnemyWithShieldStates.Idle];
    }
}
