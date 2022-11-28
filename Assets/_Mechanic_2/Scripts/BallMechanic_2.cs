using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMechanic_2 : MonoBehaviour
{
    public Rigidbody ball;
    public Transform target;
    public float h = 25;
    public float gravity = -18;
    public int resolution;

    public bool debugDraw = false;

    private float _time;
    private void Start()
    {
        ball.useGravity = false;
    }

    void LaunchControl()
    {
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchInfo().initialVelocity;
    }

    LaunchInfo CalculateLaunchInfo()
    {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacemenX = new Vector3(target.position.x - ball.transform.position.x, 0, 0);
        _time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityX = displacemenX / _time;

        return new LaunchInfo(velocityX + velocityY * Mathf.Sign(h), _time);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchControl();
            CatchBallMovement.Instance.CalculateMovement(_time);
        }
        if (debugDraw)
        {
            DrawPath();
        }
    }
    private void DrawPath()
    {
        LaunchInfo launchInfo = CalculateLaunchInfo();
        Vector3 previousDrawpoint = ball.position;

        for (int i = 0; i < resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchInfo.timeToTarget;
            Vector3 displacement = launchInfo.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2;

            Vector3 drawPoint = ball.position + displacement;

            Debug.DrawLine(previousDrawpoint, drawPoint, Color.green);
            previousDrawpoint = drawPoint;
        }
    }
}

struct LaunchInfo
{
    public Vector3 initialVelocity;
    public float timeToTarget;

    public LaunchInfo(Vector3 initialVelocity, float timeToTarget)
    {
        this.initialVelocity = initialVelocity;
        this.timeToTarget = timeToTarget;
    }
}
