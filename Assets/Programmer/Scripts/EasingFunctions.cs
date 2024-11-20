using Cysharp.Threading.Tasks;
using System.Numerics;

public static class EasingFunctions
{


    //public static async UniTask<float> EaseOutBackForUI(Vector3 startPosition, Vector3 targetPosition, float duration, float overshoot)
    //{
    //    float elapsedTime = 0f;

    //    while(elapsedTime < duration)
    //    {
    //        float t = elapsedTime / duration;

    //        float easedT = EaseOutBack(t, overshoot);

    //        Vector3 cerrentPosition
    //    }

    //}

    public static float EaseOutBack(float t, float s)
    {
        t -= 1;
        return 1 + t * t * ((s + 1) * t + s);
    }
}
