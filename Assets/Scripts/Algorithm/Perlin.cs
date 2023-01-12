using UnityEditor;

public class Perlin
{
    /// <summary>
    /// x、y、z座標、オクターブ数、および進行度を取得し、それらを使用してPerlinノイズを生成
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="octaves"></param>
    /// <param name="persistence"></param>
    /// <returns></returns>
    public static double OctavePerlin(double x, double y, double z, int octaves, double persistence)
    {
        double total = 0; // 合計値
        double frequency = 1; // 周波数
        double amplitude = 1; // 振れ幅
         
            // x、y、z座標に対してPerlinノイズを生成。その後、生成されたPerlinノイズの値がamplitudeによって乗算され、その結果がtotalに加算
        // 次に、amplitudeがpersistenceによって乗算され、frequencyは2倍になる。
        // よって、より高周波数のPerlinノイズが生成され、より小さなアンプリフィケーションが適用される。
        // オクターブのような概念を実現し、より詳細なPerlinノイズを生成する
        for (int i = 0; i < octaves; i++)
        {
            total += perlin(x * frequency, y * frequency, z * frequency) * amplitude;
            amplitude *= persistence;
            frequency *= 2;
        }

        return total;
    }

    /// <summary>
    /// Perlinノイズの計算に使用される0から255までのすべての数字をランダムに並べた数字の配列
    /// </summary>
    private static readonly int[] permutation =
    {
        151, 160, 137, 91, 90, 15, 131, 13, 201, 95, 96, 53, 194, 233, 7, 225, 140, 36, 103, 30,
        69, 142, 8, 99, 37, 240, 21, 10, 23, 190, 6, 148, 247, 120, 234, 75, 0, 26, 197, 62,
        94, 252, 219, 203, 117, 35, 11, 32, 57, 177, 33, 88, 237, 149, 56, 87, 174, 20, 125, 136,
        171, 168, 68, 175, 74, 165, 71, 134, 139, 48, 27, 166, 77, 146, 158, 231, 83, 111, 229, 122,
        60, 211, 133, 230, 220, 105, 92, 41, 55, 46, 245, 40, 244, 102, 143, 54, 65, 25, 63, 161,
        1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169, 200, 196, 135, 130, 116, 188, 159, 86,
        164, 100, 109, 198, 173, 186, 3, 64, 52, 217, 226, 250, 124, 123, 5, 202, 38, 147, 118, 126,
        255, 82, 85, 212, 207, 206, 59, 227, 47, 16, 58, 17, 182, 189, 28, 42, 223, 183, 170, 213,
        119, 248, 152, 2, 44, 154, 163, 70, 221, 153, 101, 155, 167, 43, 172, 9, 129, 22, 39, 253,
        19, 98, 108, 110, 79, 113, 224, 232, 178, 185, 112, 104, 218, 246, 97, 228, 251, 34, 242, 193,
        238, 210, 144, 12, 191, 179, 162, 241, 81, 51, 145, 235, 249, 14, 239, 107, 49, 192, 214, 31,
        181, 199, 106, 157, 184, 84, 204, 176, 115, 121, 50, 45, 127, 4, 150, 254, 138, 236, 205, 93,
        222, 114, 67, 29, 24, 72, 243, 141, 128, 195, 78, 66, 215, 61, 156, 180
    };

    /// <summary>
    /// permutation配列のコピーで、オーバーフローを避ける用に使用
    /// </summary>
    private static readonly int[] p;

    /// <summary>
    /// x、y、z座標を使用してPerlinノイズを生成
    /// </summary>
    static Perlin()
    {
        p = new int[512];
        for (int x = 0; x < 512; x++)
        {
            p[x] = permutation[x % 512];
        }
    }
    
    public static double perlin(double x, double y, double z)
    {
        /*if (repeat > 0)
        {
            x = x * repeat;
            y = y * repeat;
            z = z * repeat;
        }*/

        return 0;
    }
}