using System.Collections.Generic;
using UnityEngine;

namespace MonsterWorld
{
    public class Inventory : MonoBehaviour
    {
        private List<BaseItem> items = new();

        public void AddItem(BaseItem item)
        {
            items.Add(item);
        }
    }
}