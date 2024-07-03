using UnityEngine;
using UnityEngine.SceneManagement; 

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;

    public HealthBar healthBar;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        animator.SetTrigger("Death");

        // Reload the scene after a delay
        Invoke("RestartScene", 2.0f); // 2 seconds delay before restarting the scene
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
