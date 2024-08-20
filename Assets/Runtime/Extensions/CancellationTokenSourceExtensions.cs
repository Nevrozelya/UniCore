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

            // Try/catch needed in case the token has already been cancelled/disposed,
            // and there is no easy way to check if already cancelled/disposed
            // without reflection or token class inheritance.

            // We decided to separator Cancel() and Dispose()
            // in two separate try/catch blocks so a failed Cancel()
            // doesn't prevent Dispose() from happening!

            try
            {
                token.Cancel();
            }
            catch { }

            try
            {
                token.Dispose();
            }
            catch { }
        }

        public static CancellationTokenSource Renew(this CancellationTokenSource token)
        {
            token.CancelAndDispose();
            return new CancellationTokenSource();
        }
    }
}
