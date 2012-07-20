using System;
using System.Runtime.InteropServices;
namespace mxcanfuncsharp
{
    /// <summary>
    /// The Moxa CAN Interface Board Function library is a software interface that allows users to access
    /// the CAN controller on the Moxa CAN Interface Board. The library supports up to 4 boards on one PC.
    /// The user can begin developing applications after installing the Moxa CAN Interface Boards and calling the 
    /// library via mxcanfunc.dll to control the CAN controllers in a user space program. The library 
    /// provides a set of structures and functions to process the CAN messages on the CAN Bus Network.
    /// </summary>
    public class MXCANFUNC
    {
        public const UInt16 CNIO_MAX_BOARDS = 4;
        public const UInt16 CNIO_MAX_PORTS_PER_BOARD = 2;
        public const UInt16 CNIO_SUCCESS = 1;

        public const UInt16 CNIO_PORT1_INDEX = 0;
        public const UInt16 CNIO_PORT2_INDEX = 1;

        public const Byte CNIO_STANDARD_FRAME = 0x00;
        public const Byte CNIO_EXTENDED_FRAME = 0x80;
        public const Byte CNIO_ERROR_FRAME = 0x20;
        public const Byte CNIO_RTR = 0x40;
        public const Byte CNIO_SRR = 0x10;
        public const Byte CNIO_DATA_LENGTH0 = 0x00;
        public const Byte CNIO_DATA_LENGTH1 = 0x01;
        public const Byte CNIO_DATA_LENGTH2 = 0x02;
        public const Byte CNIO_DATA_LENGTH3 = 0x03;
        public const Byte CNIO_DATA_LENGTH4 = 0x04;
        public const Byte CNIO_DATA_LENGTH5 = 0x05;
        public const Byte CNIO_DATA_LENGTH6 = 0x06;
        public const Byte CNIO_DATA_LENGTH7 = 0x07;
        public const Byte CNIO_DATA_LENGTH8 = 0x08;

        public const Byte CNIO_OPMODE_LISTEN_ONLY = 0x01;
        public const Byte CNIO_OPMODE_SELF_TEST = 0x02;
        public const Byte CNIO_OPMODE_SINGLE_ACC = 0x00;
        public const Byte CNIO_OPMODE_DUAL_ACC = 0x04;
        public const Byte CNIO_OPMODE_ERROR_FRAME = 0x08;

        public const Byte CNIO_OPT_STANDARD = 0x00;
        public const Byte CNIO_OPT_EXTENDED = 0x01;
        public const Byte CNIO_OPT_ACCEPT_RTR = 0x02;
        public const Byte CNIO_OPT_ACCEPT_RTR_ONLY = 0x04;

        public const Byte CNIO_STATUS_TX_PENDING = 0x20;
        public const Byte CNIO_STATUS_OVRRUN = 0x02;
        public const Byte CNIO_STATUS_ERRLIM = 0x40;
        public const Byte CNIO_STATUS_BUS_OFF = 0x80;
        public const UInt16 CNIO_STATUS_RESET_MODE = 0x0100;

        public const int E_ACCESS_DEVICE_FAILED = -1;
        public const int E_TX_FAILED = -2;
        public const int E_RX_FAILED = -3;
        public const int E_TX_TIMEOUT = -4;
        public const int E_RX_TIMEOUT = -5;
        public const int E_INVALID_ACC_ID_CODE_VALUE = -6;
        public const int E_INVALID_ACC_ID_MASK_VALUE = -7;
        public const int E_INVALID_SIZE_VALUE = -8;
        public const int E_INVALID_OPERATION = -9;
        public const int E_WIN32_FAILED = -10;

        public const Int16 B_10K = 0x311C;
        public const Int16 B_20K = 0x181C;
        public const Int16 B_50K = 0x091C;
        public const Int16 B_100K = 0x041C;
        public const Int16 B_125K = 0x031C;
        public const Int16 B_250K = 0x011C;
        public const Int16 B_500K = 0x001C;
        public const Int16 B_800K = 0x0016;
        public const Int16 B_1000K = 0x0014;

        /// <summary>
        /// This function enumerates the Moxa CAN Interface Boards installed in the system.
        /// </summary>
        /// <param name="dev_info">Retrieve the device information of Moxa CAN Interface Boards installed in the system.</param>
        /// <param name="size">Number of dev_info array size.</param>
        /// <returns>The number of Moxa CAN Interface Boards listed in the system.</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_enum_devices(out CNIO_DEV_INFO dev_info, Int32 size);

        /// <summary>
        /// This function creates a handle to access a specific CAN controller in a Moxa CAN Interface Board.
        /// </summary>
        /// <param name="cnioid_device">Specific CNIOID of Moxa CAN Bus Adaptor. This can be get from CNIO_DEV_INFO by cnio_enum_devices.</param>
        /// <param name="port_index">Zero index of CAN controller on Moxa CAN Interface Board.</param>
        /// <returns>Handle of the specific CAN controller.</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_open(int cnioid_device, Int32 port_index);

        /// <summary>
        /// This function closes the handle of a CAN controller.
        /// </summary>
        /// <param name="hPort">Handle of a CAN controller returned by cnio_open.</param>
        /// <returns>CNIO_SUCCESS</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_close(Int32 hPort);

        /// <summary>
        /// The function resets the CAN controller to default state, then sets configuration parameters to
        ///initialize the CAN controller. Driver buffer will be cleared.
        ///Configuration parameters include operation mode and baud rate.
        /// </summary>
        /// <param name="hPort">handle of CAN controller returned by cnio_open.</param>
        /// <param name="opmode">operation mode.</param>
        /// <param name="wBTR0BTR1">baud rate.</param>
        /// <returns>CNIO_SUCCESS</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_init(Int32 hPort, Byte opmode, Int16 wBTR0BTR1);

        /// <summary>
        /// This function sets the CAN controller to the default and disabling interrupt.
        /// The transmitting and receiving of messages will be canceled, and messages
        /// in the driver buffer will be cleared as well.
        /// </summary>
        /// <param name="hPort">handle of CAN controller returned by cnio_open.</param>
        /// <returns>CNIO_SUCCESS</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_reset(Int32 hPort);

        /// <summary>
        /// This function sets the Acceptance Code and Acceptance Mask of the CAN controller
        /// as message ID format with CNIO_OPMODE_SINGLE_ACC.
        /// The CAN controller must set to reset mode when calling the function.
        /// </summary>
        /// <param name="hPort">handle of CAN controller returned by cnio_open.</param>
        /// <param name="acc_ID_code">message ID like  Acceptance Code.</param>
        /// <param name="acc_ID_mask">message ID like  Acceptance Mask.</param>
        /// <param name="options">options of Acceptance filters.</param>
        /// <returns>CNIO_SUCCESS</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_set_filters(Int32 hPort, UInt32 acc_ID_code, UInt32 acc_ID_mask, Byte options);
        
        /// <summary>
        /// This function sets the Acceptance filters of the CAN controller directly to the
        /// Acceptance Code Register 0 to 3 and Acceptance Mask Register 0 to 3.
        /// The CAN controller must be in reset mode when calling the function.
        /// </summary>
        /// <param name="hPort">handle of CAN controller returned by cnio_open.</param>
        /// <param name="acc_code">Acceptance Code Register 0 to 3.</param>
        /// <param name="acc_mask">Acceptance Mask Register 0 to 3.</param>
        /// <returns>CNIO_SUCCESS</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_set_filters_ex(Int32 hPort, UInt32 acc_code, UInt32 acc_mask);

        /// <summary>
        /// This function sets the CAN controller operation mode and enables interrupt.
        /// </summary>
        /// <param name="hPort">handle of CAN controller returned by cnio_open.</param>
        /// <returns>CNIO_SUCCESS</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_start(Int32 hPort);

        /// <summary>
        /// This function sets the CAN controller to reset mode after disabling interrupt.
        /// The transmitting and receiving of messages will be canceled, but messages
        /// in the received buffer will still be available. Call cnio_receive_queue to get 
        /// the number of messages in the received buffer.
        /// </summary>
        /// <param name="hPort">handle of CAN Controller returned by cnio_open.</param>
        /// <returns>CNIO_SUCCESS</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_stop(Int32 hPort);

        /// <summary>
        /// This function sends a CAN message with or without block operation.
        /// 1. With timeout value of 0, the function will not be blocked and return immediately, whether the
        ///     transmission is complete or not.
        /// 2. With timeout value between 0 and INFINITE, a CAN message sends successfully within a time interval of timeout,
        ///     otherwise the transmission will fail and return E_TX_TIMEOUT.
        /// 3. With timeout value of INFINITE, the function will be blocked until the transmission is complete.
        /// </summary>
        /// <param name="hPort">handle of CAN controller returned by cnio_open.</param>
        /// <param name="msg">CAN message structure. Please refer to CNIO_MSG for details.</param>
        /// <param name="timeout">Time-out interval, in milliseconds.</param>
        /// <returns>CNIO_SUCCESS</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_send_message(Int32 hPort, ref CNIO_MSG msg, UInt32 timeout);

        /// <summary>
        /// This function gets a CAN message from the received buffer with or without block operation.
        /// 1. With timeout value of 0, the function will not be blocked and will return immediately, whether the
        ///    received buffer is empty or not.
        /// 2. With timeout value between 0 and INFINITE, call the function to get a CAN message in the received
        ///    buffer within a time interval equal to timeout, if there is any, otherwise get nothing and return E_RX_TIMEOUT.
        /// 3. With timeout value of INFINITE, the function will be blocked until a CAN message is received.
        /// 4. With CNIO_OPMODE_ERROR_FRAME set by the cnio_init function, the error message will be reported by
        ///    cnio_receive_message, if the bus error occurs. Once CNIO_ERROR_FRAME is marked in the
        ///    FrameType member of CNIO_MSG, the bus error code is recorded in the Data[0] member
        ///    of CNIO_MSG.
        /// </summary>
        /// <param name="hPort">handle of CAN controller returned by cnio_open.</param>
        /// <param name="msg">CAN message structure. Please refer to CNIO_MSG for detail.</param>
        /// <param name="timeout">Time-out interval, in milliseconds.</param>
        /// <returns>CNIO_SUCCESS</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_receive_message(Int32 hPort, out CNIO_MSG msg, UInt32 timeout);

        /// <summary>
        /// This function returns the number of messages in the received buffer.
        /// </summary>
        /// <param name="hPort">handle of CAN controller returned by cnio_open.</param>
        /// <returns>The number of messages stored in the received buffer.</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_receive_queue(Int32 hPort);

        /// <summary>
        /// This function clears data overrun status of the CAN controller.
        /// </summary>
        /// <param name="hPort">handle of CAN controller returned by cnio_open.</param>
        /// <returns>CNIO_SUCCESS</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_clear_data_overrun(Int32 hPort);

        /// <summary>
        /// This function retrieves the status of the CAN controller.
        /// Calls cnio_reset and cnio_clear_data_overrun to clear status below.
        /// </summary>
        /// <param name="hPort">handle of CAN controller returned by cnio_open.</param>
        /// <param name="dwStatus">status of CAN controller.</param>
        /// <returns>CNIO_SUCCESS</returns>
        [DllImport("mxcanfunc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 cnio_status(Int32 hPort, out UInt32 dwStatus);

        /// <summary>
        /// The CNIO_DEV_INFO structure lists device information of the Moxa CAN Interface Board.
        /// Retrieve the CNIO_DEV_INFO by calling the cnio_enum_devices function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CNIO_DEV_INFO
        {
            public Int32 CNIOID;
            public Int32 Reserved;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string ProductName;//32

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DriverName;//32

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceLocation;//32

            public Byte portCount;
        }

        /// <summary>
        /// The CNIO_MSG structure is a CAN message structure for cnio_send_message and
        /// cnio_receive_message to transmit and receive messages on the CAN Bus Network.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CNIO_MSG
        {
            public int ID;
            public Byte FrameType;
            public Byte Length;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8)]
            public Byte[] Data;//8
        }
    }
}
