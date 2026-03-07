using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SymbolData
{
    public Symbol symbol;
    public Color color;
    public Sprite sprite;

    public SymbolData(Symbol symbol, Color color, Sprite sprite = null)
    {
        this.symbol = symbol;
        this.color = color;
        this.sprite = sprite;
    }
}

public enum Symbol
{
    Magenta, Red, Green, Blue
}
