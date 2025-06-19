using UnityEngine;

public class StandOnClick : MonoBehaviour
{
    public Vector3 targetRotation = new Vector3(-90f, 0f, 0f); // Rotasi ke berdiri
    public float rotationSpeed = 180f;

    private bool shouldRotate = false;
    private Quaternion endRotation;
    private Quaternion startRotation;
    private bool hasStood = false;

    [Header("Puzzle Info")]
    public string quadID;
    public CubePuzzleManager puzzleManager;

    void Start()
    {
        startRotation = transform.rotation; ;
        endRotation = Quaternion.Euler(targetRotation);
    }

    void Update()
    {
        if (shouldRotate)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, endRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, endRotation) < 0.5f)
            {
                transform.rotation = endRotation;
                shouldRotate = false;
                hasStood = true;
                Debug.Log($"{gameObject.name} sudah berdiri ✨");

                if (puzzleManager != null)
                {
                    puzzleManager.RegisterClick(quadID);
                }
            }
        }
    }

    public void Activate()
    {
        if (!hasStood)
        {
            shouldRotate = true;
        }
    }

    void OnMouseDown()
    {
        Activate(); // Bisa di-trigger langsung dari klik juga
    }

    public void ResetState()
    {
        hasStood = false;
        shouldRotate = false;
        transform.rotation = startRotation;
    }

}
