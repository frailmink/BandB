extends trap

func _init():
	width = 3
	height = 1
	
func OnPlace(loc,tileMap):
	super.OnPlace(loc,tileMap)
	print_debug("Spike Placed")
#	var free = super.CheckIfPlaceable(loc,tileMap) 




func _on_area_2d_body_entered(body):
	if body.has_method("die") and placed:
		body.die()  # Call a "die" function on the player (you need to implement this).
