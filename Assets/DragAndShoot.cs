using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class DragAndShoot : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectile;
    public float launchSpeed = 10f;

    [Header("Trajectory Display")]
    public LineRenderer lineRenderer;
    public int linePoints = 175;
    public float timeIntervalinPoints = 0.10f;

    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;

    private Rigidbody rb;

    private bool isShoot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {

        mousePressDownPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {

        mouseReleasePos = Input.mousePosition;
        Shoot(Force: mousePressDownPos - mouseReleasePos);
    }

    private float forceMultiplier = 3;

    void Shoot(Vector3 Force)
    {
        if (isShoot)
            return;

        rb.AddForce(new Vector3(Force.x, Force.y, z: Force.y) * forceMultiplier);
        isShoot = true;
    }

    void Update()
    {
        if (lineRenderer != null)
        {
            if (Input.GetMouseButton(1))
            {
                DrawTrajectory();
                lineRenderer.enabled = true;
            }
            else
                lineRenderer.enabled = false;
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
} 

