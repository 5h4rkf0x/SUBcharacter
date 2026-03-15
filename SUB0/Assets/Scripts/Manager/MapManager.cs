using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


public class MapManager : MonoBehaviour
{
    public List<GameObject> prefaps;
    public List<GameObject> maps;

    private void Awake()
    {
        for(int i = 0; i<prefaps.Count; i++)
        {
            maps.Add(Instantiate(prefaps[i], gameObject.transform,true)); // 유니티 엔진에 있는 스크립트에 있는 prefabs 배열에 저장된 프리펩들의 클론 객체들을 maps라는 배열에 저장함
        }
    }

    public void Init()
    {
        IResetable[] resettables = GetComponentsInChildren<IResetable>(true); // IResetable(이 프로젝트에서는 초기화 해야 하는 것들) 타입을 가진 모든 객체를 배열로 resettables 변수에 저장
        foreach(var r in resettables)
        {
            r.Init(); // resettables에 저장된 모든 객체들에 있는 각각의 Init 함수 실행 --> 초기화 해야하는 객체들을 초기화한다.
        }
    }
}
