shader_type canvas_item;
render_mode skip_vertex_transform;

void vertex() {
	//float y_value = MODEL_MATRIX[3][1];
	//float scaling_intensity = pow(1000.0, y_value)/(999.0 - 1.0);
	//mat4 scaling = mat4(	vec4(1., 0., 0., 0.), 
	//						vec4(0., scaling_intensity * 0.001, 0., 0.), 
	//						vec4(0., 0., 1., 0.), 
	//						vec4(0., 0., 0., 1.));
    VERTEX = (MODEL_MATRIX * vec4(VERTEX, 0.0, 1.0)).xy;
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
									
const vec3[4] suburbsloppallete = {vec3(57.0/255.0, 44.0/255.0, 66.0/255.0),
									vec3(194.0/255.0, 63.0/255.0, 48.0/255.0),
									vec3(229.0/255.0, 124.0/255.0, 72.0/255.0),
									vec3(255.0/255.0, 244.0/255.0, 212.0/255.0)};
void fragment() {

	vec3 grayScale = texture(TEXTURE, UV).rgb;
	float alpha = texture(TEXTURE, UV).a;
	int index = int(round(grayScale.r * 4.0));
	COLOR = vec4(suburbsloppallete[index], alpha);
	
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
