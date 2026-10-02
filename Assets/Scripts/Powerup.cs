using UnityEngine;

public enum PowerupType { Magnet, Shield, DoubleDiamonds, SpeedBoost }

public class Powerup : MonoBehaviour
{
    public PowerupType type;
    public float duration = 5f;

    void Update() => transform.Rotate(0, 140f * Time.deltaTime, 0);

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.ActivatePowerup(type, duration);
        Destroy(gameObject);
    }
}
