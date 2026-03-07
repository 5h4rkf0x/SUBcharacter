using UnityEngine;

public class BulletHit : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        // 해당 총알의 애니메이션이 끝났다면 총알 비활성화 - 애니메이션 이벤트 연계로 호출
        gameObject.SetActive(false);
    }
}
