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
        Context.EventBus.Invoke(new SpawnParticlesSignal(ParticleBanks.p_EnemyBlood, Context.GetPosition()));
        Context.SoundsController.EnemyApplyDamage();

        if (Context.Health <= 0)
        {
            Context.Health = 0f;
            Context.EventBus.Invoke(new BossOnHealthChangeSignal(Context.Health / Context.MaxHealth));
            Context.Die();
            return;
        }
        Context.EventBus.Invoke(new BossOnHealthChangeSignal(Context.Health / Context.MaxHealth));
        Context.ChangeMaterial();
    }
}
