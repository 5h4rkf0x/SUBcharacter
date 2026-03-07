using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    [Header("출구 문 연결")]
    [SerializeField] private MirrorExitDoor exitDoor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (exitDoor != null && exitDoor.IsOpen)
            {
                Debug.Log("[MirrorExitTrigger] 출구 문에 닿음 - 미러 제거 및 보스씬 이동 준비");

                // 미러 제거
                if (exitDoor.mirrorTrigger != null)
                {
                    exitDoor.mirrorTrigger.Init();
                    Debug.Log("[MirrorExitTrigger] 미러플레이어 제거 완료");
                }

                SceneManager.LoadScene(exitDoor.bossSceneName);
            }
            else
            {
                Debug.Log("[MirrorExitTrigger] 문이 아직 닫혀있음");
            }
        }
    }
}