using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spinner : MonoBehaviour
{
    public float degreesPerSecondY = 15f;
    public float degreesPerSecondX = -15f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(degreesPerSecondX * Time.deltaTime, degreesPerSecondY * Time.deltaTime, 0f);
    }
}