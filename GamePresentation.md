The core gameplay of Pinlatro is essentially classic pinball, but with a small yet highly impactful twist!

<img src="Docimages/image-1.png" width="250" alt="Game Start">

Usually, the goal of a pinball game is to hit as high a score as possible before running out of lives. In Pinlatro, **time is your only limit**.

The player's sole objective is to reach a specific score quota within the given time limit. When a ball falls into the pit, it infinitely respawns at the jump pad. Naturally, the score quotas progressively increase with each round!

<img src="Docimages/image.png" width="150" alt="Game HUD">

While scoring points, players also earn money. After meeting each quota, this currency can be spent in the shop to upgrade their setup.

There are 3 main areas players can upgrade:
* **Apply specific upgrades (effects) to a ball** – these vary widely (more details below), and you can also merge pinballs together!
* **Buy additional balls** – up to 4 balls can be active on the map simultaneously (the reasoning behind this design choice is discussed later).
* **Modify the pinball map** (*Not implemented*).

<img src="Docimages/image-2.png" width="400" alt="Shop Interface">

### Upgrades System
Each pinball can hold up to 4 upgrades. With a maximum of 4 pinballs, players can manage up to 16 different upgrades simultaneously. Note that a single pinball cannot hold duplicate upgrades.

#### Examples of Upgrades:
* **Ring Chaser:** Automatically launches itself toward the closest point-scoring ring within a set cooldown.
* **Slider:** Continuously generates points every second it slides along a wall.
* **Portalball:** Creates active portals upon hitting walls, allowing other balls to pass through them to clear obstacles or chain combos.
* **Gambler:** Every point scored by this ball is randomized between 1 and 20. If it scores a 7 three times in a row, it rewards the player with a massive "777" score and money bonus.
* **Racer:** Awards points based on the ball's current velocity when colliding with walls.

* **Upgrade Merges:** Certain upgrades can be merged to produce a brand-new, significantly stronger effect. The only way to obtain these is by merging two separate pinballs, each carrying one of the required component upgrades.
This mechanic forces the player to strategize during selection, carefully balancing short-term gains against long-term merge potential. Currently, upgrades cannot be removed from a pinball once applied, though this feature may be considered in the future.

* **Slider + Racer Merge:** Generates a high volume of points while sliding along surfaces, with the payout heavily multiplied by the ball's speed.

<img src="Docimages/image-3.png" width="150" height="100" alt="Ball Upgrade 1"> <span>&nbsp;</span> <img src="Docimages/image-4.png" width="150" height="100" alt="Ball Upgrade 2">

**After merging:**

<img src="Docimages/image-6.png" width="150" alt="Merged Ball Asset"> <span>&nbsp;</span> <img src="Docimages/image-7.png" width="150" alt="Merged Ball Visual">

Of course, the map also features standard functional elements like bumpers, launchpads, and point-scoring objects, though these follow classic pinball mechanics.

### Pure Chaos: 4 Pinballs on the Pitch?
Yes, having 4 balls on the map simultaneously is undeniably chaotic, making precise flipper shots difficult. However, this design choice was deliberate: the most interesting upgrade interactions, merges, and synergies emerge when there are at least two or more balls active at the same time.

To mitigate this chaos, I conceptualized an **Auto-Flipper** feature (currently on the drawing board). These automated flippers would prevent balls from draining based on a customizable precision stat that players could upgrade. The player's role would then shift to a higher tactical level—simply controlling the general direction in which the flippers should aim, rather than worrying about split-second twitch reactions.

### Visuals & Shader Graph Challenges
A significant amount of development time was dedicated to achieving my ideal ball upgrade visuals using Shader Graph. My goal was to generate a unique, procedurally generated look for each combination of upgrades on a pinball. I wanted to avoid manual asset creation, as creating unique sprites for every single combination would quickly become unsustainable. 

While I achieved some functional results, none were fully satisfying. I plan to tackle this problem with fresh experience when I return to the project. For a deeper dive into the architecture and rendering technicalities, please refer to [TechnicalDocumentation.md](TechnicalDocumentation.md).