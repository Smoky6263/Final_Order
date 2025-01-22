using UnityEngine;
public class CinemachineSetReferenceSignal
{
    public readonly Transform NewReferenceTransform;
    public readonly bool IsFacingRight;
 
    public CinemachineSetReferenceSignal(Transform newTransform, bool isFacingRight)
    {
        NewReferenceTransform = newTransform;
        IsFacingRight = isFacingRight;
    }
}
