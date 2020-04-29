using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace BuzzLockGui.Backend.Native
{
    class Libc
    {
        internal const int AF_BLUETOOTH = 31;
        internal const int SOCK_RAW = 3;
        internal const int BTPROTO_L2CAP = 0;
        internal const int SHUT_RDWR = 2;

        [DllImport("libc", SetLastError = true, ExactSpelling = true)]
        internal static extern int socket(int domain, int type, int protocol);
        [DllImport("libc", SetLastError = true, ExactSpelling = true)]
        internal static extern int connect(int sockfd, ref Libbluetooth.sockaddr_l2 addr, uint addrlen);
        [DllImport("libc", SetLastError = true, ExactSpelling = true)]
        internal static extern int recv(int sockfd, [Out] byte[] buf, uint len, int flags);
        [DllImport("libc", SetLastError = true, ExactSpelling = true)]
        internal static extern int shutdown(int sockfd, int how);
        [DllImport("libc", SetLastError = true, ExactSpelling = true)]
        internal static extern int close(int fd);
        [DllImport("libc", ExactSpelling = true)]
        internal static extern IntPtr strerror(int errnum);

        /// <summary>
        /// Emulates the behavior of perror(3).
        /// </summary>
        internal static void perror(string message)
        {
            Console.Error.WriteLine(GetErrorString(message));
        }

        /// <summary>
        /// Emulates the behavior of perror(3), but instead of printing, throws
        /// an <see cref="IOException"/>.
        /// </summary>
        /// <param name="message">The message for the error.</param>
        /// <exception cref="IOException">Always thrown.</exception>
        internal static void ThrowError(string message)
        {
            throw new IOException(GetErrorString(message));
        }

        /// <summary>
        /// Emulates the behavior of perror(3), but instead of printing,
        /// returns the string generated.
        /// </summary>
        /// <param name="message">The message for the error.</param>
        /// <returns>The full error string.</returns>
        internal static string GetErrorString(string message)
        {
            int errorCode = GetErrno();
            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(message))
            {
                stringBuilder.Append(message);
                stringBuilder.Append(": ");
            }

            string errorString = Marshal.PtrToStringAuto(strerror(errorCode));
            stringBuilder.Append(errorString);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns the last C error code (<c>errno</c>).
        /// </summary>
        /// <returns>The value of <c>errno</c>.</returns>
        internal static int GetErrno()
        {
            return Marshal.GetLastWin32Error();
        }
    }
}
