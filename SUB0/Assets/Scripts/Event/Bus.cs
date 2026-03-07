using UnityEngine;

/// <summary>
/// Event Bus Pattern
/// 유니티에서 범용으로 사용할 수 있는 C# 이벤트 버스 시스템
/// 필요한 클래스에 IEvent를 부착해서 쓸 수 있음
/// Bus<이벤트타입>.Raise();로 호출 가능
/// OnEvent += 함수만 해주면 어디든지 받아서 처리 가능
/// </summary>
public class Bus<T> where T : IEvent
{
    public delegate void Event(T evt);
    public static event Event OnEvent;
    public static void Raise(T evt) => OnEvent?.Invoke(evt);
}

public interface IEvent
{

}
