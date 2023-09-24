extends trap

func _init():
	width = 3
	height = 1
	
func OnPlace(loc,tileMap):
	super.OnPlace(loc,tileMap)
	print_debug("Spike Placed")


	

#	var free = super.CheckIfPlaceable(loc,tileMap) 


