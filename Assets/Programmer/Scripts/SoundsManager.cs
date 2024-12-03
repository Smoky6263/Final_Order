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

    #region Fountain
    FountainFlow,
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
