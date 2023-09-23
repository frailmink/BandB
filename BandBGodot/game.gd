extends Node2D
@onready var trap = load("res://traps/platform.tscn").instantiate()
@onready var col = trap.GetCollision()
@onready var map = $Map1
@onready var tileMap = $Map1/TileMap
@onready var cursorMouse = $Map1/cursorKeyboard/Sprite2D/Marker2D
@onready var dimensions = trap.GetDimensions()
@onready var buildingMode = true
var placeable

# Called when the node enters the scene tree for the first time.
func _ready():
	$Map1/Camera2D.add_target($Map1/Player1, $Map1/Player2)
	trap.position = Vector2.ZERO
	
	col.set_disabled(true)
	map.add_child(trap, true)
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if buildingMode:
		tileMap.set_layer_enabled(0, true)
		placeable = MoveTrap(cursorMouse.global_position)
	if Input.is_action_just_pressed("click") and placeable:
		col.set_disabled(false)
		map.get_node("" + trap.name).modulate = Color("ffffff")
		trap.OnPlace()
		buildingMode = false
		tileMap.set_layer_enabled(0, false)
	
func MoveTrap(cursorLoc):
	var tempLoc = cursorLoc
	tempLoc = tileMap.local_to_map(tempLoc)
	var free = CheckIfPlaceable(tempLoc)
	tempLoc = tileMap.map_to_local(tempLoc)
	trap.global_position = tempLoc
	return free
	
func CheckIfPlaceable(loc):
	var tile
	var tempVec = loc
	for x in range(dimensions.x):
		for y in range(dimensions.y):
			tempVec = loc + Vector2i(x, y)
			tile = tileMap.get_cell_tile_data(1, tempVec)
			if tile:
				map.get_node("" + trap.name).modulate = Color("ad54ff3c")
				return false
	map.get_node("" + trap.name).modulate = Color("adff4545")
	return true
