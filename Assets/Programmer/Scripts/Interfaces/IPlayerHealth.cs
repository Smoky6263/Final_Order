using UnityEngine;
public interface IPlayerHealth
{
    public void GetDamage(float value, Vector2 damageForce);
    public void ImproveHealth();

    public void OnMedKitPickUp();
}
