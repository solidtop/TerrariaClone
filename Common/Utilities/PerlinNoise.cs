using System;
using Godot;

namespace TerrariaClone.Common.Utilities
{
    public class PerlinNoise
    {
        private const float AmplitudeScale = 1f;
        private const float FrequencyScale = 0.01f;

        private readonly int _octaves;
        private readonly float _amplitude;
        private readonly float _frequency;
        private readonly int[] _permutation;

        private static readonly int[] _gradients1D = [-1, 1];

        private static readonly int[][] _gradients2D = [
            [1, 1], [-1, 1], [1, -1], [-1, -1], // Diagonal directions
            [1, 0], [-1, 0], [0, 1], [0, -1] // Cardinal directions
        ];

        public PerlinNoise(int seed, int octaves, float frequency, float amplitude)
        {
            _octaves = octaves;
            _amplitude = amplitude * AmplitudeScale;
            _frequency = frequency * FrequencyScale;

            _permutation = GeneratePermutation(seed);
        }

        public PerlinNoise(int seed, int octaves, float frequency) : this(seed, octaves, frequency, 1) { }

        public float Sample1D(float x)
        {
            float value = 0;
            var amplitude = _amplitude;
            var frequency = _frequency;
            var persistence = 0.5f;
            var lacunarity = 2;

            for (int i = 0; i < _octaves; i++)
            {
                value += Noise(x * frequency) * amplitude;
                amplitude *= persistence;
                frequency *= lacunarity;
            }

            return value;
        }

        public float Sample2D(float x, float y)
        {
            float value = 0;
            var amplitude = _amplitude;
            var frequency = _frequency;
            var persistence = 0.5f;
            var lacunarity = 2;

            for (int i = 0; i < _octaves; i++)
            {
                value += Noise(x * frequency, y * frequency) * amplitude;
                amplitude *= persistence;
                frequency *= lacunarity;
            }

            return value;
        }

        private float Noise(float x)
        {
            var x0 = (int)MathF.Floor(x);
            var x1 = x0 + 1;

            // Relative position inside the grid cell
            var sx = x - x0;

            // Hash values for gradient selection
            var g0 = _gradients1D[Hash(x0)];
            var g1 = _gradients1D[Hash(x1)];

            // Compute dot products
            var n0 = g0 * sx;
            var n1 = g1 * (sx - 1);

            var u = Smoothstep(sx);

            return Mathf.Lerp(n0, n1, u);
        }

        private float Noise(float x, float y)
        {
            var x0 = (int)Mathf.Floor(x);
            var y0 = (int)Mathf.Floor(y);
            var x1 = x0 + 1;
            var y1 = y0 + 1;

            // Relative position inside the grid cell
            var sx = x - x0;
            var sy = y - y0;

            var g00 = _gradients2D[Hash(x0, y0)];
            var g10 = _gradients2D[Hash(x1, y0)];
            var g01 = _gradients2D[Hash(x0, y1)];
            var g11 = _gradients2D[Hash(x1, y1)];

            // Compute dot products
            var n00 = Dot(g00[0], g00[1], sx, sy);
            var n10 = Dot(g10[0], g10[1], sx - 1, sy);
            var n01 = Dot(g01[0], g01[1], sx, sy - 1);
            var n11 = Dot(g11[0], g11[1], sx - 1, sy - 1);

            // Interpolate along x
            var u = Smoothstep(sx);
            var nx0 = Mathf.Lerp(n00, n10, u);
            var nx1 = Mathf.Lerp(n01, n11, u);

            // Interpolate along y
            var v = Smoothstep(sy);

            return Mathf.Lerp(nx0, nx1, v);
        }

        private int Hash(int x)
        {
            return _permutation[x & 255] % 2;
        }

        private int Hash(int x, int y)
        {
            return _permutation[_permutation[x & 255] + y & 255] % 8;
        }

        private static float Dot(float x1, float y1, float x2, float y2)
        {
            return x1 * x2 + y1 * y2;
        }

        private static float Smoothstep(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        private static int[] GeneratePermutation(int seed)
        {
            var random = new Random(seed);
            var perm = new int[256];

            for (int i = 0; i < 256; i++)
            {
                perm[i] = random.Next();
            }

            // Shuffle 
            for (int i = 255; i > 0; i--)
            {
                int swapIndex = random.Next(i + 1);
                (perm[i], perm[swapIndex]) = (perm[swapIndex], perm[i]);
            }

            // Duplicate array to avoid overflow when hashing
            var permutation = new int[512];

            for (int i = 0; i < 512; i++)
            {
                permutation[i] = perm[i % 256];
            }

            return permutation;
        }
    }
}
