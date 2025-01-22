using UnityEngine;
using Cinemachine;
using AYellowpaper.SerializedCollections;

public class CameraShakeManager : MonoBehaviour
{
    [SerializeField] private float globalShakeForce = 1f;
    [SerializeField] private CinemachineImpulseListener _impulseListener;

    [SerializedDictionary("Particle Names", "Particle GameObjects"), SerializeField]
    private SerializedDictionary<ScreenShakeBanks, ScreenShakeProfile> _ScreenShakeBanks;

    private CinemachineImpulseSource _impulseSource;
    private CinemachineImpulseDefinition _impulseDefinition;

    private EventBus _eventBus;

    private void Awake() => _impulseSource = GetComponent<CinemachineImpulseSource>();
    private void Start()
    {
        _eventBus = GetComponent<GameManager>().EventBus;
        _eventBus.Subscribe<ScreenShakeSignal>(ScreenShakeFromProfile);
    }

    public void ScreenShakeFromProfile(ScreenShakeSignal signal)
    {
        SetupScreenShakeSettings(_ScreenShakeBanks[signal.Profile]);
        _impulseSource.GenerateImpulseWithForce(_ScreenShakeBanks[signal.Profile].impactForce);
    }

    private void SetupScreenShakeSettings(ScreenShakeProfile profile)
    {
        _impulseDefinition = _impulseSource.m_ImpulseDefinition;


        //impulseSource
        _impulseDefinition.m_ImpulseDuration = profile.impactTime;
        _impulseDefinition.m_CustomImpulseShape = profile.impulseCurve;
        _impulseSource.m_DefaultVelocity = profile.defaultVelocity;

        //impulseListener
        _impulseListener.m_ReactionSettings.m_AmplitudeGain = profile.listenerAmplitude;
        _impulseListener.m_ReactionSettings.m_FrequencyGain = profile.listenerFrequency;
        _impulseListener.m_ReactionSettings.m_Duration = profile.listenerDuration;
    }
}
