#version 330 core
in vec2 vTex;
out vec4 FragColor;

uniform sampler2D tex;
uniform vec4 uTint;
uniform vec4 uBorderColour;
uniform float uRadius;
uniform float uCircleRadius;
uniform float uCircleThickness;
uniform vec2 uSize;

void main()
{
    vec4 color = texture(tex, vTex);
    vec2 p = vTex * uSize;

    float left   = p.x;
    float right  = uSize.x - p.x;
    float top    = p.y;
    float bottom = uSize.y - p.y;

    if (uCircleRadius <= 0.0) { // box
        if (uRadius > 0.0) { // rounded
            float dx = max(uRadius - left, 0.0);
            dx = max(dx, max(uRadius - right, 0.0));

            float dy = max(uRadius - top, 0.0);
            dy = max(dy, max(uRadius - bottom, 0.0));

            float dist = length(vec2(dx, dy));

            float aa = fwidth(dist);
            float a = 1.0 - smoothstep(uRadius - aa, uRadius + aa, dist);

            if (a <= 0.0)
                discard;

            color.a *= a;
        }
        color *= uTint;
    }
    else { // circle
        vec2 center = uSize * 0.5;
        float dist = length(p - center);
        float aa = fwidth(dist);

        float fillAlpha = 1.0 - smoothstep(uCircleRadius - uCircleThickness - aa, uCircleRadius - uCircleThickness + aa, dist);

        float borderAlpha = 0.0;
        if (uCircleThickness > 0.0) {
            float outer = uCircleRadius;
            float inner = uCircleRadius - uCircleThickness;
            borderAlpha = smoothstep(inner - aa, inner + aa, dist) * (1.0 - smoothstep(outer - aa, outer + aa, dist));
        }

        vec4 fillColor = uTint;
        fillColor.a *= fillAlpha;

        vec4 borderColor = uBorderColour;
        borderColor.a *= borderAlpha;

        color = fillColor + borderColor * (1.0 - fillColor.a);

        if (color.a <= 0.0)
            discard;
    }

    FragColor = color;
}