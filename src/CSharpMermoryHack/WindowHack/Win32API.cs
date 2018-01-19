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

        #region 得到目标进程句柄的函数
        /// <summary>
        /// 得到目标进程句柄的函数
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
        [DllImport("USER32.DLL")]
        public extern static int GetWindowThreadProcessId(int hwnd, ref int lpdwProcessId);
        [DllImport("USER32.DLL")]
        public extern static int GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessId); 
        #endregion

        #region 打开进程
        /// <summary>
        /// 打开进程
        /// </summary>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="bInheritHandle"></param>
        /// <param name="dwProcessId"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public extern static int OpenProcess(int dwDesiredAccess, int bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        public extern static IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId); 
        #endregion

        #region 关闭句柄的函数
        /// <summary>
        /// 关闭句柄的函数
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
        public static extern int CloseHandle(int hObject); 
        #endregion

        #region 读内存
        /// <summary>
        /// 读内存
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        /// <param name="lpNumberOfBytesWritten"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll ")]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, int size, out IntPtr lpNumberOfBytesWritten);
        [DllImport("Kernel32.dll ")]
        public static extern Int32 ReadProcessMemory(int hProcess, int lpBaseAddress, ref int buffer,/*byte[] buffer,*/int size, int lpNumberOfBytesWritten);
        /// <summary>
        /// 读内存
        /// </summary>
        /// <param name="hProcess">进程</param>
        /// <param name="lpBaseAddress">要读取的内存地址</param>
        /// <param name="buffer">从上面那个参数地址里读出来的东西(调用这个函数的就是为了它) </param>
        /// <param name="size">长度，上一个参数，类型是int，那个长度应该用4</param>
        /// <param name="lpNumberOfBytesWritten">用0就行了</param>
        /// <returns></returns>
        [DllImport("Kernel32.dll ")]
        public static extern Int32 ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten); 
        #endregion

        #region 写内存
        /// <summary>
        /// 写内存
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        /// <param name="lpNumberOfBytesWritten"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, int size, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern Int32 WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten); 
        #endregion

        #region 创建线程
        /// <summary>
        /// 创建线程
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpThreadAttributes"></param>
        /// <param name="dwStackSize"></param>
        /// <param name="lpStartAddress"></param>
        /// <param name="lpParameter"></param>
        /// <param name="dwCreationFlags"></param>
        /// <param name="lpThreadId"></param>
        /// <returns></returns>
        [DllImport("kernel32", EntryPoint = "CreateRemoteThread")]
        public static extern int CreateRemoteThread(int hProcess, int lpThreadAttributes, int dwStackSize, int lpStartAddress, int lpParameter, int dwCreationFlags, ref int lpThreadId); 
        #endregion

        #region 开辟指定进程的内存空间
        /// <summary>
        /// 开辟指定进程的内存空间
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpAddress"></param>
        /// <param name="dwSize"></param>
        /// <param name="flAllocationType"></param>
        /// <param name="flProtect"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern System.Int32 VirtualAllocEx(System.IntPtr hProcess, System.Int32 lpAddress, System.Int32 dwSize, System.Int16 flAllocationType, System.Int16 flProtect);

        [DllImport("Kernel32.dll")]
        public static extern System.Int32 VirtualAllocEx(int hProcess, int lpAddress, int dwSize, int flAllocationType, int flProtect); 
        #endregion

        #region 释放内存空间
        /// <summary>
        /// 释放内存空间
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpAddress"></param>
        /// <param name="dwSize"></param>
        /// <param name="flAllocationType"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern System.Int32 VirtualFreeEx(int hProcess, int lpAddress, int dwSize, int flAllocationType); 
        #endregion
    }
}
