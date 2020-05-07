This tool is also available as part of the [Sonic '06 Toolkit](https://github.com/HyperPolygon64/Sonic-06-Toolkit).

Very perliminary tool to convert a 3D Mesh to a collision.bin file for '06. This is VERY wonky (rotating the model by -90 degrees on the X axis), but it KINDA works & that's better than NOT having any way to generate collision at all.

The code is heavily tied into [LibGens](https://github.com/DarioSamo/libgens-sonicglvl) & is mostly based on Sajid's previous attempt to write this. So it can be a god damn nightmare to even get compiled.

For collision properties, add one or more of these to the end of your mesh name. For example, to have a metal wall without stickiness: add @10003 to the end of the mesh name.

0/4/7/B/C/D/F = Default (Stone/Concrete)

1 = Shallow Water

2 = Wood

3 = Metal

5 = Grass

6 = Sand

8 = Snow

9 = Dirt

A = Squeak Sound upon landing or braking (Glass?)

E = Echoey Metal

10000: Wall

40000: No stand? (not present on Nonami's list)

80000: Water

100000: Death

200000: Player Only/Wall Collision

4000000: Camera Collision

10000000: No stand unless already moving

20000000: Corners Damage (says Gamage on Nonami's list, assumed typo)

28000000: Damage

40000000: Water

60000000: Deadly Water

80000000: Climeable Wall
