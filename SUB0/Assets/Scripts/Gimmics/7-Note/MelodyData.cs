using UnityEngine;
[System.Serializable]
public class MelodyData
{
    public NoteMelody noteMelody;
    public Color color;
    public AudioClip noteAudio;
    public Sprite sprite;

    public MelodyData(NoteMelody _noteMelody,  Color _color, AudioClip _noteAudio, Sprite _sprite = null)
    {
        noteMelody = _noteMelody;
        color = _color;
        noteAudio = _noteAudio;
        sprite = _sprite;
    }

    public enum NoteMelody
    {
        Do, Re, Mi, Fa, Sol, La, Si
    }

}
