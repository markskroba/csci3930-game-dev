# Final Project - Dungeon Delver

## Part 1: Enemy AI

Enemy AI is implemented mostly using the second example document, there is a `FSMState` class that contains all the different functions that define enemy behavior and that are switched on the `Update` based on the value that enum with all different states takes. A big difference in that implementation is how the movement is done - in both documents `NavMesh` was generated to allow pathfinding. However, since Dungeon Delver TileMaps are generated on the runtime, `NavMesh` could not be used - at least I did not find a way to generate it. Therefore, movement was implement more traditionally, similarly to how we did it in previous class and homework assignment - through vectors, `transform.position`, and `rb.velocity` changes. It seems to work fine at the end, even though snapping to grid no longer happens, but it seems it would not work with `NavMesh` well.

## Part 2: Additional changes

### Adding new pickup items

`DamageBoost`, `SpeedBoost` prefabs can be picked up like `Key` and `Health` ones, they provide damage/speed buffs to Dray for some limited time. `RangedAttackBoost` allows Dray to send 5 projectiles that damage enemies.

### Adding throwing/shooting capability to player

Mentioned above `RangedAttackBoost` is a pickup item that will give Dray ability to throw smaller pink-colored swords saved as `SwordProjectile` prefab. Dray gains 5 charges each time they pick up the item, each throw costs one charge. Only one sword can be thrown at a time, they have a relatively low range and disappear after some time or on contact with something, like enemies or walls. It deals 1 damage to enemies and has a knockback.

### Adding new enemy types

`Spiker` enemy was added using the existing art sprites. Its script is different from the that was provided in the initial package, and so its behavior. It still uses `Enemy` class, but it does not move and fires projectiles that are smaller versions of itself toward the player when they enter their sight range. It can be knocked back, its projectiles work similarly to those that Dray throws with `RangedAttackBoost` but can be blocked by Dray's sword.

### Adding a better UI menu

Instead of adding some new menu (I could not figure out why canvas won't include new objects even if they are its children), I handled how the game ends. When Dray's HPs fall to 0 the scene will freze and the existing panel on the right displaying their health and collected keys will display a text saying "Game Over, Game will Restart in 5 Seconds", and after 5 seconds scene will restart. This mostly involved changing the existing panel prefab, as well as `GuiPanel.cs` that is attached to it.
