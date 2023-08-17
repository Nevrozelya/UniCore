using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UniCore.Examples.Navigation
{
    public class ContextController : MonoBehaviour
    {
        [SerializeField] private Button _pushAButton;
        [SerializeField] private Button _pushBButton;
        [SerializeField] private Button _popButton;
        [SerializeField] private Button _replaceAButton;
        [SerializeField] private Button _replaceBButton;
        [SerializeField] private Button _resetContextBButton;

        public IObservable<Unit> PushAEvent => _pushAButton.OnClickAsObservable();
        public IObservable<Unit> PushBEvent => _pushBButton.OnClickAsObservable();
        public IObservable<Unit> PopEvent => _popButton.OnClickAsObservable();
        public IObservable<Unit> ReplaceAEvent => _replaceAButton.OnClickAsObservable();
        public IObservable<Unit> ReplaceBEvent => _replaceBButton.OnClickAsObservable();
        public IObservable<Unit> ResetContextEvent => _resetContextBButton.OnClickAsObservable();
    }
}
