using UnityEngine;

public class BulletHit : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
