using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData
{
    public string lastScene;
    public Vector3 playerPos;
    public Vector3 cameraPos;
}

[Serializable]
public class SaveFile
{
    public SaveData slot;
}

public class SaveManager : MonoBehaviour
{
    public SaveData currentData;

    public Vector3 initCameraPos;
    public Vector3 initPlayerPos;

    public string path;

    private void Awake() // 대충 세이브 불러오기인듯;
    {
        path = Path.Combine(Application.dataPath, "Save.json");

        if (currentData == null)
            currentData = new SaveData();
        LoadData();
    }

    public void UpdateData(Vector3 playerPos, Vector3 cameraPos)
    {
        currentData.lastScene = SceneManager.GetActiveScene().name; // 마지막에 실행된 씬을 저장
        currentData.playerPos = playerPos; // 마지막의 플레이어좌표랑 카메라 좌표 세팅
        currentData.cameraPos = cameraPos;
    }

    public void LoadData()
    {
        string json;
        SaveFile saveFile;
        if (!File.Exists(path))
        {
            currentData.lastScene = "Stage1";
            currentData.playerPos = initPlayerPos;
            currentData.cameraPos = initCameraPos;
        }
        else
        {
            json = File.ReadAllText(path);
            saveFile = JsonUtility.FromJson<SaveFile>(json);

            if (saveFile == null || saveFile.slot == null)
            {
                currentData.lastScene = "Stage1";
                currentData.playerPos = initPlayerPos;
                currentData.cameraPos = initCameraPos;
            }
            else
                currentData = saveFile.slot;
        }

        if(currentData.lastScene != SceneManager.GetActiveScene().name)
        {
            if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "Ending")
                return;
            currentData.lastScene = name;
            currentData.playerPos = initPlayerPos;
            currentData.cameraPos = initCameraPos;
        }
    }

    public void SaveData()
    {
        if(currentData == null)
        {
            return;
        }

        SaveFile saveFile = new SaveFile { slot = currentData };
        string json = JsonUtility.ToJson(saveFile, true);
        File.WriteAllText(path, json);
    }

    private void OnApplicationQuit()
    {
        SaveData();
        Debug.Log(path);
    }
}
