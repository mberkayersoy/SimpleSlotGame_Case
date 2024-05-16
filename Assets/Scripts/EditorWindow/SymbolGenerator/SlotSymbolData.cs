using Sirenix.OdinInspector;
using System;

[Serializable]
public class SlotSymbolData
{
    private string _name;
    private int _id;
    private string _sharpSpritePath;
    private string _blurSpritePath;

    public string Name { get => _name; private set => _name = value; }
    public int ID { get => _id; private set => _id = value; }
    public string SharpSpritePath { get => _sharpSpritePath; private set => _sharpSpritePath = value; }
    public string BlurSpritePath { get => _blurSpritePath; private set => _blurSpritePath = value; }

    public SlotSymbolData(string name, int id, string sharpSpritePath, string blurSpritePath)
    {
        _name = name;
        _id = id;
        _sharpSpritePath = sharpSpritePath;
        _blurSpritePath = blurSpritePath;
    }
}
