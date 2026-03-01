
using UnityEngine;

public class VRSpaceship : MonoBehaviour
{
    /// <summary>
    /// Pretty confident with this code
    /// </summary>

    public bool isTakeoff;
    public bool isLanding;


    public Transform endMarker;
    public Transform startMarker;
    //public Transform startMarkerOne; //unused

    private Transform triggerSphere;

    //public GameObject targetPlanet; //maybe?

    public Transform spaceStation;

    public Transform MarsGroup;


    public ParticleSystem dust;

    public float speed = 1.0f;

    private float index;
    public float ThrustForce = 1f;
    float time = 0.0f;

    public float RotationLandingTime = 5.0f;


    private Quaternion dockedRotation;

    public GameObject boosters;

    public GameObject BIGboosters;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.parent = spaceStation;
        GetComponent<Rigidbody>().isKinematic = true;
        index = speed * Time.deltaTime;


    }


    public void CLICKland()
    {
        boosters.SetActive(true);

        //PlayBoosters();


        Debug.Log($"isLanding: {isLanding}, isTakeoff: {isTakeoff}");
        Debug.Log($"triggerSphere in LandInSphere: {triggerSphere}");

        transform.rotation = startMarker.rotation; // or .up = Vector3.up;
                                                   //GetComponent<Rigidbody>().useGravity = true;
        isLanding = true;
        isTakeoff = false;
        transform.parent = MarsGroup;
        //transform.rotation = dockedRotation;
        transform.localScale = Vector3.one; // just in case
        transform.rotation = Quaternion.identity; // or dockedRotation if you saved it
                                                  //transform.localPosition = Vector3.zero;
                                                  //transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().detectCollisions = true;

        Debug.Log($"2isLanding: {isLanding}, isTakeoff: {isTakeoff}");
        Debug.Log($"2triggerSphere in LandInSphere: {triggerSphere}");

        time = 0.0f;

    }

    public void CLICKreturn()
    {
        PlayBoosters();
        boosters.SetActive(true);

        isLanding = false;
        isTakeoff = true;
        GetComponent<Rigidbody>().isKinematic = false;

        //if (triggerSphere != null)
        //{
        //    Debug.Log("Adding takeoff!!!");
        //    GetComponent<Rigidbody>().AddForce(transform.up * ThrustForce, ForceMode.Acceleration);
        //}

        if (isTakeoff)
        {
            if (triggerSphere == null)
            {
                ThrustOff();
            }
            else
            {
                AddTakeOff();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.L)) //Lander
        {
           CLICKland();
        }

        if (isLanding)
        {
            //isTakeoff = false;

            LandInSphere();

        }

        if (Input.GetKeyDown(KeyCode.R)) //Returner and Takeoff
        {

            CLICKreturn();
        }

        if (isTakeoff)
        {
            if (triggerSphere == null)
            {
                ThrustOff();
            }
        }

        //if (isTakeoff)
        //{
        //    if (triggerSphere != null)
        //    {
        //        TakeOff();
        //    }
        //    else
        //    {
        //        TakeOffInSphere();
        //    }
        //}

    }

    private void AddTakeOff()
    {

        if (!isTakeoff) return;

        //do the 4 dusts
        //if (dust[0]) dust[0].Play();

        //transform.position = Vector3.MoveTowards(transform.position, endMarker.position, index);

        //if (triggerSphere != null) return;

        if (!Input.GetKeyDown(KeyCode.R)) return;

        Debug.Log("Adding takeoff!!!");

        GetComponent<Rigidbody>().AddForce(transform.up * ThrustForce, ForceMode.Acceleration);

        //confident
    }


    private void ThrustOff()
    {
        //if (triggerSphere == null) return;

        //you can insted do time <= 1
        if (Vector3.Distance(transform.position, spaceStation.position) > 0.1f)
        {

            transform.position = Vector3.Lerp(transform.position, spaceStation.position, time);
            transform.rotation = Quaternion.Lerp(transform.rotation, startMarker.rotation, time);
            time += 0.2f * Time.deltaTime; //you can maybe make this 0.5


        }
        else
        {

            HardResetDocking();
            //transform.rotation = spaceStation.rotation;
            //transform.position = spaceStation.position;
            //transform.parent = spaceStation;

            //var rb = GetComponent<Rigidbody>();
            //rb.linearVelocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            ////rb.useGravity = false;
            //rb.isKinematic = true;
            //rb.detectCollisions = false;

            //isTakeoff = false;
            //triggerSphere = null;

            //dockedRotation = startMarker.rotation; // or ship's current rotation
            //transform.rotation = dockedRotation;





            //transform.rotation = spaceStation.rotation;
            //transform.position = spaceStation.position;
            //transform.parent = spaceStation;
            //GetComponent<Rigidbody>().isKinematic = true;
            //isTakeoff = false;
            //GetComponent<Rigidbody>().detectCollisions = false; //////////
            //triggerSphere = null;
        }
        //confident

    }





    private void LandInSphere()
    {
        //Debug.Log("Calling LandInSphere()");

        float localTimevariable = Time.deltaTime * RotationLandingTime;

        transform.position = Vector3.MoveTowards(transform.position, endMarker.transform.position, localTimevariable);


        //Debug.Log($"triggerSphere is: {triggerSphere}");
        if (triggerSphere == null) return;


        var normal = transform.position - triggerSphere.position;

        var direction = Vector3.RotateTowards(transform.up, normal, localTimevariable, Mathf.Deg2Rad * 1f); // 1 degree per frame max
        //var direction = Vector3.RotateTowards(transform.up, normal, localTimevariable, 0);

        transform.up = direction;


        //confident
    }




    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entered Trigger Sphere!");
        if (!other.gameObject.CompareTag("MarsTriggerZone")) return;

        Debug.Log("Entered Trigger Sphere AND IS A SPACESHIP!");
        triggerSphere = other.transform;


        isLanding = true;     // Auto-trigger landing when entering Mars zone
        isTakeoff = false;

    }

    private void OnTriggerExit(Collider other)
    {

        //Debug.Log("Exited Trigger Sphere!");

        if (!other.gameObject.CompareTag("MarsTriggerZone")) return;

        Debug.Log("Exited Trigger Sphere AND IS A SPACESHIP!");

        triggerSphere = null;

    }






    private void HardResetDocking()
    {
        var rb = GetComponent<Rigidbody>();

        // stop all physics movement
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // freeze physics
        rb.isKinematic = true;
        rb.detectCollisions = false;

        // reset parent & local transform
        transform.parent = spaceStation;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        //set rotation manually because station is rotating
        transform.rotation = spaceStation.rotation;

     
        isTakeoff = false;
        isLanding = false;
        triggerSphere = null;

        //log it
        Debug.Log("?? Ship hard docked at space station.???");
    }


    public void PlayBoosters()
    {
        ParticleSystem[] particles = BIGboosters.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particles)
        {
            ps.Play();
        }
    }

}
