using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Stages
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewSpawnerEnemiesConfig", fileName = "SpawnerEnemiesConfig")]
    public class SpawnerEnemiesConfig : ScriptableObject
    {
        [field:SerializeField] public Vector3 SpawnPosition {get; set;} = Vector3.zero;
        [field:SerializeField] public float Radius {get; set;} = 4f;
        [field:SerializeField] public Vector2 Offset {get; set;} = Vector2.zero;
    }
}