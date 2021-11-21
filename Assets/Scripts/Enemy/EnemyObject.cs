using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "MyObjects", order = 0)]
    public class EnemyObject : Stats
    {
        public string enemyName;
        public Sprite sprite;
        public float size;
        
        public AI.AIType aiType;
        
        public float detectionDistance;
    }
}