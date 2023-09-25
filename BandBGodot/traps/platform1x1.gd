extends trap

func _init():
	grounded = false

func OnPlace(loc,tileMap):
	super.OnPlace(loc,tileMap)
	print_debug("hehe")
