using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;

    Animator anim;
    [SerializeField] private Text scoreText;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            scoreText.text = $"Highschore: {ScoreManager.score}";
        }
    }

    public void ShowWarning()
    {
        anim.SetTrigger("Warning");
    }
}