using UnityEngine;

public class BossGiantHealth : IEnemyHealth
{
    public BossGiantHealth(IBoss context)
    {
        Context = context;
        Context.Health = Context.MaxHealth;
    }

    private IBoss Context;

    public void ApplyDamage(float value, Vector2 applyDamageForce)
    {
        Context.Health -= value;
        Context.VFXManager.SpawnBloodParticles(Context.GetPosition(), Context.VFXManager.EnemyBlood);
        Context.SoundsController.EnemyApplyDamage();

        if (Context.Health <= 0)
        {
            Context.Die();
            return;
        }

        Context.ChangeMaterial();
    }
}
