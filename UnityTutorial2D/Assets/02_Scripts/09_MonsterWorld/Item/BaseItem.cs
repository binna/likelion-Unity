using UnityEngine;

namespace MonsterWorld
{
    public interface BaseItem
    {
        public enum Type
        {
            Gold,
            Green,
            Blue,
            Hp,
            Mp,
        }
        
        Type GetItemType();
        
        void Get();
        void OnCollisionEnter2D(Collision2D other);
    }
}