using System.Collections;
using System.Runtime.Versioning;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FanSwitch : MonoBehaviour, IResetable
{
    // 이 스크립트가 Fan 부모에 속해 있다면 스위치 사용 가능

    // Fan의 isOnWhenStart가 true라면 스위치 눌렀을 때 "time만큼 꺼짐"
    // Fan의 isOnWhenStart가 false라면 스위치 눌렀을 때 "time만큼 켜짐"

    [SerializeField] FanController fan;
    [SerializeField] float time = 4.0f;

    [SerializeField] TilemapRenderer pushTile;
    [SerializeField] TilemapRenderer noPushTile;

    bool isActive;

    void Start()
    {
        fan = GetComponentInChildren<FanController>();

        noPushTile.enabled = true;
        pushTile.enabled = false;

        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isActive)
        {
            StartCoroutine(FanTimer());
        }
    }

    IEnumerator FanTimer()
    {
        isActive = true;

        noPushTile.enabled = false;
        pushTile.enabled = true;

        if (fan.isOnWhenStart) { fan.TurnOff(); }
        else { fan.TurnOn(); }

        yield return new WaitForSeconds(time);

        if (fan.isOnWhenStart) { fan.TurnOn(); }
        else { fan.TurnOff(); }

        noPushTile.enabled = true;
        pushTile.enabled = false;

        isActive = false;
    }

    public void Init()
    {
        StopCoroutine(FanTimer());

        noPushTile.enabled = true;
        pushTile.enabled = false;

        isActive = false;
    }
}
