using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    [SerializeField]
    private int _frameRange = 60;

    private int[] _fpsBuffer;
    private int _fpsBufferIndex;
    
    public int AverageFPS { get; private set; }

    private void Update()
    {
        if(_fpsBuffer == null || _frameRange != _fpsBuffer.Length)
        {
            InitializeBuffer();
        }
        UpdateBuffer();
        CalculateFps();
    }

    private void InitializeBuffer()
    {
        if(_frameRange <= 0)
            _frameRange = 1;

        _fpsBuffer = new int[_frameRange];
        _fpsBufferIndex = 0;
    }

    private void UpdateBuffer()
    {
        _fpsBuffer[_fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);

        if(_fpsBufferIndex >= _frameRange)
        {
            _fpsBufferIndex = 0;
        }
    }

    private void CalculateFps()
    {
        int sum = 0;

        for (int i = 0; i < _frameRange; i++)
        {
            sum += _fpsBuffer[i];
        }

        AverageFPS = sum / _frameRange;
    }
}
