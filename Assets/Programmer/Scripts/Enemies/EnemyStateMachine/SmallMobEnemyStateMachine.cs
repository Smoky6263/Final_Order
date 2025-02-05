
using FMODUnity;

public class SmallMobEnemyStateMachine : EnemyStateMachine
{
    public override void Die()
    {
        RuntimeManager.PlayOneShot("event:/SFX/MobsSFX/SmallMobDeath");
        Destroy(transform.parent.gameObject);
    }
}
