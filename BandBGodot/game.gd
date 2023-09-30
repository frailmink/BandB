extends Node2D
@onready var map = $Map1
@onready var tileMap = $Map1/TileMap
@onready var cursorMouse = $Map1/cursorKeyboard/Sprite2D/Marker2D
@onready var cursorController = $Map1/cursorController/Sprite2D/Marker2D
@onready var cursorM = $Map1/cursorKeyboard
@onready var cursorC = $Map1/cursorController
@onready var buildingMode = true
@onready var numTraps = 3
var trapKeyboard
var trapController
var colKeyboard
var colController
var dimensionsKeyboard
var buildingModeMouse
var dimensionsController
var buildingModeController
var trapArrayKeyboard = []
var trapArrayController = []
var currentTrapIndexKeyboard = 0
var currentTrapIndexController = 0
var playersDead = false
var spawned = false 
# Called when the node enters the scene tree for the first time.
func _ready():
	#$Map1/Camera2D.add_target($Map1/cursorKeyboard)
	StartOverAgain()
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
		#Spawn_Players()
	if playersDead:
		StartOverAgain()
	if buildingMode:
		if buildingModeMouse:
			CheckIfKeyboardChangedTrap()
			buildingModeMouse = building(cursorMouse, trapKeyboard, colKeyboard, "Click_Mouse")
		if buildingModeController:
			CheckIfControllerChangedTrap()
			buildingModeController = building(cursorController, trapController, colController, "Click_Controller")
		if buildingModeController == false and buildingModeMouse == false:
			buildingMode = false
	else:
		tileMap.set_layer_enabled(0, false)
		if !spawned:
			Spawn_Players()
			
			
		
func building(loc, trapName, col, buttonName):
	var placeable = MoveTrap(loc.global_position, trapName)
	if Input.is_action_just_pressed(buttonName) and placeable:
		map.get_node("" + trapName.name).modulate = Color("ffffff")
		col.set_disabled(false)
		trapName.OnPlace(tileMap.local_to_map(loc.global_position), tileMap)
		return false
	return true
	
	
func MoveTrap(cursorLoc, trapName):
	var tempLoc = cursorLoc
	tempLoc = tileMap.local_to_map(tempLoc)
	var free = trapName.CheckIfPlaceable(tempLoc,tileMap)
	tempLoc = tileMap.map_to_local(tempLoc)
	trapName.global_position = tempLoc
	return free
	
func StartOverAgain():
	spawned = false
	playersDead = false
	tileMap.set_layer_enabled(0, true)	
	trapArrayKeyboard = GetNewTraps()
	trapKeyboard = load("res://traps/" + trapArrayKeyboard[0] + ".tscn").instantiate()
	trapArrayController = GetNewTraps()
	trapController = load("res://traps/" + trapArrayController[0] + ".tscn").instantiate()
	buildingMode = true
	buildingModeMouse = true
	buildingModeController = true
	colKeyboard = trapKeyboard.GetCollision()
	colController = trapController.GetCollision()
	dimensionsKeyboard = trapKeyboard.GetDimensions()
	dimensionsController = trapController.GetDimensions()
	SetTrapAgain(trapKeyboard, colKeyboard, cursorMouse.global_position)
	SetTrapAgain(trapController, colController, cursorController.global_position)
	MoveCursorsToFront()

func SetTrapAgain(trapName, col, loc):
	trapName.position = loc
	col.set_disabled(true)
	map.add_child(trapName, true)
	
func GetNewTraps():
	var tempArray = GlobalTrapNames.traps.duplicate()
	var trapArray = []
	var randInt
	for index in range(numTraps):
		randInt = randi() % tempArray.size()
		trapArray.append(tempArray[randInt])
		tempArray.remove_at(randInt)
	return trapArray
	
func CheckIfControllerChangedTrap():
	if Input.is_action_just_pressed("Rotate_Left_Controller"):
		currentTrapIndexController -= 1
		if currentTrapIndexController < 0:
			currentTrapIndexController = trapArrayController.size() - 1
		map.remove_child(trapController)
		trapController = load("res://traps/" + trapArrayController[currentTrapIndexController] + ".tscn").instantiate()
		colController = trapController.GetCollision()
		dimensionsController = trapController.GetDimensions()
		SetTrapAgain(trapController, colController, cursorController.global_position)
		MoveCursorsToFront()
	elif Input.is_action_just_pressed("Rotate_Right_Controller"):
		currentTrapIndexController += 1
		if currentTrapIndexController >= trapArrayController.size():
			currentTrapIndexController = 0
		map.remove_child(trapController)
		trapController = load("res://traps/" + trapArrayController[currentTrapIndexController] + ".tscn").instantiate()
		colController = trapController.GetCollision()
		dimensionsController = trapController.GetDimensions()
		SetTrapAgain(trapController, colController, cursorController.global_position)
		MoveCursorsToFront()

func CheckIfKeyboardChangedTrap():
	if Input.is_action_just_pressed("Rotate_Left_Keyboard"):
		currentTrapIndexKeyboard -= 1
		if currentTrapIndexKeyboard < 0:
			currentTrapIndexKeyboard = trapArrayKeyboard.size() - 1
		map.remove_child(trapKeyboard)
		trapKeyboard = load("res://traps/" + trapArrayKeyboard[currentTrapIndexKeyboard] + ".tscn").instantiate()
		colKeyboard = trapKeyboard.GetCollision()
		dimensionsKeyboard = trapKeyboard.GetDimensions()
		SetTrapAgain(trapKeyboard, colKeyboard, cursorMouse.global_position)
		MoveCursorsToFront()
	elif Input.is_action_just_pressed("Rotate_Right_Keyboard"):
		currentTrapIndexKeyboard += 1
		if currentTrapIndexKeyboard >= trapArrayKeyboard.size():
			currentTrapIndexKeyboard = 0
		map.remove_child(trapKeyboard)
		trapKeyboard = load("res://traps/" + trapArrayKeyboard[currentTrapIndexKeyboard] + ".tscn").instantiate()
		colKeyboard = trapKeyboard.GetCollision()
		dimensionsKeyboard = trapKeyboard.GetDimensions()
		SetTrapAgain(trapKeyboard, colKeyboard, cursorMouse.global_position)
		MoveCursorsToFront()

func Spawn_Players():
	playersDead = false
	spawned = true 
	var player2 = preload("res://player2.tscn").instantiate()
	var player1 = preload("res://player1.tscn").instantiate()
	MoveCursorsToFront()
	$Map1.add_child(player2)
	$Map1.add_child(player1)
	$Map1/Camera2D.add_target($Map1/Player2)
	$Map1/Camera2D.add_target($Map1/Player1)
	player2.set_global_position($Map1/SpawnPoint.position) 
	player1.set_global_position($Map1/SpawnPoint.position)
#		var spawn_position = Vector2(100, 200)
#		player1.global_position = spawn_position
#		player2.global_position = spawn_position
		#targets.append(new_player1)
		#targets.append(new_player2)
		
func MoveCursorsToFront():
	cursorM.move_to_front()
	cursorC.move_to_front()
