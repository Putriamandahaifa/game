using UnityEngine;

public class CubePivotReset : MonoBehaviour
{
    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.rotation;
    }

    public void ResetPivot()
    {
        transform.rotation = startRotation;
    }
}