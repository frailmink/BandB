extends trap


# Called when the node enters the scene tree for the first time.
func _init():
	width = 3
	height = 1
	grounded = false
	# changes the layer where the blocks are going to be placed in the tilemap
	# to allow grounded traps to be placed on top of them
	layerPlace = 3
	
func OnPlace(loc,tileMap):
	super.OnPlace(loc,tileMap)
	print_debug("lol")
