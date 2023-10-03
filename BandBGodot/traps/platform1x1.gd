extends trap

func _init():
	grounded = false
	layerPlace = 3

func OnPlace(loc,tileMap):
	super.OnPlace(loc,tileMap)
	print_debug("hehe")
