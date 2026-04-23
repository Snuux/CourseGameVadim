# Entity Way: как организован gameplay-код в этом проекте

## Содержание

1. [Зачем нужен этот документ](#зачем-нужен-этот-документ)
2. [Короткий ответ: что это за подход](#короткий-ответ-что-это-за-подход)
3. [Чем это отличается от классического ECS](#чем-это-отличается-от-классического-ecs)
4. [Главная формула проекта](#главная-формула-проекта)
5. [Из каких слоёв состоит код](#из-каких-слоёв-состоит-код)
6. [Из чего состоит одна сущность](#из-чего-состоит-одна-сущность)
7. [Как проходит один кадр](#как-проходит-один-кадр)
8. [Жизненный цикл сущности](#жизненный-цикл-сущности)
9. [Пример: как работает атака героя](#пример-как-работает-атака-героя)
10. [Как правильно добавлять новую фичу](#как-правильно-добавлять-новую-фичу)
11. [Правила организации кода](#правила-организации-кода)
12. [Что здесь считается ошибкой](#что-здесь-считается-ошибкой)
13. [Быстрый чеклист перед коммитом](#быстрый-чеклист-перед-коммитом)

---

## Зачем нужен этот документ

`UNITY_COMPOSITION_GUIDE.md` объясняет общий компонентный подход в Unity.

Этот документ делает другое: он фиксирует **именно тот стиль архитектуры, который реально используется в этом курсовом проекте**.

Если коротко, то проект построен не вокруг "толстых" `MonoBehaviour`, и не вокруг классического ECS с глобальными системами и фильтрами по всем данным мира. Здесь используется более приземлённый и очень практичный стиль:

- есть runtime-сущность `Entity`;
- у неё есть набор маленьких компонентов состояния;
- у неё есть набор систем, прикреплённых именно к этой сущности;
- команды приходят через `ReactiveEvent` и `ReactiveVariable`;
- Unity-prefab даёт только ссылки на реальные scene-объекты и visual layer;
- AI и input управляют сущностью через state machine, а не напрямую через физику и HP.

---

## Короткий ответ: что это за подход

Лучшее рабочее название для этого проекта:

**Reactive Entity Composition**  
или по-русски:  
**компонентная entity-архитектура с локальными системами**

Это что-то между:

- композиционным Unity-подходом;
- ECS-мышлением;
- state-driven gameplay архитектурой.

Самая важная мысль:

> Система здесь обычно работает не "по всем сущностям с компонентом X", а "внутри конкретной сущности, к которой она была подключена фабрикой".

То есть это не "мир данных + глобальные системы", а скорее:

```text
Entity = маленький контейнер состояния + набор локальных систем + связи с prefab/view
```

---

## Чем это отличается от классического ECS

### В классическом ECS обычно так

- компоненты в основном data-only;
- системы глобальные;
- система делает запрос вроде "дай все сущности с Position + Velocity";
- логика чаще живёт в world/system layer, а не внутри самой сущности.

### В этом проекте так

- компоненты тоже маленькие, но это не только "сырые данные";
- они могут хранить `ReactiveVariable<T>`, `ReactiveEvent`, `ICompositeCondition`, `Transform`, `Rigidbody` и другие runtime-объекты;
- системы вешаются на конкретную `Entity` через `entity.AddSystem(...)`;
- `EntitiesLifeContext` просто вызывает `entity.OnUpdate(deltaTime)` для каждой сущности;
- уже внутри `Entity` вызываются только её собственные системы.

### Поэтому это не чистый ECS

Но это всё ещё ECS-подобное мышление, потому что:

- состояние дробится на маленькие компоненты;
- логика дробится на маленькие системы;
- фича собирается как композиция;
- есть явное разделение между данными, поведением, управлением и отображением.

---

## Главная формула проекта

```text
Scene Bootstrap + DI
    -> Factories
        -> Entity
            -> Components
            -> Conditions
            -> Systems
            -> Brain
            -> Mono Views / Registrators
```

Или ещё проще:

```text
Prefab даёт ссылки и визуал
Entity хранит состояние
Systems меняют состояние
Brain пишет команды
Views подписываются на изменения
```

---

## Из каких слоёв состоит код

### 1. Bootstrap и DI

Это верхний уровень сцены.

Файлы:

- `Assets/_Project/Develop/Runtime/Gameplay/Infrastructure/GameplayBootstrap.cs`
- `Assets/_Project/Develop/Runtime/Gameplay/Infrastructure/GameplayContextRegistrations.cs`
- `Assets/_Project/Develop/Runtime/Infrastructure/DI/DIContainer.cs`

Задача этого слоя:

- зарегистрировать сервисы;
- создать фабрики;
- поднять UI;
- создать главного героя;
- запускать update-контексты.

### 2. EntitiesCore

Это центр gameplay runtime-модели.

Файлы:

- `Assets/_Project/Develop/Runtime/Gameplay/EntitiesCore/Entity.cs`
- `Assets/_Project/Develop/Runtime/Gameplay/EntitiesCore/EntitiesLifeContext.cs`
- `Assets/_Project/Develop/Runtime/Gameplay/EntitiesCore/EntitiesFactory.cs`

Задача этого слоя:

- хранить компоненты сущности;
- хранить системы сущности;
- инициализировать и обновлять их;
- управлять жизненным циклом сущностей.

### 3. Features

Это основной gameplay-код.

Папки вида:

- `Gameplay/Features/MovementFeature`
- `Gameplay/Features/Attack`
- `Gameplay/Features/LifeCycle`
- `Gameplay/Features/Sensors`
- `Gameplay/Features/Teleport`
- `Gameplay/Features/AI`

Обычно каждая feature содержит:

- набор компонентов;
- набор систем;
- иногда view;
- иногда registrator;
- иногда сервис или фабрику.

### 4. Mono layer

Это связка с Unity-сценой и prefab.

Файлы:

- `Gameplay/EntitiesCore/Mono/MonoEntity.cs`
- `Gameplay/EntitiesCore/Mono/MonoEntityRegistrator.cs`
- `Gameplay/EntitiesCore/Mono/EntityView.cs`

Задача слоя:

- зарегистрировать Unity-ссылки в `Entity`;
- подписать визуальные элементы на runtime-состояние;
- связать физические коллайдеры с логической сущностью.

### 5. Brain / AI / Input layer

Это не "боевая логика", а источник команд.

Файлы:

- `Gameplay/Features/AI/BrainsFactory.cs`
- `Gameplay/Features/AI/AIBrainsContext.cs`
- `Gameplay/Features/AI/States/*`
- `Gameplay/Features/InputFeature/DesktopInput.cs`

Задача этого слоя:

- решить, что сущность хочет сделать;
- записать это желание в компоненты;
- не лезть напрямую в физику, урон и смерть.

### 6. UI / Presenters

Это уже отдельный presentation-слой поверх gameplay.

Он читает состояние сервисов, stage context, героя и так далее, но не должен тащить на себе core gameplay-логику.

---

## Из чего состоит одна сущность

На практике сущность здесь собирается из пяти типов вещей.

| Что это | Примеры | Зачем нужно |
|---|---|---|
| Состояние | `CurrentHealth`, `MoveSpeed`, `IsDead`, `CurrentTarget` | Хранить текущее runtime-состояние |
| Команды / requests | `StartAttackRequest`, `TeleportRequested`, `EnergySpendRequest` | Попросить систему что-то сделать |
| События | `StartAttackEvent`, `TakeDamageEvent`, `AttackDelayEndEvent` | Сообщить, что действие уже произошло |
| Условия | `CanMove`, `CanStartAttack`, `MustDie`, `MustSelfRelease` | Отделить правила от механики |
| Unity-ссылки | `TransformComponent`, `RigidbodyComponent`, `ShootPoint`, `BodyCollider` | Дать системам доступ к Unity API |

### Примеры компонентов из проекта

```csharp
public class MoveDirection : IEntityComponent
{
    public ReactiveVariable<Vector3> Value;
}

public class CanMove : IEntityComponent
{
    public ICompositeCondition Value;
}

public class StartAttackRequest : IEntityComponent
{
    public ReactiveEvent Value;
}
```

Это ключевая идея проекта:

> Компонент здесь почти всегда очень маленький. Он не "думает". Он хранит состояние, событие, условие или ссылку.

---

## Как проходит один кадр

Самый важный порядок лежит в `GameplayBootstrap.Update()`:

1. `AIBrainsContext.Update(deltaTime)`
2. `EntitiesLifeContext.Update(deltaTime)`
3. `GameplayStateContext.Update(deltaTime)`

Что это значит practically:

- brain сначала пишет команды и направления;
- потом entity-системы обрабатывают эти команды;
- потом уже верхнеуровневое состояние сцены делает свои переходы.

Это хороший порядок, потому что:

- input/AI принимает решение в начале кадра;
- сущность реагирует в этот же кадр;
- state machine сцены работает уже поверх обновлённого gameplay-состояния.

---

## Жизненный цикл сущности

### 1. Фабрика создаёт пустую `Entity`

Обычно через `EntitiesFactory`.

### 2. Создаётся prefab-представление

`MonoEntitiesFactory.Create(entity, position, prefabPath)`:

- инстанцирует `MonoEntity`;
- вызывает `Link(entity)`;
- registrator-ы добавляют Unity-компоненты в `Entity`;
- view начинают слушать `entity.Initialized`.

### 3. Фабрика добавляет gameplay-компоненты

Например для героя:

```csharp
entity
    .AddMoveDirection()
    .AddMoveSpeed(new ReactiveVariable<float>(baseStats[StatTypes.MoveSpeed]))
    .AddCurrentHealth(new ReactiveVariable<float>(baseStats[StatTypes.MaxHealth]))
    .AddStartAttackRequest()
    .AddStartAttackEvent()
    .AddAttackCooldownCurrentTime();
```

### 4. Фабрика собирает правила через условия

Пример реального паттерна из проекта:

```csharp
ICompositeCondition canStartAttack = new CompositeCondition()
    .Add(new FuncCondition(() => entity.IsDead.Value == false))
    .Add(new FuncCondition(() => entity.InAttackProcess.Value == false))
    .Add(new FuncCondition(() => entity.IsMoving.Value == false))
    .Add(new FuncCondition(() => entity.InAttackCooldown.Value == false));
```

Это очень важная черта проекта:

> Правила "можно / нельзя / нужно" живут отдельно от самих систем.

### 5. Фабрика подключает системы

Например:

```csharp
entity
    .AddSystem(new RigidbodyMovementSystem())
    .AddSystem(new StartAttackSystem())
    .AddSystem(new AttackProcessTimerSystem())
    .AddSystem(new InstantShootSystem(this))
    .AddSystem(new ApplyDamageSystem())
    .AddSystem(new DeathSystem());
```

### 6. При необходимости подключается brain

Например `MainHeroFactory` создаёт brain для героя, а `EnemiesFactory` создаёт brain для врага.

### 7. Сущность добавляется в `EntitiesLifeContext`

Только после этого вызывается `entity.Initialize()`, и системы реально начинают работать.

### 8. Когда сущность больше не нужна, она релизится

`SelfReleaseSystem` вызывает `EntitiesLifeContext.Release(entity)`, после чего:

- сущность удаляется из списка;
- вызывается `Dispose()` у её систем;
- `MonoEntitiesFactory` уничтожает соответствующий prefab instance;
- collider registry очищается.

---

## Пример: как работает атака героя

Это хороший пример полного pipeline в этом проекте.

### Шаг 1. Brain решает атаковать

`AttackTriggerState` не создаёт projectile сам.
Он просто вызывает:

```csharp
_attackRequest.Invoke();
```

То есть brain не делает боевую логику. Он только отправляет команду.

### Шаг 2. `StartAttackSystem` принимает request

Система подписана на `StartAttackRequest`.
Когда приходит request, она:

- проверяет `CanStartAttack`;
- ставит `InAttackProcess = true`;
- вызывает `StartAttackEvent`.

### Шаг 3. Таймеры атаки начинают процесс

`AttackProcessTimerSystem` двигает время атаки.

`AttackDelayEndTriggerSystem` слушает таймер и в нужный момент вызывает `AttackDelayEndEvent`.

`EndAttackSystem` завершает процесс атаки и вызывает `EndAttackEvent`.

`AttackCooldownTimerSystem` включает и выключает кулдаун.

### Шаг 4. Выстрел создаётся отдельной системой

`InstantShootSystem` слушает `AttackDelayEndEvent` и только тогда создаёт projectile:

```csharp
_entitiesFactory.CreateProjectile(
    _shootPoint.position,
    _shootPoint.forward,
    _damage.Value,
    _entity);
```

### Шаг 5. Projectile живёт как отдельная сущность

Projectile получает свои:

- компоненты движения;
- компоненты сенсоров;
- урон;
- команду на смерть при столкновении.

### Шаг 6. Урон применяется через событие

Контактная система вызывает:

```csharp
EntitiesHelper.TryTakeDamageFrom(_entity, contactEntity, _damage.Value);
```

А уже `ApplyDamageSystem` на цели:

- проверяет `CanApplyDamage`;
- уменьшает `CurrentHealth`;
- вызывает `TakeDamageEvent`.

### Шаг 7. View реагирует на состояние

Например `ApplyDamageView` просто подписывается на `TakeDamageEvent` и спавнит effect.

То есть visual feedback тоже отделён от gameplay-решения.

---

## Как правильно добавлять новую фичу

Ниже самый полезный рабочий алгоритм для этого проекта.

### 1. Сначала реши, это feature для какой сущности

Например:

- только для героя;
- только для врагов;
- для любого персонажа;
- для projectile;
- для отдельного service/context уровня.

Если фича относится к конкретной сущности, почти всегда её сборка пойдёт через `EntitiesFactory`, `MainHeroFactory` или `EnemiesFactory`.

### 2. Определи минимальные компоненты

Обычно нужно выделить:

- состояние;
- request;
- event;
- conditions;
- ссылки на Unity-объекты, если без них никак.

Пример мышления для dash:

- `DashRequested`
- `DashDirection`
- `DashDistance`
- `InDashProcess`
- `CanStartDash`
- `DashCompletedEvent`

### 3. Напиши системы с одной ответственностью

Хороший стиль для этого проекта:

- одна система стартует процесс;
- другая считает таймер;
- третья применяет физику;
- четвёртая завершает процесс;
- view отдельно показывает визуал.

Это лучше, чем одна система на 300 строк.

### 4. Если нужен prefab reference, сделай registrator

Например:

- `ShootPointEntityRegistrator`
- `BodyColliderRegistrator`
- `TransformEntityRegistrator`

Логика:

- prefab хранит ссылку;
- registrator перекладывает её в `Entity`;
- системы потом работают уже только через `Entity`.

### 5. Если нужен visual response, сделай `EntityView`

View:

- подписывается на `ReactiveVariable` или `ReactiveEvent`;
- обновляет анимацию, VFX, UI marker;
- не меняет core gameplay state.

### 6. Собери условия отдельным объектом

Не вшивай сложные проверки в каждую систему повторно.

Правильно:

```csharp
ICompositeCondition canStartDash = new CompositeCondition()
    .Add(new FuncCondition(() => entity.IsDead.Value == false))
    .Add(new FuncCondition(() => entity.InDashProcess.Value == false));
```

### 7. Подключи всё в фабрике

Именно фабрика в этом проекте определяет "архетип" сущности.

То есть фабрика решает:

- какие компоненты есть;
- какие условия есть;
- какие системы есть;
- какой brain подключается;
- в какой context сущность попадает.

### 8. Обнови generated API

После добавления новых `IEntityComponent` нужно сгенерировать API для `Entity`.

Смотри:

- `Assets/_Project/Develop/Editor/EntityAPIGenerator.cs`
- menu item: `Tools/GenerateEntityAPI`
- output: `Gameplay/EntitiesCore/Generated/EntityApi.cs`

После этого появятся удобные методы:

- `AddMyComponent(...)`
- `TryGetMyComponent(...)`
- `entity.MyComponent`

### 9. Если фичей должен управлять AI или input, меняй brain, а не систему движения/атаки

Правильная идея:

- brain пишет в `MoveDirection`;
- brain вызывает `StartAttackRequest`;
- brain выставляет teleport target;
- а сами системы уже обрабатывают это.

---

## Правила организации кода

### 1. `MonoBehaviour` не должен быть центром gameplay-логики

В этом проекте `MonoBehaviour` в gameplay-слое чаще всего делает одно из трёх:

- регистрирует ссылку в `Entity`;
- отображает состояние;
- играет роль scene bootstrap.

Если `MonoBehaviour` начинает сам двигать, атаковать, убивать, считать кулдауны и рулить AI, это уже отход от стиля проекта.

### 2. Компоненты должны быть маленькими

Нормально:

- один `Value`;
- одна ссылка;
- один буфер;
- одно событие;
- одно условие.

Плохо:

- компонент с десятью несвязанными полями;
- компонент, который знает слишком много о нескольких features сразу.

### 3. Системы должны кэшировать ссылки в `OnInit`

Это текущий стандарт проекта.

Система в `OnInit` вытаскивает всё нужное из `Entity`, а потом работает уже с кэшированными полями.

Это делает код:

- проще;
- быстрее;
- чище;
- понятнее по зависимостям.

### 4. Условия должны быть декларативными

Хорошо, когда можно прочитать:

- `CanMove`
- `CanRotate`
- `CanApplyDamage`
- `MustDie`
- `MustSelfRelease`

и сразу понять правила сущности.

### 5. Requests и events важнее прямых вызовов

Если одно действие должно запускать несколько реакций, лучше использовать событие или request-компонент, чем прямой вызов одного конкретного метода.

### 6. Фабрика является местом сборки поведения

Если нужно понять, "как устроен герой", первым делом открывай фабрику героя и базовую `EntitiesFactory`.

Именно там видно:

- состав сущности;
- активные системы;
- условия;
- связи со stat/attack/life-cycle features.

### 7. Brain управляет намерением, а не механикой

`PlayerInputMovementState` пишет направление.

`AttackTriggerState` отправляет запрос на атаку.

`FindTargetState` меняет текущую цель.

Это хороший стиль. Его стоит сохранять.

### 8. View только отображает

`WalkingView`, `ApplyDamageView`, `CurrentTargetView` показывают состояние, но не должны быть местом бизнес-логики.

---

## Что здесь считается ошибкой

### Ошибка 1. Делать "god object" на `MonoBehaviour`

Например один `HeroController`, который:

- читает input;
- двигает rigidbody;
- считает кулдаун;
- бьёт врагов;
- спавнит projectile;
- играет анимации;
- переключает UI.

Такой код противоречит фактической архитектуре проекта.

### Ошибка 2. Пихать scene-зависимости напрямую в системы через `GetComponent`

Системы должны брать данные из `Entity`, а не искать Unity-компоненты сами.

Если системе нужен `Rigidbody`, `Transform`, `ShootPoint`, `CapsuleCollider`, это должно прийти через registrator и компонент.

### Ошибка 3. Смешивать AI и механику

AI не должен сам уменьшать HP или телепортировать rigidbody напрямую.

AI должен:

- выбрать цель;
- отправить команду;
- задать направление.

А уже systems делают механику.

### Ошибка 4. Ломать локальность фичи

Если feature `Attack` начинает массово править код в `Movement`, `LifeCycle`, `UI`, `Input`, `AI` без ясной причины, значит фича плохо декомпозирована.

### Ошибка 5. Дублировать условия в нескольких местах

Если правило "нельзя атаковать во время движения" нужно в нескольких частях кода, оно должно жить в `CanStartAttack` или родственном condition object, а не быть скопированным по проекту.

### Ошибка 6. Менять состояние мимо реактивного слоя

Если в проекте уже принято работать через `ReactiveVariable` и `ReactiveEvent`, не нужно параллельно вводить второй скрытый источник правды.

---

## Быстрый чеклист перед коммитом

```text
□ Новая фича лежит в своей папке внутри Gameplay/Features?
□ Компоненты маленькие и атомарные?
□ Системы разделены по ответственности?
□ Unity-ссылки приходят через registrator, а не через GetComponent в системе?
□ Rules вынесены в Can*/Must* condition?
□ Команды идут через Request/Event, а не через хаотичные прямые вызовы?
□ Visual/animation/VFX живут в EntityView, а не в core system?
□ Сущность собирается фабрикой, а не случайным кодом по сцене?
□ Если добавлен новый IEntityComponent, обновлён Entity API?
□ Brain управляет намерением, а не механикой?
```

---

## Итог

Если совсем коротко, этот проект стоит воспринимать так:

```text
Не "классический ECS"
Не "обычный MonoBehaviour-ооп-суп"
А "Entity-centric reactive composition"
```

Главные идеи:

1. `Entity` хранит состояние и список локальных систем.
2. `Factory` собирает сущность как законченный gameplay-архетип.
3. `Brain` принимает решения и пишет команды.
4. `System` реализует механику.
5. `View` показывает результат.
6. `Condition` хранит правила.
7. `ReactiveVariable` и `ReactiveEvent` склеивают всё вместе.

Если придерживаться именно этого мышления, новый код будет выглядеть как естественное продолжение существующего проекта, а не как чужой архитектурный стиль, случайно попавший в репозиторий.

---

**Версия:** 1.0  
**Дата:** 2026-04-19  
**Проект:** CourseGameVadim
