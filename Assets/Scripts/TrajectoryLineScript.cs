using UnityEngine;

public class TrajectoryLineScript : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField, Min(10)] private int lineSegments = 60;
    [SerializeField, Min(1)] private float timeofJumpFlight = 5f;

    public void ShowTrajectoryLine(Vector3 startPoint, Vector3 startVelocity)
    {
        float timeStep = timeofJumpFlight / lineSegments;
        Vector3[] lineRendererPoints = CalculateTrajectoryLine(startPoint, startVelocity, timeStep);

        lineRenderer.positionCount = lineSegments;
        lineRenderer.SetPositions(lineRendererPoints);
        lineRenderer.enabled = true;
    }

    public void HideTrajectoryLine()
    {
        lineRenderer.enabled = false;
    }

    private Vector3[] CalculateTrajectoryLine(Vector3 startPoint, Vector3 startVelocity, float timeStep)
    {
        Vector3[] lineRendererPoints = new Vector3[lineSegments];
        for (int i = 0; i < lineSegments; i++)
        {
            float timeOffset = timeStep * i;

            Vector3 progressBeforeGravity = startVelocity * timeOffset;
            Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
            Vector3 newPosition = startPoint + progressBeforeGravity - gravityOffset;

            lineRendererPoints[i] = newPosition;
        }

        return lineRendererPoints;
    }
}
