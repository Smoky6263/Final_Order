using UnityEngine;

public class SoundsController : MonoBehaviour
{
    public SoundsManager SoundsManager { get; set; }

    #region Player Sounds
    public void PlayerDodge() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.PlayerDodge, transform.position);
    
    public void PlayerJumpLand() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.PlayerJumpLand, transform.position);
    
    public void PlayerJumpStart() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.PlayerStartJump, transform.position);

    public void PlayerFootStep() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.PlayerFootStep, transform.position);

    public void PlayerGuitarSwing() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.PlayerGuitarSwing, transform.position);

    public void PlayerOnStairs() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.PlayerOnStairs, transform.position);

    #endregion

    #region Enemy Sounds
    public void EnemyShieldFootSteps() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.EnemyShieldFootSteps, transform.position);
    public void EnemyApplyDamage() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.EnemyGetDamage, transform.position);
    public void EnemySmallFootSteps() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.EnemySmallFootsteps, transform.position);
    #endregion

    #region GiantBoss Sounds
    public void GiantBossRoar() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.GiantBossRoar, transform.position);
    #endregion


    #region Fountain Sounds
    public void FountainFlow() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.FountainFlow, transform.position);
    #endregion

    #region MedKit Sounds
    public void MedkKitPickUp() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.MedKitPickUp, transform.position);
    public void MedkKitUse() => SoundsManager.PlaySoundOneShot(FMOD_SoundBanks.MedKitUse, transform.position);
    #endregion

    public void PlaySound(FMOD_SoundBanks soundBank) => SoundsManager.PlaySoundOneShot(soundBank, transform.position);
}
