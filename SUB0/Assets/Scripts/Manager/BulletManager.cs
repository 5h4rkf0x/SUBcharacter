using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefap;
    public GameObject hitPrefap;
    List<GameObject> bulletPool;
    List<GameObject> hitPool;

    int bulletIndex = 0;
    int hitIndex = 0;
    int bulletCount = 15;
    int maxActiveBullet = 15;
    public int activeBullet;

    private void Awake()
    {

        bulletPool = new List<GameObject>();
        hitPool = new List<GameObject>();
        for(int i = 0; i< bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefap, transform);
            GameObject hit = Instantiate(hitPrefap, transform);
            bullet.SetActive(false);
            hit.SetActive(false);
            bulletPool.Add(bullet);
            hitPool.Add(hit);
        }
    }

    public void GetBullet(Vector2 pos, Vector2 dir)
    {
        if (activeBullet >= maxActiveBullet) // 현재 활성화된 총알의 갯수가 최대의 총알 개수 이상이면 무시
            return;

        GameObject bullet = bulletPool[bulletIndex];
        bullet.SetActive(true);
        activeBullet++;
        bullet.transform.position = pos;
        bullet.GetComponent<Bullet>().Init(dir);

        bulletIndex = (bulletIndex + 1) % bulletCount;
    }

    public void Hit(Vector3 pos)
    {
        // 총알은 최대 15개까지, 해당 번호의 총알의 위치정보를 저장하고 활성화
        int index = hitIndex;
        hitIndex = (hitIndex + 1) % bulletCount;
        hitPool[index].transform.position = pos;
        hitPool[index].SetActive(true);
    }
}
