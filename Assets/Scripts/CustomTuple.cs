using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomTuple<K, V>
{
    public K Key;
    public V Value;
    public CustomTuple(K key, V value)
    {
        Key = key; 
        Value = value;
    }
}
