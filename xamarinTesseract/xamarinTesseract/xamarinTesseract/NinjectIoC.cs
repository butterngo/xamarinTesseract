using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace xamarinTesseract
{
    public static class NinjectIoC
    {
        public static StandardKernel Container { get; set; }

        public static void Initialize()
        {
            var kernel = new StandardKernel();
            //To to
            NinjectIoC.Container = kernel;
        }
    }
}
