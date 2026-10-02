using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Diamonds { get; private set; }
    public float Distance { get; private set; }

    Transform player;
    float startZ;

    void Awake()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null) startZ = player.position.z;
    }

    void Update()
    {
        if (player != null)
            Distance = Mathf.Max(0, player.position.z - startZ);
    }

    public void AddDiamonds(int amount)
    {
        Diamonds += amount;
        PlayerPrefs.SetInt("diamonds", Diamonds);
    }

    public void ActivatePowerup(PowerupType type, float duration)
    {
        // Hook for gameplay modifiers/UI.
        Debug.Log($"Powerup: {type} for {duration}s");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
