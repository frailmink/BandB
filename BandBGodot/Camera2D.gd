extends Camera2D

var target1: Node2D
var target2: Node2D
var zoom_speed: float = 0.1
var zoom_min: float = 0.5
var zoom_max: float = 2.0
var smoothing: float = 0.1

func add_target(t1, t2):
	target1 = t1
	target2 = t2
	
func _process(delta):
	var midpoint = (target1.global_position + target2.global_position) / 2
	var distance = target1.global_position.distance_to(target2.global_position)
	
	var desired_zoom = clamp(1000 / distance, zoom_min, zoom_max)
	zoom.x = lerp(zoom.x, desired_zoom, zoom_speed)
	zoom.y = lerp(zoom.y, desired_zoom, zoom_speed)
	
	global_position = global_position.lerp(midpoint, smoothing)
