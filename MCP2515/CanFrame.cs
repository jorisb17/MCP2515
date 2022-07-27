using System;
using System.Collections;
using System.Text;
using System.Threading;

namespace MCP2515
{
    public class CanFrame
    {

        /* special address description flags for the CAN_ID */
        public const ulong CAN_EFF_FLAG = 0x80000000UL; /* EFF/SFF is set in the MSB */
        public const ulong CAN_RTR_FLAG = 0x40000000UL;/* remote transmission request */
        public const ulong CAN_ERR_FLAG = 0x20000000UL; /* error message frame */

        /* valid bits in CAN ID for frame formats */
        public const ulong CAN_SFF_MASK = 0x000007FFUL; /* standard frame format (SFF) */
        public const ulong CAN_EFF_MASK = 0x1FFFFFFFUL; /* extended frame format (EFF) */
        public const ulong CAN_ERR_MASK = 0x1FFFFFFFUL; /* omit EFF, RTR, ERR flags */

        /*
         * Controller Area Network Identifier structure
         *
         * bit 0-28 : CAN identifier (11/29 bit)
         * bit 29   : error message frame flag (0 = data frame, 1 = error message)
         * bit 30   : remote transmission request flag (1 = rtr frame)
         * bit 31   : frame format flag (0 = standard 11 bit, 1 = extended 29 bit)
         */

        public const byte CAN_SFF_ID_BITS = 11;
        public const byte CAN_EFF_ID_BITS = 29;

        /* CAN payload length and DLC definitions according to ISO 11898-1 */
        public const byte CAN_MAX_DLC = 8;
        public const byte CAN_MAX_DLEN = 8;

        public uint can_id;  /* 32 bit CAN_ID + EFF/RTR/ERR flags */
        public byte can_dlc; /* frame payload length in byte (0 .. CAN_MAX_DLEN) */
        public byte[] data = new byte[CAN_MAX_DLEN];
    }
}
