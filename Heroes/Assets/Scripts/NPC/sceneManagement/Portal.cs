using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 initialDirection = Vector2.up; // Set the default initial direction
    public float fadeDuration = 1f;

    private Fader fader;

    private void Start()
    {
        // Find the Fader object in the scene and get the Fader component
        fader = FindObjectOfType<Fader>();
        if (fader == null)
        {
            Debug.LogError("Fader object not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the portal to " + sceneToLoad);

            // Set the player's initial direction
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetInitialDirection(initialDirection);
            }

            // Fade out and load the new scene
            if (fader != null)
            {
                StartCoroutine(fader.FadeAndLoadScene(sceneToLoad, fadeDuration));
            }
            else
            {
                // Load the new scene directly if Fader is not found
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
