using System.Threading;

namespace UniCore.Extensions
{
    public static class CancellationTokenSourceExtensions
    {
        public static void CancelAndDispose(this CancellationTokenSource token)
        {
            if (token == null)
            {
                return;
            }

            // Try/catch needed in case the token has already been disposed,
            // and there is no easy way to check if already disposed
            // without reflection or token class inheritance.

            try
            {
                token.Cancel();
                token.Dispose();
            }
            catch { }
        }
    }
}
