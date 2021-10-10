using MandelbrotGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MandelbrotGenerator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GeneratorController : BaseController
    {
        private readonly ILogger<GeneratorController> _logger;

        public GeneratorController(ILogger<GeneratorController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult Get([FromBody] CallingParam param)
        {
            if (param == null) return BadRequest();

            try
            {
                var bmp = Fractal(param.Window, param.Domain, param.MaxIteration, param.SmoothColor, CalcMandelbrot);

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getdefault")]
        public IActionResult GetDefault()
        {
            var param = new CallingParam
            {
                MaxIteration = 500,
                SmoothColor = false,
                Window = new WindowBoundary
                {
                    MinX = 0,
                    MaxX = 800,
                    MinY = 0,
                    MaxY = 800
                },
                Domain = new DomainBoundary
                {
                    MinY = -1.31,
                    MaxY = 0.1,
                    MinX = -1.7,
                    MaxX = -0.7
                }
            };

            try
            {
                var bmp = FractalWithSeed(param.Window, param.Domain, param.MaxIteration, param.SmoothColor, 
                    new Complex
                    {
                        Real = -1.0,
                        Imaginary = -2.0
                    }, 
                    CalcMandelbrot);

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getmandelbrot")]
        public IActionResult GetMandelbrot([FromBody] CallingParam param)
        {
            if (param == null) return BadRequest();

            try
            {
                var bmp = Fractal(param.Window, param.Domain, param.MaxIteration, param.SmoothColor, CalcMandelbrot);

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("gettriplemandelbrot")]
        public IActionResult GetTripleMandelbrot([FromBody] CallingParam param)
        {
            if (param == null) return BadRequest();

            try
            {
                var bmp = Fractal(param.Window, param.Domain, param.MaxIteration, param.SmoothColor, CalcTrippleMandelbrot);

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getquadruplemandelbrot")]
        public IActionResult GetQuadrupleMandelbrot([FromBody] CallingParam param)
        {
            if (param == null) return BadRequest();

            try
            {
                var bmp = Fractal(param.Window, param.Domain, param.MaxIteration, param.SmoothColor, CalcQuadruppleMandelbrot);

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getfivefoldmandelbrot")]
        public IActionResult GetFiveFoldMandelbrot([FromBody] CallingParam param)
        {
            if (param == null) return BadRequest();

            try
            {
                var bmp = Fractal(param.Window, param.Domain, param.MaxIteration, param.SmoothColor, CalcFiveFoldMandelbrot);

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getsixfoldmandelbrot")]
        public IActionResult GetSixFoldMandelbrot([FromBody] CallingParam param)
        {
            if (param == null) return BadRequest();

            try
            {
                var bmp = Fractal(param.Window, param.Domain, param.MaxIteration, param.SmoothColor, CalcSixFoldMandelbrot);

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
            catch
            {
                return BadRequest();
            }
        }

        private Bitmap Fractal(WindowBoundary window, DomainBoundary domain, int iterMax, bool smoothColor, Func<Complex, Complex, Complex> func)
        {
            var escapeMap = GetNumberIterations(window, domain, iterMax, func);

            var bmp = new Bitmap(window.MaxX - window.MinX, window.MaxY - window.MinY, PixelFormat.Format24bppRgb);

            var k = 0;
            using (var g = Graphics.FromImage(bmp))
            {
                for (int i = window.MinY; i < window.MaxY; ++i)
                {
                    for (int j = window.MinX; j < window.MaxX; ++j)
                    {
                        int n = escapeMap[k];

                        var rgb = smoothColor ? GetRgbSmooth(n, iterMax) : GetRgbPiecewiseLinear(n, iterMax);
                        bmp.SetPixel(j, i, Color.FromArgb(255, rgb.Item1, rgb.Item2, rgb.Item3));

                        k++;
                    }
                }
            }

            return bmp;
        }

        private Bitmap FractalWithSeed(WindowBoundary window, DomainBoundary domain, int iterMax, bool smoothColor, Complex seed, Func<Complex, Complex, Complex> func)
        {
            var escapeMap = GetNumberIterationsWithSeed(window, domain, iterMax, seed, func);

            var bmp = new Bitmap(window.MaxX - window.MinX, window.MaxY - window.MinY, PixelFormat.Format24bppRgb);

            var k = 0;
            using (var g = Graphics.FromImage(bmp))
            {
                for (int i = window.MinY; i < window.MaxY; ++i)
                {
                    for (int j = window.MinX; j < window.MaxX; ++j)
                    {
                        int n = escapeMap[k];

                        var rgb = smoothColor ? GetRgbSmooth(n, iterMax) : GetRgbPiecewiseLinear(n, iterMax);
                        bmp.SetPixel(j, i, Color.FromArgb(255, rgb.Item1, rgb.Item2, rgb.Item3));

                        k++;
                    }
                }
            }

            return bmp;
        }

        private Complex CalcMandelbrot(Complex z, Complex c)
        {
            return (z * z) + c;
        }

        private Complex CalcTrippleMandelbrot(Complex z, Complex c)
        {
            return (z * z * z) + c;
        }

        private Complex CalcQuadruppleMandelbrot(Complex z, Complex c)
        {
            return (z * z * z * z) + c;
        }

        private Complex CalcFiveFoldMandelbrot(Complex z, Complex c)
        {
            return (z * z * z * z * z) + c;
        }

        private Complex CalcSixFoldMandelbrot(Complex z, Complex c)
        {
            return (z * z * z * z * z * z) + c;
        }

        private int Escape(Complex c, int iter_max, Func<Complex, Complex, Complex> func)
        {
            var z = new Complex(0.0, 0.0);
            int iter = 0;

            while (z.Abs() < 2.0 && iter < iter_max)
            {
                z = func(z, c);
                iter++;
            }

            return iter;
        }

        private Complex Scale(WindowBoundary window, DomainBoundary domain, Complex c)
        {
            var aux = new Complex(
                c.Real / (double)((window.MaxX - window.MinX) * (domain.MaxX - domain.MinX) + domain.MinX),
                c.Imaginary / (double)((window.MaxY - window.MinY) * (domain.MaxY - domain.MinY) + domain.MinY));
            return aux;
        }

        private IDictionary<int, int> GetNumberIterations(WindowBoundary window, DomainBoundary domain, int iterMax, Func<Complex, Complex, Complex> func)
        {
            var escapeMap = new Dictionary<int, int>();

            int k = 0;
            for (int i = window.MinY; i < window.MaxY; ++i)
            {
                for (int j = window.MinX; j < window.MaxX; ++j)
                {
                    var c = new Complex(j, i);
                    c = Scale(window, domain, c);
                    escapeMap[k] = Escape(c, iterMax, func);
                    k++;
                }
            }

            return escapeMap;
        }

        private IDictionary<int, int> GetNumberIterationsWithSeed(WindowBoundary window, DomainBoundary domain, int iterMax, Complex seed, Func<Complex, Complex, Complex> func)
        {
            var escapeMap = new Dictionary<int, int>();

            int k = 0;
            for (int i = window.MinY; i < window.MaxY; ++i)
            {
                for (int j = window.MinX; j < window.MaxX; ++j)
                {
                    var c = new Complex(j, i) + seed;
                    c = Scale(window, domain, c);
                    escapeMap[k] = Escape(c, iterMax, func);
                    k++;
                }
            }

            return escapeMap;
        }

        private (byte, byte, byte) GetRgbPiecewiseLinear(int n, int iterMax)
        {
            int N = 256; // colors per element
            int N3 = N * N * N;
            // map n on the 0..1 interval (real numbers)
            double t = (double)n / (double)iterMax;
            // expand n on the 0 .. 256^3 interval (integers)
            n = (int)(t * (double)N3);

            int b = n / (N * N);
            int nn = n - b * N * N;
            int r = nn / N;
            int g = nn - r * N;

            return ((byte)r, (byte)g, (byte)b);
        }

        private (byte, byte, byte) GetRgbSmooth(int n, int iterMax)
        {
            // map n on the 0..1 interval
            double t = (double)n / (double)iterMax;

            // Use smooth polynomials for r, g, b
            int r = (int)(9 * (1 - t) * t * t * t * 255);
            int g = (int)(15 * (1 - t) * (1 - t) * t * t * 255);
            int b = (int)(8.5 * (1 - t) * (1 - t) * (1 - t) * t * 255);

            return ((byte)r, (byte)g, (byte)b);
        }
    }
}
