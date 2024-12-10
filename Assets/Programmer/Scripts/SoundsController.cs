using UnityEngine;

public class SoundsController : MonoBehaviour
{
    [SerializeField] private SoundsManager _soundsManager;

    #region Player Sounds
    public void PlayerDodge() => _soundsManager.PlaySoundOneShot(FMOD_SoundBanks.PlayerDodge, transform.position);
    
    public void PlayerJumpLand() => _soundsManager.PlaySoundOneShot(FMOD_SoundBanks.PlayerJumpLand, transform.position);
    
    public void PlayerJumpStart() => _soundsManager.PlaySoundOneShot(FMOD_SoundBanks.PlayerStartJump, transform.position);

    public void PlayerFootStep() => _soundsManager.PlaySoundOneShot(FMOD_SoundBanks.PlayerFootStep, transform.position);
    
    public void PlayerGuitarSwing() => _soundsManager.PlaySoundOneShot(FMOD_SoundBanks.PlayerGuitarSwing, transform.position);

    public void PlayerOnStairs() => _soundsManager.PlaySoundOneShot(FMOD_SoundBanks.PlayerOnStairs, transform.position);

    #endregion

    #region Enemy Sounds
    public void EnemyShieldFootSteps() => _soundsManager.PlaySoundOneShot(FMOD_SoundBanks.EnemyShieldFootSteps, transform.position);
    public void EnemyShieldGetDamage() => _soundsManager.PlaySoundOneShot(FMOD_SoundBanks.EnemyShieldGetDamage, transform.position);
    public void EnemySmallFootSteps() => _soundsManager.PlaySoundOneShot(FMOD_SoundBanks.SmallEnemyFootsteps, transform.position);
    #endregion

    #region Fountain Sounds
    public void FountainFlow() => _soundsManager.PlaySoundOneShot(FMOD_SoundBanks.FountainFlow, transform.position);
    #endregion

    #region MedKit Sounds
    public void MedkKitPickUp() => _soundsManager.PlaySoundOneShot(FMOD_SoundBanks.MedKitPickUp, transform.position);
    public void MedkKitUse() => _soundsManager.PlaySoundOneShot(FMOD_SoundBanks.MedKitUse, transform.position);
    #endregion
}
