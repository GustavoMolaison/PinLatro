The basic structure is extremely simple in order to separate the aspect of creating new maps from the core mechanics.

<img src="Docimages/image-8.png" width="150" height="100" alt="Ball Upgrade 1"> 

- **Main Camera**: The camera in this project is quite basic; it only moves between the table and the shop based on player input.
- **Table-Testing**: These are GameObjects with colliders that make up the physical map.
- **Essentials**: This is a prefab. When placed in any map with a set pinball respawn point, the game is instantly ready to play. This ensures that future content development is extremely straightforward.

The Essentials prefab contains several key features, such as lighting, input handling, and the main canvas (which tracks score and time). However, these are just the basics; our main focus will be the GameManager.

<img src="Docimages/image-9.png" width="150" height="100" alt="Ball Upgrade 1">

All children of the GameManager communicate strictly with each other or directly with the GameManager itself.

<img src="Docimages/image-11.png" width="150" height="100" alt="Ball Upgrade 1">

I will discuss some of the most important components.

<img src="Docimages/image-12.png" width="250" height="250" alt="Ball Upgrade 1">

**PinballManager** ensures that the pinballs are in their correct positions and that there are never too many or too few of them in play.
If we want to add a new pinball, we use a specific function from this script, depending on where the ball originates.

```csharp
public void AddNewBallThroughShop()
{
    MoneySystem.Instance.takeMoney(MainShop.Instance.newSlotCost[allBalls.Count - 1]);

    GameObject newBallParent = Instantiate(ballPrefab, newBallSpawn.transform.position, Quaternion.identity);
    Ball newBall = newBallParent.GetComponent<Ball>();
        
    allBalls.Add(newBall);
    newBall.BallToWaitingRoom();
    
    newBall.upgradeHolderUI = upgradeHolderUIs[allBalls.Count - 1];
    newBall.ballStatue = ballStatues[allBalls.Count - 1];

    upgradeHolderUIs[allBalls.Count - 1].EnrolledBall = newBall;
    ballStatues[allBalls.Count - 1].EnrolledBall = newBall;

    NewSlotHelper.Instance.costTmp.text = $"Cost: {MainShop.Instance.newSlotCost[Instance.allBalls.Count - 1]}";
}
```

This function takes a prefab and spawns it at the designated spawn point. It then adds the newly created ball to the tracking lists. Every variable here can be modified directly in the Inspector.

The script worth looking into is the `Pinball` class itself. 
It is by far the longest and most important piece of code in the game.

If we want to implement upgrades, we need to track various states—such as its speed or whether it is in the air. All of this logic lands in this script. While that part is straightforward, what is actually interesting is how upgrades are handled here. There can be up to 4 of them active, and they alter the gameplay dynamically. 

The solution used here relies on C# Actions:

```csharp
ball_mechanics?.Invoke();
ball_mechanicsBallref?.Invoke(this);
```
Every upgrade function is subscribed to or unsubscribed from these two Action delegates. If an upgrade requires a reference to the pinball itself, we use ball_mechanicsBallref?.Invoke(this);

These delegates are invoked every frame to check if the specific conditions of the upgrades are met; if they are, the upgrade effects are applied. The conditions checked each frame are designed to be lightweight and computationally cheap.


Finally, I want to present the Upgrade architecture.

Each upgrade is defined by its own ScriptableObject.

<img src="Docimages/image-13.png" width="200" height="200" alt="Ball Upgrade 1">

It contains everything we need to define an upgrade. (The "Index in Sprite Map" is an experimental field and is not currently in use).

If we want to add a new upgrade, we simply append it to the Upgrade List within the upgrade system.

<img src="Docimages/image-14.png" width="300" height="300" alt="Ball Upgrade 1">

When we click a button in the shop, it simply calls this function from the upgrade system:

```csharp
public UpgradesSO GetUniformRandomUpgrade()
{
    if (upgrades.Count == 0) return null;
    return upgrades[UnityEngine.Random.Range(0, upgrades.Count)];
}
```
It assigns a random upgrade to a button—it's that simple!

Last but not least, I want to discuss Shader Graphs, which ohh gaved me some trouble.

 My goal was to make every upgrade sequence unique, while keeping the visual transitions nice and natural-looking based on the base color of each upgrade. That turned out to be a very difficult task. Mixing colors dynamically is not easy.


<img src="Docimages/image-16.png" width="700" height="500" alt="Ball Upgrade 1">


This is my testing Shader Graph. I experimented with a lot of techniques: Lerp interpolation, additive mixing, random-based addition, and subtraction. I even tried mixing around mid-tones. Nothing gave satisfactory results.

The best approach I found was dividing the sprites into two parts: background and symbol. When combining them, only the background of one and the sprite of the other would be added together. However, with more than one synergy, this also started to look bad. I could easily achieve nice colors by simply switching them based on specific variables, but then the new pinballs wouldn't reflect the visual vibe of the components they were made from.

I have a few new ideas, but I've learned the hard way that concepts which seem mathematically genius can fail completely in practice. I will share the results here once I test them.