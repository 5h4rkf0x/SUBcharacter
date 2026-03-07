using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static MelodyData;

public class MelodyPuzzle : MonoBehaviour, IResetable
{
    public List<MelodyPlatform> platforms;
    public List<Sprite> availableSprites;
    public List<AudioClip> noteAudio;
    public List<Color> colors;

    public List<MelodyData> correctMelody;

    [SerializeField] private MelodyUI melodyUI;
    [SerializeField] private MelodyComparer melodyComparer;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        InitMelody();
    }

    public void InitMelody()
    {
        correctMelody = new List<MelodyData>();
        
        for (int i = 0; i < platforms.Count; i++)
        {
            int rand = UnityEngine.Random.Range(0, Enum.GetValues(typeof(NoteMelody)).Length);
            NoteMelody note = (NoteMelody)rand;

            var color = colors[rand];
            color.a = 1f;

            var data = new MelodyData(note, color, noteAudio[rand], availableSprites[rand]);
            platforms[i].melodyData = data;
            platforms[i].ResetPlatform();
        }

        var selected = platforms.OrderBy(x => UnityEngine.Random.value).Take(3).ToList();
        foreach (var platform in selected)
        {
            correctMelody.Add(platform.melodyData);
        }

        melodyComparer.correctMelodys = correctMelody.ToArray();
        melodyUI.StartMelody(correctMelody);

    }
}
