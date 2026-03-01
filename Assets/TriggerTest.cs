using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, 5f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"TRIGGERED with {other.name}");
    }
}