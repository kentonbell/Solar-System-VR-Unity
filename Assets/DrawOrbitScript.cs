
using UnityEngine;

public class DrawOrbitScript : MonoBehaviour //for Line Renderer draws a circle
{

    public LineRenderer circleRenderer;

    public float thickness = 0.2f;

    public float radius;
    // Start is called before the first frame update
    void Start()
    {



        if (circleRenderer == null)
        {
            Debug.Log("NULL CIRCLE RENDERERRRRRRRRR");
            return;
        }

        circleRenderer.positionCount = 100;
        circleRenderer.useWorldSpace = false;
        circleRenderer.startWidth = thickness;
        circleRenderer.endWidth = thickness;
        circleRenderer.material = new Material(Shader.Find("Sprites/Default")); // makes it visible
        circleRenderer.startColor = Color.white;
        circleRenderer.endColor = Color.white;

        DrawCircle(100, radius);

    }
    void DrawCircle(int steps, float radius)
    {

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float interval = (float)currentStep / steps;
            float currentRadian = interval * 2 * Mathf.PI;
            float xAngle = Mathf.Cos(currentRadian);
            float yAngle = Mathf.Sin(currentRadian);
            float xCurrent = xAngle * radius;
            float yCurrent = yAngle * radius;
            Vector3 currentPosition = new Vector3(xCurrent, 0, yCurrent);
            circleRenderer.SetPosition(currentStep, currentPosition);
        }

    }

}