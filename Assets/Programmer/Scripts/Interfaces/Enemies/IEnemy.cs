
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IEnemy
{
    public float MaxHealth { get; }
    public float Health { get; set; }
    public bool OnPause { get; set; }
    public VFXManager VFXManager { get; }
    public EnemyHealth HealthManager { get; }
    public PauseManager PauseManager { get; }
    public SoundsController SoundsController { get; }
    public Vector3 GetPosition();
    public void Die();
    public UniTask ChangeMaterial();
}
