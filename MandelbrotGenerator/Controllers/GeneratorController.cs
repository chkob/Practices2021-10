using MandelbrotGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

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
            if (param.ImageBlockSize <= 0) return BadRequest();

            var imageBlockSize = param.ImageBlockSize;

            if (imageBlockSize < 64) imageBlockSize = 64;

            var now = DateTime.Now;
            var unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var nowTimestamp = (DateTime.UtcNow.Subtract(new DateTime(now.Year, 1, 1))).TotalSeconds;
            var nowMillisecs = now.Millisecond;

            var rng = new Random(nowMillisecs);

            var pixelsize = 2;
            var size = imageBlockSize * pixelsize;
            var width = size;
            var height = size;

            using (var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    var penColor = Color.FromArgb(255, 255, 255, 255);

                    if (param.Colored)
                    {
                        var red = rng.Next(60, 255);
                        var green = rng.Next(60, 255);
                        var blue = rng.Next(60, 255);

                        penColor = Color.FromArgb(255, red, green, blue);
                    }

                    var backgroundColor = Color.FromArgb(255, 30, 30, 30);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(backgroundColor);

                    if (param.ForceBGTransparent)
                    {
                        bmp.MakeTransparent(backgroundColor);
                    }
                }

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
        }

        [HttpGet("getdefault")]
        public IActionResult GetDefault()
        {
            var param = new CallingParam
            {
                ImageBlockSize = 200,
                Colored = true
            };

            var imageBlockSize = param.ImageBlockSize;

            if (imageBlockSize < 64) imageBlockSize = 64;

            var now = DateTime.Now;
            var unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var nowTimestamp = (DateTime.UtcNow.Subtract(new DateTime(now.Year, 1, 1))).TotalSeconds;
            var nowMillisecs = now.Millisecond;

            var rng = new Random(nowMillisecs);

            var pixelsize = 2;
            var size = imageBlockSize * pixelsize;
            var width = size;
            var height = size;

            using (var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    var penColor = Color.FromArgb(255, 255, 255, 255);

                    if (param.Colored)
                    {
                        var red = rng.Next(60, 255);
                        var green = rng.Next(60, 255);
                        var blue = rng.Next(60, 255);

                        penColor = Color.FromArgb(255, red, green, blue);
                    }

                    var backgroundColor = Color.FromArgb(255, 30, 30, 30);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(backgroundColor);

                    if (param.ForceBGTransparent)
                    {
                        bmp.MakeTransparent(backgroundColor);
                    }
                }

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
        }

        [HttpGet("getmandelbrot")]
        public IActionResult GetMandelbrot([FromBody] CallingParam param)
        {
            if (param == null) return BadRequest();
            if (param.ImageBlockSize <= 0) return BadRequest();

            var imageBlockSize = param.ImageBlockSize;

            if (imageBlockSize < 64) imageBlockSize = 64;

            var now = DateTime.Now;
            var unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var nowTimestamp = (DateTime.UtcNow.Subtract(new DateTime(now.Year, 1, 1))).TotalSeconds;
            var nowMillisecs = now.Millisecond;

            var rng = new Random(nowMillisecs);

            var pixelsize = 2;
            var size = imageBlockSize * pixelsize;
            var width = size;
            var height = size;

            using (var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    var penColor = Color.FromArgb(255, 255, 255, 255);

                    if (param.Colored)
                    {
                        var red = rng.Next(60, 255);
                        var green = rng.Next(60, 255);
                        var blue = rng.Next(60, 255);

                        penColor = Color.FromArgb(255, red, green, blue);
                    }

                    var backgroundColor = Color.FromArgb(255, 30, 30, 30);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(backgroundColor);

                    if (param.ForceBGTransparent)
                    {
                        bmp.MakeTransparent(backgroundColor);
                    }
                }

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
        }

        [HttpGet("gettriplemandelbrot")]
        public IActionResult GetTripleMandelbrot([FromBody] CallingParam param)
        {
            if (param == null) return BadRequest();
            if (param.ImageBlockSize <= 0) return BadRequest();

            var imageBlockSize = param.ImageBlockSize;

            if (imageBlockSize < 64) imageBlockSize = 64;

            var now = DateTime.Now;
            var unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var nowTimestamp = (DateTime.UtcNow.Subtract(new DateTime(now.Year, 1, 1))).TotalSeconds;
            var nowMillisecs = now.Millisecond;

            var rng = new Random(nowMillisecs);

            var pixelsize = 2;
            var size = imageBlockSize * pixelsize;
            var width = size;
            var height = size;

            using (var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    var penColor = Color.FromArgb(255, 255, 255, 255);

                    if (param.Colored)
                    {
                        var red = rng.Next(60, 255);
                        var green = rng.Next(60, 255);
                        var blue = rng.Next(60, 255);

                        penColor = Color.FromArgb(255, red, green, blue);
                    }

                    var backgroundColor = Color.FromArgb(255, 30, 30, 30);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(backgroundColor);

                    if (param.ForceBGTransparent)
                    {
                        bmp.MakeTransparent(backgroundColor);
                    }
                }

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
        }

        [HttpGet("getquadruplemandelbrot")]
        public IActionResult GetQuadrupleMandelbrot([FromBody] CallingParam param)
        {
            if (param == null) return BadRequest();
            if (param.ImageBlockSize <= 0) return BadRequest();

            var imageBlockSize = param.ImageBlockSize;

            if (imageBlockSize < 64) imageBlockSize = 64;

            var now = DateTime.Now;
            var unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var nowTimestamp = (DateTime.UtcNow.Subtract(new DateTime(now.Year, 1, 1))).TotalSeconds;
            var nowMillisecs = now.Millisecond;

            var rng = new Random(nowMillisecs);

            var pixelsize = 2;
            var size = imageBlockSize * pixelsize;
            var width = size;
            var height = size;

            using (var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    var penColor = Color.FromArgb(255, 255, 255, 255);

                    if (param.Colored)
                    {
                        var red = rng.Next(60, 255);
                        var green = rng.Next(60, 255);
                        var blue = rng.Next(60, 255);

                        penColor = Color.FromArgb(255, red, green, blue);
                    }

                    var backgroundColor = Color.FromArgb(255, 30, 30, 30);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(backgroundColor);

                    if (param.ForceBGTransparent)
                    {
                        bmp.MakeTransparent(backgroundColor);
                    }
                }

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
        }

        [HttpGet("getfivefoldmandelbrot")]
        public IActionResult GetFiveFoldMandelbrot([FromBody] CallingParam param)
        {
            if (param == null) return BadRequest();
            if (param.ImageBlockSize <= 0) return BadRequest();

            var imageBlockSize = param.ImageBlockSize;

            if (imageBlockSize < 64) imageBlockSize = 64;

            var now = DateTime.Now;
            var unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var nowTimestamp = (DateTime.UtcNow.Subtract(new DateTime(now.Year, 1, 1))).TotalSeconds;
            var nowMillisecs = now.Millisecond;

            var rng = new Random(nowMillisecs);

            var pixelsize = 2;
            var size = imageBlockSize * pixelsize;
            var width = size;
            var height = size;

            using (var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    var penColor = Color.FromArgb(255, 255, 255, 255);

                    if (param.Colored)
                    {
                        var red = rng.Next(60, 255);
                        var green = rng.Next(60, 255);
                        var blue = rng.Next(60, 255);

                        penColor = Color.FromArgb(255, red, green, blue);
                    }

                    var backgroundColor = Color.FromArgb(255, 30, 30, 30);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(backgroundColor);

                    if (param.ForceBGTransparent)
                    {
                        bmp.MakeTransparent(backgroundColor);
                    }
                }

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
        }

        [HttpGet("getsixfoldmandelbrot")]
        public IActionResult GetSixFoldMandelbrot([FromBody] CallingParam param)
        {
            if (param == null) return BadRequest();
            if (param.ImageBlockSize <= 0) return BadRequest();

            var imageBlockSize = param.ImageBlockSize;

            if (imageBlockSize < 64) imageBlockSize = 64;

            var now = DateTime.Now;
            var unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var nowTimestamp = (DateTime.UtcNow.Subtract(new DateTime(now.Year, 1, 1))).TotalSeconds;
            var nowMillisecs = now.Millisecond;

            var rng = new Random(nowMillisecs);

            var pixelsize = 2;
            var size = imageBlockSize * pixelsize;
            var width = size;
            var height = size;

            using (var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    var penColor = Color.FromArgb(255, 255, 255, 255);

                    if (param.Colored)
                    {
                        var red = rng.Next(60, 255);
                        var green = rng.Next(60, 255);
                        var blue = rng.Next(60, 255);

                        penColor = Color.FromArgb(255, red, green, blue);
                    }

                    var backgroundColor = Color.FromArgb(255, 30, 30, 30);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(backgroundColor);

                    if (param.ForceBGTransparent)
                    {
                        bmp.MakeTransparent(backgroundColor);
                    }
                }

                var ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);

                return new FileContentResult(ms.ToArray(), "image/png");
            }
        }
    }
}
