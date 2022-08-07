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


            int cnt = 0;
            var cs1 = GpioController.GetDefault().OpenPin(SC20100.GpioPin.PD3);
            var cs2 = GpioController.GetDefault().OpenPin(SC20100.GpioPin.PD14);

            var settings1 = new SpiConnectionSettings()
            {
                ChipSelectType = SpiChipSelectType.Gpio,
                ChipSelectLine = cs1,
                Mode = SpiMode.Mode0,
                ClockFrequency = 10_000_000,
                ChipSelectActiveState = false
            };

            var message = new CanMessage()
            {
                Data = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x2E, 0x20, 0x20 },
                ArbitrationId = 0x12345678,
                Length = 8,
                RemoteTransmissionRequest = false,
                ExtendedId = true,
                FdCan = false,
                BitRateSwitch = false
            };


            var controller = SpiController.FromName(SC20100.SpiBus.Spi3);

            var spi1 = controller.GetDevice(settings1);

            MCP2515 can1 = new(spi1);
            while(can1.Reset() != CanInterface.ERROR.ERROR_OK);

            while(can1.SetBitrate(MCP2515.CAN_SPEED.CAN_125KBPS) != CanInterface.ERROR.ERROR_OK) ;

            while(can1.SetNormalMode() != CanInterface.ERROR.ERROR_OK);

            var settings2 = new SpiConnectionSettings()
            {
                ChipSelectType = SpiChipSelectType.Gpio,
                ChipSelectLine = cs2,
                Mode = SpiMode.Mode0,
                ClockFrequency = 10_000_000,
                ChipSelectActiveState = false
            };

            var spi2 = controller.GetDevice(settings2);

            MCP2515 can2 = new(spi2);
            while(can2.Reset() != CanInterface.ERROR.ERROR_OK);

            while(can2.SetBitrate(MCP2515.CAN_SPEED.CAN_125KBPS)!= CanInterface.ERROR.ERROR_OK);

            while(can2.SetNormalMode() != CanInterface.ERROR.ERROR_OK);


            CanMessage canMsg1 = new();
            canMsg1.ArbitrationId = 0x00FD0900;
            canMsg1.ExtendedId = true;
            canMsg1.RemoteTransmissionRequest = false;
            canMsg1.Length = 8;
            canMsg1.Data[0] = 0x8E;
            canMsg1.Data[1] = 0x87;
            canMsg1.Data[2] = 0x32;
            canMsg1.Data[3] = 0xFA;
            canMsg1.Data[4] = 0x26;
            canMsg1.Data[5] = 0x8E;
            canMsg1.Data[6] = 0xBE;
            canMsg1.Data[7] = 0x86;

            can1.SetFilterMask(MCP2515.MASK.MASK1, true, 0x00FFFF00);
            can1.SetFilter(MCP2515.RXF.RXF1, true, 0x00FD0900);


            Debug.WriteLine("------- CAN Read ----------");
            Debug.WriteLine("ID  DLC   DATA");

            CanMessage res;
            CanInterface.ERROR err;

            while (true)
            {
                //can.WriteMessage(message);
                err = can2.SendMessage(canMsg1);
                Thread.Sleep(1000);
                         
                err = can1.ReadMessage(out res);
                if (err == CanInterface.ERROR.ERROR_OK)
                {
                    Debug.Write(res.ArbitrationId.ToString("X08"));
                    Debug.Write(" ");
                    Debug.Write(res.Length.ToString("X08"));
                    Debug.Write(" ");

                    for (int i = 0; i < res.Length; i++)
                    {  // print the data
                        Debug.Write(res.Data[i].ToString("X08"));
                        Debug.Write(" ");
                    }
                    Debug.WriteLine("");
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
