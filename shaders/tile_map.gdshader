shader_type canvas_item;

void vertex() {
	// Called for every vertex the material is visible on.
}

const vec3[4] greenpallete = {vec3(90.0/255.0, 57.0/255.0, 33.0/255.0),
							vec3(107.0/255.0, 140.0/255.0, 66.0/255.0),
							vec3(123.0/255.0, 198.0/255.0, 123.0/255.0),
							vec3(255.0/255.0, 255.0/255.0, 181.0/255.0)};

const vec3[4] ubcapallete = {vec3(0.0/255.0, 33.0/255.0, 69.0/255.0),
							vec3(18.0/255.0, 87.0/255.0, 165.0/255.0),
							vec3(64.0/255.0, 180.0/255.0, 229.0/255.0),
							vec3(222.0/255.0, 222.0/255.0, 222.0/255.0)};
							
const vec3[4] liquidcitypallete = 	{vec3(50.0/255.0, 47.0/255.0, 61.0/255.0),
									vec3(152.0/255.0, 96.0/255.0, 117.0/255.0),
									vec3(238.0/255.0, 148.0/255.0, 137.0/255.0),
									vec3(242.0/255.0, 255.0/255.0, 232.0/255.0)};
									
void fragment() {

	vec3 grayScale = texture(TEXTURE, UV).rgb;
	float alpha = texture(TEXTURE, UV).a;
	int index = int(round(grayScale.r * 4.0));
	COLOR = vec4(greenpallete[index], alpha);
	
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
