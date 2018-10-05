using System;

namespace GoldenSectionSearch
{
	class MainClass
	{
		// The function according to the task
		public static double F(double x)
		{
			return (-4.0) * Math.Pow(x, 4.0) + 1.0 + x + Math.Pow(x, 2.0);
		}


		public static void Main(string[] args)
		{
			GSSearch s = new GSSearch(0, 1, F, 0.0001);
			double result = s.search();

			Console.WriteLine("The maximum value of the function at {0} is {1}.",result, F(result));

			Console.ReadKey();
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
