
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IEnemy
{
    public float MaxHealth { get; }
    public float Health { get; set; }
    public VFXManager VFXManager { get; }
    public EnemyHealth HealthManager { get; }
    public Vector3 GetPosition();
    public void Die();
    public UniTask ChangeMaterial();
}
