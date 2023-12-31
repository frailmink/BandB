
extends Node2D
class_name trap

var width = 1
var height = 1
var grounded = true 
var placed = false
var layerPlace = 2

func GetCollision():
	return $Sprite2D/StaticBody2D/CollisionPolygon2D

func GetDimensions():
	return Vector2(width, height)

func OnPlace(loc,tileMap):
	var tempVec = loc
	placed = true 
	for x in range(width):
			for y in range(height):
				tempVec = loc + Vector2i(x, y)
				tileMap.set_cell(layerPlace, tempVec, 1, Vector2i(0,0))

func CheckIfPlaceable(loc,tileMap):
	var tile
	var tempVec = loc
	for layers in range(1, tileMap.get_layers_count()):
		for x in range(width):
			for y in range(height):
				tempVec = loc + Vector2i(x, y)
				tile = tileMap.get_cell_tile_data(layers, tempVec)
				if tile:
					modulate = Color("ad54ff3c")
					return false
	
	if grounded:
		return Grounded(loc, tileMap)
	else:
		modulate = Color("adff4545")
		return true

func Grounded(loc,tileMap):
	var tileFromLayer1
	var tileFromLayer2
	var tempVec = loc
	for x in range(width):
		tempVec = loc + Vector2i(x, 1)
		tileFromLayer1 = tileMap.get_cell_tile_data(1, tempVec)
		# gets tile from layer 3
		tileFromLayer2 = tileMap.get_cell_tile_data(3, tempVec)
		if tileFromLayer1 == null and tileFromLayer2 == null:
			modulate = Color("ad54ff3c")
			return false
	modulate = Color("adff4545")
	return true 



