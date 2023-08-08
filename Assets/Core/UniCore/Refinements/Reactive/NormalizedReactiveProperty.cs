using UniRx;
using UnityEngine;

namespace UniCore.Refinements.Reactive
{
    public class NormalizedReactiveProperty : ReactiveProperty<float>
    {
        protected override void SetValue(float value)
        {
            float clamped = Mathf.Clamp01(value);
            base.SetValue(clamped);
        }
    }
}
