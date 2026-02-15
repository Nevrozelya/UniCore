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
        private readonly bool _activateEntries;

        private Queue<T> _queue;

        public NaivePool(
            T template,
            Transform container,
            bool activateEntries = false,
            int targetSize = DEFAULT_TARGET_SIZE)
        {
            _template = template;
            _container = container;
            _activateEntries = activateEntries;
            _targetSize = targetSize;
        }

        public T Rent()
        {
            return Rent(out _);
        }

        public T Rent(out bool isCreation)
        {
            T instance;

            if (_queue.IsNullOrEmpty())
            {
                isCreation = true;
                instance = GameObject.Instantiate(_template, _container);
            }
            else
            {
                isCreation = false;
                instance = _queue.Dequeue();
            }

            if (_activateEntries)
            {
                instance.gameObject.SetActive(true);
            }

            return instance;
        }

        public void Release(T instance)
        {
            if (instance == null)
            {
                return;
            }

            if (_activateEntries)
            {
                instance.gameObject.SetActive(false);
            }

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
