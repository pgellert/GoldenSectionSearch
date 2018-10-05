using System;

namespace GramophoneOptimization
{
	class MainClass
	{
		const double r = 6.5;
		const double R = 16;
		const double l = 24;
		const double precision = 0.000001;

		static double phi(double d, double x) {
			return Math.Asin((l * l + x * x - d * d) / (2 * l * x));
			//return Math.Acos((d * d - l * l - x * x) / (2 * l * x));
		}

		static double deltaPhi(double d)
		{
			double max = phi(d,new GSSearch(r, R, x => phi(d, x), precision).search());
			double min = Math.Min(phi(d, r), phi(d, R));
			return max - min;
		}

		public static void Main(string[] args)
		{
			double d_optimal = new GSSearch(Math.Max(R, l - r), l + r, d => -deltaPhi(d), precision).search();
			double theta = (phi(d_optimal, r) + phi(d_optimal, R)) / 2.0;

			Console.WriteLine("d = {0}", Math.Round(d_optimal, 5));
			Console.WriteLine("Theta = {0}", Math.Round(theta, 5));
		}

		public class GSSearch
		{
			public double PRECISION;
			public static double phi = ((Math.Sqrt(5) - 1) / 2);
			Func<double, double> f;
			double start, x, y, end, fX, fY;

			public GSSearch(double start, double end, Func<double, double> func, double precision)
			{
				this.start = start;
				this.end = end;

				this.f = func;

				this.x = end - (end - start) * phi;
				this.y = end - (x - start);

				this.fX = f(x);
				this.fY = f(y);

				this.PRECISION = precision;
			}

			public double search()
			{
				while (2 * PRECISION <= end - start)
				{
					// In case the two function values are equal, we pick the interval in the middle
					if (fX == fY)
					{
						this.start = x;
						this.end = y;

						this.x = end - (end - start) * phi;
						this.y = end - (x - start);
						this.fX = f(x);
						this.fY = f(y);
					}
					// If the function value on the left is grater, the mode must be in the (start,y) interval
					else if (fX > fY)
					{
						this.end = y;

						this.y = x;
						this.fY = fX;

						this.x = start + end - y;
						this.fX = f(x);
					}
					// If the function value on the right is grater, the mode must be in the (x,end) interval
					else {
						this.start = x;

						this.x = y;
						this.fX = fY;

						this.y = end - (x - start);
						this.fY = f(y);
					}
				}
				return x;
			}
		}
	}
}

