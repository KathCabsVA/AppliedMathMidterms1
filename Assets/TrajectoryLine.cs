using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectile;
    public float launchSpeed = 10f;

    [Header("Trajectory Display")]
    public LineRenderer lineRenderer;
    public int linePoints = 175;
    public float timeIntervalinPoints = 0.10f;

    void Update()
    {
        if(lineRenderer != null) 
        { 
            if(Input.GetMouseButton(1))
            {
                DrawTrajectory();
                lineRenderer.enabled = true;
            }
            else
                lineRenderer.enabled = false;
        }
        if (Input.GetMouseButton(0))
        {
            var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
            _projectile.GetComponent<Rigidbody>().velocity = launchSpeed * launchPoint.up;
        }
    }

    void DrawTrajectory()
    {
        Vector3 origin = launchPoint.position;
        Vector3 startVelocity = launchSpeed * launchPoint.up;
        lineRenderer.positionCount = linePoints;
        float time = 0;
        for (int i = 0; i < linePoints; i++)
        {
            //s = u*t + 1/2*g*t*t
            var x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            var z = (startVelocity.z * time) + (Physics.gravity.z / 2 * time * time);
            Vector3 point = new Vector3(x, 0, z);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalinPoints;
        }
    }
}
