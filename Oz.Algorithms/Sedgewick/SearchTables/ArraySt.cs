// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Sedgewick.SearchTables;

public class ArraySt<TK, T> : ISearchTable<TK, T>
    where TK : IComparable<TK> where T : class
{
    private TK[] _keys;
    private T[] _values;

    public ArraySt()
    {
        _keys = new TK[8];
        _values = new T[8];
        Size = 0;
    }

    public bool IsEmpty => Size == 0;

    public int Size { get; private set; }

    public IEnumerable<TK> Keys
    {
        get
        {
            var keysCopy = new TK[Size];
            Array.Copy(_keys, 0, keysCopy, 0, Size);
            return keysCopy;
        }
    }

    public IEnumerable<T> Values
    {
        get
        {
            var valuesCopy = new T[Size];
            Array.Copy(_values, 0, valuesCopy, 0, Size);
            return valuesCopy;
        }
    }

    public void Put(TK key, T value)
    {
        for (var i = 0; i < Size; i++)
        {
            if (_keys[i].CompareTo(key) == 0)
            {
                _values[i] = value;
                if (_values[i] == null)
                {
                    Delete(_keys[i]);
                }

                return;
            }
        }

        if (Size >= _keys.Length)
        {
            Enlarge(Size * 2);
        }

        _keys[Size] = key;
        _values[Size] = value;
        Size++;
    }

    public void Delete(TK key)
    {
        var keyIndex = -1;
        for (var i = 0; i < Size; i++)
        {
            if (_keys[i].CompareTo(key) == 0)
            {
                keyIndex = i;
                break;
            }
        }

        if (keyIndex >= 0)
        {
            for (var i = keyIndex; i < Size - 1; i++)
            {
                _keys[i] = _keys[i + 1];
                _values[i] = _values[i + 1];
            }

            _values[Size - 1] = null;
            Size--;

            if (Size > 16 && Size < _keys.Length / 2)
            {
                Shrink(_keys.Length / 2);
            }
        }
    }

    public T Get(TK key)
    {
        for (var i = 0; i < Size; i++)
        {
            if (_keys[i].CompareTo(key) == 0)
            {
                return _values[i];
            }
        }

        return null;
    }

    public bool Contains(TK key)
    {
        for (var i = 0; i < Size; i++)
        {
            if (_keys[i].CompareTo(key) == 0)
            {
                return true;
            }
        }

        return false;
    }

    private void Enlarge(int newSize)
    {
        var newKeys = new TK[newSize];
        var newValues = new T[newSize];

        Array.Copy(_keys, 0, newKeys, 0, Size);
        Array.Copy(_values, 0, newValues, 0, Size);

        _keys = newKeys;
        _values = newValues;
    }

    private void Shrink(int newSize)
    {
        var newKeys = new TK[newSize];
        var newValues = new T[newSize];

        Array.Copy(_keys, 0, newKeys, 0, Size);
        Array.Copy(_values, 0, newValues, 0, Size);

        _keys = newKeys;
        _values = newValues;
    }
}