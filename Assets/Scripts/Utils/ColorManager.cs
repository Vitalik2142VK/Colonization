using UnityEngine;

public static class ColorManager
{
    private static float MaxValueColor = 1.0f;
    private static float MinValueColor = 0.0f;

    public static Color GetRandomColor()
    {
        float r = GetRandomColorValue();
        float g = GetRandomColorValue();
        float b = GetRandomColorValue();

        Color color = new Color(r, g, b);

        return color;
    }

    private static float GetRandomColorValue()
    {
        return Random.Range(MinValueColor, MaxValueColor);
    }
}
