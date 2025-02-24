using System;
using System.Collections.Generic;
using System.Linq;


public class Paginate<T> : IPaginate<T>
{
    public int From { get; set; }
    public int Index { get; set; }
    public int Size { get; set; }
    public int Count { get; set; }
    public int Pages { get; set; }
    public IList<T> Items { get; set; }
    public bool HasPrevious => Index > 0;
    public bool HasNext => Index + 1 < Pages;

    public Paginate(IEnumerable<T> source, int index, int size, int from)
    {
        var enumerable = source as T[] ?? source.ToArray();
        
        Count = enumerable.Count();
        Pages = (int)Math.Ceiling(Count / (double)size);
        Index = index;
        Size = size;
        From = from;
        Items = enumerable.Skip(from).Take(size).ToList();
    }
} 