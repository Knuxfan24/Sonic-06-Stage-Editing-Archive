This tool is also available as part of the [Sonic '06 Toolkit](https://github.com/HyperPolygon64/Sonic-06-Toolkit).

A preliminary tool to convert a 3D mesh (OBJ recommended) to a `collision.bin` file for Sonic '06. Requires the mesh to be Z-up.

The code is heavily tied into [LibGens](https://github.com/DarioSamo/libgens-sonicglvl) and is mostly based on Sajid's previous attempt to write this.

For collision properties, add one or more of these to the end of your mesh name and combine them.

0/4/7/B/C/D/F = Concrete

1 = Water

2 = Wood

3 = Metal

5 = Grass

6 = Sand

8 = Snow

9 = Dirt

A = Glass

E = Metal (Echo)

10000: Wall

40000: No Stand

40000000/80000: Water

100000: Death

200000: Player Only Collision

4000000: Camera Only Collision

10000000: Fall if player stops moving

20000000: Corner Damage

28000000: Damage

60000000: Deadly Water

80000000: Climbable Wall