using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public static class StaticHelper
{
    public static readonly Color EmptyColour = new Color(0, 0, 0, 0.5f);
    public static readonly Color TapColour = new Color32(124, 255, 175, 252);
    public static readonly Color HoldColour = new Color32(113, 207, 232, 252);
    public static readonly Color FlickColour = new Color32(218, 155, 232, 252);
    
    public static void AddOnce<T>(this HashSet<T> _list, T _item)
    {
        if (_item != null && !_list.Contains(_item))
            _list.Add(_item);
    }
    
    public static void AddOnce<T>(this List<T> _list, T _item)
    {
        if (_item != null && !_list.Contains(_item))
            _list.Add(_item);
    }
    
    public static void SetActiveFast(this GameObject _gameObject, bool _active)
    {
        if (_gameObject.activeSelf == _active)
            return;

        _gameObject.SetActive(_active);
    }

    public static List<T> RandomSubList<T>(this Random _rng, List<T> _list, int _maxElements)
    {
        if (_list.Count <= 0)
            return new List<T>(_list);
        if (_list.Count < _maxElements)
            return _list;
        List<T> list = new();
        List<int> indexList = new();
        for (int i = 0; i < _maxElements; ++i)
        {
            int k = _rng.Next(_list.Count);
            while(indexList.Contains(k))
                k = _rng.Next(_list.Count);
            indexList.Add(k);
            list.Add(_list[k]);
        }
        return list;
    }

    public static void Shuffle<T> (this Random _rng, T[] _array)
    {
        int n = _array.Length;
        while (n > 1) 
        {
            int k = _rng.Next(n--);
            (_array[n], _array[k]) = (_array[k], _array[n]);
        }
    }
    
    public static void Shuffle<T> (this Random _rng, List<T> _list)
    {
        int n = _list.Count;
        while (n > 1) 
        {
            int k = _rng.Next(n--);
            (_list[n], _list[k]) = (_list[k], _list[n]);
        }
    }
}
