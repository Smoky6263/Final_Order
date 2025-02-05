
public class SmallMobEnemyStateMachine : EnemyStateMachine
{
    public override void Die()
    {
        //DO SOMETHING
        Destroy(transform.parent.gameObject);
    }
}
