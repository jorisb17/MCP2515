using GHIElectronics.TinyCLR.Devices.Can;
using System;
using System.Collections;
using System.Text;
using System.Threading;

namespace MCP2515
{
    public abstract class CanInterface
    {

        public abstract int WriteErrorCount { get; set; }

        public abstract int ReadErrorCount { get; set; }


        public delegate void OnCanMessageReceived(CanInterface sender);

        public abstract event OnCanMessageReceived OnMessageReceived;

        public abstract void Enable();
        public abstract void Disable();

        public abstract bool WriteMessage(CanMessage message);

        public abstract int WriteMessages(CanMessage[] messages, int offset, int count);

        public abstract ERROR ReadMessage(out CanMessage message);

        public abstract int ReadMessages(CanMessage[] messages, int offset, int count);

        public abstract void SetNominalBitTiming(CanBitTiming bitTiming);

        public abstract void SetDataBitTiming(CanBitTiming bitTiming);

        public abstract void ClearWriteBuffer();

        public abstract void ClearReadBuffer();

        public enum ERROR
        {
            ERROR_OK = 0,
            ERROR_FAIL = 1,
            ERROR_ALLTXBUSY = 2,
            ERROR_FAILINIT = 3,
            ERROR_FAILTX = 4,
            ERROR_NOMSG = 5
        };
    }
}
