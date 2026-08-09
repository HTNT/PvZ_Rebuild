# PVZ_MVS Project Context - Comprehensive Documentation

## 1. PROJECT OVERVIEW

### Mục tiêu
Clone Plants vs. Zombies bằng Unity theo hướng OOP, SOLID và kiến trúc mở rộng, dễ thêm plant/zombie types mới.

### Công nghệ Stack
- **Engine**: Unity (2D URP)
- **Language**: C# 
- **Architecture Pattern**: OOP + SOLID
- **Asset System**: ScriptableObject for data serialization
- **Grid System**: Custom 5×9 grid for lane-based positioning

### Game Concept
- 5 lanes (rows) × 9 columns
- Plants defend against zombies moving from right to left
- ShooterPlants attack zombies in their lane
- Grid-based cell positioning system
- 1 cell = 1m² (configurable in GridManager)

---

## 2. PROJECT STRUCTURE

```
d:\UNITY\PVZ_MVS\
├── Assets/
│   ├── Scripts/
│   │   ├── Grid/
│   │   │   ├── Cell.cs ✅ COMPLETE
│   │   │   ├── Grid.cs ✅ COMPLETE
│   │   │   └── GridManager.cs ✅ COMPLETE (MonoBehaviour)
│   │   ├── Plants/
│   │   │   ├── Plant.cs ✅ COMPLETE (Abstract base)
│   │   │   └── ShooterPlant.cs 🔄 IN PROGRESS (Skeleton)
│   │   ├── Data/
│   │   │   ├── PlanData.cs ✅ COMPLETE (Abstract ScriptableObject)
│   │   │   └── ShooterPlantData.cs ✅ COMPLETE (Abstract ScriptableObject)
│   │   ├── Zombies/ 📁 EMPTY (Not started)
│   │   ├── Projectiles/ 📁 EMPTY (Not started)
│   │   ├── Managers/ 📁 EMPTY (Not started)
│   │   ├── Gameplay/ 📁 EMPTY (Not started)
│   │   ├── Interfaces/ 📁 EMPTY (Not started)
│   │   └── Utilities/ 📁 EMPTY (Not started)
│   ├── ScriptableObjects/
│   │   ├── PlantData/
│   │   │   ├── PeaShooter.cs (in Assets/ScriptableObjects/PlantData/)
│   │   │   └── PeashooterData.asset ✅ CREATED
│   │   ├── ZombieData/ 📁 EMPTY
│   │   └── LevelData/ 📁 EMPTY
│   ├── Prefabs/
│   │   ├── Plants/ 📁 EMPTY
│   │   ├── Zombies/ 📁 EMPTY
│   │   ├── Projectiles/ 📁 EMPTY
│   │   ├── UI/ 📁 EMPTY
│   │   └── Grid/ 📁 EMPTY
│   ├── Arts/ 📁 EMPTY (No sprites yet)
│   ├── Audio/ 📁 EMPTY (No sound)
│   ├── Animations/ 📁 EMPTY (No animation clips)
│   ├── Scenes/
│   │   ├── Bootstrap.unity (Entry point)
│   │   ├── GamePlay.unity (Main gameplay)
│   │   ├── MainMenu.unity (Menu system)
│   │   └── SampleScene.unity (Template)
│   └── Settings/
│       ├── InputSystem_Actions.inputactions
│       ├── UniversalRP.asset
│       ├── Renderer2D.asset
│       └── DefaultVolumeProfile.asset
├── .gitignore ✅ CREATED
└── PVZ_MVS_Project_Context.md (This file)
```

---

## 3. NAMESPACE CONVENTIONS

```
PVZ_MVS.Scripts.Grid          → Grid system classes
PVZ_MVS.Scripts.Plants        → Plant gameplay classes  
PVZ_MVS.Scripts.Data          → ScriptableObject data configs
PVZ_MVS.Scripts.Zombies       → Zombie gameplay classes (TODO)
PVZ_MVS.Scripts.Projectiles   → Projectile system (TODO)
PVZ_MVS.Scripts.Managers      → Game/Resource managers (TODO)
PVZ_MVS.Scripts.Interfaces    → Abstract interfaces (TODO)
PVZ_MVS.Scripts.Utilities     → Helper functions (TODO)
```

**Rule**: Never mix gameplay code and data in same namespace.

---

## 4. IMPLEMENTED SYSTEMS (✅ Complete)

### 4.1 Grid System

#### Cell.cs
```
- Public Properties:
  • int Row { get; }
  • int Column { get; }
  • Vector3 WorldPosition { get; }
- Constructor: Cell(int row, int column, Vector3 worldPosition)
- Purpose: Immutable data container for grid cell
```

#### Grid.cs (Non-MonoBehaviour)
```
- Public Properties:
  • int Rows { get; }
  • int Columns { get; }
  • float CellSize { get; }
  • Vector3 Origin { get; }
- Public Methods:
  • Cell GetCell(int row, int column)
  • Vector2Int GetCellPosition(Vector3 worldPosition)
  • bool IsValidCell(int row, int column)
  • Vector3 GetWorldPosition(int row, int column)
- Constructor: Grid(int rows, int columns, float cellSize, Vector3 origin)
- Grid Setup: 5 rows × 9 columns, 1f cell size
- World Positions: Calculated with center of cells (0.5f offset)
```

#### GridManager.cs (MonoBehaviour)
```
- Initializes Grid (5×9, 1f cellSize)
- Handles mouse input for cell selection
- Gizmos visualization for debug (Gizmos.DrawCube for each cell)
- Logs cell info when clicked
```

### 4.2 Plant System

#### Plant.cs (Abstract MonoBehaviour)
```
- Fields:
  • [SerializeField] PlantData _data
  • private int _currentHp
  
- Properties:
  • PlantData Data { get; }
  • int CurrentHp { get; }
  
- Virtual Methods:
  • void Initialize()
    → Assigns _currentHp = _data.MaxHp
    → Validates _data is not null
    
  • void TakeDamage(int damage)
    → Reduces _currentHp by damage amount
    → Calls Die() if _currentHp <= 0
    
  • void Die()
    → Destroys gameObject
    
- Design Pattern: Template Method (subclasses override Initialize/TakeDamage/Die)
```

#### ShooterPlant.cs (Abstract, extends Plant)
```
- Fields:
  • float _attackTimer (initialized but not used)
  • [COMMENTED] Zombie _currentTarget
  
- Property:
  • ShooterPlantData ShooterData => (ShooterPlantData)Data
    → Direct cast from parent Plant.Data
    
- Virtual Methods (EMPTY SKELETON - NO IMPLEMENTATION):
  • void Update()
  • void FindTarget()
  • void HandleAttack()
  
- Abstract Method:
  • void Shoot() (must be implemented by subclasses like Peashooter)
  
- Status: ALL METHODS ARE EMPTY - LOGIC NOT YET IMPLEMENTED
```

### 4.3 Data System

#### PlantData.cs (Abstract ScriptableObject)
```
- Serializable Fields:
  • string _plantName
  • int _cost
  • int _maxHp
  • Sprite _icon
  • GameObject _prefab (plant prefab to instantiate)
  • string _description (TextArea)
  
- Read-only Properties (all public getters):
  • string PlantName
  • int Cost
  • int MaxHp
  • Sprite Icon
  • GameObject Prefab
  • string Description
  
- Purpose: Base configuration for all plant types
- Extensibility: Subclasses add specific mechanics data
```

#### ShooterPlantData.cs (Abstract, extends PlantData)
```
- Serializable Fields:
  • int _damage
  • float _attackRange
  • float _attackCooldown
  • GameObject _projectilePrefab
  
- Read-only Properties:
  • int Damage
  • float AttackRange
  • float AttackCooldown
  • GameObject ProjectilePrefab
  
- Purpose: Config for all shooting-based plants
- Inheritance: Inherits _plantName, _cost, _maxHp, _icon, _prefab, _description from PlantData
```

#### PeaShooter.cs (Concrete ScriptableObject Definition)
```
- File Location: Assets/ScriptableObjects/PlantData/PeaShooter.cs
- Inherits: ShooterPlantData
- Attributes:
  • [CreateAssetMenu(menuName = "PVZ/Plants/PeaShooter", fileName = "PeashooterData")]
- Purpose: Factory for creating PeashooterData.asset in Inspector
- Asset Created: PeashooterData.asset (in Assets/ScriptableObjects/PlantData/)
```

---

## 5. IN PROGRESS (🔄 Sprint 5)

### ShooterPlant Implementation

**Status**: Skeleton created, NO LOGIC IMPLEMENTED

**What's Needed**:

1. **Update() method**
   - Call FindTarget() every frame
   - Call HandleAttack() if _attackTimer is ready
   - Decrease _attackTimer over time

2. **FindTarget() method**
   - Query ZombieManager for zombies in this plant's lane
   - Find the closest zombie within _attackRange
   - Set _currentTarget (currently commented out)

3. **HandleAttack() method**
   - Reset _attackTimer to ShooterData.AttackCooldown
   - Call Shoot() to spawn projectile

4. **Shoot() abstract method**
   - To be implemented by Peashooter subclass
   - Should instantiate projectile at plant position
   - Calculate direction toward _currentTarget

**Blocking Issues**:
- ZombieManager doesn't exist yet (needed for FindTarget)
- Projectile system not implemented yet
- Need to spawn projectiles and track them

---

## 6. NOT STARTED (❌ TODO)

### 6.1 Zombie System
```
Missing:
- Zombie.cs (abstract base class)
  • Health system
  • Movement along lane
  • Damage to plants
  • Death animation
  
- BasicZombie.cs (concrete type)
  • ZombieData reference
  
- ZombieData.cs (ScriptableObject)
  • Speed, health, damage, sprite
  
- ZombieManager.cs
  • Track zombies per lane
  • Query zombies by lane/position
  • Spawn zombie waves
```

### 6.2 Projectile System
```
Missing:
- Projectile.cs
  • Velocity-based movement
  • Collision detection with zombies
  • Lifetime (destroy after time)
  • Damage on hit
  • Visual (sprite/mesh)
  
- Different projectile types (pea, etc.)
```

### 6.3 Game Manager
```
Missing:
- GameManager.cs
  • Wave/level progression
  • Player resource management (sun)
  • Plant placement system
  • Win/lose conditions
  • Pause/resume
  
- UIManager.cs
  • HUD (health, sun counter, waves)
  • Plant selection UI
  
- LevelManager.cs
  • Wave definitions
  • Zombie spawn patterns
```

### 6.4 Input & Gameplay
```
Missing:
- InputManager abstraction
- Plant placement interaction
- Touch/mouse input handling
- UI input handling
```

---

## 7. KEY CODE DETAILS

### Grid Configuration (GridManager.cs)
```csharp
private Grid _grid;

private void Awake() {
    _grid = new Grid(
        rows: 5,
        columns: 9,
        cellSize: 1f,
        origin: Vector3.zero
    );
}
```

### Plant.Initialize() Flow
```csharp
public virtual void Initialize(){
    if(_data == null){
        Debug.LogError($"{name} chua duoc gan plantdata.");
        return;
    }
    _currentHp = _data.MaxHp;
}
```

### Data Access in ShooterPlant
```csharp
protected ShooterPlantData ShooterData => (ShooterPlantData)Data;

// Usage:
float range = ShooterData.AttackRange;
int damage = ShooterData.Damage;
float cooldown = ShooterData.AttackCooldown;
```

---

## 8. ARCHITECTURE DECISIONS

### Why ScriptableObject for Data?
- Easy to configure in Inspector
- No code change needed for new plant/zombie values
- Serialization by Unity
- Reusable across scenes

### Why Abstract Classes?
- Plant.cs, ShooterPlant.cs - force subclasses to implement specific logic
- PlantData.cs, ShooterPlantData.cs - provide common interface for data

### Why Non-MonoBehaviour Grid?
- Pure logic, no Unity dependencies
- Easy to test (unit tests)
- Can be instantiated multiple times if needed
- GridManager is MonoBehaviour wrapper

### Lane-Based System
- Zombies move in lanes (rows)
- ShooterPlants target zombies in same lane
- Simplifies pathfinding (no complex A*)

---

## 9. CURRENT LIMITATIONS & KNOWNS

### Code Quality
- ✅ Good namespace organization
- ✅ Proper use of abstract classes
- ✅ ScriptableObject pattern applied
- ❌ No interface definitions (Interfaces/ folder empty)
- ❌ No dependency injection
- ❌ No event system for communication between systems
- ❌ Magic numbers in GridManager (5, 9, 1f hardcoded)

### Missing Infrastructure
- ❌ No Logger utility
- ❌ No Object pooling system
- ❌ No Configuration system (for magic numbers)
- ❌ No Service Locator / Dependency Injection container
- ❌ No Unit tests

### Art/Assets
- ❌ No sprites (Plants, Zombies, Projectiles)
- ❌ No animations
- ❌ No audio (SFX, music)
- ❌ No particle effects

---

## 10. RECOMMENDED SPRINT ROADMAP

### Sprint 5 (Current) - ShooterPlant
1. Implement ShooterPlant.Update() → FindTarget() → HandleAttack() → Shoot()
2. Create Peashooter.cs with Shoot() implementation
3. Verify ShooterPlant fires when updated manually

### Sprint 6 - Zombie System
1. Create Zombie.cs base class
2. Create BasicZombie concrete type
3. Create ZombieData ScriptableObject
4. Create ZombieManager for lane management
5. Create zombie prefab and wire it up

### Sprint 7 - Projectiles
1. Create Projectile.cs with velocity/lifetime
2. Implement collision detection with zombies
3. Create Pea projectile prefab
4. Test ShooterPlant → Zombie damage flow

### Sprint 8 - Game Manager & Waves
1. Create GameManager for coordination
2. Create LevelManager for wave definitions
3. Implement wave spawning
4. Wire up win/lose conditions

### Sprint 9 - UI & Polish
1. Create UIManager
2. Build HUD (sun counter, wave display)
3. Plant selection UI
4. Audio system

---

## 11. KNOWN ISSUES & BUGS

1. **ShooterPlant._currentTarget commented out**
   - Will need uncommented when Zombie.cs is created
   - Reference: `// private Zombie _currentTarget;`

2. **Missing ZombieManager dependency**
   - ShooterPlant.FindTarget() has no way to query zombies
   - Blocked implementation of FindTarget()

3. **No projectile instantiation**
   - ShooterPlant.Shoot() cannot be implemented until Projectile system exists
   - ShooterPlantData._projectilePrefab not used yet

4. **Grid is hardcoded**
   - GridManager has magic numbers (5, 9, 1f)
   - Should be configurable via Scriptable Object or config file

---

## 12. DEVELOPMENT GUIDELINES

### Coding Standards
- Use PascalCase for public properties/methods
- Use _camelCase for private fields
- Always use `=>` for simple property getters
- Abstract methods in abstract classes force implementation
- Virtual methods allow override flexibility

### OOP Principles Applied
- **Inheritance**: Plant → ShooterPlant → Peashooter
- **Polymorphism**: TakeDamage can be overridden, virtual Update
- **Encapsulation**: Private fields with public read-only properties
- **Abstraction**: Plant.cs, PlantData.cs enforce interface contracts

### Testing Approach (Recommended for Next Session)
```csharp
// Test Grid positioning
var grid = new Grid(5, 9, 1f, Vector3.zero);
var cell = grid.GetCell(2, 4);
Assert.AreEqual(cell.WorldPosition, new Vector3(4.5f, 2.5f, 0));

// Test Plant damage
var plant = new Peashooter();
plant.Initialize();
plant.TakeDamage(10);
Assert.AreEqual(plant.CurrentHp, plant.Data.MaxHp - 10);
```

---

## 13. FILE LOCATIONS REFERENCE

| Component | File Path |
|-----------|-----------|
| Grid System | Assets/Scripts/Grid/*.cs |
| Plant System | Assets/Scripts/Plants/*.cs |
| Data System | Assets/Scripts/Data/*.cs |
| PeaShooter Data | Assets/Scripts/ScriptableObjects/PlantData/PeaShooter.cs |
| PeaShooter Asset | Assets/ScriptableObjects/PlantData/PeashooterData.asset |
| Scene - Bootstrap | Assets/Scenes/Bootstrap.unity |
| Scene - GamePlay | Assets/Scenes/GamePlay.unity |
| Scene - MainMenu | Assets/Scenes/MainMenu.unity |

---

## 14. NEXT SESSION ENTRY POINTS

**If continuing ShooterPlant implementation:**
- Read this file sections 5, 10, 11
- Open ShooterPlant.cs and implement Update/FindTarget/HandleAttack skeleton
- Create ZombieManager stub to unblock FindTarget()

**If starting fresh:**
- Read sections 1-4 for context and completed work
- Review section 10 for recommended sprint
- Check section 12 for coding standards

**If creating Zombie System next:**
- Review section 6.1 for requirements
- Check section 8 for architecture decisions
- Follow same pattern as Plant system: Zombie.cs → ZombieData.cs → ZombieManager.cs

---

## 15. QUICK REFERENCE - CURRENT STATE

```
Lines of Code:
- Grid.cs: ~50 lines (functional)
- Plant.cs: ~30 lines (functional)
- ShooterPlant.cs: ~30 lines (skeleton only)
- Data files: ~50 lines total (functional)

Total Implemented: ~160 lines of actual logic
Estimated Game Completeness: ~25%

Critical Path: ShooterPlant → Zombie → Projectile → GameManager
Blocker: ZombieManager needed for ShooterPlant.FindTarget()
```

---

**Last Updated**: August 6, 2026  
**Session Type**: Continuing from ChatGPT session  
**Sprint**: Sprint 5 - ShooterPlant Implementation
