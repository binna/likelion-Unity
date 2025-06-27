using UnityEngine;

namespace MonsterWorld
{
    public class Potion : MonoBehaviour, BaseItem
    {
        [SerializeField]
        private BaseItem.Type type;
        
        [SerializeField]
        private int price;

        private Inventory _inventory;

        private void Awake()
        {
            _inventory = FindFirstObjectByType<Inventory>();
        }

        public BaseItem.Type GetItemType()
        {
            return type;
        }

        public void Get()
        {
            Debug.Log($"{type} {name}을 획득했습니다.");
            _inventory.AddItem(this);

            gameObject.SetActive(false);
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Get();
            }
        }
    }
}