#version 450

layout(location = 0) in vec2 aPos;
layout(location = 1) in vec2 aTex;

layout(location = 0) out vec2 vTex;

layout(binding = 0) uniform Constants {
    mat4 transform;
    mat4 uProjection;
    vec4 uTintTop;
    vec4 uTintBottom;
    vec4 uBorderTop;
    vec4 uBorderBottom;
    vec2 uSize;
    float uRadius;
    float uCircleRadius;
    float uCircleThickness;
    float BoxBorderThickness;
};

void main()
{
    vTex = aTex;
    gl_Position = uProjection * transform * vec4(aPos, 0.0, 1.0);
}