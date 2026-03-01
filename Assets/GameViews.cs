using UnityEngine;
using UnityEngine.SceneManagement;

public class GameViews : MonoBehaviour
{

    public bool frontView;
    public bool topView;
    public bool topView2;
    public bool sunView;
    public bool earthView;
    public bool marsView;
    public bool spaceshipView;


    private int ToggleIndex = 0;

    public GameObject earth;
    public GameObject mars;
    public GameObject spaceship;



    void DisableAllViews()
    {
        frontView = false;
        topView = false;
        topView2 = false;
        sunView = false;
        earthView = false;
        marsView = false;
        spaceshipView = false;
    }

    bool IsAllDisabled()
    {
        return !frontView && !topView && !topView2 && !sunView && !earthView && !marsView && !spaceshipView;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T)) //toggle views
        {
            //Debug.Log("T pressed1. frontView: " + frontView + ", topView: " + topView);

            DisableAllViews();

            if (ToggleIndex == 0)
            {
                frontView = true;
                ToggleIndex++;
                
            }
            else if (ToggleIndex == 1)
            {
                topView = true;
                ToggleIndex++;

            }
            else if (ToggleIndex == 2)
            {
                topView2 = true;
                ToggleIndex++;
            }
            else
            {
                frontView = true; //start over
                ToggleIndex = 1; //get ready for next
            }


            //Debug.Log("T pressed2. frontView: " + frontView + ", topView: " + topView);

        }
        
        if (frontView)
        {
            transform.position = new Vector3(40f, 20f, -23f);
            transform.rotation = Quaternion.AngleAxis(-40f, Vector3.up);
            //transform.rotation = Quaternion.identity;
        }


        if (topView)
        {
            transform.position = new Vector3(0f, 478f, 0f);
            transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
        }

        if (topView2)
        {
            transform.position = new Vector3(0f, 70f, 0f);
            transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
        }


        if (Input.GetKeyDown(KeyCode.I)) //go to starting view
        {
            DisableAllViews();
        }


        if (Input.GetKeyDown(KeyCode.N)) SceneManager.LoadScene(SceneManager.GetActiveScene().name); //restart


        if (IsAllDisabled()) //starting view
        {         
            transform.position = new Vector3(0f, 200f, 0f);
            transform.rotation = Quaternion.AngleAxis(65f, Vector3.right);
        }



        if (Input.GetKeyDown(KeyCode.P)) //get sunview
        {
            DisableAllViews();
            sunView = true;                       
        }

        if (sunView) { 
            transform.position = new Vector3(814f, 51f, 473f);
            transform.rotation = Quaternion.AngleAxis(0f, Vector3.left);
            transform.rotation = Quaternion.AngleAxis(263f, Vector3.up);

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DisableAllViews();
            earthView = true;
        }


        if (earthView)
        {

            //transform.rotation = Quaternion.AngleAxis(-40f, Vector3.up);

            Vector3 offset = new Vector3(9f, 10f, -7f);
            Vector3 desiredPosition = earth.transform.position + offset;
            transform.position = desiredPosition;
            transform.LookAt(earth.transform);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            DisableAllViews();
            marsView = true;
        }

        if (marsView)
        {
            Vector3 offset = new Vector3(10f, 7f, -5f);
            Vector3 desiredPosition = mars.transform.position + offset;
            transform.position = desiredPosition;
            transform.LookAt(mars.transform);
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            DisableAllViews();
            spaceshipView = true;
        }

        if (spaceshipView)
        {
            Vector3 offset = new Vector3(3f, 1f, -3f);
            Vector3 desiredPosition = spaceship.transform.position + offset;
            transform.position = desiredPosition;
            transform.LookAt(spaceship.transform);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q)) //exit
        {
            transform.position = new Vector3(40f, 20f, -23f);
            transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
            Application.Quit();
        }

        

    } // end Update



    


}
