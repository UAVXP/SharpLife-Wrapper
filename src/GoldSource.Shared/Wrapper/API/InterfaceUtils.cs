/***
*
*	Copyright (c) 1996-2001, Valve LLC. All rights reserved.
*	
*	This product contains software technology licensed from Id 
*	Software, Inc. ("Id Technology").  Id Technology (c) 1996 Id Software, Inc. 
*	All Rights Reserved.
*
*   This source code contains proprietary and confidential information of
*   Valve LLC and its suppliers.  Access to this code is restricted to
*   persons who have executed a written SDK license with Valve.  Any access,
*   use or distribution of this code by or to any unlicensed person is illegal.
*
****/

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace GoldSource.Shared.Wrapper.API
{
    public static class InterfaceUtils
    {
        //All interfaces are either public or internal
        private const BindingFlags SharedBindingFlags = BindingFlags.Public | BindingFlags.NonPublic;

        private const BindingFlags InterfaceDelegateBindingFlags = SharedBindingFlags;

        //Implementations are static or instance methods
        private const BindingFlags ImplementationBindingFlags = SharedBindingFlags | BindingFlags.Static | BindingFlags.Instance;

        //Fields are instances of delegates
        private const BindingFlags InterfaceFieldBindingFlags = SharedBindingFlags | BindingFlags.Instance;

        private static void DebugLog(string message)
        {
            Logger.Instance.Debug(message);
        }

        private static void ValidateInterfaceData(string prefix, Type iface, object ifaceInstance, Type impl)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            if (iface == null)
            {
                throw new ArgumentNullException(nameof(iface));
            }

            if (ifaceInstance == null)
            {
                throw new ArgumentNullException(nameof(ifaceInstance));
            }

            if (impl == null)
            {
                throw new ArgumentNullException(nameof(impl));
            }

            if (!ifaceInstance.GetType().Equals(iface))
            {
                throw new ArgumentException($"The given interface instance is not of type {iface.FullName} (actual type: {ifaceInstance.GetType().FullName})");
            }
        }

        private static void InternalInitializeField(string prefix, Type iface, object ifaceInstance, Type impl, object implInstance, FieldInfo field)
        {
            var name = field.Name;

            if (!name.StartsWith(prefix))
            {
                throw new InitializeFieldException("Field name does not start with prefix");
            }

            name = name.Substring(prefix.Length);

            DebugLog($"Getting delegate {name}");

            //Get the delegate
            var ifaceDelegate = iface.GetNestedType(name, InterfaceDelegateBindingFlags);

            if (ifaceDelegate == null)
            {
                throw new InitializeFieldException("Interface delegate does not exist");
            }

            if (!field.FieldType.Equals(ifaceDelegate))
            {
                throw new InitializeFieldException($"Field {field.Name} does not have delegate type {ifaceDelegate.FullName}");
            }

            DebugLog($"Getting method {name}");

            //Find the implementation
            var method = impl.GetMethod(name, ImplementationBindingFlags);

            if (method == null)
            {
                throw new InitializeFieldException("Implementation method does not exist");
            }

            if (implInstance == null && !method.IsStatic)
            {
                throw new InitializeFieldException($"Implementation method {method.Name} must be static");
            }

            DebugLog($"Creating delegate {name}");

            //Create the delegate instance
            var implementation = Delegate.CreateDelegate(ifaceDelegate, implInstance, method);

            if (implementation == null)
            {
                throw new InitializeFieldException("Couldn't create delegate instance");
            }

            DebugLog($"Setting value {name}");

            field.SetValue(ifaceInstance, implementation);
        }

        /// <summary>
        /// Initializes a delegate field in an interface with the implementation from the implementation type
        /// </summary>
        /// <param name="prefix">Name prefix for delegate field</param>
        /// <param name="ifaceInstance">Instance of the interface</param>
        /// <param name="impl">Implementation type</param>
        /// <param name="implInstance">Instance of the implementation. null if the interface uses static methods</param>
        /// <param name="field">Field to initialize</param>
        /// <returns>Whether initialization succeeded</returns>
        public static void InitializeField(string prefix, object ifaceInstance, Type impl, object implInstance, FieldInfo field)
        {
            var iface = ifaceInstance.GetType();

            ValidateInterfaceData(prefix, iface, ifaceInstance, impl);

            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            InternalInitializeField(prefix, iface, ifaceInstance, impl, implInstance, field);
        }

        /// <summary>
        /// Initializes all fields of the given interface with implementations from the given implementation
        /// </summary>
        /// <param name="prefix">Name prefix for delegate field</param>
        /// <param name="ifaceInstance">Instance of the interface</param>
        /// <param name="impl">Implementation type</param>
        /// <param name="implInstance">Instance of the implementation. null if the interface uses static methods</param>
        public static void InitializeFields(string prefix, object ifaceInstance, Type impl, object implInstance = null)
        {
            var iface = ifaceInstance.GetType();

            ValidateInterfaceData(prefix, iface, ifaceInstance, impl);

            foreach (var field in iface.GetFields(InterfaceFieldBindingFlags))
            {
                InternalInitializeField(prefix, iface, ifaceInstance, impl, implInstance, field);
            }
        }

        /// <summary>
        /// Result values for <see cref="CopyStringToUnmanagedBuffer"/>
        /// </summary>
        public enum UnmanagedStringResult
        {
            Success = 0,
            Truncated
        }

        /// <summary>
        /// Converts and copies a managed string to an unmanaged UTF8 buffer
        /// </summary>
        /// <param name="str"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferSizeInBytes"></param>
        public static unsafe UnmanagedStringResult CopyStringToUnmanagedBuffer(string str, byte* buffer, int bufferSizeInBytes)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            var utf8String = Encoding.UTF8.GetBytes(str);

            var length = Math.Min(utf8String.Length, bufferSizeInBytes - 1);

            Marshal.Copy(utf8String, 0, new IntPtr(buffer), length);

            //Ensure null termination
            buffer[Math.Max(0, length - 1)] = (byte)'\0';

            return length == utf8String.Length ? UnmanagedStringResult.Success : UnmanagedStringResult.Truncated;
        }

        public static IntPtr AllocateUnmanagedString(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            var utf8String = Encoding.UTF8.GetBytes(str + '\0');

            var nativeMemory = Marshal.AllocHGlobal(utf8String.Length);

            Marshal.Copy(utf8String, 0, nativeMemory, utf8String.Length);

            return nativeMemory;
        }

        public static bool SetupInterface<TIFace, TImpl>(string delegateInstanceNamePrefix, out TIFace pFunctionTable, TImpl implementation)
            where TIFace : class, new()
            where TImpl : class
        {
            pFunctionTable = new TIFace();

            //Automatically initialize all delegates with their respective implementations
            //The format is as follows:
            //Each delegate has a name N
            //Each delegate field instance has a name pfnN
            //Each implementation has a method N
            //For fields with no implementation, null is required
            //This is done automatically because it's an internal wrapper to handle native->managed calls
            //managed->native is done by marshalling the function pointers to delegates when the interface is passed to managed code
            try
            {
                InitializeFields(delegateInstanceNamePrefix, pFunctionTable, typeof(TImpl), implementation);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e, "Error while initializing interface");
                pFunctionTable = null;
                return false;
            }

            return true;
        }
    }
}
