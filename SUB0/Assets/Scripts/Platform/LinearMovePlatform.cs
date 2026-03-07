using UnityEngine;

public class LinearMovePlatform : MonoBehaviour
{
    // 선형 이동.

    [SerializeField] Vector3 station2;
    [SerializeField] float speed = 2f;

    private Vector3 station1;
    private Vector3 direction;
    private bool toStation2;

    private void Start()
    {
        station1 = transform.position;
        toStation2 = true;
        Caldir();
    }

    void Caldir()
    {
        Vector3 destination;

        if (toStation2) { destination = station2; }
        else { destination = station1; }

        direction = (destination - transform.position).normalized;
    }

    private void MovePlatform()
    {
        Vector3 destination = toStation2 ? station2 : station1;

        transform.position += direction * speed * Time.fixedDeltaTime;

        Vector3 toDest = destination - transform.position; // 지나침 확인용

        if (Vector3.Dot(toDest, direction) <= 0f)
        {
            transform.position = destination;
            toStation2 = !toStation2;
            Caldir();
        }
    }

    private void FixedUpdate()
    {
         MovePlatform();
    }


    
}
