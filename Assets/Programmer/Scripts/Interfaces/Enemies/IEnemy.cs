using UnityEngine.PlayerLoop;

public interface IEnemy
{
    public IEnemyHealth HealthManager { get; }
}