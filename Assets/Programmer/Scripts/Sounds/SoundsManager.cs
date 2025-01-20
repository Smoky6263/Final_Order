using AYellowpaper.SerializedCollections;
using UnityEngine;
using FMODUnity;

public enum FMOD_SoundBanks
{
    #region Player Sounds
    PlayerFootStep,
    PlayerDodge,
    PlayerStartJump,
    PlayerJumpLand,
    PlayerGuitarSwing,
    PlayerOnStairs,
    #endregion

    #region Enemy With Shield
    EnemyShieldFootSteps,
    EnemyShieldGetDamage,
    #endregion

    #region Small Enemy
    SmallEnemyFootsteps,
    #endregion

    #region GiantBoss
    GiantBossAttack,
    GiantBossJumpStart,
    GiantBossJumpLand,
    GiantBossRoar,
    #endregion

    #region Fountain
    FountainFlow, FountainInteract,
    #endregion

    #region MedKit
    MedKitPickUp, MedKitUse,
    #endregion

}

public class SoundsManager : MonoBehaviour
{
    [SerializedDictionary("Sound Names", "Sound Banks")]
    public SerializedDictionary<FMOD_SoundBanks,EventReference> _soundBanks;

    public void PlaySoundOneShot(FMOD_SoundBanks eventReference, Vector3 position)
    {
        if (_soundBanks[eventReference].IsNull == false)
            RuntimeManager.PlayOneShot(_soundBanks[eventReference], position);
    }
}
