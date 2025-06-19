using UnityEngine;

public class QuadClickTrigger : MonoBehaviour
{
    public StandOnClick parent;

    void OnMouseDown()
    {
        if (parent != null)
        {
            parent.Activate();
        }
    }
}