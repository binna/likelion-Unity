using UnityEngine;

namespace MonsterWorld
{
    public class Coin : MonoBehaviour, BaseItem
    {
        public enum Type
        {
            Gold,
            Green,
            Blue
        }

        public Type type;
        public float price;
        
        private Inventory _inventory;
        
        public GameObject obj { get; set; }

        private void Awake()
        {
            _inventory = FindFirstObjectByType<Inventory>();

            obj = gameObject;
        }

        private void OnMouseDown()
        {
            Get();
        }

        public void Get()
        {
            Debug.Log($"{type} {name}을 획득했습니다.");
            _inventory.AddItem(this);
            
            gameObject.SetActive(false);
        }
    }
}