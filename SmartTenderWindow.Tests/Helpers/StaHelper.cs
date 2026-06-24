using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace SmartTenderWindow.Tests.Helpers
{
    internal static class StaHelper
    {
        /// <summary>
        /// Runs <paramref name="action"/> on an STA thread and rethrows any exception
        /// on the calling thread, preserving the original stack trace.
        /// </summary>
        internal static void Run(Action action)
        {
            ExceptionDispatchInfo captured = null;

            var thread = new Thread(() =>
            {
                try { action(); }
                catch (Exception ex) { captured = ExceptionDispatchInfo.Capture(ex); }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            captured?.Throw();
        }
    }
}
