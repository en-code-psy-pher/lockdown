#version 330 core

out vec4 FragColor;

in vec3 color;
in vec2 texCoord;
in vec3 normal;
in vec3 currentPos;

uniform sampler2D tex0;

uniform vec4 lightColor;
uniform vec3 lightPos;
uniform vec3 camPos;

void main()
{
    float ambient = 0.20f;

    normalize(normal);

    vec3 lightDirection = normalize(lightPos - currentPos);

    // max to prevent negative values
    float diffuse = max(dot(normal, lightDirection), 0.0f);

    float specularLight = 0.50f; 
    vec3 viewDirection = normalize(camPos - currentPos);
    vec3 reflectionDirection = reflect(-lightDirection, normal);
    float specularAmount = pow(max(dot(viewDirection, reflectionDirection), 0.0f), 8);
    float specular = specularAmount * specularLight;

    FragColor = texture(tex0, texCoord) * lightColor * (diffuse + ambient + specular);
}