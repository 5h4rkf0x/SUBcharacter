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
            cam = Camera.main;
        }
        instance = this;

        audioSource = GetComponent<AudioSource>();

        Instantiate(playerPrefap, gameObject.transform,true);
        player = GetComponentInChildren<Player>();
    }

    private void Start()
    {
        cam.transform.position = saveManager.currentData.cameraPos;
    }

    private void Update()
    {
        Restart();
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            BGM.Play();
            player.Init(saveManager.currentData.playerPos);
            cam.transform.position = saveManager.currentData.cameraPos;
            mapManager.Init();
        }
    }
}
