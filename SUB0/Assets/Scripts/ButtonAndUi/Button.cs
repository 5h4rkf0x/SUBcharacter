using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public SaveManager save;

    public void StartGame()
    {
        Debug.Log("StartGame"); // 하단에 콘솔 찍히는거
        SceneManager.LoadScene(save.currentData.lastScene); // 외부파일에 있는 세이브 파일을 가져와서 해당하는 씬 호출
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame"); // 하단에 콘솔 22
        Application.Quit(); // 앱 꺼버리는거?
    }
}
