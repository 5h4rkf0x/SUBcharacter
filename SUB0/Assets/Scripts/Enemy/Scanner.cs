using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D target;
    public Transform nearestTarget;

    void FixedUpdate()
    {
        target = Physics2D.CircleCast(transform.position, scanRange, Vector2.zero, 0, targetLayer); // target 변수에 원 모양의 범위를 생성 --> 매개변수는 순서대로 scanner를 호출한 객체의 위치, 범위, 원의 이동 방향, 원의 이동 거리, 어떤 레이어에 있는 객체를 스캔할 것인가?
        if(target)
        {
            nearestTarget = GetNearest(); // nearestTarget에 인식된 객체 저장
        }
        
    }

    Transform GetNearest()
    {
        Transform result = null; // 결과값 초기화
        float diff = 100; // 가까운 적까지의 거리 --> 초기화 값 == 100

        
        Vector3 myPos = transform.position; // 이 스크립트를 가진 객체의 위치
        Vector2 targetPos = target.transform.position; // target 오브젝트의 위치 --> 위의 FixedUpdate() 참고?
        float curDiff = Vector3.Distance(myPos, targetPos); // 타겟과 자기자신의 거리

        if (curDiff < diff) // 기준거리보다 가깝다면 변수 초기화
        {
            diff = curDiff; // 현재 가장 가까운 객체의 거리로 초기화
            result = target.transform; // 가까운 객체의 위치 반환
        }

 
        return result;                 // 반환되는 위치
    }
}

