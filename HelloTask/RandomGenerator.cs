using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloTaskApp
{
    class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random;
        private readonly object _syncObject;

        public RandomGenerator()
        {
            _random = new Random();
            _syncObject = new object();
        }

        public double GetRandom()
        {
            lock (_syncObject)
            {
                return _random.NextDouble();
            }
        }
    }
}
