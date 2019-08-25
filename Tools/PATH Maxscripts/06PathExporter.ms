--Created by Aruki (https://github.com/arukibree)
--Fixed by Radfordhound

clearListener()
fileIn "IO.ms"

yzflip = true

offset_array = #()
fn addOffset f = (
	append offset_array (ftell f)
)

path_file = getSaveFileName caption:"Save paths" types:"Sonic 06 Path (*.path)|*.path"

if (path_file != undefined) do (
	f = fopen path_file "wb"

	PathArray = #()
	for p in selection where (isKindOf p shape) do append PathArray p
	PathCount = PathArray.count

	-- header
	WriteLongBE f 0 -- file size filler
	WriteLongBE f 0 -- offset table offset filler
	WriteLongBE f 0 -- offset table size filler
	WriteLongBE f 0
	WriteLongBE f 0
	WriteLongBE f 0x3142
	WriteLongBE f 0x42494E41
	WriteLongBE f 0

	addOffset f
	WriteLongBE f 0x10 -- path table offset - always 0x10
	WriteLongBE f PathCount -- path count
	addOffset f
	WriteLongBE f 0 -- node table offset filler
	WriteLongBE f PathCount -- node count - equal to path count

	for p = 1 to PathCount do
	(
		addOffset f
		WriteLongBE f 0 -- spline info offset filler
		WriteLongBE f (numSplines PathArray[p]) -- spline count
		WriteFloatBE f 2.0 -- unknown, always 2.0
	)

	SplineInfoOffsetArray = #()
	for p = 1 to PathCount do
	(
		append SplineInfoOffsetArray (ftell f)
		
		for s = 1 to (numSplines PathArray[p]) do
		(
			addOffset f
			WriteLongBE f 0 -- spline offset filler
			WriteLongBE f (numKnots PathArray[p] s) -- knot count
			WriteLongBE f 0 -- unknown, always 0
		)
	)

	SplineStartOffsetArray = #()
	for p = 1 to PathCount do
	(
		KnotCount = numKnots PathArray[p] 1
		mismatch = false
		
		for s = 1 to (numSplines PathArray[p]) do
		(
			append SplineStartOffsetArray (ftell f)
			if ((numKnots PathArray[p] s) != KnotCount) then mismatch = true
			
			for k = 1 to (numKnots PathArray[p] s) do
			(
				InvMtx = inverse PathArray[p].transform
				
				pos = (getKnotPoint PathArray[p] s k) * InvMtx
				invec = (getInVec PathArray[p] s k) * InvMtx
				outvec = (getOutVec PathArray[p] s k) * InvMtx
				
				if (getKnotType PathArray[p] s k) == #corner then WriteLongBE f 0x1
				else WriteLongBE f 0x0
				
				if (yzflip == false) then (
					WriteFloatBE f pos.x
					WriteFloatBE f pos.y
					WriteFloatBE f pos.z
					WriteFloatBE f invec.x
					WriteFloatBE f invec.y
					WriteFloatBE f invec.z
					WriteFloatBE f outvec.x
					WriteFloatBE f outvec.y
					WriteFloatBE f outvec.z
				)
				else (
					WriteFloatBE f pos.x
					WriteFloatBE f pos.z
					WriteFloatBE f -pos.y
					WriteFloatBE f invec.x
					WriteFloatBE f invec.z
					WriteFloatBE f -invec.y
					WriteFloatBE f outvec.x
					WriteFloatBE f outvec.z
					WriteFloatBE f -outvec.y
				)
			)
		)
		
		if (mismatch == true) then messageBox("Warning: " + PathArray[p].name + " has a mismatched vertex count")
	)

	NodeTableOffset = ftell f
	for n = 1 to PathCount do (
		WriteLongBE f (n - 1) -- node number
		
		pos = PathArray[n].pos
		rot = PathArray[n].rotation
		
		if (yzflip == false) then (
			WriteFloatBE f pos.x
			WriteFloatBE f pos.y
			WriteFloatBE f pos.z
			WriteFloatBE f rot.x
			WriteFloatBE f rot.y
			WriteFloatBE f rot.z
			WriteFloatBE f rot.w
		)
		else (
			WriteFloatBE f pos.x
			WriteFloatBE f pos.z
			WriteFloatBE f -pos.y
			WriteFloatBE f rot.x
			WriteFloatBE f rot.z
			WriteFloatBE f -rot.y
			WriteFloatBE f rot.w
		)
		
		
		addOffset f
		WriteLongBE f 0 -- node name offset filler
	)

	NodeNameOffsets = #()
	for n = 1 to PathCount do (
		NodeNameOffsets[n] = ftell f
		WriteString f PathArray[n].name
	)

	while ((mod (ftell f) 4) != 0) do WriteByte f 0

	-- offset table
	OffsetTableOffset = ftell f
	last = 0x20
	for o = 1 to offset_array.count do (
		val = offset_array[o] - last
		
		if (val <= 0xFC) then (
			valToWrite = bit.or 0x40 (bit.shift val -2)
			WriteByte f valToWrite
		)
		else if (val <= 0xFFFC) then (
			valToWrite = bit.or 0x8000 (bit.shift val -2)
			WriteShortBE f valToWrite
		)
		else (
			valToWrite = bit.or 0xC0000000 (bit.shift val -2)
			WriteLongBE f valToWrite
		)
		
		last = offset_array[o]
	)
	while ((mod (ftell f) 4) != 0) do WriteByte f 0
	OffsetTableSize = (ftell f) - OffsetTableOffset
	FileSize = ftell f

	-- Done first pass writing the file! Now we go back to fill in offsets and sizes
	fseek f 0 #seek_set
	WriteLongBE f FileSize
	WriteLongBE f (OffsetTableOffset - 0x20)
	WriteLongBE f OffsetTableSize

	fseek f 0x28 #seek_set
	WriteLongBE f (NodeTableOffset - 0x20)
	fseek f 0x4 #seek_cur

	for p = 1 to PathCount do (
		WriteLongBE f (SplineInfoOffsetArray[p] - 0x20)
		fseek f 0x8 #seek_cur
	)
	
	spline = 1
	for p = 1 to PathCount do (
		for s = 1 to (numSplines PathArray[p]) do (
			WriteLongBE f (SplineStartOffsetArray[spline] - 0x20)
			fseek f 0x8 #seek_cur
			spline += 1
		)
	)

	fseek f NodeTableOffset #seek_set
	for n = 1 to PathCount do (
		fseek f 0x20 #seek_cur
		WriteLongBE f (NodeNameOffsets[n] - 0x20)
	)

	messageBox("Path file saved successfully!")
	fclose f
)
