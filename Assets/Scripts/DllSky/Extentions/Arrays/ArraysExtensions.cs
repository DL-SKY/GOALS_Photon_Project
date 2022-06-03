namespace DllSky.Extansions
{
    public static class ArraysExtensions
    {
        public static float GetAvg(this float[,] array, int x, int y)
        {
            int count = 0;
            float sum = 0.0f;

            for (int iX = x - 1; iX <= x + 1; iX++)
                for (int iY = y - 1; iY <= y + 1; iY++)
                {
                    if (iX < 0 || iY < 0 || iX >= array.GetLength(0) || iY >= array.GetLength(1))
                        continue;

                    count++;
                    sum += array[iX, iY];
                }

            return sum / (count == 0 ? 1 : count);
        }
    }
}
