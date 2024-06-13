using UnityEngine;
using UnityEngine.SceneManagement; // Required to switch scenes

public class Portal : MonoBehaviour
{
    public string sceneToLoad; // The name of the scene to switch to

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider entered: " + other.gameObject.name); // Add this line to log collisions

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the portal"); // Add this line to confirm player detection
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
