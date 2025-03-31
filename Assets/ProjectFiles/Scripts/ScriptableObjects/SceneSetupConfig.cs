using UnityEngine;

namespace ProjectFiles.Scripts.Common.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SceneSetupConfig", menuName = "SceneSetup/SceneSetup", order = 0)]
    public sealed class SceneSetupConfig : ScriptableObject
    {
        [SerializeField] private EnemyEntity enemyEntityPrefab;
        [SerializeField] private LayerMask bulletLayerMask;

        public EnemyEntity EnemyEntityPrefab => enemyEntityPrefab;
        public int  BulletLayerMask => bulletLayerMask.value;
    }
    
}