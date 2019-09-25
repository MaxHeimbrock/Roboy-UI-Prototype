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

    public PC_CircularBuffer(int size)
    {
        _queue = new Queue<T>(size);
        _size = size;
    }

    /// <summary>
    /// Adds an object to the end of the Queue.
    /// </summary>
    /// <param name="obj">The object to add to the Queue. The value can be null.</param>
    public void Add(T obj)
    {
        if (_cloning)
        {
            return;
        }

        if (_queue.Count == _size)
        {
            _queue.Dequeue();
            _queue.Enqueue(obj);
        }
        else
            _queue.Enqueue(obj);
    }

    /// <summary>
    /// Removes and returns the object at the beginning of the Queue.
    /// </summary>
    /// <returns>The object that is removed from the beginning of the Queue.</returns>
    public T Read()
    {
        return _queue.Dequeue();
    }

    /// <summary>
    /// Returns the object at the beginning of the Queue without removing it.
    /// </summary>
    /// <returns>The object at the beginning of the Queue.</returns>
    public T Peek()
    {
        return _queue.Peek();
    }

    /// <summary>
    /// Copies the Queue elements to a new list
    /// </summary>
    /// <returns>The list containing all elements of the Queue.</returns>
    public List<T> ToList()
    {
        _cloning = true;
        Queue<T> tmpQueue = new Queue<T>(_queue);
        _cloning = false;
        return tmpQueue.ToList();
    }

    /// <summary>
    /// Gets the number of elements contained in the Queue.
    /// </summary>
    /// <returns>The number of elements contained in the Queue.</returns>
    public int Count()
    {
        return _queue.Count;
    }
}