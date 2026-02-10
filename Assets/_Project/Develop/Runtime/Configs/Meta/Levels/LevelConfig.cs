using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Levels   
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public string Symbols { get; private set; }
        [field: SerializeField] public int Length { get; private set; }
    }
}