using GHIElectronics.TinyCLR.Devices.Can;
using GHIElectronics.TinyCLR.Devices.Gpio;
using GHIElectronics.TinyCLR.Devices.Spi;
using GHIElectronics.TinyCLR.Pins;
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace MCP2515
{
    internal class Program
    {
        static void Main()
        {


            var can = CanController.FromName(SC20100.CanBus.Can1);

            var propagationPhase1 = 13;
            var phase2 = 2;
            var baudratePrescaler = 12;
            var synchronizationJumpWidth = 1;
            var useMultiBitSampling = false;

            can.SetNominalBitTiming(new CanBitTiming(propagationPhase1, phase2, baudratePrescaler,
                synchronizationJumpWidth, useMultiBitSampling));

            can.MessageReceived += Can_MessageReceived;
            //can.ErrorReceived += Can_ErrorReceived;

            int cnt = 0;
            var cs1 = GpioController.GetDefault().OpenPin(SC20100.GpioPin.PD3);
            var cs2 = GpioController.GetDefault().OpenPin(SC20100.GpioPin.PD14);

            var settings1 = new SpiConnectionSettings()
            {
                ChipSelectType = SpiChipSelectType.Gpio,
                ChipSelectLine = cs1,
                Mode = SpiMode.Mode0,
                ClockFrequency = 4_000_000,
                ChipSelectActiveState = false
            };

            var message = new CanMessage()
            {
                Data = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x2E, 0x20, 0x20 },
                ArbitrationId = 0x11,
                Length = 6,
                RemoteTransmissionRequest = false,
                ExtendedId = false,
                FdCan = false,
                BitRateSwitch = false
            };

            var LdrButton = GpioController.GetDefault().OpenPin(SC20100.GpioPin.PE3);
            LdrButton.SetDriveMode(GpioPinDriveMode.InputPullUp);

            var controller = SpiController.FromName(SC20100.SpiBus.Spi3);

            var spi1 = controller.GetDevice(settings1);

            MCP2515 can1 = new(spi1);
            MCP2515.ERROR err;
            err = can1.Reset();
            Debug.WriteLine("Reset: " + err);

            err =can1.SetBitrate(MCP2515.CAN_SPEED.CAN_125KBPS);
            Debug.WriteLine("Set Bitrate: " + err);

            can1.SetNormalMode();
            Debug.WriteLine("Set Normal: " + err);

            var settings2 = new SpiConnectionSettings()
            {
                ChipSelectType = SpiChipSelectType.Gpio,
                ChipSelectLine = cs2,
                Mode = SpiMode.Mode0,
                ClockFrequency = 4_000_000,
                ChipSelectActiveState = false
            };

            var spi2 = controller.GetDevice(settings2);


            can.Enable();

            CanFrame canMsg1 = new();
            canMsg1.can_id = 0x0F6;
            canMsg1.can_dlc = 8;
            canMsg1.data[0] = 0x8E;
            canMsg1.data[1] = 0x87;
            canMsg1.data[2] = 0x32;
            canMsg1.data[3] = 0xFA;
            canMsg1.data[4] = 0x26;
            canMsg1.data[5] = 0x8E;
            canMsg1.data[6] = 0xBE;
            canMsg1.data[7] = 0x86;

            


            while (true)
            {
                //can.WriteMessage(message);
                Thread.Sleep(5000);
                err = can1.SendMessage(canMsg1);
                Debug.WriteLine("Send MSG: " + err);
                if(err == MCP2515.ERROR.ERROR_ALLTXBUSY)
                {
                    can1.ClearTXInterrupts();
                }
            }
        }

        private static void Can_ErrorReceived(CanController sender, ErrorReceivedEventArgs e)
        {
            Debug.WriteLine("Error " + e.ToString());
        }

        private static void Can_MessageReceived(CanController sender, MessageReceivedEventArgs e)
        {
            sender.ReadMessage(out var message);

            Debug.WriteLine("Arbitration ID: 0x" + message.ArbitrationId.ToString("X8"));
            Debug.WriteLine("Is extended ID: " + message.ExtendedId.ToString());
            Debug.WriteLine("Is remote transmission request: "
                + message.RemoteTransmissionRequest.ToString());

            Debug.WriteLine("Time stamp: " + message.Timestamp.ToString());

            var data = "";
            for (var i = 0; i < message.Length; i++) data += Convert.ToChar(message.Data[i]);

            Debug.WriteLine("Data: " + data);
        }
    }
}
