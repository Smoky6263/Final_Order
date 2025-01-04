using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;

    [SerializeField] private float globalShakeForce = 1f;
    [SerializeField] private CinemachineImpulseListener _impulseListener;

    private CinemachineImpulseDefinition _impulseDefinition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }

    public void ScreenShakeFromProfile(ScreenShakeProfile profile, CinemachineImpulseSource impulseSource)
    {
        SetupScreenShakeSettings(profile, impulseSource);

        impulseSource.GenerateImpulseWithForce(profile.impactForce);
    }

    private void SetupScreenShakeSettings(ScreenShakeProfile profile, CinemachineImpulseSource impulseSource)
    {
        _impulseDefinition = impulseSource.m_ImpulseDefinition;


        //impulseSource
        _impulseDefinition.m_ImpulseDuration = profile.impactTime;
        _impulseDefinition.m_CustomImpulseShape = profile.impulseCurve;
        impulseSource.m_DefaultVelocity = profile.defaultVelocity;

        //impulseListener
        _impulseListener.m_ReactionSettings.m_AmplitudeGain = profile.listenerAmplitude;
        _impulseListener.m_ReactionSettings.m_FrequencyGain = profile.listenerFrequency;
        _impulseListener.m_ReactionSettings.m_Duration = profile.listenerDuration;
    }
}
