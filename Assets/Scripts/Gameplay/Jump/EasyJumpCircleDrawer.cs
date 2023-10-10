using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyJumpCircleDrawer : MonoBehaviour
{
    [SerializeField]
    private Color _Color;
    

    public LineRenderer circleRenderer;

    public int steps = 200;

    public float radius = 1f;

    private void DrawCircle(int steps, float radius)
    {
        circleRenderer.startColor = _Color;
        circleRenderer.endColor = _Color;

        circleRenderer.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x, y, 0);

            circleRenderer.SetPosition(currentStep, currentPosition);
        }
    }

    public void Draw()
    {
        DrawCircle(steps, radius);
    }
}
