using UnityEngine;
using UnityEngine.SceneManagement;

public class VRGameViews : MonoBehaviour
{

    public bool isfrontView;
    public bool istopView;
    public bool istopView2;
    public bool issunView;
    public bool isearthView;
    public bool ismarsView;
    public bool isspaceshipView;


    //private int ToggleIndex = 0;

    public GameObject earth;
    public GameObject mars;
    public GameObject spaceship;



    void DisableAllViews()
    {
        isfrontView = false;
        istopView = false;
        istopView2 = false;
        issunView = false;
        isearthView = false;
        ismarsView = false;
        isspaceshipView = false;
    }

    bool IsAllDisabled()
    {
        return !isfrontView && !istopView && !istopView2 && !issunView && !isearthView && !ismarsView && !isspaceshipView;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CLICKstartView();
    }





    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) SceneManager.LoadScene(SceneManager.GetActiveScene().name); //restart

        if (isearthView)
        {

            earthView();

        }

        if (ismarsView)
        {
            marsView();
        }

        if (isspaceshipView)
        {
            spaceshipView();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            DisableAllViews();
            CLICKstartView();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DisableAllViews();
            CLICKsunView();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            DisableAllViews();
            CLICKtopView();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            DisableAllViews();
            CLICKtopView2();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            DisableAllViews();
            CLICKfrontView();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DisableAllViews();
            CLICKspaceshipView();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            DisableAllViews();
            CLICKearthView();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            DisableAllViews();
            CLICKmarsView();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q)) //exit
        {
            CLICKexit();
        }





    }// end Update


    public void CLICKfrontView()
    {
        transform.position = new Vector3(40f, 20f, -23f);
        transform.rotation = Quaternion.AngleAxis(-40f, Vector3.up);
        //transform.rotation = Quaternion.identity;
    }


    public void CLICKtopView()
    {
        transform.position = new Vector3(0f, 478f, 0f);
        transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
    }

    public void CLICKtopView2()
    {
        transform.position = new Vector3(0f, 70f, 0f);
        transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
    }






    public void CLICKstartView()
    {
        transform.position = new Vector3(0f, 200f, 0f);
        transform.rotation = Quaternion.AngleAxis(65f, Vector3.right);
    }



    public void CLICKsunView()
    {
        transform.position = new Vector3(814f, 51f, 473f);
        transform.rotation = Quaternion.AngleAxis(0f, Vector3.left);
        transform.rotation = Quaternion.AngleAxis(263f, Vector3.up);

    }




    public void CLICKearthView()
    {


        DisableAllViews();
        isearthView = true;

    }

    public void CLICKmarsView()
    {
        DisableAllViews();
        ismarsView = true;
    }




    public void CLICKspaceshipView()
    {
        DisableAllViews();
        isspaceshipView = true;
    }


    public void earthView()
    {

        //transform.rotation = Quaternion.AngleAxis(-40f, Vector3.up);

        Vector3 offset = new Vector3(9f, 10f, -7f);
        Vector3 desiredPosition = earth.transform.position + offset;
        transform.position = desiredPosition;
        transform.LookAt(earth.transform);
    }



    public void marsView()
    {
        Vector3 offset = new Vector3(10f, 7f, -5f);
        Vector3 desiredPosition = mars.transform.position + offset;
        transform.position = desiredPosition;
        transform.LookAt(mars.transform);
    }




    public void spaceshipView()
    {
        Vector3 offset = new Vector3(3f, 1f, -3f);
        Vector3 desiredPosition = spaceship.transform.position + offset;
        transform.position = desiredPosition;
        transform.LookAt(spaceship.transform);
    }

    public void CLICKexit()
    {
        transform.position = new Vector3(40f, 20f, -23f);
        transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
        Application.Quit();
    }

}





