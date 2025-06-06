using System;
using System.Collections.Generic;
using UniCore.Extensions.Language;
using UnityEngine;

namespace UniCore.Scriptables
{
    public class DictionaryLibrary<TKey, TItem> : ScriptableObject
    {
        [Serializable]
        private class KeyValueBinding<TBindingKey, TBindingItem>
        {
            public TBindingKey Key;
            public TBindingItem Value;
        }

        [SerializeField] private List<KeyValueBinding<TKey, TItem>> _bindings;

        private Dictionary<TKey, TItem> _dictionary;

        public TItem Get(TKey key)
        {
            CreateDictionary();

            if (_dictionary.IsNullOrEmpty())
            {
                return default;
            }

            return _dictionary.GetValueOrDefault(key, default);
        }

        private void CreateDictionary()
        {
            if (_dictionary != null)
            {
                return;
            }

            if (_bindings.IsNullOrEmpty())
            {
                return;
            }

            _dictionary = new();

            foreach (KeyValueBinding<TKey, TItem> binding in _bindings)
            {
                _dictionary[binding.Key] = binding.Value;
            }
        }
    }
}
