using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowHack
{
    class Win32Api
    {
        public const int OPEN_PROCESS_ALL = 2035711;
        public const int PAGE_READWRITE = 4;
        public const int PROCESS_CREATE_THREAD = 2;
        public const int PROCESS_HEAP_ENTRY_BUSY = 4;
        public const int PROCESS_VM_OPERATION = 8;
        public const int PROCESS_VM_READ = 256;
        public const int PROCESS_VM_WRITE = 32;

        private const int PAGE_EXECUTE_READWRITE = 0x4;
        private const int MEM_COMMIT = 4096;
        private const int MEM_RELEASE = 0x8000;
        private const int MEM_DECOMMIT = 0x4000;
        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;




        #region 查找窗体
        /// <summary>
        /// 查找窗体
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        #endregion

        //得到目标进程句柄的函数
        [DllImport("USER32.DLL")]
        public extern static int GetWindowThreadProcessId(
            int hwnd,
            ref int lpdwProcessId
            );
        [DllImport("USER32.DLL")]
        public extern static int GetWindowThreadProcessId(
            IntPtr hwnd,
            ref int lpdwProcessId
            );

        //打开进程
        [DllImport("kernel32.dll")]
        public extern static int OpenProcess(
            int dwDesiredAccess,
            int bInheritHandle,
            int dwProcessId
            );
        [DllImport("kernel32.dll")]
        public extern static IntPtr OpenProcess(
            uint dwDesiredAccess,
            int bInheritHandle,
            uint dwProcessId
            );

        //关闭句柄的函数
        [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
        public static extern int CloseHandle(
            int hObject
            );

        //读内存
        [DllImport("Kernel32.dll ")]
        public static extern Int32 ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [In, Out] byte[] buffer,
            int size,
            out IntPtr lpNumberOfBytesWritten
            );
        [DllImport("Kernel32.dll ")]
        public static extern Int32 ReadProcessMemory(
            int hProcess,
            int lpBaseAddress,
            ref int buffer,
            //byte[] buffer,
            int size,
            int lpNumberOfBytesWritten
            );
        [DllImport("Kernel32.dll ")]
        public static extern Int32 ReadProcessMemory(
            int hProcess,
            int lpBaseAddress,
            byte[] buffer,
            int size,
            int lpNumberOfBytesWritten
            );

        //写内存
        [DllImport("kernel32.dll")]
        public static extern Int32 WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [In, Out] byte[] buffer,
            int size,
            out IntPtr lpNumberOfBytesWritten
            );

        [DllImport("kernel32.dll")]
        public static extern Int32 WriteProcessMemory(
            int hProcess,
            int lpBaseAddress,
            byte[] buffer,
            int size,
            int lpNumberOfBytesWritten
            );

        //创建线程
        [DllImport("kernel32", EntryPoint = "CreateRemoteThread")]
        public static extern int CreateRemoteThread(
            int hProcess,
            int lpThreadAttributes,
            int dwStackSize,
            int lpStartAddress,
            int lpParameter,
            int dwCreationFlags,
            ref int lpThreadId
            );

        //开辟指定进程的内存空间
        [DllImport("Kernel32.dll")]
        public static extern System.Int32 VirtualAllocEx(
         System.IntPtr hProcess,
         System.Int32 lpAddress,
         System.Int32 dwSize,
         System.Int16 flAllocationType,
         System.Int16 flProtect
         );

        [DllImport("Kernel32.dll")]
        public static extern System.Int32 VirtualAllocEx(
        int hProcess,
        int lpAddress,
        int dwSize,
        int flAllocationType,
        int flProtect
        );

        //释放内存空间
        [DllImport("Kernel32.dll")]
        public static extern System.Int32 VirtualFreeEx(
        int hProcess,
        int lpAddress,
        int dwSize,
        int flAllocationType
        );
    }
}
