using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Microsoft.AspNetCore.Http;
using KonSolutionsApi.API.Middleware;

namespace KonSolutionsApi.Benchmarks
{
    [MemoryDiagnoser]
    public class SecurityHeadersMiddlewareBenchmark
    {
        private SecurityHeadersMiddleware _middleware = null!;
        private DefaultHttpContext _context = null!;

        [GlobalSetup]
        public void Setup()
        {
            static Task next(HttpContext _) => Task.CompletedTask;

            _middleware = new SecurityHeadersMiddleware(next);
            _context = new DefaultHttpContext();

            _context.Response.Headers.Clear();
        }

        [Benchmark]
        public Task InvokeAsync() => _middleware.InvokeAsync(_context);
    }
}