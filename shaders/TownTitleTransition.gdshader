shader_type canvas_item;
render_mode skip_vertex_transform;

global uniform int pallete_index;

// -3 = faded to darkest color (index 0)
//  0 = normal colors
// +3 = faded to lightest color (index 3)
// -4 or +4 = completely transparent (optional, if you want the sprite to hide at the end of a fade)
uniform int fade_offset : hint_range(-4, 4, 1) = 0;

const vec3 palletes[16] = { 
	vec3(90.0 / 255.0, 57.0 / 255.0, 33.0 / 255.0),
	vec3(107.0 / 255.0, 140.0 / 255.0, 66.0 / 255.0),
	vec3(123.0 / 255.0, 198.0 / 255.0, 123.0 / 255.0),
	vec3(255.0 / 255.0, 255.0 / 255.0, 181.0 / 255.0),

	vec3(0.0 / 255.0, 33.0 / 255.0, 69.0 / 255.0),
	vec3(18.0 / 255.0, 87.0 / 255.0, 165.0 / 255.0),
	vec3(64.0 / 255.0, 180.0 / 255.0, 229.0 / 255.0),
	vec3(222.0 / 255.0, 222.0 / 255.0, 222.0 / 255.0),

	vec3(50.0 / 255.0, 47.0 / 255.0, 61.0 / 255.0),
	vec3(152.0 / 255.0, 96.0 / 255.0, 117.0 / 255.0),
	vec3(238.0 / 255.0, 148.0 / 255.0, 137.0 / 255.0),
	vec3(242.0 / 255.0, 255.0 / 255.0, 232.0 / 255.0),

	vec3(57.0 / 255.0, 44.0 / 255.0, 66.0 / 255.0),
	vec3(194.0 / 255.0, 63.0 / 255.0, 48.0 / 255.0),
	vec3(229.0 / 255.0, 124.0 / 255.0, 72.0 / 255.0),
	vec3(255.0 / 255.0, 244.0 / 255.0, 212.0 / 255.0)
};

void vertex() {
	VERTEX = (MODEL_MATRIX * vec4(VERTEX, 0.0, 1.0)).xy;
}

void fragment() {
	vec4 tex = texture(TEXTURE, UV);
	
	// Map 0.0 - 1.0 grayscale to 0 - 3
	int base_index = int(round(tex.r * 4.0));
	
	// Shift the palette index and clamp it so it stays within the 4 colors of the current palette
	int shifted_index = clamp(base_index + fade_offset, 0, 3);
	
	// Optional: Make the UI element invisible if pushed past +/- 3 (at -4 or 4)
	float final_alpha = (abs(fade_offset) >= 4) ? 0.0 : tex.a;

	COLOR = vec4(palletes[4 * pallete_index + shifted_index], final_alpha);
}