using UnityEngine;

public class EnemyHealth : IEnemyHealth
{
    public EnemyHealth(IStandartEnemy context)
    {
        Context = context;
        Context.Health = Context.MaxHealth;
    }

    private IStandartEnemy Context;

    public void ApplyDamage(float value, Vector2 applyDamageForce)
    {
        Context.Health -= value;
        Context.EventBus.Invoke(new SpawnParticlesSignal(ParticleBanks.p_EnemyBlood, Context.GetPosition()));
        Context.SoundsController.EnemyApplyDamage();
        
        if (Context.Health <= 0)
        {
            Context.Die();
            return;
        }

        Context.ChangeMaterial();
        Context.RigidBody2D.AddForce(applyDamageForce, ForceMode2D.Impulse);

    }
}
