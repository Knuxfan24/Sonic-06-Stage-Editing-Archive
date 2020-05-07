--Created by Paraxade0
--Fixed by brianuuuSonic

clearListener()
fileIn "ImporterIO.ms"

yzflip = true

struct splinepath (pathdef_offset, spline_count, unknownFloat1, splines, nodepos, noderot, nodenameoffset)
struct spline (spline_offset, num_vertices, unknownInt2, vertices)
struct vertex (flag, xpos, ypos, zpos, invec_xpos, invec_ypos, invec_zpos, outvec_xpos, outvec_ypos, outvec_zpos)

pathfilename = GetOpenFileName caption:"Open Sonic 06 Path File" types:"Sonic 06 Path File (*.path)|*.path"
if (pathfilename != undefined) and (doesFileExist pathfilename == true) do (

f = fopen pathfilename "rb"
fseek f 0x20 #seek_set

path_table_offset = ReadLongBE f + 0x20
path_table_num_elements = ReadLongBE f
node_table_offset = ReadLongBE f + 0x20
node_table_num_elements = ReadLongBE f
format "path_table_offset: %\npath_table_num_elements: %\nnode_table_offset: %\nnode_table_num_elements: %\n\n" path_table_offset path_table_num_elements node_table_offset node_table_num_elements

patharray = #()

fseek f path_table_offset #seek_set
for i = 1 to path_table_num_elements do (
	patharray[i] = splinepath splines:#()
	patharray[i].pathdef_offset = ReadLongBE f + 0x20
	patharray[i].spline_count = ReadLongBE f
	patharray[i].unknownFloat1 = ReadFloatBE f
	format "spline %:\npathdef_offset: %\nspline_count: %\nunknownFloat1: %\n\n" i patharray[i].pathdef_offset patharray[i].spline_count patharray[i].unknownFloat1
)

format "done reading spline declarations; now reading spline definitions\n\n"

for i = 1 to path_table_num_elements do (
	fseek f pathArray[i].pathdef_offset #seek_set
	format "path %:\n\n" i
	for splinenum = 1 to pathArray[i].spline_count do (
		pathArray[i].splines[splinenum] = spline vertices:#()
		pathArray[i].splines[splinenum].spline_offset = ReadLongBE f + 0x20
		pathArray[i].splines[splinenum].num_vertices = ReadLongBE f
		pathArray[i].splines[splinenum].unknownInt2 = ReadLongBE f
		format "spline %\nspline_offset: %\nnum_vertices: %\nnunknownInt2: %\n\n" splinenum patharray[i].splines[splinenum].spline_offset patharray[i].splines[splinenum].num_vertices patharray[i].splines[splinenum].unknownInt2
	)
)

format "done reading spline definitions; now reading splines\n\n"

for i = 1  to path_table_num_elements do (
	format "path %:\n\n" i
	for splinenum = 1 to pathArray[i].spline_count do (
		format "spline %:\n\n" splinenum
		fseek f pathArray[i].splines[splinenum].spline_offset #seek_set
		for v = 1 to pathArray[i].splines[splinenum].num_vertices do (
			pathArray[i].splines[splinenum].vertices[v] = vertex flag:0
			pathArray[i].splines[splinenum].vertices[v].flag = ReadLongBE f
			pathArray[i].splines[splinenum].vertices[v].xpos = ReadFloatBE f
			pathArray[i].splines[splinenum].vertices[v].ypos = ReadFloatBE f
			pathArray[i].splines[splinenum].vertices[v].zpos = ReadFloatBE f
			pathArray[i].splines[splinenum].vertices[v].invec_xpos = ReadFloatBE f
			pathArray[i].splines[splinenum].vertices[v].invec_ypos = ReadFloatBE f
			pathArray[i].splines[splinenum].vertices[v].invec_zpos = ReadFloatBE f
			pathArray[i].splines[splinenum].vertices[v].outvec_xpos = ReadFloatBE f
			pathArray[i].splines[splinenum].vertices[v].outvec_ypos = ReadFloatBE f
			pathArray[i].splines[splinenum].vertices[v].outvec_zpos = ReadFloatBE f
			format "vertex %:\nflag: %\nxpos: %\nypos: %\nzpos: %\ninvec_xpos: %\ninvec_ypos: %\ninvec_zpos: %\noutvec_xpos: %\noutvec_ypos: %\noutvec_zpos: %\n\n" v pathArray[i].splines[splinenum].vertices[v].flag pathArray[i].splines[splinenum].vertices[v].xpos pathArray[i].splines[splinenum].vertices[v].ypos pathArray[i].splines[splinenum].vertices[v].zpos pathArray[i].splines[splinenum].vertices[v].invec_xpos pathArray[i].splines[splinenum].vertices[v].invec_ypos pathArray[i].splines[splinenum].vertices[v].invec_zpos pathArray[i].splines[splinenum].vertices[v].outvec_xpos pathArray[i].splines[splinenum].vertices[v].outvec_ypos pathArray[i].splines[splinenum].vertices[v].outvec_zpos
		)
	)
)

fseek	 f node_table_offset #seek_set

for i = 1 to node_table_num_elements do (
	nodenum = ReadLongBE f
	xpos = ReadFloatBE f
	ypos = ReadFloatBE f
	zpos = ReadFloatBE f
	xrot = ReadFloatBE f
	yrot = ReadFloatBE f
	zrot = ReadFloatBE f
	wrot = ReadFloatBE f
	pathArray[i].nodenameoffset = ReadLongBE f + 0x20
	if yzflip == false then (
		pathArray[i].nodepos = [xpos, ypos, zpos]
		pathArray[i].noderot = quat xrot yrot zrot wrot
	)
	if yzflip == true then (
		pathArray[i].nodepos = [xpos, -zpos, ypos]
		pathArray[i].noderot = quat xrot -zrot yrot wrot
	)
	format "node %\nnode pos: %\nnode rot: %\nnode name offset: %\n\n" i pathArray[i].nodepos pathArray[i].noderot pathArray[i].nodenameoffset
)

for i = 1 to pathArray.count do (
	newspline = line name:("Sonic06spline" + i as string)
	for splinenum = 1 to pathArray[i].spline_count do (
		addnewspline newspline
		for v = 1 to pathArray[i].splines[splinenum].vertices.count do (
			flag = pathArray[i].splines[splinenum].vertices[v].flag
			
			if yzflip == false then (
				xyzpos = [pathArray[i].splines[splinenum].vertices[v].xpos, pathArray[i].splines[splinenum].vertices[v].ypos, pathArray[i].splines[splinenum].vertices[v].zpos]
				invecpos = [pathArray[i].splines[splinenum].vertices[v].invec_xpos, pathArray[i].splines[splinenum].vertices[v].invec_ypos, pathArray[i].splines[splinenum].vertices[v].invec_zpos]
				outvecpos = [pathArray[i].splines[splinenum].vertices[v].outvec_xpos, pathArray[i].splines[splinenum].vertices[v].outvec_ypos, pathArray[i].splines[splinenum].vertices[v].outvec_zpos]
			)
			
			if yzflip == true then (
				xyzpos = [pathArray[i].splines[splinenum].vertices[v].xpos, -pathArray[i].splines[splinenum].vertices[v].zpos, pathArray[i].splines[splinenum].vertices[v].ypos]
				invecpos = [pathArray[i].splines[splinenum].vertices[v].invec_xpos, -pathArray[i].splines[splinenum].vertices[v].invec_zpos, pathArray[i].splines[splinenum].vertices[v].invec_ypos]
				outvecpos = [pathArray[i].splines[splinenum].vertices[v].outvec_xpos, -pathArray[i].splines[splinenum].vertices[v].outvec_zpos, pathArray[i].splines[splinenum].vertices[v].outvec_ypos]
			)
		
			if flag == 0 then addknot newspline splinenum #bezier #curve xyzpos invecpos outvecpos
			if flag == 1 then addknot newspline splinenum #corner #line xyzpos invecpos outvecpos
		)
	)
	newspline.rotation = pathArray[i].noderot
	updateShape newspline
	newspline.pos = pathArray[i].nodepos
	updateShape newspline
	fseek f pathArray[i].nodenameoffset #seek_set
	nodename = ReadString f
	if nodename != undefined then newspline.name = nodename
	format "created spline %\n" newspline
) 

fclose f

)