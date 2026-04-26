using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Meta.Features.Abilities
{
    public class AbilitiesHolderService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly ConfigsProviderService _configsProviderService;
        private readonly Dictionary<Ability, ReactiveVariable<bool>> _abilities = new();

        public AbilitiesHolderService(PlayerDataProvider playerDataProvider, ConfigsProviderService configsProviderService)
        {
            _configsProviderService = configsProviderService;

            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);
        }

        public List<Ability> AvailableAbilities => _abilities.Keys.ToList();

        public IReadOnlyVariable<bool> IsBought(Ability ability) => _abilities[ability];
        
        private AbilitiesConfigsContainer PlayerAbilityConfig => _configsProviderService.GetConfig<AbilitiesConfigsContainer>();

        private AbilityConfig GetAbilityBy(string id) => PlayerAbilityConfig.AbilityConfigs.FirstOrDefault(p => p.ID == id);

        public void ReadFrom(PlayerData data)
        {
            foreach (KeyValuePair<string, bool> abilityData in data.AbilitiesData)
            {
                Ability ability = _abilities.Keys.FirstOrDefault(a => a.ID == abilityData.Key);
                
                if (ability != null)
                    _abilities[ability].Value = abilityData.Value;
            }
        }

        public void WriteTo(PlayerData data)
        {
            if (data.AbilitiesData == null) 
                data.AbilitiesData = new Dictionary<string, bool>();

            foreach (var ability in _abilities)
            {
                if (data.AbilitiesData.ContainsKey(ability.Key.ID))
                    data.AbilitiesData[ability.Key.ID] = ability.Value.Value;
                else
                    data.AbilitiesData.Add(ability.Key.ID, ability.Value.Value);
            }
        }
    }
}