using UnityEngine;

public class BallMechanic_1 : MonoBehaviour
{
    public Rigidbody ball;
    public Transform target;
    public float h = 25;
    public float gravity = -18;

    private void Start()
    {
        ball.useGravity = false;
    }

    void LaunchControl()
    {
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchVelocity();
    }

    Vector3 CalculateLaunchVelocity()
    {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacemenX = new Vector3(target.position.x - ball.transform.position.x, 0, 0);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityX = displacemenX / time;
        return velocityX + velocityY * Mathf.Sign(h);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchControl();
        }
    }

}
