using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xalami.ExtensionMethods
{
    public static class LocalTaskExtensions
    {
        /// <summary>
        /// Returns a <see cref="Task"/> that has already completed. Useful
        /// if you have a method that returns Task, but no async calls inside
        /// and do not wish to mark the method as async.
        /// Functionally equivalent to .NET 4.6's Task.CompletedTask.
        /// </summary>
        /// <returns></returns>
        public static Task CompletedTask => Task.FromResult<byte>(0);
    }
}
