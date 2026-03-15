using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefap;
    public AudioSource audioSource;
    public AudioSource BGM;
    public AudioSource gameoverBGM;
    public Player player;
    public BulletManager bulletManager;
    public Camera cam;
    public static GameManager instance;
    public SaveManager saveManager;
    public MapManager mapManager;

    private void Awake()
    {

        if (cam == null)
        {
            cam = Camera.main;  // GameManager 객체 자체 대입(MainCamera 태그 확인)
        }
        instance = this; // 실행시킨 객체를 instance 전역변수에 저장, GameManager.instance. 사용할때 탐색을 안해도 되게끔 해줌

        audioSource = GetComponent<AudioSource>(); // 오디오 객체 가져오기

        Instantiate(playerPrefap, gameObject.transform,true);
        player = GetComponentInChildren<Player>(); // 플레이어 객체 가져오기
    }

    private void Start()
    {
        cam.transform.position = saveManager.currentData.cameraPos; // 현재의 카메라의 위치를 세이브 파일의 카메라 위치로 변경
    }

    private void Update()
    {
        Restart(); // 매 프레임마다 r키를 눌렀는지 확인
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R)) // r키를 눌렀는가?
        {
            BGM.Play(); // 브금 초기화
            player.Init(saveManager.currentData.playerPos); // 플레이어 초기화
            cam.transform.position = saveManager.currentData.cameraPos; // 카메라 초기화
            mapManager.Init(); // 맵 초기화
        }
    }
}
