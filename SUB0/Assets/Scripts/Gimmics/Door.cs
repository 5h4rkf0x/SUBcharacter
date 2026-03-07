using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        GameManager.instance.cam.transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y,-10f);
    }
}
