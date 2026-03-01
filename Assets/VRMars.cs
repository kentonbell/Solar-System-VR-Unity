using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class VRMars : MonoBehaviour
{


	public bool isTakeoff;
	public bool isLanding;


	public Transform endMarker;
	public Transform startMarker;
	

	private Transform triggerSphere;



	public Transform spaceStation;


	public ParticleSystem dust;

	public float speed = 10f;

	private float index;
	public float ThrustForce = 10f;
	//float time = 0.0f;



	public float radius = 20f;
	public float strength = 100f;
	public float lesserStrength = 100f;

	float tempStrength;

	

	private Vector3 startPos;


	void Start()
	{
		tempStrength = strength;


	}

	


    void Update()
	{
		

		if (Input.GetKeyDown(KeyCode.L)) //Lander
		{
			strength = tempStrength;
			isLanding = true;
			isTakeoff = false;
			//transform.parent = null;
			//GetComponent<Rigidbody>().isKinematic = false;

			//time = 0.0f;
		}

		if (isLanding)
		{
			//isTakeoff = false;

			//LandInSphere();

		}

		if (Input.GetKeyDown(KeyCode.R)) //Returner and Takeoff
		{
			strength = lesserStrength;

			isLanding = false;
			isTakeoff = true;
			//GetComponent<Rigidbody>().isKinematic = false;


			//TakeOff();

			/*
			if (isTakeoff)
			{
				if (triggerSphere != null)
				{
					TakeOff();
				}
				else
				{
					TakeOffInSphere();
				}
			} */
		}


	
        Collider[] objects = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider col in objects)
        {
            if (col.GetComponent<Rigidbody>())
            { //Must be rigidbody
                applyGravity(col);
            }
        }

    }


	//private void OnTriggerEnter(Collider other)
	//{

 //       if (!other.gameObject.CompareTag("MarsTriggerZone")) return;
	//	Debug.Log("Entered Trigger Sphere AND IS A SPACESHIP!");

	//	triggerSphere = other.transform;
		

	//}


	//private void OnTriggerExit(Collider other)
 //   {

 //       if (!other.gameObject.CompareTag("MarsTriggerZone")) return;
	//	Debug.Log("Exited Trigger Sphere AND IS A SPACESHIP!");

	//	triggerSphere = null;

 //   }







    void applyGravity(Collider col)
	{
		float dynamicDistance = Mathf.Abs((Vector3.Distance(transform.position, col.transform.position)) - (radius));
		float multiplier = dynamicDistance / radius;

		col.GetComponent<Rigidbody>().AddForce((strength * (transform.position - col.transform.position).normalized) * multiplier, ForceMode.Force);

	}








	//private void OnCollisionEnter(Collision collision)
	//{

	//	//Debug.Log("On collision enter not implemented!!!");
	//	/*
	//	Color randomlySelectedColor = GetRandomColor();
	//	GetComponent<Renderer>().material.color = randomlySelectedColor;
	//	ParticleSystem anim = Instantiate(dust, collision.contacts[0].point, Quaternion.identity);
	//	*/

	//}








}
