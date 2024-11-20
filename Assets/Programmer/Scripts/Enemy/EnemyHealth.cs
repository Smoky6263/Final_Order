using UnityEngine;

public class EnemyHealth
{
    public EnemyHealth(IEnemy context)
    {
        Context = context;
        Context.Health = Context.MaxHealth;
    }

    private IEnemy Context;

    public void GetDamage(float value)
    {
        Context.Health -= value;
        Context.VFXManager.SpawnBloodParticles(Context.GetPosition());
        Context.ChangeMaterial();

        if (Context.Health <= 0)
        {
            Context.Die();
        }
    }
}
