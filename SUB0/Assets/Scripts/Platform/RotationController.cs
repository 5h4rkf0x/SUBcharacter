using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private Transform platform;
    [SerializeField] private float rotationSpeed = 50f;


    private void FixedUpdate()
    {
        platform.RotateAround(center.position, Vector3.forward, rotationSpeed * Time.fixedDeltaTime);

        platform.rotation = Quaternion.identity;
    }
}
