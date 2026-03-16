using System;
using System.Collections.Generic;

namespace Tema2_Variant15
{
    // Класс для представления точки на плоскости
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X:F3}; {Y:F3})";
        }
    }

    // Класс с геометрическими методами
    public static class GeometryHelper
    {
        private const double EPSILON = 1e-10;

        // Метод для нахождения точек пересечения прямой и окружности
        // Прямая задается двумя точками (p1, p2)
        // Окружность задается центром (cx, cy) и радиусом r
        public static List<Point> FindLineCircleIntersection(
            double x1, double y1, double x2, double y2,
            double cx, double cy, double r)
        {
            if (r <= 0)
                throw new ArgumentException("Радиус окружности должен быть положительным числом.");

            if (Math.Abs(x1 - x2) < EPSILON && Math.Abs(y1 - y2) < EPSILON)
                throw new ArgumentException("Точки прямой должны быть различными.");

            List<Point> intersections = new List<Point>();

            // Переносим систему координат так, чтобы центр окружности был в начале
            double dx = x2 - x1;
            double dy = y2 - y1;
            double fx = x1 - cx;
            double fy = y1 - cy;

            // Решаем квадратное уравнение At² + Bt + C = 0
            double a = dx * dx + dy * dy;
            double b = 2 * (fx * dx + fy * dy);
            double c = fx * fx + fy * fy - r * r;

            double discriminant = b * b - 4 * a * c;

            if (discriminant < -EPSILON)
            {
                return intersections;
            }

            if (Math.Abs(discriminant) < EPSILON)
            {
                double t = -b / (2 * a);
                double px = x1 + t * dx;
                double py = y1 + t * dy;
                intersections.Add(new Point(px, py));
            }
            else
            {
                double sqrtD = Math.Sqrt(discriminant);
                double t1 = (-b + sqrtD) / (2 * a);
                double t2 = (-b - sqrtD) / (2 * a);

                double px1 = x1 + t1 * dx;
                double py1 = y1 + t1 * dy;
                intersections.Add(new Point(px1, py1));

                double px2 = x1 + t2 * dx;
                double py2 = y1 + t2 * dy;
                intersections.Add(new Point(px2, py2));
            }

            return intersections;
        }

        // Метод для нахождения точек пересечения двух окружностей
        // Первая окружность: центр (cx1, cy1), радиус r1
        // Вторая окружность: центр (cx2, cy2), радиус r2
        public static List<Point> FindCircleCircleIntersection(
            double cx1, double cy1, double r1,
            double cx2, double cy2, double r2)
        {
            if (r1 <= 0 || r2 <= 0)
                throw new ArgumentException("Радиусы окружностей должны быть положительными числами.");

            List<Point> intersections = new List<Point>();

            // Расстояние между центрами окружностей
            double d = Math.Sqrt((cx2 - cx1) * (cx2 - cx1) + (cy2 - cy1) * (cy2 - cy1));

            // Окружности не пересекаются (слишком далеко друг от друга)
            if (d > r1 + r2 + EPSILON)
            {
                return intersections;
            }

            // Одна окружность внутри другой
            if (d < Math.Abs(r1 - r2) - EPSILON)
            {
                return intersections;
            }

            // Окружности совпадают
            if (d < EPSILON && Math.Abs(r1 - r2) < EPSILON)
            {
                throw new InvalidOperationException("Окружности совпадают (бесконечное количество точек пересечения).");
            }

            // Вычисляем расстояние от первого центра до линии пересечения
            double a = (r1 * r1 - r2 * r2 + d * d) / (2 * d);

            // Координаты точки на линии, соединяющей центры
            double px = cx1 + a * (cx2 - cx1) / d;
            double py = cy1 + a * (cy2 - cy1) / d;

            // Высота от линии центров до точек пересечения
            double h2 = r1 * r1 - a * a;

            if (h2 < -EPSILON)
            {
                return intersections;
            }

            if (Math.Abs(h2) < EPSILON)
            {
                intersections.Add(new Point(px, py));
            }
            else
            {
                double h = Math.Sqrt(h2);

                double ix1 = px + h * (cy2 - cy1) / d;
                double iy1 = py - h * (cx2 - cx1) / d;
                intersections.Add(new Point(ix1, iy1));

                double ix2 = px - h * (cy2 - cy1) / d;
                double iy2 = py + h * (cx2 - cx1) / d;
                intersections.Add(new Point(ix2, iy2));
            }

            return intersections;
        }

        // Вспомогательный метод для проверки валидности координат
        public static void ValidateCoordinates(params double[] values)
        {
            foreach (var value in values)
            {
                if (double.IsNaN(value) || double.IsInfinity(value))
                {
                    throw new ArgumentException("Координаты должны быть конечными числами.");
                }
            }
        }
    }
}
