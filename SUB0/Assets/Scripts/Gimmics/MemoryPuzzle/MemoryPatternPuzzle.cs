using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 여기에 r키 누르면 리셋되는 인터페이스 넣어야할듯
/// </summary>
public class MemoryPatternPuzzle : MonoBehaviour, IResetable
{

    public List<ColorPlatform> platforms;
    public List<Sprite> availableSprites;
    public List<Color> colors;

    public List<SymbolData> correctPattern;

    [SerializeField] private PatternUIManager patternUIManager;
    [SerializeField] private SymbolComparer symbolComparer;



    private void Start()
    {
        Init();
    }

    public void Init()
    {
        InitPuzzle();
    }

    public void InitPuzzle()
    {
        correctPattern = new List<SymbolData>();

        for (int i = 0; i < platforms.Count; i++)
        {
            int rand = Random.Range(0, availableSprites.Count);
            Symbol symbol = (Symbol)rand;

            var color = colors[rand];
            color.a = 1f;

            var data = new SymbolData(symbol, color, availableSprites[rand]);
            platforms[i].symbolData = data;
            platforms[i].ResetPlatform();
        }

        var selected = platforms.OrderBy(x => Random.value).Take(3).ToList();
        foreach (var platform in selected)
        {
            correctPattern.Add(platform.symbolData);
        }

        symbolComparer.correctSymbol = correctPattern.ToArray();
        patternUIManager.StartPuzzle(correctPattern);
    }

   
}

