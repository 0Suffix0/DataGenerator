using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator_Core
{
    interface IGenerator
    {
        public int NumberRange(int start, int end);
        public float FloatRange(float start, float end);
        public string String(int start, int end);
        public string Name();
        public string City();
        public string Country();
        public DateTime DateTime(DateTime start, DateTime end);
    }
}
