namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportTargetPosition TeleportTargetPositionC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportTargetPosition>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> TeleportTargetPosition => TeleportTargetPositionC.Value;

		public bool TryGetTeleportTargetPosition(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportTargetPosition component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeleportTargetPosition()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportTargetPosition() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeleportTargetPosition(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportTargetPosition() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportRadius TeleportRadiusC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportRadius>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> TeleportRadius => TeleportRadiusC.Value;

		public bool TryGetTeleportRadius(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportRadius component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeleportRadius()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportRadius() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeleportRadius(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportRadius() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportCostEnergy TeleportCostEnergyC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportCostEnergy>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> TeleportCostEnergy => TeleportCostEnergyC.Value;

		public bool TryGetTeleportCostEnergy(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportCostEnergy component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeleportCostEnergy()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportCostEnergy() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeleportCostEnergy(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportCostEnergy() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Teleport.CanStartTeleport CanStartTeleportC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Teleport.CanStartTeleport>();

		public _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition CanStartTeleport => CanStartTeleportC.Value;

		public bool TryGetCanStartTeleport(out _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Teleport.CanStartTeleport component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCanStartTeleport(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.CanStartTeleport() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportRequested TeleportRequestedC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportRequested>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> TeleportRequested => TeleportRequestedC.Value;

		public bool TryGetTeleportRequested(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportRequested component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeleportRequested()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportRequested() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeleportRequested(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportRequested() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportInProcess TeleportInProcessC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportInProcess>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> TeleportInProcess => TeleportInProcessC.Value;

		public bool TryGetTeleportInProcess(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportInProcess component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeleportInProcess()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportInProcess() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeleportInProcess(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportInProcess() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportCompleted TeleportCompletedC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportCompleted>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> TeleportCompleted => TeleportCompletedC.Value;

		public bool TryGetTeleportCompleted(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportCompleted component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeleportCompleted()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportCompleted() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeleportCompleted(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Teleport.TeleportCompleted() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Team TeamC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Team>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<_Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Teams> Team => TeamC.Value;

		public bool TryGetTeam(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<_Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Teams> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Team component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<_Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Teams>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeam()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Team() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<_Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Teams>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTeam(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<_Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Teams> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Team() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.StatFeature.BaseStats BaseStatsC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.StatFeature.BaseStats>();

		public System.Collections.Generic.Dictionary<_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatTypes, System.Single> BaseStats => BaseStatsC.Value;

		public bool TryGetBaseStats(out System.Collections.Generic.Dictionary<_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatTypes, System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.StatFeature.BaseStats component);
			if(result)
				value = component.Value;
			else
				value = default(System.Collections.Generic.Dictionary<_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatTypes, System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddBaseStats()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.StatFeature.BaseStats() { Value = new System.Collections.Generic.Dictionary<_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatTypes, System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddBaseStats(System.Collections.Generic.Dictionary<_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatTypes, System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.StatFeature.BaseStats() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.StatFeature.ModifiedStats ModifiedStatsC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.StatFeature.ModifiedStats>();

		public System.Collections.Generic.Dictionary<_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatTypes, System.Single> ModifiedStats => ModifiedStatsC.Value;

		public bool TryGetModifiedStats(out System.Collections.Generic.Dictionary<_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatTypes, System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.StatFeature.ModifiedStats component);
			if(result)
				value = component.Value;
			else
				value = default(System.Collections.Generic.Dictionary<_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatTypes, System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddModifiedStats()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.StatFeature.ModifiedStats() { Value = new System.Collections.Generic.Dictionary<_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatTypes, System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddModifiedStats(System.Collections.Generic.Dictionary<_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatTypes, System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.StatFeature.ModifiedStats() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.StatFeature.StatsEffects StatsEffectsC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatsEffects>();

		public _Project.Develop.Runtime.Gameplay.Features.StatFeature.StatsEffectsList StatsEffects => StatsEffectsC.Value;

		public bool TryGetStatsEffects(out _Project.Develop.Runtime.Gameplay.Features.StatFeature.StatsEffectsList value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.StatFeature.StatsEffects component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatsEffectsList);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddStatsEffects()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.StatFeature.StatsEffects() { Value = new _Project.Develop.Runtime.Gameplay.Features.StatFeature.StatsEffectsList() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddStatsEffects(_Project.Develop.Runtime.Gameplay.Features.StatFeature.StatsEffectsList value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.StatFeature.StatsEffects() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.SpawnFeature.SpawnInitialTime SpawnInitialTimeC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.SpawnFeature.SpawnInitialTime>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> SpawnInitialTime => SpawnInitialTimeC.Value;

		public bool TryGetSpawnInitialTime(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.SpawnFeature.SpawnInitialTime component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddSpawnInitialTime()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.SpawnFeature.SpawnInitialTime() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddSpawnInitialTime(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.SpawnFeature.SpawnInitialTime() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.SpawnFeature.SpawnCurrentTime SpawnCurrentTimeC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.SpawnFeature.SpawnCurrentTime>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> SpawnCurrentTime => SpawnCurrentTimeC.Value;

		public bool TryGetSpawnCurrentTime(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.SpawnFeature.SpawnCurrentTime component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddSpawnCurrentTime()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.SpawnFeature.SpawnCurrentTime() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddSpawnCurrentTime(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.SpawnFeature.SpawnCurrentTime() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.SpawnFeature.InSpawnProcess InSpawnProcessC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.SpawnFeature.InSpawnProcess>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> InSpawnProcess => InSpawnProcessC.Value;

		public bool TryGetInSpawnProcess(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.SpawnFeature.InSpawnProcess component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddInSpawnProcess()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.SpawnFeature.InSpawnProcess() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddInSpawnProcess(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.SpawnFeature.InSpawnProcess() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Sensors.BodyCollider BodyColliderC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Sensors.BodyCollider>();

		public UnityEngine.CapsuleCollider BodyCollider => BodyColliderC.Value;

		public bool TryGetBodyCollider(out UnityEngine.CapsuleCollider value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Sensors.BodyCollider component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.CapsuleCollider);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddBodyCollider(UnityEngine.CapsuleCollider value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Sensors.BodyCollider() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Sensors.ContactsDetectingMask ContactsDetectingMaskC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Sensors.ContactsDetectingMask>();

		public UnityEngine.LayerMask ContactsDetectingMask => ContactsDetectingMaskC.Value;

		public bool TryGetContactsDetectingMask(out UnityEngine.LayerMask value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Sensors.ContactsDetectingMask component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.LayerMask);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddContactsDetectingMask(UnityEngine.LayerMask value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Sensors.ContactsDetectingMask() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Sensors.ContactCollidersBuffer ContactCollidersBufferC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Sensors.ContactCollidersBuffer>();

		public _Project.Develop.Runtime.Utilities.Buffer<UnityEngine.Collider> ContactCollidersBuffer => ContactCollidersBufferC.Value;

		public bool TryGetContactCollidersBuffer(out _Project.Develop.Runtime.Utilities.Buffer<UnityEngine.Collider> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Sensors.ContactCollidersBuffer component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Buffer<UnityEngine.Collider>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddContactCollidersBuffer(_Project.Develop.Runtime.Utilities.Buffer<UnityEngine.Collider> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Sensors.ContactCollidersBuffer() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Sensors.ContactEntitiesBuffer ContactEntitiesBufferC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Sensors.ContactEntitiesBuffer>();

		public _Project.Develop.Runtime.Utilities.Buffer<_Project.Develop.Runtime.Gameplay.EntitiesCore.Entity> ContactEntitiesBuffer => ContactEntitiesBufferC.Value;

		public bool TryGetContactEntitiesBuffer(out _Project.Develop.Runtime.Utilities.Buffer<_Project.Develop.Runtime.Gameplay.EntitiesCore.Entity> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Sensors.ContactEntitiesBuffer component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Buffer<_Project.Develop.Runtime.Gameplay.EntitiesCore.Entity>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddContactEntitiesBuffer(_Project.Develop.Runtime.Utilities.Buffer<_Project.Develop.Runtime.Gameplay.EntitiesCore.Entity> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Sensors.ContactEntitiesBuffer() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Sensors.DeathMask DeathMaskC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Sensors.DeathMask>();

		public UnityEngine.LayerMask DeathMask => DeathMaskC.Value;

		public bool TryGetDeathMask(out UnityEngine.LayerMask value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Sensors.DeathMask component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.LayerMask);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddDeathMask(UnityEngine.LayerMask value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Sensors.DeathMask() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Sensors.IsTouchDeathMask IsTouchDeathMaskC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Sensors.IsTouchDeathMask>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> IsTouchDeathMask => IsTouchDeathMaskC.Value;

		public bool TryGetIsTouchDeathMask(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Sensors.IsTouchDeathMask component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddIsTouchDeathMask()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Sensors.IsTouchDeathMask() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddIsTouchDeathMask(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Sensors.IsTouchDeathMask() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Sensors.IsTouchAnotherTeam IsTouchAnotherTeamC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Sensors.IsTouchAnotherTeam>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> IsTouchAnotherTeam => IsTouchAnotherTeamC.Value;

		public bool TryGetIsTouchAnotherTeam(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Sensors.IsTouchAnotherTeam component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddIsTouchAnotherTeam()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Sensors.IsTouchAnotherTeam() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddIsTouchAnotherTeam(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Sensors.IsTouchAnotherTeam() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection MoveDirectionC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> MoveDirection => MoveDirectionC.Value;

		public bool TryGetMoveDirection(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMoveDirection()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMoveDirection(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed MoveSpeedC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> MoveSpeed => MoveSpeedC.Value;

		public bool TryGetMoveSpeed(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMoveSpeed()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMoveSpeed(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.MovementFeature.IsMoving IsMovingC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MovementFeature.IsMoving>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> IsMoving => IsMovingC.Value;

		public bool TryGetIsMoving(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.MovementFeature.IsMoving component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddIsMoving()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.IsMoving() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddIsMoving(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.IsMoving() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.MovementFeature.CanMove CanMoveC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MovementFeature.CanMove>();

		public _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition CanMove => CanMoveC.Value;

		public bool TryGetCanMove(out _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.MovementFeature.CanMove component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCanMove(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.CanMove() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.MovementFeature.RotationDirection RotationDirectionC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MovementFeature.RotationDirection>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> RotationDirection => RotationDirectionC.Value;

		public bool TryGetRotationDirection(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.MovementFeature.RotationDirection component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddRotationDirection()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.RotationDirection() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddRotationDirection(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.RotationDirection() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.MovementFeature.RotationSpeed RotationSpeedC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MovementFeature.RotationSpeed>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> RotationSpeed => RotationSpeedC.Value;

		public bool TryGetRotationSpeed(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.MovementFeature.RotationSpeed component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddRotationSpeed()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.RotationSpeed() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddRotationSpeed(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.RotationSpeed() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.MovementFeature.CanRotate CanRotateC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MovementFeature.CanRotate>();

		public _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition CanRotate => CanRotateC.Value;

		public bool TryGetCanRotate(out _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.MovementFeature.CanRotate component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCanRotate(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.CanRotate() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.MainHero.IsMainHero IsMainHeroC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MainHero.IsMainHero>();

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddIsMainHero()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MainHero.IsMainHero() ); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.LifeCycle.CurrentHealth CurrentHealthC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.LifeCycle.CurrentHealth>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> CurrentHealth => CurrentHealthC.Value;

		public bool TryGetCurrentHealth(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.LifeCycle.CurrentHealth component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCurrentHealth()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.CurrentHealth() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCurrentHealth(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.CurrentHealth() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.LifeCycle.MaxHealth MaxHealthC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.LifeCycle.MaxHealth>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> MaxHealth => MaxHealthC.Value;

		public bool TryGetMaxHealth(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.LifeCycle.MaxHealth component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMaxHealth()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.MaxHealth() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMaxHealth(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.MaxHealth() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.LifeCycle.HealthBarPoint HealthBarPointC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.LifeCycle.HealthBarPoint>();

		public UnityEngine.Transform HealthBarPoint => HealthBarPointC.Value;

		public bool TryGetHealthBarPoint(out UnityEngine.Transform value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.LifeCycle.HealthBarPoint component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.Transform);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddHealthBarPoint(UnityEngine.Transform value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.HealthBarPoint() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.LifeCycle.IsDead IsDeadC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.LifeCycle.IsDead>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> IsDead => IsDeadC.Value;

		public bool TryGetIsDead(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.LifeCycle.IsDead component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddIsDead()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.IsDead() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddIsDead(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.IsDead() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.LifeCycle.MustDie MustDieC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.LifeCycle.MustDie>();

		public _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition MustDie => MustDieC.Value;

		public bool TryGetMustDie(out _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.LifeCycle.MustDie component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMustDie(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.MustDie() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.LifeCycle.MustSelfRelease MustSelfReleaseC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.LifeCycle.MustSelfRelease>();

		public _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition MustSelfRelease => MustSelfReleaseC.Value;

		public bool TryGetMustSelfRelease(out _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.LifeCycle.MustSelfRelease component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMustSelfRelease(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.MustSelfRelease() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime DeathProcessInitialTimeC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> DeathProcessInitialTime => DeathProcessInitialTimeC.Value;

		public bool TryGetDeathProcessInitialTime(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddDeathProcessInitialTime()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddDeathProcessInitialTime(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime DeathProcessCurrentTimeC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> DeathProcessCurrentTime => DeathProcessCurrentTimeC.Value;

		public bool TryGetDeathProcessCurrentTime(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddDeathProcessCurrentTime()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddDeathProcessCurrentTime(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.LifeCycle.InDeathProcess InDeathProcessC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.LifeCycle.InDeathProcess>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> InDeathProcess => InDeathProcessC.Value;

		public bool TryGetInDeathProcess(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.LifeCycle.InDeathProcess component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddInDeathProcess()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.InDeathProcess() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddInDeathProcess(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.InDeathProcess() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.LifeCycle.DisableCollidersOnDeath DisableCollidersOnDeathC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.LifeCycle.DisableCollidersOnDeath>();

		public System.Collections.Generic.List<UnityEngine.Collider> DisableCollidersOnDeath => DisableCollidersOnDeathC.Value;

		public bool TryGetDisableCollidersOnDeath(out System.Collections.Generic.List<UnityEngine.Collider> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.LifeCycle.DisableCollidersOnDeath component);
			if(result)
				value = component.Value;
			else
				value = default(System.Collections.Generic.List<UnityEngine.Collider>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddDisableCollidersOnDeath()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.DisableCollidersOnDeath() { Value = new System.Collections.Generic.List<UnityEngine.Collider>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddDisableCollidersOnDeath(System.Collections.Generic.List<UnityEngine.Collider> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LifeCycle.DisableCollidersOnDeath() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.LevelUpFeature.Experience ExperienceC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.LevelUpFeature.Experience>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> Experience => ExperienceC.Value;

		public bool TryGetExperience(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.LevelUpFeature.Experience component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddExperience()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LevelUpFeature.Experience() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddExperience(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LevelUpFeature.Experience() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.LevelUpFeature.Level LevelC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.LevelUpFeature.Level>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Int32> Level => LevelC.Value;

		public bool TryGetLevel(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Int32> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.LevelUpFeature.Level component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Int32>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddLevel()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LevelUpFeature.Level() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Int32>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddLevel(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Int32> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.LevelUpFeature.Level() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Energy.MaxEnergy MaxEnergyC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Energy.MaxEnergy>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> MaxEnergy => MaxEnergyC.Value;

		public bool TryGetMaxEnergy(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Energy.MaxEnergy component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMaxEnergy()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Energy.MaxEnergy() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMaxEnergy(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Energy.MaxEnergy() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Energy.CurrentEnergy CurrentEnergyC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Energy.CurrentEnergy>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> CurrentEnergy => CurrentEnergyC.Value;

		public bool TryGetCurrentEnergy(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Energy.CurrentEnergy component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCurrentEnergy()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Energy.CurrentEnergy() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCurrentEnergy(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Energy.CurrentEnergy() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Energy.EnergyRecoverAmount EnergyRecoverAmountC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Energy.EnergyRecoverAmount>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> EnergyRecoverAmount => EnergyRecoverAmountC.Value;

		public bool TryGetEnergyRecoverAmount(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Energy.EnergyRecoverAmount component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddEnergyRecoverAmount()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Energy.EnergyRecoverAmount() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddEnergyRecoverAmount(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Energy.EnergyRecoverAmount() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Energy.EnergyRecoverInterval EnergyRecoverIntervalC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Energy.EnergyRecoverInterval>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> EnergyRecoverInterval => EnergyRecoverIntervalC.Value;

		public bool TryGetEnergyRecoverInterval(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Energy.EnergyRecoverInterval component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddEnergyRecoverInterval()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Energy.EnergyRecoverInterval() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddEnergyRecoverInterval(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Energy.EnergyRecoverInterval() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Energy.EnergySpendRequest EnergySpendRequestC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Energy.EnergySpendRequest>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> EnergySpendRequest => EnergySpendRequestC.Value;

		public bool TryGetEnergySpendRequest(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Energy.EnergySpendRequest component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddEnergySpendRequest()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Energy.EnergySpendRequest() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddEnergySpendRequest(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Energy.EnergySpendRequest() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Energy.EnergySpendEvent EnergySpendEventC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Energy.EnergySpendEvent>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent EnergySpendEvent => EnergySpendEventC.Value;

		public bool TryGetEnergySpendEvent(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Energy.EnergySpendEvent component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddEnergySpendEvent()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Energy.EnergySpendEvent() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddEnergySpendEvent(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Energy.EnergySpendEvent() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage.BodyContactDamage BodyContactDamageC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage.BodyContactDamage>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> BodyContactDamage => BodyContactDamageC.Value;

		public bool TryGetBodyContactDamage(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage.BodyContactDamage component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddBodyContactDamage()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage.BodyContactDamage() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddBodyContactDamage(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage.BodyContactDamage() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.StartAttackRequest StartAttackRequestC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.StartAttackRequest>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent StartAttackRequest => StartAttackRequestC.Value;

		public bool TryGetStartAttackRequest(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.StartAttackRequest component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddStartAttackRequest()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.StartAttackRequest() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddStartAttackRequest(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.StartAttackRequest() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.StartAttackEvent StartAttackEventC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.StartAttackEvent>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent StartAttackEvent => StartAttackEventC.Value;

		public bool TryGetStartAttackEvent(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.StartAttackEvent component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddStartAttackEvent()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.StartAttackEvent() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddStartAttackEvent(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.StartAttackEvent() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.CanStartAttack CanStartAttackC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.CanStartAttack>();

		public _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition CanStartAttack => CanStartAttackC.Value;

		public bool TryGetCanStartAttack(out _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.CanStartAttack component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCanStartAttack(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.CanStartAttack() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.EndAttackEvent EndAttackEventC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.EndAttackEvent>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent EndAttackEvent => EndAttackEventC.Value;

		public bool TryGetEndAttackEvent(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.EndAttackEvent component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddEndAttackEvent()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.EndAttackEvent() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddEndAttackEvent(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.EndAttackEvent() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.AttackProcessInitialTime AttackProcessInitialTimeC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.AttackProcessInitialTime>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> AttackProcessInitialTime => AttackProcessInitialTimeC.Value;

		public bool TryGetAttackProcessInitialTime(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.AttackProcessInitialTime component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackProcessInitialTime()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackProcessInitialTime() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackProcessInitialTime(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackProcessInitialTime() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.AttackProcessCurrentTime AttackProcessCurrentTimeC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.AttackProcessCurrentTime>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> AttackProcessCurrentTime => AttackProcessCurrentTimeC.Value;

		public bool TryGetAttackProcessCurrentTime(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.AttackProcessCurrentTime component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackProcessCurrentTime()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackProcessCurrentTime() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackProcessCurrentTime(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackProcessCurrentTime() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.InAttackProcess InAttackProcessC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.InAttackProcess>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> InAttackProcess => InAttackProcessC.Value;

		public bool TryGetInAttackProcess(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.InAttackProcess component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddInAttackProcess()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.InAttackProcess() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddInAttackProcess(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.InAttackProcess() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.AttackDelayTime AttackDelayTimeC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.AttackDelayTime>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> AttackDelayTime => AttackDelayTimeC.Value;

		public bool TryGetAttackDelayTime(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.AttackDelayTime component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackDelayTime()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackDelayTime() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackDelayTime(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackDelayTime() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.AttackDelayEndEvent AttackDelayEndEventC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.AttackDelayEndEvent>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent AttackDelayEndEvent => AttackDelayEndEventC.Value;

		public bool TryGetAttackDelayEndEvent(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.AttackDelayEndEvent component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackDelayEndEvent()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackDelayEndEvent() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackDelayEndEvent(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackDelayEndEvent() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.InstantAttackDamage InstantAttackDamageC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.InstantAttackDamage>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> InstantAttackDamage => InstantAttackDamageC.Value;

		public bool TryGetInstantAttackDamage(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.InstantAttackDamage component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddInstantAttackDamage()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.InstantAttackDamage() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddInstantAttackDamage(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.InstantAttackDamage() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.ShootPoint ShootPointC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.ShootPoint>();

		public UnityEngine.Transform ShootPoint => ShootPointC.Value;

		public bool TryGetShootPoint(out UnityEngine.Transform value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.ShootPoint component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.Transform);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddShootPoint(UnityEngine.Transform value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.ShootPoint() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.MustCancelAttack MustCancelAttackC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.MustCancelAttack>();

		public _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition MustCancelAttack => MustCancelAttackC.Value;

		public bool TryGetMustCancelAttack(out _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.MustCancelAttack component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMustCancelAttack(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.MustCancelAttack() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.AttackCanceledEvent AttackCanceledEventC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.AttackCanceledEvent>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent AttackCanceledEvent => AttackCanceledEventC.Value;

		public bool TryGetAttackCanceledEvent(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.AttackCanceledEvent component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackCanceledEvent()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackCanceledEvent() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackCanceledEvent(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackCanceledEvent() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.AttackCooldownInitialTime AttackCooldownInitialTimeC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.AttackCooldownInitialTime>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> AttackCooldownInitialTime => AttackCooldownInitialTimeC.Value;

		public bool TryGetAttackCooldownInitialTime(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.AttackCooldownInitialTime component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackCooldownInitialTime()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackCooldownInitialTime() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackCooldownInitialTime(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackCooldownInitialTime() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.AttackCooldownCurrentTime AttackCooldownCurrentTimeC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.AttackCooldownCurrentTime>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> AttackCooldownCurrentTime => AttackCooldownCurrentTimeC.Value;

		public bool TryGetAttackCooldownCurrentTime(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.AttackCooldownCurrentTime component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackCooldownCurrentTime()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackCooldownCurrentTime() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAttackCooldownCurrentTime(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.AttackCooldownCurrentTime() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.InAttackCooldown InAttackCooldownC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.InAttackCooldown>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> InAttackCooldown => InAttackCooldownC.Value;

		public bool TryGetInAttackCooldown(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.InAttackCooldown component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddInAttackCooldown()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.InAttackCooldown() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddInAttackCooldown(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.InAttackCooldown() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.Attack.Area.AreaAttackRadius AreaAttackRadiusC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.Attack.Area.AreaAttackRadius>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> AreaAttackRadius => AreaAttackRadiusC.Value;

		public bool TryGetAreaAttackRadius(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.Attack.Area.AreaAttackRadius component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAreaAttackRadius()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.Area.AreaAttackRadius() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAreaAttackRadius(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.Attack.Area.AreaAttackRadius() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageRequest TakeDamageRequestC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageRequest>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> TakeDamageRequest => TakeDamageRequestC.Value;

		public bool TryGetTakeDamageRequest(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageRequest component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTakeDamageRequest()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageRequest() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTakeDamageRequest(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageRequest() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageEvent TakeDamageEventC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageEvent>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> TakeDamageEvent => TakeDamageEventC.Value;

		public bool TryGetTakeDamageEvent(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageEvent component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTakeDamageEvent()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageEvent() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTakeDamageEvent(_Project.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageEvent() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.ApplyDamage.CanApplyDamage CanApplyDamageC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.ApplyDamage.CanApplyDamage>();

		public _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition CanApplyDamage => CanApplyDamageC.Value;

		public bool TryGetCanApplyDamage(out _Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.ApplyDamage.CanApplyDamage component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCanApplyDamage(_Project.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.ApplyDamage.CanApplyDamage() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.AI.CurrentTarget CurrentTargetC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.AI.CurrentTarget>();

		public _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<_Project.Develop.Runtime.Gameplay.EntitiesCore.Entity> CurrentTarget => CurrentTargetC.Value;

		public bool TryGetCurrentTarget(out _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<_Project.Develop.Runtime.Gameplay.EntitiesCore.Entity> value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.AI.CurrentTarget component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<_Project.Develop.Runtime.Gameplay.EntitiesCore.Entity>);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCurrentTarget()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.AI.CurrentTarget() { Value = new _Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<_Project.Develop.Runtime.Gameplay.EntitiesCore.Entity>() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCurrentTarget(_Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<_Project.Develop.Runtime.Gameplay.EntitiesCore.Entity> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.AI.CurrentTarget() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.AbilityFeature.AbilitiesComponent AbilitiesC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.AbilityFeature.AbilitiesComponent>();

		public _Project.Develop.Runtime.Configs.Gameplay.Abilities.AbilitiesList Abilities => AbilitiesC.Value;

		public bool TryGetAbilities(out _Project.Develop.Runtime.Configs.Gameplay.Abilities.AbilitiesList value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Features.AbilityFeature.AbilitiesComponent component);
			if(result)
				value = component.Value;
			else
				value = default(_Project.Develop.Runtime.Configs.Gameplay.Abilities.AbilitiesList);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAbilities()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.AbilityFeature.AbilitiesComponent() { Value = new _Project.Develop.Runtime.Configs.Gameplay.Abilities.AbilitiesList() }); 
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddAbilities(_Project.Develop.Runtime.Configs.Gameplay.Abilities.AbilitiesList value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.AbilityFeature.AbilitiesComponent() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Common.RigidbodyComponent RigidbodyC => GetComponent<_Project.Develop.Runtime.Gameplay.Common.RigidbodyComponent>();

		public UnityEngine.Rigidbody Rigidbody => RigidbodyC.Value;

		public bool TryGetRigidbody(out UnityEngine.Rigidbody value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Common.RigidbodyComponent component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.Rigidbody);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddRigidbody(UnityEngine.Rigidbody value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Common.RigidbodyComponent() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Common.TransformComponent TransformC => GetComponent<_Project.Develop.Runtime.Gameplay.Common.TransformComponent>();

		public UnityEngine.Transform Transform => TransformC.Value;

		public bool TryGetTransform(out UnityEngine.Transform value)
		{
			bool result = TryGetComponent(out _Project.Develop.Runtime.Gameplay.Common.TransformComponent component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.Transform);
			return result;
		}

		public _Project.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTransform(UnityEngine.Transform value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Common.TransformComponent() {Value = value}); 
		}

	}
}
