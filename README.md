**The University of Melbourne**

# COMP30019 – Graphics and Interaction

## Teamwork plan/summary

<!-- [[StartTeamworkPlan]] PLEASE LEAVE THIS LINE UNTOUCHED -->

<!-- Fill this section by Milestone 1 (see specification for details) -->

### Roles:

- **Andy Low** - Development Manager: Deals with project organization, developing a timeline and making sure the team is on track. Gameplay Developer: Deals with gameplay elements such as enemy wave spawning, difficulty as time progresses in-game, different towers and weapons, aiming mechanics and explosion particle effects.
- **Bowen Fan** - Gameplay Developer: Deals with gameplay elements mainly the shop. Handles logic like game progression and upgrades, user input and controls. Sound Designer: Maintains the in-game audio manager and code for playing sounds and music. UI designer: implements in game menu and ui and other user interactions.
- **Xi Chen** - Game Designer: Visual design, making sure the game looks appealing. Works with shaders, supported with documentation and querying and observational methods. Edited trailer video.
- **Yu Cao** - System Developer: Deals with map generation. Development Management: Writes the report and evaluation such as querying and observational techniques and documents improvements to be made to the game.

<!-- [[EndTeamworkPlan]] PLEASE LEAVE THIS LINE UNTOUCHED -->

## Final report

Link to Trailer Video - [here](https://www.youtube.com/watch?v=bPIBJF3TSLo)

Remember that _"this document"_ should be `well written` and formatted **appropriately**.
Below are examples of markdown features available on GitHub that might be useful in your report.
For more details you can find a guide [here](https://docs.github.com/en/github/writing-on-github).

### Table of contents

- [Game Summary](#game-summary)
- [How to play](#how-to-play)
- [Design](#design)
- [Evaluation](#evaluation)
- [Technologies](#technologies)
- [References](#references)
- [Extra GIFs](#extra-gifs)

### Game Summary

The Tower is a tower-defense game that begins with the player controlling a tower in the center of the map. Enemies will spawn around the tower and try to walk towards the center to attack the player. The players' goal is to survive as long as you can under the attacks of the enemies as each wave more enemies are spawned.

<p align="left">
  <img src="Gifs/Gameplay.gif" width="500">
</p>

At the start of each game, a random map will be procedurally generated including many terrain properties such as obstacles including lakes, trees, rocks etc. The player attacks by clicking with the mouse to fire attacking projectiles to kill the enemies. With every round progression money will be generated. The enemies will be generated in waves with a short countdown time in between. 

<p align="left">
  <img src="Gifs/ProcGen.gif" width="500">
</p>

There will be a shop system in the game. The battle shop is accessible anytime during the game. In the battle shop, the player is able to buy quick buffs that increase the stats of all towers in game. They can also buy more towers to aid in destroying enemies

### How to play

- A and D to rotate camera
- Mouse to aim The Tower
- LMB to Fire from The Tower
- T to Launch Mortar
- RMB to zoom in

## Design

### Gameplay Related Design

#### Low poly design

- With sharp edges for a clean and modern look.
- Less demanding on the computer allowing smoother gameplay and better user experience.
- Gives a Retro feel to The Tower similar to nostalgic early computer games.

### Graphics Pipeline (Shaders)

#### Tower Shader

- Location: Assets > Shaders > TowerShader.shader
- The tower shader simulates a fluid texture for the towers, which indicates the water elements that confronts the enemies with fire.
- It contains 2 inputs. MainTex is the texture of the fluid, and MainCol is the color of the fluid. Both attributes can be customized to improve adaptability.
- In the vertex shader, the vertex coordinates of towers are calculated using sin and cos functions to create an oscillating effect to make them appear like jelly.
- In the fragment shader, the texture is printed based on the time to also create a fluid effect. The final output color is multiplied with a constant to make the final result brighter.

#### Enemy Shader

- Location: Assets > Shaders > EnemyShader.shader
- The enemy shader simulates a fire texture for the enemies of The Tower.
- It contains 5 inputs. NoiseTex is the texture of fire which can be customized. GradientTex controls the transparency of fire effects. With this the fire effect can fade out when it reaches the top of the model. The rest three properties are set to be the color of fire in different stages.
- In the fragment shader, the noiseValue is calculated based on time to provide a scrolling effect of the fire. The gradientValue helps on calculating the transparency based on the position of the pixels on the model.
- By subtracting fire sections with lower gradient from the entire flame, the fire can be divided into three sections stored in the flame variables. The different colors of the fire can then be assigned based on their gradient in the fire, representing a natural burning effect.

### Procedural Generation

We used Perlin noise as the procedural generation technique for our terrain generation.

We firstly intended to generate the basic Perlin noise and to see how it looks like, so we created a RawImage object and wrote the Perlin noise function, which is a built-in function called Mathf.PerlinNoise(), and connected the image to show the Perlin noise 2D heightmap.

Then the next step is to generate our terrain by using the Perlin noise map to have different heights. We generated a fitted terrain under the 3D Object section and made it connected with our NoiseManager class to match our 2D height map textures. As we found that the generated noise can be affected by the scale parameter and seeding parameter, we used the Immediate Mode GUI in unity, which is referred to by OnGUI() built-in function, to build two scale controllers at the left top of our heightmap.

After doing this, we can easily generate our 3D terrain to be flat or rugged by changing the scale and seeding parameters as we connect terrain with our main scene. Another advantage of using 3D object terrain is that we can randomly place trees, grasses, rocks by choosing the scope and amount of them.

In conclusion, our map is created by Perlin noise generated terrain, and that’s the procedural generation technique we used in this project.

### Particle System

#### Explosion particle system

- Location: Assets > Prefabs > Explosion.prefab
- The particle system is split into 3 layers for more depth, namely the smoke, explosion fire and sparks layers.
- Particles spray randomly upwards towards the sky in a hemisphere rather than sphere shape to reduce resources required to load the effect since particles emitted towards the ground cannot be seen by the player.
- Random smoke rotation with time to give smoke more realistic volume and 3D appearance. Random size of particles between 2 constants were chosen for the explosion fire and smoke to further simulate the randomness of explosions in reality.

## Evaluation

### Querying and Observation Methods

- The querying technique used in this project is doing interview with several fixed questions. 
- The observational methods are 'think aloud' and 'cooperative evaluation'. 

- For both quering and observation, we have invited 5 students with different majors to provide their opinions.
We have developed questions for interviews and they are listed below, including both closed and open questions.
1. Do you think it's easy to play? (Yes or No)
2. Does every element in the game can be easily differentiate? (Yes or No)
3. Do you think you need instructions? (Yes or No)
4. Do you think the shop system is good? (Yes or No)
5. Do you think this game is hard? (Rate from 1 to 10)
6. How was our overall looking? (Rate from 1 to 10)
7. List one thing that you like the most?
8. List one thing that you dislike the most?

- We let our participants firstly did these questions, and we collect their response to analyse.
We found that every participants answered 'Yes' for question 1 and 3, which means although our game is quite easy to play, we still need to add instruction page to teach them. 
1 out of 5 chose 'No' for question 2, so most of them thought our game elements are distinguishable.
It's same for question 4 so that we still need some improvements for our shop system.

For the scalar questions, the average rating of question 5 is 2.8, which shows that our game is pretty simple and easy to play, so maybe we need to make it more challenging. 
We have received the average rating of 7.4 for question 6, and we can predict that most of them gave 7 or 8 scores for our game design, which is an encouragement for us. 

In the part of the open questions, we noticed that there are some overlap answers, and we concluded the most 2 frequent answers from them. 
We have 'good game design style' and 'easy to play' for question 7.
We have 'no instructions' and 'simple and boring for shooting enemies' for question 8.


- We chose to use both 'think aloud' and 'cooperative evaluation' for our observation to discover some interesting differences from our participants, so we break them into two groups to use different methods. 

- As two of them used 'think aloud', we were helping them when they playing the game and giving some questions.
We have discussed about how we can improve the game more, and they have mentioned that we can have more sections like notifications to remind the player. They also made compliments about the zooming function which is helpful when aiming and also visually appealing. One of the participant also noticed that the enemies hidden behind trees and rocks cannot be attacked by normal weapons. This reminds us to make instructions about using bombs. For our shop system, one of them stated that we should add the $cash amount to notify players.

- As rest of them used 'cooperative evaluation', we planned to have discussions after they experienced our game to have more general opinions. They told us we'd better to have some health function of our enemies and create waves notification. They also mentioned that the game is repeating itself which makes it less attractive during later stages, and they all forgot to rotate the camera when the enemies were spawned behind them.

- We found that the same thing they suggested us is to add notifications in our game, and we need to improve our enemies with more gaming fucntions. 


### Post-Evaluation Changes Documentation

- As we finished the interviews from our participants, we decided to focus on how to improve the difficulty of this game and add the instructions for our players. 

- We set up different waves of enemies to have several degrees of difficulties, and we added one instruction page to show how this game works and how to control the towers. 
- Also the explosion design is not quite match with our overall game looking, so we changed explosions to a boxy look to promote consistency in the game’s design. 


- As we finished the observations, we have a clear idea of what to improve. 
- We added Health bar to indicate health status of enemies and towers and a timer to notify player of time remaining till next round, which can increases tension and preparedness.
- We also added giant enemies that are significantly more threatening and powerful to make the game more exciting for the players.
- We brought the $cash amount outside into the game scene to notify players of how many money they have without the need of entering the shop.

### Technologies

Project is created with:

- Unity 2022.1.9f1
- Simple Nature Pack from Unity Asset Store for obstacles places around the world
- Music from opengameart.org
- In-game sounds from opengameart.org and freesound.org

### References

#### Code snippets adapted from youtube tutorials

- [Unity top Down Shooter playlist](https://www.youtube.com/playlist?list=PLiyfvmtjWC_XBKJVuCtMXrkNnMDNB16W9)
- [Tower defense game tutorial](https://www.youtube.com/playlist?list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0)
- [Using noise as procedural generation](https://medium.com/geekculture/how-to-use-math-noises-for-procedural-generation-in-unity-c-44902a21d8e)
- [Make procedural land map](https://medium.com/@liux4989/make-procedural-landmass-map-in-unity-e874113bf693)
- [Unity main menu tutorial](https://www.youtube.com/watch?v=FfaG9TvCe5g)
- [Unity shop system tutorial](https://www.youtube.com/watch?v=Oie-G5xuQNA)
- [In game music by nemansymphony](https://opengameart.org/content/the-fall)
- [Shader tutorial](https://www.youtube.com/watch?v=SPKDjHkLnY4)
- [Fire shader tutorial](https://www.febucci.com/2019/05/fire-shader/)
- [Font - Cozette Vector](https://github.com/slavfox/Cozette)

### Extra GIFs

<p align="center">
  <img src="Gifs/TowerExplosion.gif" width="500">
</p>

