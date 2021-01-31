using UnityEngine;

namespace Shared.Scriptable_References
{
    [CreateAssetMenu(fileName = "AnimationSettings", menuName = "Anim/Animation Settings", order = 0)]
    public class AnimationsReference : ScriptableObject
    {
        public float Duration = .5f;
        public float SmallScaleFactor = .7f;
        public float StapleScaleFactor = .3f;
        public float HoverAmount = 20f;
    }
}