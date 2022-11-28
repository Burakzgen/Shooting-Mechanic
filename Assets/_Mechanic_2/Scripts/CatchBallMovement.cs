using UnityEngine;

public class CatchBallMovement : MonoBehaviour
{
    public static CatchBallMovement Instance;
    public float speed;
    public Transform target;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.right * speed * Time.fixedDeltaTime;
        if (Input.GetKeyDown(KeyCode.A))
        {
            MakeTargetRandom();
        }
    }
    public void CalculateMovement(float time)
    {
        float distance=target.position.x-transform.position.x;
        speed = distance / time; 


    }
    private void MakeTargetRandom()
    {
        int random = Random.Range(-20, 30);
        Vector3 newPos = target.position;
        newPos.x = random;
        target.position = newPos;
    }
}
