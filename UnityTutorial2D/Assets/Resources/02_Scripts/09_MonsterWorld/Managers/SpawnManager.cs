using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterWorld
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] 
        private GameObject[] monsters;

        [SerializeField] 
        private GameObject[] items;
        
        private List<BaseMonster> _monsterList = new();

        IEnumerator Start()
        {
            // 몬스터 스폰
            while (true)
            {
                yield return new WaitForSeconds(3f);
                
                var monsterIdx = Random.Range(0, monsters.Length);
                var randomX = Random.Range(-8, 8);
                var randomY = Random.Range(-3, 5);

                GameObject newMonster = Instantiate(monsters[monsterIdx], new Vector3(randomX, randomY, 0), Quaternion.identity);
                
                _monsterList.Add(newMonster.GetComponent<BaseMonster>());

                bool isFaceLeft = Random.Range(0, 2) == 0;
                newMonster.GetComponent<BaseMonster>().SetFacingDirection(isFaceLeft);
            }
        }

        public void DropItem(Vector3 dropPosition)
        {
            var randomIndex = Random.Range(0, items.Length);
            
            Instantiate(items[randomIndex], dropPosition, Quaternion.identity);
        }
    }
}