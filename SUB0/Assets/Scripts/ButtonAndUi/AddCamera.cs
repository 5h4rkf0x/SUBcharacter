using UnityEngine;
using UnityEngine.UI;

public class AddCamera : MonoBehaviour
{
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }
    private void Start()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = GameManager.instance.cam;
    }
}
