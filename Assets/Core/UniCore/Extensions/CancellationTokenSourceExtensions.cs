using System.Threading;

namespace UniCore.Extensions
{
    public static class CancellationTokenSourceExtensions
    {
        public static void CancelAndDispose(this CancellationTokenSource token)
        {
            if (token != null)
            {
                token.Cancel();
                token.Dispose();
            }
        }
    }
}
