using System;

namespace LineSegmentApp
{
    public class LineSegment
    {
        private double _x1;
        private double _x2;

        public LineSegment(double x1, double x2)
        {
            _x1 = x1;
            _x2 = x2;
            Normalize();
        }

        public LineSegment(LineSegment other)
        {
            _x1 = other._x1;
            _x2 = other._x2;
        }

        public double X1
        {
            get => _x1;
            set => _x1 = value;
        }

        public double X2
        {
            get => _x2;
            set => _x2 = value;
        }

        private void Normalize()
        {
            if (_x1 > _x2)
            {
                double temp = _x1;
                _x1 = _x2;
                _x2 = temp;
            }
        }

        public bool Contains(double number)
        {
            return number >= _x1 && number <= _x2;
        }

        public override string ToString()
        {
            return $"[{_x1}, {_x2}]";
        }

        public static double operator !(LineSegment segment)
        {
            return segment._x2 - segment._x1;
        }

        public static LineSegment operator ++(LineSegment segment)
        {
            return new LineSegment(segment._x1 + 1, segment._x2 + 1);
        }

        public static explicit operator int(LineSegment segment)
        {
            return (int)segment._x1;
        }

        public static implicit operator double(LineSegment segment)
        {
            return segment._x2;
        }

        public static LineSegment operator +(LineSegment segment, int d)
        {
            return new LineSegment(segment._x1 + d, segment._x2 + d);
        }

        public static LineSegment operator +(int d, LineSegment segment)
        {
            return segment + d;
        }

        public static bool operator <(LineSegment segment, int number)
        {
            return segment.Contains(number);
        }

        public static bool operator >(LineSegment segment, int number)
        {
            return !(segment < number);
        }
    }
}