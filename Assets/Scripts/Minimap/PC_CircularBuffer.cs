using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// PC_CircularBuffer is a circular buffer structure, which uses a queue internally.
/// When the buffer is full, the first object will be discarded in order to make room for a new one.
/// </summary>
/// <typeparam name="T">type of the circular buffer</typeparam>
public class PC_CircularBuffer<T>
{
    Queue<T> _queue;
    int _size;
    private bool _cloning = false;

    public PC_CircularBuffer(int size) {
        _queue = new Queue<T>(size);
        _size = size;
    }

    public void Add(T obj) {
        if (_cloning)
        {
            return;
        }
        if (_queue.Count == _size) {
            _queue.Dequeue();
            _queue.Enqueue(obj);
        } else
            _queue.Enqueue(obj);
    }
    public T Read() {
        return _queue.Dequeue();
    }

    public T Peek() {
        return _queue.Peek();
    }

    public List<T> ToList()
    {
        _cloning = true;
        Queue<T> tmpQueue = new Queue<T>(_queue);
        _cloning = false;
        return tmpQueue.ToList();
    }

    public int Count()
    {
        return _queue.Count;
    }
}
