// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Sedgewick.SearchTables;

public interface ISearchTable<TK, T> where TK : IComparable<TK> where T : class
{
    bool IsEmpty { get; }
    
    int Size { get; }
    
    IEnumerable<TK> Keys { get; }
    
    IEnumerable<T> Values { get; }

    void Put(TK key, T value);

    void Delete(TK key);

    T Get(TK key);

    bool Contains(TK key);
    
    
}