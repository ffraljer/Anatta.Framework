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
        if (uRadius > 0.0) { // rounded corners
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

        if (uCircleThickness > 0.0) { // ring
            float outer = uCircleRadius;
            float inner = uCircleRadius - uCircleThickness;

            float aa = fwidth(dist);

            float outerAlpha = 1.0 - smoothstep(outer - aa, outer + aa, dist);
            float innerAlpha = smoothstep(inner - aa, inner + aa, dist);

            float ringAlpha = outerAlpha * innerAlpha;

            if (ringAlpha <= 0.0)
                discard;

            color = uBorderColour;
            color.a *= ringAlpha;
        } else { // filled circle
            float aa = fwidth(dist);
            float alpha = 1.0 - smoothstep(uCircleRadius - aa, uCircleRadius + aa, dist);

            if (alpha <= 0.0)
                discard;

            color *= uTint;
            color.a *= alpha;
        }
    }

    FragColor = color;
}