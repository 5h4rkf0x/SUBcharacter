using UnityEngine;

public class SwitchComparer : MonoBehaviour, IResetable
{
    public static SwitchComparer Instance;

    [SerializeField] private MirrorExitDoor mirrorExitDoor;
    [SerializeField] private SwitchButton[] switches; // 연결할 버튼들

    public void Init()
    {
        mirrorExitDoor.CloseDoor();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void CheckSwitches()
    {
        if (mirrorExitDoor.IsOpen) 
        { 
            Debug.Log("이미 문 열림");
            return; 
        } // 이미 문 열렸으면 다시 체크 안 함

        foreach (var s in switches)
        {
            if (!s.IsPressed)
            {
                Debug.Log($"[SwitchComparer] 스위치 {s.gameObject.name} 이(가) 아직 안 밟힘");
                return;
            }// 하나라도 안 밟혔으면 그냥 리턴
        }

        Debug.Log("[SwitchComparer] 모든 스위치가 눌림 → 출구 열림");
        mirrorExitDoor.OpenDoor(); // 둘 다 밟혔으면 문 열기
    }

   
}
