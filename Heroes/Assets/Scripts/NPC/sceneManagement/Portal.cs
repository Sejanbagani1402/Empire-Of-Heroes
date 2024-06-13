using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 initialDirection = Vector2.up; // Set the default initial direction

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

            // Load the new scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
