using System.Collections.Generic;
using UniCore.Extensions.Language;
using UnityEngine;

namespace UniCore.Helpers
{
    public class NaivePool<T> where T : Component
    {
        private const int DEFAULT_TARGET_SIZE = 5;

        private readonly T _template;
        private readonly Transform _container;
        private readonly int _targetSize;

        private Queue<T> _queue;

        public NaivePool(T template, Transform container, int targetSize = DEFAULT_TARGET_SIZE)
        {
            _template = template;
            _container = container;
            _targetSize = targetSize;
        }

        public T Rent()
        {
            return Rent(out _);
        }

        public T Rent(out bool isCreation)
        {
            if (_queue.IsNullOrEmpty())
            {
                T instance = GameObject.Instantiate(_template, _container);
                isCreation = true;

                return instance;
            }
            else
            {
                isCreation = false;

                return _queue.Dequeue();
            }
        }

        public void Release(T instance)
        {
            if (_queue.IsNullOrEmpty() || _queue.Count < _targetSize)
            {
                _queue ??= new();
                _queue.Enqueue(instance);
            }
            else
            {
                GameObject.Destroy(instance.gameObject);
            }
        }
    }
}
