using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BlazorNativeIssueRepro
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            TestMethods();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }

        public static unsafe void TestMethods()
        {
            try
            {
                var buffer = TestCase.GetBuffer64Ret();
                Console.WriteLine($"BufferRet: {buffer[0]}");
                Console.WriteLine($"BufferRet: {buffer[7]}");
                Console.WriteLine($"BufferRet: {buffer[63]}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BufferRet: {ex}");
            }

            try
            {
                Buffer64 buffer = default;
                TestCase.GetBuffer64PRef(&buffer);
                Console.WriteLine($"GetBufferPRef: {buffer[0]}");
                Console.WriteLine($"GetBufferPRef: {buffer[7]}");
                Console.WriteLine($"GetBufferPRef: {buffer[63]}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetBufferPRef: {ex}");
            }
        }
    }

    [InlineArray(64)]
    public struct Buffer64
    {
        private byte _element0; 
    }

    public static unsafe partial class TestCase
    {
        [LibraryImport("testcode", EntryPoint = "GetBuffer64Ret")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial Buffer64 GetBuffer64Ret();

        [LibraryImport("testcode", EntryPoint = "GetBuffer64PRef")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial void GetBuffer64PRef(Buffer64* buffer);
    }
}
