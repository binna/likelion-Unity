using System.Collections.Generic;
using UnityEngine;

namespace MonsterWorld
{
    public class Inventory : MonoBehaviour
    {
        private static Dictionary<string, int> items = new();

        public void AddItem(BaseItem item)
        {
            var itemName = $"{item.GetType()}_{item.GetItemType()}";
            items.TryGetValue(itemName, out var count);
            items[$"{itemName}"] = ++count;

            Debug.Log($"인벤토리 안 =========================");
            foreach (var each in items)
            {
                Debug.Log($"{each.Key} : {each.Value}개");
            }
            Debug.Log($"===================================");
        }
    }
}