//=========================================================================
//	  Copyright (c) 2016 SonicGLvl
//
//    This file is part of SonicGLvl, a community-created free level editor 
//    for the PC version of Sonic Generations.
//
//    SonicGLvl is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    SonicGLvl is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//    
//
//    Read AUTHORS.txt, LICENSE.txt and COPYRIGHT.txt for more details.
//=========================================================================

#include "LibGens.h"
#include "Light.h"
#include "GITextureGroup.h"
#include "FreeImage.h"
#include "FBX.h"
#include "FBXManager.h"
#include "S06Collision.h"
#include "HavokEnviroment.h"

using namespace LibGens;

int main(int argc, char** argv) 
{
	LibGens::HavokEnviroment havok_enviroment(100 * 1024 * 1024);
	cout << "File\n";
	string file = std::string(argv[1]);
	cout << file << "\n";
	cout << "FBX Manager\n";
	FBXManager fbxMan = FBXManager();
	cout << "FBX\n";
	FBX* fbx = fbxMan.importFBX(file);
	cout << "Sonic Collision\n";
	SonicCollision col = SonicCollision(fbx);
	cout << "Collision Save\n";
	col.save("collision.bin");
	cout << "Done\n";
    return 0;
}