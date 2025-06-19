using UnityEngine;

namespace MonsterWorld
{
    public interface BaseItem
    {
        GameObject obj { get; set; }
        void Get();
    }
}