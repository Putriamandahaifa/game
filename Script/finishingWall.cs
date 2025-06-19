using UnityEngine;

public class finishingWall : MonoBehaviour
{
    public GameObject hint4;
    public GameObject warta4;

    private bool playerNearby = false;
    private bool hintShown = false;
    private bool wartaShown = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Player masuk ke area trigger.");
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player dekat: Tekan E untuk masuk ");
            hint4.SetActive(true);
            hintShown = true;
        }

        if (hintShown && Input.GetMouseButtonDown(0))
        {
            hint4.SetActive(false);
            warta4.SetActive(true);
            hintShown = false;
            wartaShown = true;
        }

        else if (wartaShown && Input.GetMouseButtonDown(0))
        {
            warta4.SetActive(false);
            wartaShown = false;
        }
    }
}
