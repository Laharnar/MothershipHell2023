using UnityEngine;

namespace Combat.AI
{
    public class ColorRead : ReactiveBase
    {
        [Header("auto parent|self")]
        public ColorGroup group;
        public SpriteRenderer sprite;
        public bool applyInEditor = false;
        private void OnValidate()
        {
            if(group == null)
                group = GetComponentInParent<ColorGroup>();
            if (sprite == null)
            {
                sprite = GetComponent<SpriteRenderer>();
            }
            if (applyInEditor && sprite && group)
            {
                sprite.color = group.color;
            }
        }
    }
}