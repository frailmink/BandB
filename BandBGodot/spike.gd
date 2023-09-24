extends trap

func _init():
	width = 2
	height = 1
	grounded = true
	
func OnPlace():
	print_debug("Spike Placed")

#	var free = super.CheckIfPlaceable(loc,tileMap) 


