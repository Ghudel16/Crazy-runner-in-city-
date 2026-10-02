using UnityEngine;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour
{
    public Transform player;
    public int initialSegments = 12;
    public float segmentLength = 18f;
    public float width = 8f;
    public float heightStep = 1.5f;
    public Material platformMaterial;

    readonly Queue<GameObject> active = new Queue<GameObject>();
    float nextZ;
    float currentY;

    void Start()
    {
        for (int i = 0; i < initialSegments; i++) SpawnSegment(i == 0 ? 0 : Random.Range(-2, 3));
    }

    void Update()
    {
        while (player != null && player.position.z + initialSegments * segmentLength > nextZ)
            SpawnSegment(Random.Range(-2, 3));

        while (active.Count > 0 && player != null && active.Peek().transform.position.z < player.position.z - segmentLength * 4)
            Destroy(active.Dequeue());
    }

    void SpawnSegment(int slopeSteps)
    {
        float startY = currentY;
        float endY = currentY + slopeSteps * heightStep;

        GameObject segment = GameObject.CreatePrimitive(PrimitiveType.Cube);
        segment.name = "PlatformSegment";
        segment.transform.position = new Vector3(0, (startY + endY) * 0.5f - 0.5f, nextZ);
        segment.transform.localScale = new Vector3(width, 1f, segmentLength);

        if (Mathf.Abs(endY - startY) > 0.01f)
        {
            float angle = Mathf.Atan2(endY - startY, segmentLength) * Mathf.Rad2Deg;
            segment.transform.rotation = Quaternion.Euler(-angle, 0, 0);
        }

        if (platformMaterial != null)
            segment.GetComponent<Renderer>().sharedMaterial = platformMaterial;

        active.Enqueue(segment);
        nextZ += segmentLength;
        currentY = endY;
    }
}
