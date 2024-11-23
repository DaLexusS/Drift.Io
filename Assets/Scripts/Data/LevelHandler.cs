
using Unity.Mathematics;

public static class LevelHandler
{
    private const float CONSTANT = 0.1f;
    private const float POWER = 2f;

    public static float ReturnXPCalculation(float level)
    {
        float calculate = (float)math.pow(level / CONSTANT, POWER); // (current level / constant) ^ power


        if (level == 0)
        {
            return 50;
        }

        return calculate;
    }
}
