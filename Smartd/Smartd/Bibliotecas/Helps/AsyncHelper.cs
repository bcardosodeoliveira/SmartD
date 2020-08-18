using System.Threading.Tasks;

namespace Smartd.Bibliotecas.Helps
{
    /// <summary>
    /// Provides a simple functionality to avoid deadlocks in the libraries code.
    /// </summary>
    public static class AsyncHelper
    {
        /// <summary>
        /// Allows to forget about synchronization context. Just call this method at the beginning of your async method.
        /// </summary>
        /// <returns></returns>
        public static async Task Wait(int timeout = 200) => await Task.Delay(timeout);
    }
}
