using UnityEngine;
using System.Collections.Generic;
//using System;
//using System.Diagnostics;
//using System.Numerics;

public class VREarthS : MonoBehaviour
{

    public int asteroidCount = 50;
    public float radius = 1.77f;
    public float strength = 1f;

    public AudioSource explosion;
    public ParticleSystem MassiveImpact;

    public GameObject asteroidPrefabOne;
    public GameObject asteroidPrefabTwo;
    public GameObject asteroidPrefabThree;
    public GameObject asteroidPrefabFour;
    public GameObject asteroidPrefabFive;
    public GameObject asteroidPrefabSix;
    public GameObject asteroidPrefabSeven;

    private List<GameObject> asteroidList = new List<GameObject>();
    private Transform parent;
    private int index = -1;

    private List<GameObject> prefabList = new List<GameObject>();


    public float asteroidSpacingNoiseVariance = 0.3f;

    public float asteroidScale = 7f; // size multiplier
    public bool randomizeScale = true; //if you want to turn off scale entirely
    public float scaleVariance = 2f;


    public float explosionSize = 0.01f;

    void Start()
    {
        parent = transform;

        // Add all prefabs to list
        prefabList.Add(asteroidPrefabOne);
        prefabList.Add(asteroidPrefabTwo);
        prefabList.Add(asteroidPrefabThree);
        prefabList.Add(asteroidPrefabFour);
        prefabList.Add(asteroidPrefabFive);
        prefabList.Add(asteroidPrefabSix);
        prefabList.Add(asteroidPrefabSeven);

        SpawnAsteroids();
    }

    public void CLICKhitEarth()
    {
        if (asteroidList.Count > 0)
        {
            index = Random.Range(0, asteroidList.Count);
            GameObject asteroid = asteroidList[index];

            Rigidbody rb = asteroid.GetComponent<Rigidbody>();
            if (rb != null) //always check
            {
                rb.isKinematic = false; // NOW it's affected by physics
            }

            Debug.Log($"Pulling asteroid index: {index}");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            CLICKhitEarth();
        }

        if (index != -1 && index < asteroidList.Count)
        {
            GameObject asteroid = asteroidList[index];
            if (asteroid != null)
            {
                Rigidbody rb = asteroid.GetComponent<Rigidbody>();
                if (rb != null && !rb.isKinematic)
                {
                    Vector3 direction = (transform.position - asteroid.transform.position).normalized;
                    rb.AddForce(direction * strength * 10f, ForceMode.Acceleration);
                }
            }
        }
    }

    void SpawnAsteroids()
    {
        float angleStep = 360f / asteroidCount;

        for (int i = 0; i < asteroidCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            float y = 0;

            Vector3 noise = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.5f, 0.5f),
            Random.Range(-1f, 1f)
            ) * asteroidSpacingNoiseVariance;

            Vector3 position = new Vector3(x, y, z) + noise;

            GameObject prefab = prefabList[Random.Range(0, prefabList.Count)];
            GameObject asteroid = Instantiate(prefab, parent);
            asteroid.transform.localPosition = position;

            if (randomizeScale)
            {
                float variation = Random.Range(-scaleVariance, scaleVariance);
                float scale = asteroidScale + variation;
                asteroid.transform.localScale = Vector3.one * Mathf.Max(scale, 0.1f); // prevent scale < 0
            }
            else
            {
                asteroid.transform.localScale = Vector3.one * asteroidScale;
            }

            Rigidbody rb = asteroid.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            asteroidList.Add(asteroid); //and then repeat this 50 times!
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (index == -1 || asteroidList.Count == 0) return; //always assume the worst :)

        GameObject collided = collision.gameObject;

        if (collided == asteroidList[index]) //instead of compare tag as "Astroid"
        {
            if (explosion != null) explosion.Play();

            if (MassiveImpact != null) // just to make sure it exists
            {
                Vector3 contactPoint = collision.contacts[0].point;
                Quaternion rotation = Quaternion.identity;

                ParticleSystem impact = Instantiate(MassiveImpact, contactPoint, rotation, transform); // Parent to Earth
                impact.transform.localScale = Vector3.one * explosionSize;
                impact.Play();
                Destroy(impact.gameObject, 3f);
            }

            Destroy(collided);
            asteroidList.RemoveAt(index);
            index = -1;
        }
    }

    //private void OnCollisionEnter2(Collision collision) //used for old astroid explosion
    //{
    //    Color randomlySelectedColor = GetRandomColor();
    //    GetComponent<Renderer>().material.color = randomlySelectedColor;
    //    ParticleSystem anim = Instantiate(MassiveImpact, collision.contacts[0].point, Quaternion.identity);
    //    Destroy(anim.gameObject, 3f);
    //}


}