using UnityEngine;

public class ArenaListController : MonoBehaviour
{
    private Arena _arena;

    public void Init(Arena arena)
    {
        _arena = arena;
    }

    private void OnDestroy()
    {
        _arena.OnEnemyDeath(this);
    }
}
