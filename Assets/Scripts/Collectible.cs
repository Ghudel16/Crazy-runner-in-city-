using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1;
    public float spinSpeed = 180f;

    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddDiamonds(value);
            Destroy(gameObject);
        }
    }
}
