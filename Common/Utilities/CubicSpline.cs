using System;
using System.Numerics;

namespace TerrariaClone.Common.Utilities
{
    public class CubicSpline
    {
        private readonly float[] _x;
        private readonly float[] _y;

        public CubicSpline(Vector2[] points)
        {
            if (points == null || points.Length < 2)
                throw new ArgumentException("Spline requires at least two data points.", nameof(points));

            _x = new float[points.Length];
            _y = new float[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                _x[i] = points[i].X;
                _y[i] = points[i].Y;
            }
        }

        public float Interpolate(float x)
        {
            x = Godot.Mathf.Clamp(x, _x[0], _x[^1]);

            if (Godot.Mathf.IsEqualApprox(x, _x[0]))
                return _y[0];
            if (Godot.Mathf.IsEqualApprox(x, _x[^1]))
                return _y[_x.Length - 1];

            int i = FindInterval(x);
            if (i == -1)
                return _y[^1];

            float h = _x[i + 1] - _x[i];
            float t = (x - _x[i]) / h;

            float m0 = ComputeTangent(i);
            float m1 = ComputeTangent(i + 1);

            // Cubic Hermite basis functions
            float t2 = t * t;
            float t3 = t2 * t;
            float h00 = 2 * t3 - 3 * t2 + 1;
            float h10 = t3 - 2 * t2 + t;
            float h01 = -2 * t3 + 3 * t2;
            float h11 = t3 - t2;

            return h00 * _y[i] + h10 * m0 * h + h01 * _y[i + 1] + h11 * m1 * h;
        }

        public float Interpolate(int x)
        {
            return Interpolate((float)x);
        }

        private int FindInterval(float x)
        {
            for (int i = 0; i < _x.Length - 1; i++)
            {
                if (x >= _x[i] && x <= _x[i + 1])
                    return i;
            }
            return -1;
        }

        private float ComputeTangent(int i)
        {
            int n = _x.Length;
            if (i == 0)
            {
                // Forward difference for the first point.
                return (_y[1] - _y[0]) / (_x[1] - _x[0]);
            }
            else if (i == n - 1)
            {
                // Backward difference for the last point.
                return (_y[n - 1] - _y[n - 2]) / (_x[n - 1] - _x[n - 2]);
            }
            else
            {
                // Central difference for interior points.
                return (_y[i + 1] - _y[i - 1]) / (_x[i + 1] - _x[i - 1]);
            }
        }
    }
}
