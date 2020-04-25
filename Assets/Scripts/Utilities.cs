using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Utilities
{
	public static bool SampleWithProbability(this Random generator, float p)
	{
		return generator.NextDouble() < p;
	}
}
