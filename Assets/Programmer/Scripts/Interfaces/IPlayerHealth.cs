using UnityEngine;
public interface IPlayerHealth
{
    public void ApplyDamage(float value, Vector2 damageForce, float throwTime);
    public void ImproveHealth();

    public void OnMedKitPickUp();
}
