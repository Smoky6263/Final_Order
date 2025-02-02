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
public enum ParticleBanks
{
    p_Dust,
    p_BossDust,
    p_EnemyBlood,
    p_PlayerBlood,
    p_Healing,
    p_ArenaKey
}
public enum ScreenShakeBanks
{
    GiantBossRoar,
    PlayerGetDamage,
    PlayerHit
}
public enum EnemyTypes
{
    MobWithShield,
    SmallMob,
    MilishnikBig,
    MeleeMobSmall
}