using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] Scanner scanner;                    // 타겟 감지용 스캐너
    [SerializeField] GameObject bulletPrefab;           // 프리팹으로 만든 미사일
    [SerializeField] GameObject loadedMissile;
    [SerializeField] Transform firePoint;                // 발사 위치

    [SerializeField] bool isFired = false;

    private void Awake()
    {
        scanner = GetComponent<Scanner>();
        loadedMissile = Instantiate(bulletPrefab,transform.parent);
        
    }

    void Update()
    {
        if(scanner.nearestTarget) // 타겟이 있으면
        {
            Vector3 targetPos = scanner.nearestTarget.position; // 타겟 위치 저장
            Vector3 direction = targetPos - transform.position; // 타겟 방향 저장

            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction); // 미사일 방향 변경
            Fire();
        }
    }

    private void LateUpdate()
    {
        if ((!loadedMissile.activeSelf && scanner.target)) // 자신의 활성 상태가 거짓이고, 타겟이 존재하지 않는다면 isFired = false
        {
            isFired = false;
        }
    }

    void Fire()
    {
        if (isFired || GameManager.instance.player.isDead) // isFired가 참이거나, 플레이어가 죽었을 때는 무시
            return;
        isFired = true; // isFired를 참으로 변경
        loadedMissile.GetComponent<BulletShooter>().Launch(firePoint.position); // 미사일 발사  --> 미사일 이동 로직 어디감...?
        
    }
}
