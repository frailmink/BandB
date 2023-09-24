extends Camera2D

var zoom_speed: float = 0.1
var zoom_min: float = 0.5
var zoom_max: float = 2.0
var smoothing: float = 0.1
var targetinhos: Array = []


func add_target(t):
	targetinhos.append(t)
	
func _process(delta):
	var mid = Vector2.ZERO
	#var count = 0
	#var midpoint = (target1.global_position + target2.global_position) / 2
	#var distance = target1.global_position.distance_to(target2.global_position)
	
	for target in targetinhos:
		#count += 1
		mid += target.global_position
		
	var midpoint = mid / targetinhos.size()
	var max_Disnatce = 0.0
	var min_Disnatce = 0.0
	for target in targetinhos:
		var distance = target.global_position.distance_to(midpoint)
		max_Disnatce = max(max_Disnatce, distance)
	#var desired_zoom = clamp(1000 / distance, zoom_min, zoom_max)
	var desired_zoom = clamp(1000 / max_Disnatce, zoom_min, zoom_max)
	zoom.x = lerp(zoom.x, desired_zoom, zoom_speed)
	zoom.y = lerp(zoom.y, desired_zoom, zoom_speed)
	
	global_position = global_position.lerp(midpoint, smoothing)

func Delete_Target(t):
	for target in targetinhos:
		if target == t:
			targetinhos.erase(target) 
	print_debug(targetinhos)
