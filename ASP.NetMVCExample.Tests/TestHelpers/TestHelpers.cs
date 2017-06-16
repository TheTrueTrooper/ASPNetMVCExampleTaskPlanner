using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ASP.NetMVCExample.Tests.TestHelpers
{
    /// <summary>
    /// an example of testing units custom asserts
    /// Note these ones populate the 
    /// </summary>
    static class TestHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ObjectToTest"></param>
        /// <param name="TestName"></param>
        /// <param name="FailedMessage"></param>
        /// <param name="ObjectName"></param>
        [DebuggerHidden]
        public static void IsNotNull<T>(T ObjectToTest, string TestName = null, string FailedMessage = null, string ObjectName = null)
        {
            ObjectName = ObjectName != null ? "named " + ObjectName : typeof(T).Name;
            TestName = TestName ?? "Is Not Null test on " + ObjectName + ".";
            FailedMessage = FailedMessage ?? "Object " + ObjectName + " is Null.";
            Console.WriteLine("Starting " + TestName + ".");
            if (ObjectToTest == null)
                throw new AssertFailedException(FailedMessage);
            //Assert.IsNotNull(ObjectToTest, FailedMessage + " Test " + TestName + " failed with a object " + ObjectName + " that was null in the " + CallerName + " method at line " + CallerLine + ".");
            Console.WriteLine("Completed the test.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="J"></typeparam>
        /// <param name="ObjectToTest1"></param>
        /// <param name="ObjectToTest2"></param>
        /// <param name="TestName"></param>
        /// <param name="FailedMessage"></param>
        /// <param name="ObjectName1"></param>
        /// <param name="ObjectName2"></param>
        [DebuggerHidden]
        public static void AreEqual<J>(J ObjectToTest1, J ObjectToTest2, string TestName = null, string FailedMessage = null, string ObjectName1 = null, string ObjectName2 = null)
        {
            ObjectName1 = ObjectName1 != null ? "named " + ObjectName1 : typeof(J).Name;
            ObjectName2 = ObjectName2 != null ? "named " + ObjectName2 : typeof(J).Name;
            TestName = TestName ?? "Are equal on " + ObjectName1 + " and " + ObjectName2 + ".";
            FailedMessage = FailedMessage ?? "Objects are not equal" + ObjectName1 + "<" + ObjectToTest1 + "> != <" + ObjectToTest1 + ">.";
            Console.WriteLine("Starting " + TestName + ".");
            if ((ObjectToTest1 is IComparable && ((IComparable)ObjectToTest1).CompareTo(ObjectToTest2) != 0) || ObjectToTest1.Equals(ObjectName2))
                throw new AssertFailedException(FailedMessage);
            //Assert.IsNotNull(ObjectToTest, FailedMessage + " Test " + TestName + " failed with a object " + ObjectName + " that was null in the " + CallerName + " method at line " + CallerLine + ".");
            Console.WriteLine("Completed the test.");
        }

    }
}
