shader_type canvas_item;

global uniform int pallete_index;

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

void fragment() {
    // Read the grayscale brightness directly from the node's Inspector Color
    float brightness = COLOR.r;
    
    // Use strict thresholds to lock the index between 0 and 3.
    // This prevents math bugs and color-space crushing entirely.
    int index = 0;
    if (brightness > 0.8) {
        index = 3; // Pure White / Lightest
    } else if (brightness > 0.5) {
        index = 2; // Light Gray
    } else if (brightness > 0.2) {
        index = 1; // Dark Gray
    } else {
        index = 0; // Black / Darkest
    }

    // We still multiply the alpha by TEXTURE.a so Labels keep their text shapes 
    // instead of turning into solid blocks.
    float final_alpha = COLOR.a * texture(TEXTURE, UV).a;
    
    // Apply the mapped color
    COLOR = vec4(palletes[(4 * pallete_index) + index], final_alpha);
}