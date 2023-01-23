# unity3d-assets-lootShoot
Assets for a 3D video game in Unity.
## Game description
	The game currently has one scene where the player can move, look around and shoot weapons from a first person perspective. In order to use a weapon, the player needs to equip it. They can do that by opening inventory UI, generate new items, then drag an item from "Generated items" section and drop it to a slot in "Equipped" section. Aside from the items that are of type "Weapon", players an attach items of type "Mod" to equipped weapon.
	Items are procedurally generated. Every item has a type, name, icon, level, quality, and stats. Item level and quality can affect the values of item stats. Higher weapon level increases weapon damage and higher weapon quality increases the number of mods player can attach to a weapon. Mods don't have a level (Level = 0), but stat values of a mod increase with higher quality.
![Alt text](/Screenshots/looting.jpg "Looting")
	When a player equips any item, total stats of all items are calculated and shown in "Total stats" section. After that, the weapon in the player's hand is configured to work by description in "Total stats" section. The player can shoot and reload the weapon. When the player shoots an object marked as an enemy, damage to that enemy is calculated and shown as floating numbers on a screen.
![Alt text](/Screenshots/shooting.jpg "Shooting")