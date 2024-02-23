using UnityEngine;

namespace Edleron
{
    public interface IDamagable
    {
        public float Healt { set; get; }
        public bool Targetable { set; get; }
        public void OnHit(float damage, Vector2 knobcback);
        public void OnHit(float damage);
    }
}