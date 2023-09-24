extends trap


# Called when the node enters the scene tree for the first time.
func _init():
	width = 3
	height = 1
	grounded = false
	
func OnPlace():
	print_debug("lol")
