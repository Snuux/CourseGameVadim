using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.Features.AI;
using _Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using _Project.Develop.Runtime.Gameplay.Features.Attack;
using _Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage;
using _Project.Develop.Runtime.Gameplay.Features.LifeCycle;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using _Project.Develop.Runtime.Gameplay.Features.Sensors;
using _Project.Develop.Runtime.Utilities;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public BodyCollider BodyColliderC => GetComponent<BodyCollider>();

		public UnityEngine.CapsuleCollider BodyCollider => BodyColliderC.Value;

		public bool TryGetBodyCollider(out UnityEngine.CapsuleCollider value)
		{
			bool result = TryGetComponent(out BodyCollider component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.CapsuleCollider);
			return result;
		}

		public Entity AddBodyCollider(UnityEngine.CapsuleCollider value)
		{
			return AddComponent(new BodyCollider() {Value = value}); 
		}

		public ContactsDetectingMask ContactsDetectingMaskC => GetComponent<ContactsDetectingMask>();

		public UnityEngine.LayerMask ContactsDetectingMask => ContactsDetectingMaskC.Value;

		public bool TryGetContactsDetectingMask(out UnityEngine.LayerMask value)
		{
			bool result = TryGetComponent(out ContactsDetectingMask component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.LayerMask);
			return result;
		}

		public Entity AddContactsDetectingMask(UnityEngine.LayerMask value)
		{
			return AddComponent(new ContactsDetectingMask() {Value = value}); 
		}

		public ContactCollidersBuffer ContactCollidersBufferC => GetComponent<ContactCollidersBuffer>();

		public Buffer<UnityEngine.Collider> ContactCollidersBuffer => ContactCollidersBufferC.Value;

		public bool TryGetContactCollidersBuffer(out Buffer<UnityEngine.Collider> value)
		{
			bool result = TryGetComponent(out ContactCollidersBuffer component);
			if(result)
				value = component.Value;
			else
				value = default(Buffer<UnityEngine.Collider>);
			return result;
		}

		public Entity AddContactCollidersBuffer(Buffer<UnityEngine.Collider> value)
		{
			return AddComponent(new ContactCollidersBuffer() {Value = value}); 
		}

		public ContactEntitiesBuffer ContactEntitiesBufferC => GetComponent<ContactEntitiesBuffer>();

		public Buffer<Entity> ContactEntitiesBuffer => ContactEntitiesBufferC.Value;

		public bool TryGetContactEntitiesBuffer(out Buffer<Entity> value)
		{
			bool result = TryGetComponent(out ContactEntitiesBuffer component);
			if(result)
				value = component.Value;
			else
				value = default(Buffer<Entity>);
			return result;
		}

		public Entity AddContactEntitiesBuffer(Buffer<Entity> value)
		{
			return AddComponent(new ContactEntitiesBuffer() {Value = value}); 
		}

		public DeathMask DeathMaskC => GetComponent<DeathMask>();

		public UnityEngine.LayerMask DeathMask => DeathMaskC.Value;

		public bool TryGetDeathMask(out UnityEngine.LayerMask value)
		{
			bool result = TryGetComponent(out DeathMask component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.LayerMask);
			return result;
		}

		public Entity AddDeathMask(UnityEngine.LayerMask value)
		{
			return AddComponent(new DeathMask() {Value = value}); 
		}

		public IsTouchDeathMask IsTouchDeathMaskC => GetComponent<IsTouchDeathMask>();

		public ReactiveVariable<System.Boolean> IsTouchDeathMask => IsTouchDeathMaskC.Value;

		public bool TryGetIsTouchDeathMask(out ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out IsTouchDeathMask component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Boolean>);
			return result;
		}

		public Entity AddIsTouchDeathMask()
		{
			return AddComponent(new IsTouchDeathMask() { Value = new ReactiveVariable<System.Boolean>() }); 
		}

		public Entity AddIsTouchDeathMask(ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new IsTouchDeathMask() {Value = value}); 
		}

		public MoveDirection MoveDirectionC => GetComponent<MoveDirection>();

		public ReactiveVariable<UnityEngine.Vector3> MoveDirection => MoveDirectionC.Value;

		public bool TryGetMoveDirection(out ReactiveVariable<UnityEngine.Vector3> value)
		{
			bool result = TryGetComponent(out MoveDirection component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<UnityEngine.Vector3>);
			return result;
		}

		public Entity AddMoveDirection()
		{
			return AddComponent(new MoveDirection() { Value = new ReactiveVariable<UnityEngine.Vector3>() }); 
		}

		public Entity AddMoveDirection(ReactiveVariable<UnityEngine.Vector3> value)
		{
			return AddComponent(new MoveDirection() {Value = value}); 
		}

		public MoveSpeed MoveSpeedC => GetComponent<MoveSpeed>();

		public ReactiveVariable<System.Single> MoveSpeed => MoveSpeedC.Value;

		public bool TryGetMoveSpeed(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out MoveSpeed component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddMoveSpeed()
		{
			return AddComponent(new MoveSpeed() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddMoveSpeed(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new MoveSpeed() {Value = value}); 
		}

		public IsMoving IsMovingC => GetComponent<IsMoving>();

		public ReactiveVariable<System.Boolean> IsMoving => IsMovingC.Value;

		public bool TryGetIsMoving(out ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out IsMoving component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Boolean>);
			return result;
		}

		public Entity AddIsMoving()
		{
			return AddComponent(new IsMoving() { Value = new ReactiveVariable<System.Boolean>() }); 
		}

		public Entity AddIsMoving(ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new IsMoving() {Value = value}); 
		}

		public CanMove CanMoveC => GetComponent<CanMove>();

		public ICompositeCondition CanMove => CanMoveC.Value;

		public bool TryGetCanMove(out ICompositeCondition value)
		{
			bool result = TryGetComponent(out CanMove component);
			if(result)
				value = component.Value;
			else
				value = default(ICompositeCondition);
			return result;
		}

		public Entity AddCanMove(ICompositeCondition value)
		{
			return AddComponent(new CanMove() {Value = value}); 
		}

		public RotationDirection RotationDirectionC => GetComponent<RotationDirection>();

		public ReactiveVariable<UnityEngine.Vector3> RotationDirection => RotationDirectionC.Value;

		public bool TryGetRotationDirection(out ReactiveVariable<UnityEngine.Vector3> value)
		{
			bool result = TryGetComponent(out RotationDirection component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<UnityEngine.Vector3>);
			return result;
		}

		public Entity AddRotationDirection()
		{
			return AddComponent(new RotationDirection() { Value = new ReactiveVariable<UnityEngine.Vector3>() }); 
		}

		public Entity AddRotationDirection(ReactiveVariable<UnityEngine.Vector3> value)
		{
			return AddComponent(new RotationDirection() {Value = value}); 
		}

		public RotationSpeed RotationSpeedC => GetComponent<RotationSpeed>();

		public ReactiveVariable<System.Single> RotationSpeed => RotationSpeedC.Value;

		public bool TryGetRotationSpeed(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out RotationSpeed component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddRotationSpeed()
		{
			return AddComponent(new RotationSpeed() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddRotationSpeed(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new RotationSpeed() {Value = value}); 
		}

		public CanRotate CanRotateC => GetComponent<CanRotate>();

		public ICompositeCondition CanRotate => CanRotateC.Value;

		public bool TryGetCanRotate(out ICompositeCondition value)
		{
			bool result = TryGetComponent(out CanRotate component);
			if(result)
				value = component.Value;
			else
				value = default(ICompositeCondition);
			return result;
		}

		public Entity AddCanRotate(ICompositeCondition value)
		{
			return AddComponent(new CanRotate() {Value = value}); 
		}

		public CurrentHealth CurrentHealthC => GetComponent<CurrentHealth>();

		public ReactiveVariable<System.Single> CurrentHealth => CurrentHealthC.Value;

		public bool TryGetCurrentHealth(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out CurrentHealth component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddCurrentHealth()
		{
			return AddComponent(new CurrentHealth() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddCurrentHealth(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new CurrentHealth() {Value = value}); 
		}

		public MaxHealth MaxHealthC => GetComponent<MaxHealth>();

		public ReactiveVariable<System.Single> MaxHealth => MaxHealthC.Value;

		public bool TryGetMaxHealth(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out MaxHealth component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddMaxHealth()
		{
			return AddComponent(new MaxHealth() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddMaxHealth(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new MaxHealth() {Value = value}); 
		}

		public IsDead IsDeadC => GetComponent<IsDead>();

		public ReactiveVariable<System.Boolean> IsDead => IsDeadC.Value;

		public bool TryGetIsDead(out ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out IsDead component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Boolean>);
			return result;
		}

		public Entity AddIsDead()
		{
			return AddComponent(new IsDead() { Value = new ReactiveVariable<System.Boolean>() }); 
		}

		public Entity AddIsDead(ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new IsDead() {Value = value}); 
		}

		public MustDie MustDieC => GetComponent<MustDie>();

		public ICompositeCondition MustDie => MustDieC.Value;

		public bool TryGetMustDie(out ICompositeCondition value)
		{
			bool result = TryGetComponent(out MustDie component);
			if(result)
				value = component.Value;
			else
				value = default(ICompositeCondition);
			return result;
		}

		public Entity AddMustDie(ICompositeCondition value)
		{
			return AddComponent(new MustDie() {Value = value}); 
		}

		public MustSelfRelease MustSelfReleaseC => GetComponent<MustSelfRelease>();

		public ICompositeCondition MustSelfRelease => MustSelfReleaseC.Value;

		public bool TryGetMustSelfRelease(out ICompositeCondition value)
		{
			bool result = TryGetComponent(out MustSelfRelease component);
			if(result)
				value = component.Value;
			else
				value = default(ICompositeCondition);
			return result;
		}

		public Entity AddMustSelfRelease(ICompositeCondition value)
		{
			return AddComponent(new MustSelfRelease() {Value = value}); 
		}

		public DeathProcessInitialTime DeathProcessInitialTimeC => GetComponent<DeathProcessInitialTime>();

		public ReactiveVariable<System.Single> DeathProcessInitialTime => DeathProcessInitialTimeC.Value;

		public bool TryGetDeathProcessInitialTime(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out DeathProcessInitialTime component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddDeathProcessInitialTime()
		{
			return AddComponent(new DeathProcessInitialTime() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddDeathProcessInitialTime(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new DeathProcessInitialTime() {Value = value}); 
		}

		public DeathProcessCurrentTime DeathProcessCurrentTimeC => GetComponent<DeathProcessCurrentTime>();

		public ReactiveVariable<System.Single> DeathProcessCurrentTime => DeathProcessCurrentTimeC.Value;

		public bool TryGetDeathProcessCurrentTime(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out DeathProcessCurrentTime component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddDeathProcessCurrentTime()
		{
			return AddComponent(new DeathProcessCurrentTime() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddDeathProcessCurrentTime(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new DeathProcessCurrentTime() {Value = value}); 
		}

		public InDeathProcess InDeathProcessC => GetComponent<InDeathProcess>();

		public ReactiveVariable<System.Boolean> InDeathProcess => InDeathProcessC.Value;

		public bool TryGetInDeathProcess(out ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out InDeathProcess component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Boolean>);
			return result;
		}

		public Entity AddInDeathProcess()
		{
			return AddComponent(new InDeathProcess() { Value = new ReactiveVariable<System.Boolean>() }); 
		}

		public Entity AddInDeathProcess(ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new InDeathProcess() {Value = value}); 
		}

		public DisableCollidersOnDeath DisableCollidersOnDeathC => GetComponent<DisableCollidersOnDeath>();

		public System.Collections.Generic.List<UnityEngine.Collider> DisableCollidersOnDeath => DisableCollidersOnDeathC.Value;

		public bool TryGetDisableCollidersOnDeath(out System.Collections.Generic.List<UnityEngine.Collider> value)
		{
			bool result = TryGetComponent(out DisableCollidersOnDeath component);
			if(result)
				value = component.Value;
			else
				value = default(System.Collections.Generic.List<UnityEngine.Collider>);
			return result;
		}

		public Entity AddDisableCollidersOnDeath()
		{
			return AddComponent(new DisableCollidersOnDeath() { Value = new System.Collections.Generic.List<UnityEngine.Collider>() }); 
		}

		public Entity AddDisableCollidersOnDeath(System.Collections.Generic.List<UnityEngine.Collider> value)
		{
			return AddComponent(new DisableCollidersOnDeath() {Value = value}); 
		}

		public BodyContactDamage BodyContactDamageC => GetComponent<BodyContactDamage>();

		public ReactiveVariable<System.Single> BodyContactDamage => BodyContactDamageC.Value;

		public bool TryGetBodyContactDamage(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out BodyContactDamage component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddBodyContactDamage()
		{
			return AddComponent(new BodyContactDamage() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddBodyContactDamage(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new BodyContactDamage() {Value = value}); 
		}

		public StartAttackRequest StartAttackRequestC => GetComponent<StartAttackRequest>();

		public ReactiveEvent StartAttackRequest => StartAttackRequestC.Value;

		public bool TryGetStartAttackRequest(out ReactiveEvent value)
		{
			bool result = TryGetComponent(out StartAttackRequest component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveEvent);
			return result;
		}

		public Entity AddStartAttackRequest()
		{
			return AddComponent(new StartAttackRequest() { Value = new ReactiveEvent() }); 
		}

		public Entity AddStartAttackRequest(ReactiveEvent value)
		{
			return AddComponent(new StartAttackRequest() {Value = value}); 
		}

		public StartAttackEvent StartAttackEventC => GetComponent<StartAttackEvent>();

		public ReactiveEvent StartAttackEvent => StartAttackEventC.Value;

		public bool TryGetStartAttackEvent(out ReactiveEvent value)
		{
			bool result = TryGetComponent(out StartAttackEvent component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveEvent);
			return result;
		}

		public Entity AddStartAttackEvent()
		{
			return AddComponent(new StartAttackEvent() { Value = new ReactiveEvent() }); 
		}

		public Entity AddStartAttackEvent(ReactiveEvent value)
		{
			return AddComponent(new StartAttackEvent() {Value = value}); 
		}

		public CanStartAttack CanStartAttackC => GetComponent<CanStartAttack>();

		public ICompositeCondition CanStartAttack => CanStartAttackC.Value;

		public bool TryGetCanStartAttack(out ICompositeCondition value)
		{
			bool result = TryGetComponent(out CanStartAttack component);
			if(result)
				value = component.Value;
			else
				value = default(ICompositeCondition);
			return result;
		}

		public Entity AddCanStartAttack(ICompositeCondition value)
		{
			return AddComponent(new CanStartAttack() {Value = value}); 
		}

		public EndAttackEvent EndAttackEventC => GetComponent<EndAttackEvent>();

		public ReactiveEvent EndAttackEvent => EndAttackEventC.Value;

		public bool TryGetEndAttackEvent(out ReactiveEvent value)
		{
			bool result = TryGetComponent(out EndAttackEvent component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveEvent);
			return result;
		}

		public Entity AddEndAttackEvent()
		{
			return AddComponent(new EndAttackEvent() { Value = new ReactiveEvent() }); 
		}

		public Entity AddEndAttackEvent(ReactiveEvent value)
		{
			return AddComponent(new EndAttackEvent() {Value = value}); 
		}

		public AttackProcessInitialTime AttackProcessInitialTimeC => GetComponent<AttackProcessInitialTime>();

		public ReactiveVariable<System.Single> AttackProcessInitialTime => AttackProcessInitialTimeC.Value;

		public bool TryGetAttackProcessInitialTime(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out AttackProcessInitialTime component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddAttackProcessInitialTime()
		{
			return AddComponent(new AttackProcessInitialTime() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddAttackProcessInitialTime(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new AttackProcessInitialTime() {Value = value}); 
		}

		public AttackProcessCurrentTime AttackProcessCurrentTimeC => GetComponent<AttackProcessCurrentTime>();

		public ReactiveVariable<System.Single> AttackProcessCurrentTime => AttackProcessCurrentTimeC.Value;

		public bool TryGetAttackProcessCurrentTime(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out AttackProcessCurrentTime component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddAttackProcessCurrentTime()
		{
			return AddComponent(new AttackProcessCurrentTime() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddAttackProcessCurrentTime(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new AttackProcessCurrentTime() {Value = value}); 
		}

		public InAttackProcess InAttackProcessC => GetComponent<InAttackProcess>();

		public ReactiveVariable<System.Boolean> InAttackProcess => InAttackProcessC.Value;

		public bool TryGetInAttackProcess(out ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out InAttackProcess component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Boolean>);
			return result;
		}

		public Entity AddInAttackProcess()
		{
			return AddComponent(new InAttackProcess() { Value = new ReactiveVariable<System.Boolean>() }); 
		}

		public Entity AddInAttackProcess(ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new InAttackProcess() {Value = value}); 
		}

		public AttackDelayTime AttackDelayTimeC => GetComponent<AttackDelayTime>();

		public ReactiveVariable<System.Single> AttackDelayTime => AttackDelayTimeC.Value;

		public bool TryGetAttackDelayTime(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out AttackDelayTime component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddAttackDelayTime()
		{
			return AddComponent(new AttackDelayTime() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddAttackDelayTime(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new AttackDelayTime() {Value = value}); 
		}

		public AttackDelayEndEvent AttackDelayEndEventC => GetComponent<AttackDelayEndEvent>();

		public ReactiveEvent AttackDelayEndEvent => AttackDelayEndEventC.Value;

		public bool TryGetAttackDelayEndEvent(out ReactiveEvent value)
		{
			bool result = TryGetComponent(out AttackDelayEndEvent component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveEvent);
			return result;
		}

		public Entity AddAttackDelayEndEvent()
		{
			return AddComponent(new AttackDelayEndEvent() { Value = new ReactiveEvent() }); 
		}

		public Entity AddAttackDelayEndEvent(ReactiveEvent value)
		{
			return AddComponent(new AttackDelayEndEvent() {Value = value}); 
		}

		public InstantAttackDamage InstantAttackDamageC => GetComponent<InstantAttackDamage>();

		public ReactiveVariable<System.Single> InstantAttackDamage => InstantAttackDamageC.Value;

		public bool TryGetInstantAttackDamage(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out InstantAttackDamage component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddInstantAttackDamage()
		{
			return AddComponent(new InstantAttackDamage() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddInstantAttackDamage(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new InstantAttackDamage() {Value = value}); 
		}

		public ShootPoint ShootPointC => GetComponent<ShootPoint>();

		public UnityEngine.Transform ShootPoint => ShootPointC.Value;

		public bool TryGetShootPoint(out UnityEngine.Transform value)
		{
			bool result = TryGetComponent(out ShootPoint component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.Transform);
			return result;
		}

		public Entity AddShootPoint(UnityEngine.Transform value)
		{
			return AddComponent(new ShootPoint() {Value = value}); 
		}

		public MustCancelAttack MustCancelAttackC => GetComponent<MustCancelAttack>();

		public ICompositeCondition MustCancelAttack => MustCancelAttackC.Value;

		public bool TryGetMustCancelAttack(out ICompositeCondition value)
		{
			bool result = TryGetComponent(out MustCancelAttack component);
			if(result)
				value = component.Value;
			else
				value = default(ICompositeCondition);
			return result;
		}

		public Entity AddMustCancelAttack(ICompositeCondition value)
		{
			return AddComponent(new MustCancelAttack() {Value = value}); 
		}

		public AttackCanceledEvent AttackCanceledEventC => GetComponent<AttackCanceledEvent>();

		public ReactiveEvent AttackCanceledEvent => AttackCanceledEventC.Value;

		public bool TryGetAttackCanceledEvent(out ReactiveEvent value)
		{
			bool result = TryGetComponent(out AttackCanceledEvent component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveEvent);
			return result;
		}

		public Entity AddAttackCanceledEvent()
		{
			return AddComponent(new AttackCanceledEvent() { Value = new ReactiveEvent() }); 
		}

		public Entity AddAttackCanceledEvent(ReactiveEvent value)
		{
			return AddComponent(new AttackCanceledEvent() {Value = value}); 
		}

		public AttackCooldownInitialTime AttackCooldownInitialTimeC => GetComponent<AttackCooldownInitialTime>();

		public ReactiveVariable<System.Single> AttackCooldownInitialTime => AttackCooldownInitialTimeC.Value;

		public bool TryGetAttackCooldownInitialTime(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out AttackCooldownInitialTime component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddAttackCooldownInitialTime()
		{
			return AddComponent(new AttackCooldownInitialTime() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddAttackCooldownInitialTime(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new AttackCooldownInitialTime() {Value = value}); 
		}

		public AttackCooldownCurrentTime AttackCooldownCurrentTimeC => GetComponent<AttackCooldownCurrentTime>();

		public ReactiveVariable<System.Single> AttackCooldownCurrentTime => AttackCooldownCurrentTimeC.Value;

		public bool TryGetAttackCooldownCurrentTime(out ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out AttackCooldownCurrentTime component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Single>);
			return result;
		}

		public Entity AddAttackCooldownCurrentTime()
		{
			return AddComponent(new AttackCooldownCurrentTime() { Value = new ReactiveVariable<System.Single>() }); 
		}

		public Entity AddAttackCooldownCurrentTime(ReactiveVariable<System.Single> value)
		{
			return AddComponent(new AttackCooldownCurrentTime() {Value = value}); 
		}

		public InAttackCooldown InAttackCooldownC => GetComponent<InAttackCooldown>();

		public ReactiveVariable<System.Boolean> InAttackCooldown => InAttackCooldownC.Value;

		public bool TryGetInAttackCooldown(out ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out InAttackCooldown component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<System.Boolean>);
			return result;
		}

		public Entity AddInAttackCooldown()
		{
			return AddComponent(new InAttackCooldown() { Value = new ReactiveVariable<System.Boolean>() }); 
		}

		public Entity AddInAttackCooldown(ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new InAttackCooldown() {Value = value}); 
		}

		public TakeDamageRequest TakeDamageRequestC => GetComponent<TakeDamageRequest>();

		public ReactiveEvent<System.Single> TakeDamageRequest => TakeDamageRequestC.Value;

		public bool TryGetTakeDamageRequest(out ReactiveEvent<System.Single> value)
		{
			bool result = TryGetComponent(out TakeDamageRequest component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveEvent<System.Single>);
			return result;
		}

		public Entity AddTakeDamageRequest()
		{
			return AddComponent(new TakeDamageRequest() { Value = new ReactiveEvent<System.Single>() }); 
		}

		public Entity AddTakeDamageRequest(ReactiveEvent<System.Single> value)
		{
			return AddComponent(new TakeDamageRequest() {Value = value}); 
		}

		public TakeDamageEvent TakeDamageEventC => GetComponent<TakeDamageEvent>();

		public ReactiveEvent<System.Single> TakeDamageEvent => TakeDamageEventC.Value;

		public bool TryGetTakeDamageEvent(out ReactiveEvent<System.Single> value)
		{
			bool result = TryGetComponent(out TakeDamageEvent component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveEvent<System.Single>);
			return result;
		}

		public Entity AddTakeDamageEvent()
		{
			return AddComponent(new TakeDamageEvent() { Value = new ReactiveEvent<System.Single>() }); 
		}

		public Entity AddTakeDamageEvent(ReactiveEvent<System.Single> value)
		{
			return AddComponent(new TakeDamageEvent() {Value = value}); 
		}

		public CanApplyDamage CanApplyDamageC => GetComponent<CanApplyDamage>();

		public ICompositeCondition CanApplyDamage => CanApplyDamageC.Value;

		public bool TryGetCanApplyDamage(out ICompositeCondition value)
		{
			bool result = TryGetComponent(out CanApplyDamage component);
			if(result)
				value = component.Value;
			else
				value = default(ICompositeCondition);
			return result;
		}

		public Entity AddCanApplyDamage(ICompositeCondition value)
		{
			return AddComponent(new CanApplyDamage() {Value = value}); 
		}

		public CurrentTarget CurrentTargetC => GetComponent<CurrentTarget>();

		public ReactiveVariable<Entity> CurrentTarget => CurrentTargetC.Value;

		public bool TryGetCurrentTarget(out ReactiveVariable<Entity> value)
		{
			bool result = TryGetComponent(out CurrentTarget component);
			if(result)
				value = component.Value;
			else
				value = default(ReactiveVariable<Entity>);
			return result;
		}

		public Entity AddCurrentTarget()
		{
			return AddComponent(new CurrentTarget() { Value = new ReactiveVariable<Entity>() }); 
		}

		public Entity AddCurrentTarget(ReactiveVariable<Entity> value)
		{
			return AddComponent(new CurrentTarget() {Value = value}); 
		}

		public RigidbodyComponent RigidbodyC => GetComponent<RigidbodyComponent>();

		public UnityEngine.Rigidbody Rigidbody => RigidbodyC.Value;

		public bool TryGetRigidbody(out UnityEngine.Rigidbody value)
		{
			bool result = TryGetComponent(out RigidbodyComponent component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.Rigidbody);
			return result;
		}

		public Entity AddRigidbody(UnityEngine.Rigidbody value)
		{
			return AddComponent(new RigidbodyComponent() {Value = value}); 
		}

		public TransformComponent TransformC => GetComponent<TransformComponent>();

		public UnityEngine.Transform Transform => TransformC.Value;

		public bool TryGetTransform(out UnityEngine.Transform value)
		{
			bool result = TryGetComponent(out TransformComponent component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.Transform);
			return result;
		}

		public Entity AddTransform(UnityEngine.Transform value)
		{
			return AddComponent(new TransformComponent() {Value = value}); 
		}

	}
}
