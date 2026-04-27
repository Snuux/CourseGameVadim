using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature
{
    public class AbilityOnStateChangedActivatorSystem : IInitializableSystem, IDisposableSystem
    {
        private readonly StageProviderService _stageProviderService;
        private readonly ShopAbilitiesConfig _shopAbilitiesConfig;

        private AbilitiesList _abilitiesList;

        public AbilityOnStateChangedActivatorSystem(
            StageProviderService stageProviderService,
            ShopAbilitiesConfig shopAbilitiesConfig)
        {
            _stageProviderService = stageProviderService;
            _shopAbilitiesConfig = shopAbilitiesConfig;
        }

        public void OnInit(Entity entity)
        {
            _abilitiesList = entity.Abilities;
            _stageProviderService.StageStarted += OnStageStarted;
        }

        private void OnStageStarted(int stageNumber)
        {
            if (_abilitiesList == null)
                return;

            foreach (Ability ability in _abilitiesList.Elements)
            {
                AbilityActivationTypes activationType = _shopAbilitiesConfig.GetConfigBy(ability.ID).ActivateOnType;

                switch (activationType)
                {
                    case AbilityActivationTypes.OnAdd:
                        break;

                    case AbilityActivationTypes.LevelBegin:
                        if (stageNumber == 1)
                            ability.Activate();
                        break;

                    case AbilityActivationTypes.StageBegin:
                        ability.Activate();
                        break;
                }
            }
        }

        public void OnDispose()
        {
            _stageProviderService.StageStarted -= OnStageStarted;
        }
    }
}
