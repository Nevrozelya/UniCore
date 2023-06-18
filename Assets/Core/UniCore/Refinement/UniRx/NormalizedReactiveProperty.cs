using UniRx;
using UnityEngine;

namespace UniCore.Refinement.UniRx
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
