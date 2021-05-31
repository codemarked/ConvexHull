using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvexHull
{
    public interface HullMethod
    {
        void initialize(List<Point> points);

        void execute();

        bool wasExecuted();

        List<Point> getResult();
    }
}
