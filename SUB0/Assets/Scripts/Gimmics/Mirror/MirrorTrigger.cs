using UnityEngine;

public class MirrorTrigger : MonoBehaviour, IResetable
{
    [Header("미러 플레이어 프리팹 및 기준선")]
    [SerializeField] private GameObject mirrorPrefab;
    [SerializeField] private float mirrorCenterX = 0f;
    [SerializeField] MirrorExitDoor exitDoor;

    private GameObject mirrorInstance;
    private bool isTriggered = false;

    public void Init()
    {
        // 이미 만들어진 미러 플레이어 제거
        if (mirrorInstance != null)
        {
            mirrorInstance.SetActive(false);
            Debug.Log("[MirrorTrigger] 미러 플레이어 제거됨!");
        }

        // 트리거 초기화
        isTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered && !exitDoor.IsOpen && collision.CompareTag("Player"))
        {
            isTriggered = true;

            // 미러 플레이어 위치 계산
            Vector3 realPos = collision.transform.position;
            float mirroredX = mirrorCenterX - (realPos.x - mirrorCenterX);
            Vector3 mirrorPos = new Vector3(mirroredX, realPos.y, realPos.z);

            Debug.Log($"[MirrorTrigger] 플레이어 X: {realPos.x}, 미러 X: {mirroredX}");
            // 위치 넣어서 Instantiate
            mirrorInstance = Instantiate(mirrorPrefab, mirrorPos, Quaternion.identity);


            // 나머지 연결
            MirrorPlayer mp = mirrorInstance.GetComponent<MirrorPlayer>();
            if (mp != null)
            {
                mp.mirrorCenterX = mirrorCenterX;
                mp.realPlayer = collision.transform;
                mp.realSprite = collision.GetComponent<SpriteRenderer>();
                mp.mirrorSprite = mirrorInstance.GetComponent<SpriteRenderer>();
            }


            Debug.Log("[MirrorTrigger] 미러 플레이어 생성됨!");
        }
    }

  
}
