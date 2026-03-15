#version 330 core
in vec2 vTex;
out vec4 FragColor;

uniform sampler2D tex;
uniform vec4 uTint;

void main()
{
    vec4 color = texture(tex, vTex);
    FragColor = color * uTint;
}
