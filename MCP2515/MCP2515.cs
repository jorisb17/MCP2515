using GHIElectronics.TinyCLR.Devices.Gpio;
using GHIElectronics.TinyCLR.Devices.Spi;
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace MCP2515
{
    /// <summary>
    /// Represents a class that handles the CAN tranceiver MCP2515.
    /// </summary>
    public class MCP2515
    {
        /*
         *  Speed 8M
         */
        public const byte MCP_8MHz_1000kBPS_CFG1 = (0x00);
        public const byte MCP_8MHz_1000kBPS_CFG2 = (0x80);
        public const byte MCP_8MHz_1000kBPS_CFG3 = (0x80);

        public const byte MCP_8MHz_500kBPS_CFG1 = (0x00);
        public const byte MCP_8MHz_500kBPS_CFG2 = (0x90);
        public const byte MCP_8MHz_500kBPS_CFG3 = (0x82);

        public const byte MCP_8MHz_250kBPS_CFG1 = (0x00);
        public const byte MCP_8MHz_250kBPS_CFG2 = (0xB1);
        public const byte MCP_8MHz_250kBPS_CFG3 = (0x85);

        public const byte MCP_8MHz_200kBPS_CFG1 = (0x00);
        public const byte MCP_8MHz_200kBPS_CFG2 = (0xB4);
        public const byte MCP_8MHz_200kBPS_CFG3 = (0x86);

        public const byte MCP_8MHz_125kBPS_CFG1 = (0x01);
        public const byte MCP_8MHz_125kBPS_CFG2 = (0xB1);
        public const byte MCP_8MHz_125kBPS_CFG3 = (0x85);

        public const byte MCP_8MHz_100kBPS_CFG1 = (0x01);
        public const byte MCP_8MHz_100kBPS_CFG2 = (0xB4);
        public const byte MCP_8MHz_100kBPS_CFG3 = (0x86);

        public const byte MCP_8MHz_80kBPS_CFG1 = (0x01);
        public const byte MCP_8MHz_80kBPS_CFG2 = (0xBF);
        public const byte MCP_8MHz_80kBPS_CFG3 = (0x87);

        public const byte MCP_8MHz_50kBPS_CFG1 = (0x03);
        public const byte MCP_8MHz_50kBPS_CFG2 = (0xB4);
        public const byte MCP_8MHz_50kBPS_CFG3 = (0x86);

        public const byte MCP_8MHz_40kBPS_CFG1 = (0x03);
        public const byte MCP_8MHz_40kBPS_CFG2 = (0xBF);
        public const byte MCP_8MHz_40kBPS_CFG3 = (0x87);

        public const byte MCP_8MHz_33k3BPS_CFG1 = (0x47);
        public const byte MCP_8MHz_33k3BPS_CFG2 = (0xE2);
        public const byte MCP_8MHz_33k3BPS_CFG3 = (0x85);

        public const byte MCP_8MHz_31k25BPS_CFG1 = (0x07);
        public const byte MCP_8MHz_31k25BPS_CFG2 = (0xA4);
        public const byte MCP_8MHz_31k25BPS_CFG3 = (0x84);

        public const byte MCP_8MHz_20kBPS_CFG1 = (0x07);
        public const byte MCP_8MHz_20kBPS_CFG2 = (0xBF);
        public const byte MCP_8MHz_20kBPS_CFG3 = (0x87);

        public const byte MCP_8MHz_10kBPS_CFG1 = (0x0F);
        public const byte MCP_8MHz_10kBPS_CFG2 = (0xBF);
        public const byte MCP_8MHz_10kBPS_CFG3 = (0x87);

        public const byte MCP_8MHz_5kBPS_CFG1 = (0x1F);
        public const byte MCP_8MHz_5kBPS_CFG2 = (0xBF);
        public const byte MCP_8MHz_5kBPS_CFG3 = (0x87);

        /*
         *  speed 16M
         */
        public const byte MCP_16MHz_1000kBPS_CFG1 = (0x00);
        public const byte MCP_16MHz_1000kBPS_CFG2 = (0xD0);
        public const byte MCP_16MHz_1000kBPS_CFG3 = (0x82);

        public const byte MCP_16MHz_500kBPS_CFG1 = (0x00);
        public const byte MCP_16MHz_500kBPS_CFG2 = (0xF0);
        public const byte MCP_16MHz_500kBPS_CFG3 = (0x86);

        public const byte MCP_16MHz_250kBPS_CFG1 = (0x41);
        public const byte MCP_16MHz_250kBPS_CFG2 = (0xF1);
        public const byte MCP_16MHz_250kBPS_CFG3 = (0x85);

        public const byte MCP_16MHz_200kBPS_CFG1 = (0x01);
        public const byte MCP_16MHz_200kBPS_CFG2 = (0xFA);
        public const byte MCP_16MHz_200kBPS_CFG3 = (0x87);

        public const byte MCP_16MHz_125kBPS_CFG1 = (0x03);
        public const byte MCP_16MHz_125kBPS_CFG2 = (0xF0);
        public const byte MCP_16MHz_125kBPS_CFG3 = (0x86);


        public const byte MCP_16MHz_100kBPS_CFG1 = (0x03);
        public const byte MCP_16MHz_100kBPS_CFG2 = (0xFA);
        public const byte MCP_16MHz_100kBPS_CFG3 = (0x87);

        public const byte MCP_16MHz_80kBPS_CFG1 = (0x03);
        public const byte MCP_16MHz_80kBPS_CFG2 = (0xFF);
        public const byte MCP_16MHz_80kBPS_CFG3 = (0x87);

        public const byte MCP_16MHz_83k3BPS_CFG1 = (0x03);
        public const byte MCP_16MHz_83k3BPS_CFG2 = (0xBE);
        public const byte MCP_16MHz_83k3BPS_CFG3 = (0x07);

        public const byte MCP_16MHz_50kBPS_CFG1 = (0x07);
        public const byte MCP_16MHz_50kBPS_CFG2 = (0xFA);
        public const byte MCP_16MHz_50kBPS_CFG3 = (0x87);

        public const byte MCP_16MHz_40kBPS_CFG1 = (0x07);
        public const byte MCP_16MHz_40kBPS_CFG2 = (0xFF);
        public const byte MCP_16MHz_40kBPS_CFG3 = (0x87);

        public const byte MCP_16MHz_33k3BPS_CFG1 = (0x4E);
        public const byte MCP_16MHz_33k3BPS_CFG2 = (0xF1);
        public const byte MCP_16MHz_33k3BPS_CFG3 = (0x85);

        public const byte MCP_16MHz_20kBPS_CFG1 = (0x0F);
        public const byte MCP_16MHz_20kBPS_CFG2 = (0xFF);
        public const byte MCP_16MHz_20kBPS_CFG3 = (0x87);

        public const byte MCP_16MHz_10kBPS_CFG1 = (0x1F);
        public const byte MCP_16MHz_10kBPS_CFG2 = (0xFF);
        public const byte MCP_16MHz_10kBPS_CFG3 = (0x87);

        public const byte MCP_16MHz_5kBPS_CFG1 = (0x3F);
        public const byte MCP_16MHz_5kBPS_CFG2 = (0xFF);
        public const byte MCP_16MHz_5kBPS_CFG3 = (0x87);

        /*
         *  speed 20M
         */
        public const byte MCP_20MHz_1000kBPS_CFG1 = (0x00);
        public const byte MCP_20MHz_1000kBPS_CFG2 = (0xD9);
        public const byte MCP_20MHz_1000kBPS_CFG3 = (0x82);

        public const byte MCP_20MHz_500kBPS_CFG1 = (0x00);
        public const byte MCP_20MHz_500kBPS_CFG2 = (0xFA);
        public const byte MCP_20MHz_500kBPS_CFG3 = (0x87);


        public const byte MCP_20MHz_250kBPS_CFG1 = (0x41);
        public const byte MCP_20MHz_250kBPS_CFG2 = (0xFB);
        public const byte MCP_20MHz_250kBPS_CFG3 = (0x86);

        public const byte MCP_20MHz_200kBPS_CFG1 = (0x01);
        public const byte MCP_20MHz_200kBPS_CFG2 = (0xFF);
        public const byte MCP_20MHz_200kBPS_CFG3 = (0x87);

        public const byte MCP_20MHz_125kBPS_CFG1 = (0x03);
        public const byte MCP_20MHz_125kBPS_CFG2 = (0xFA);
        public const byte MCP_20MHz_125kBPS_CFG3 = (0x87);

        public const byte MCP_20MHz_100kBPS_CFG1 = (0x04);
        public const byte MCP_20MHz_100kBPS_CFG2 = (0xFA);
        public const byte MCP_20MHz_100kBPS_CFG3 = (0x87);

        public const byte MCP_20MHz_83k3BPS_CFG1 = (0x04);
        public const byte MCP_20MHz_83k3BPS_CFG2 = (0xFE);
        public const byte MCP_20MHz_83k3BPS_CFG3 = (0x87);

        public const byte MCP_20MHz_80kBPS_CFG1 = (0x04);
        public const byte MCP_20MHz_80kBPS_CFG2 = (0xFF);
        public const byte MCP_20MHz_80kBPS_CFG3 = (0x87);

        public const byte MCP_20MHz_50kBPS_CFG1 = (0x09);
        public const byte MCP_20MHz_50kBPS_CFG2 = (0xFA);
        public const byte MCP_20MHz_50kBPS_CFG3 = (0x87);

        public const byte MCP_20MHz_40kBPS_CFG1 = (0x09);
        public const byte MCP_20MHz_40kBPS_CFG2 = (0xFF);
        public const byte MCP_20MHz_40kBPS_CFG3 = (0x87);

        public const byte MCP_20MHz_33k3BPS_CFG1 = (0x0B);
        public const byte MCP_20MHz_33k3BPS_CFG2 = (0xFF);
        public const byte MCP_20MHz_33k3BPS_CFG3 = (0x87);

        public enum CAN_CLOCK
        {
            MCP_20MHZ,
            MCP_16MHZ,
            MCP_8MHZ
        };

        public enum CAN_SPEED
        {
            CAN_5KBPS,
            CAN_10KBPS,
            CAN_20KBPS,
            CAN_31K25BPS,
            CAN_33KBPS,
            CAN_40KBPS,
            CAN_50KBPS,
            CAN_80KBPS,
            CAN_83K3BPS,
            CAN_95KBPS,
            CAN_100KBPS,
            CAN_125KBPS,
            CAN_200KBPS,
            CAN_250KBPS,
            CAN_500KBPS,
            CAN_1000KBPS
        };

        public enum CAN_CLKOUT
        {
            CLKOUT_DISABLE = -1,
            CLKOUT_DIV1 = 0x0,
            CLKOUT_DIV2 = 0x1,
            CLKOUT_DIV4 = 0x2,
            CLKOUT_DIV8 = 0x3,
        };

        public enum ERROR
        {
            ERROR_OK = 0,
            ERROR_FAIL = 1,
            ERROR_ALLTXBUSY = 2,
            ERROR_FAILINIT = 3,
            ERROR_FAILTX = 4,
            ERROR_NOMSG = 5
        };

        public enum MASK
        {
            MASK0,
            MASK1
        };

        public enum RXF
        {
            RXF0 = 0,
            RXF1 = 1,
            RXF2 = 2,
            RXF3 = 3,
            RXF4 = 4,
            RXF5 = 5
        };

        public enum RXBn
        {
            RXB0 = 0,
            RXB1 = 1
        };

        public enum TXBn
        {
            TXB0 = 0,
            TXB1 = 1,
            TXB2 = 2
        };

        public enum /*class*/ CANINTF : byte
        {
            CANINTF_RX0IF = 0x01,
            CANINTF_RX1IF = 0x02,
            CANINTF_TX0IF = 0x04,
            CANINTF_TX1IF = 0x08,
            CANINTF_TX2IF = 0x10,
            CANINTF_ERRIF = 0x20,
            CANINTF_WAKIF = 0x40,
            CANINTF_MERRF = 0x80
        };

        public enum /*class*/ EFLG : byte
        {
            EFLG_RX1OVR = (1 << 7),
            EFLG_RX0OVR = (1 << 6),
            EFLG_TXBO = (1 << 5),
            EFLG_TXEP = (1 << 4),
            EFLG_RXEP = (1 << 3),
            EFLG_TXWAR = (1 << 2),
            EFLG_RXWAR = (1 << 1),
            EFLG_EWARN = (1 << 0)
        };

        const byte CANCTRL_REQOP = 0xE0;
        const byte CANCTRL_ABAT = 0x10;
        const byte CANCTRL_OSM = 0x08;
        const byte CANCTRL_CLKEN = 0x04;
        const byte CANCTRL_CLKPRE = 0x03;

        enum /*class*/ CANCTRL_REQOP_MODE : byte
        {
            CANCTRL_REQOP_NORMAL = 0x00,
            CANCTRL_REQOP_SLEEP = 0x20,
            CANCTRL_REQOP_LOOPBACK = 0x40,
            CANCTRL_REQOP_LISTENONLY = 0x60,
            CANCTRL_REQOP_CONFIG = 0x80,
            CANCTRL_REQOP_POWERUP = 0xE0
        };

        const byte CANSTAT_OPMOD = 0xE0;
        const byte CANSTAT_ICOD = 0x0E;

        const byte CNF3_SOF = 0x80;

        const byte TXB_EXIDE_MASK = 0x08;
        const byte DLC_MASK = 0x0F;
        const byte RTR_MASK = 0x40;

        const byte RXBnCTRL_RXM_STD = 0x20;
        const byte RXBnCTRL_RXM_EXT = 0x40;
        const byte RXBnCTRL_RXM_STDEXT = 0x00;
        const byte RXBnCTRL_RXM_MASK = 0x60;
        const byte RXBnCTRL_RTR = 0x08;
        const byte RXB0CTRL_BUKT = 0x04;
        const byte RXB0CTRL_FILHIT_MASK = 0x03;
        const byte RXB1CTRL_FILHIT_MASK = 0x07;
        const byte RXB0CTRL_FILHIT = 0x00;
        const byte RXB1CTRL_FILHIT = 0x01;

        const byte MCP_SIDH = 0;
        const byte MCP_SIDL = 1;
        const byte MCP_EID8 = 2;
        const byte MCP_EID0 = 3;
        const byte MCP_DLC = 4;
        const byte MCP_DATA = 5;

        enum /*class*/ STAT : byte
        {
            STAT_RX0IF = (1 << 0),
            STAT_RX1IF = (1 << 1)
        };

        const byte STAT_RXIF_MASK = (byte)(STAT.STAT_RX0IF | STAT.STAT_RX1IF);

        enum /*class*/ TXBnCTRL : byte
        {
            TXB_ABTF = 0x40,
            TXB_MLOA = 0x20,
            TXB_TXERR = 0x10,
            TXB_TXREQ = 0x08,
            TXB_TXIE = 0x04,
            TXB_TXP = 0x03
        };

        const byte EFLG_ERRORMASK = (byte)(EFLG.EFLG_RX1OVR
                                            | EFLG.EFLG_RX0OVR
                                            | EFLG.EFLG_TXBO
                                            | EFLG.EFLG_TXEP
                                            | EFLG.EFLG_RXEP);

        byte INSTRUCTION_WRITE = 0x02;
        byte INSTRUCTION_READ = 0x03;
        byte INSTRUCTION_BITMOD = 0x05;
        byte INSTRUCTION_LOAD_TX0 = 0x40;
        byte INSTRUCTION_LOAD_TX1 = 0x42;
        byte INSTRUCTION_LOAD_TX2 = 0x44;
        byte INSTRUCTION_RTS_TX0 = 0x81;
        byte INSTRUCTION_RTS_TX1 = 0x82;
        byte INSTRUCTION_RTS_TX2 = 0x84;
        byte INSTRUCTION_RTS_ALL = 0x87;
        byte INSTRUCTION_READ_RX0 = 0x90;
        byte INSTRUCTION_READ_RX1 = 0x94;
        byte INSTRUCTION_READ_STATUS = 0xA0;
        byte INSTRUCTION_RX_STATUS = 0xB0;
        byte INSTRUCTION_RESET = 0xC0;

        enum /*class*/ REGISTER : byte
        {
            MCP_RXF0SIDH = 0x00,
            MCP_RXF0SIDL = 0x01,
            MCP_RXF0EID8 = 0x02,
            MCP_RXF0EID0 = 0x03,
            MCP_RXF1SIDH = 0x04,
            MCP_RXF1SIDL = 0x05,
            MCP_RXF1EID8 = 0x06,
            MCP_RXF1EID0 = 0x07,
            MCP_RXF2SIDH = 0x08,
            MCP_RXF2SIDL = 0x09,
            MCP_RXF2EID8 = 0x0A,
            MCP_RXF2EID0 = 0x0B,
            MCP_CANSTAT = 0x0E,
            MCP_CANCTRL = 0x0F,
            MCP_RXF3SIDH = 0x10,
            MCP_RXF3SIDL = 0x11,
            MCP_RXF3EID8 = 0x12,
            MCP_RXF3EID0 = 0x13,
            MCP_RXF4SIDH = 0x14,
            MCP_RXF4SIDL = 0x15,
            MCP_RXF4EID8 = 0x16,
            MCP_RXF4EID0 = 0x17,
            MCP_RXF5SIDH = 0x18,
            MCP_RXF5SIDL = 0x19,
            MCP_RXF5EID8 = 0x1A,
            MCP_RXF5EID0 = 0x1B,
            MCP_TEC = 0x1C,
            MCP_REC = 0x1D,
            MCP_RXM0SIDH = 0x20,
            MCP_RXM0SIDL = 0x21,
            MCP_RXM0EID8 = 0x22,
            MCP_RXM0EID0 = 0x23,
            MCP_RXM1SIDH = 0x24,
            MCP_RXM1SIDL = 0x25,
            MCP_RXM1EID8 = 0x26,
            MCP_RXM1EID0 = 0x27,
            MCP_CNF3 = 0x28,
            MCP_CNF2 = 0x29,
            MCP_CNF1 = 0x2A,
            MCP_CANINTE = 0x2B,
            MCP_CANINTF = 0x2C,
            MCP_EFLG = 0x2D,
            MCP_TXB0CTRL = 0x30,
            MCP_TXB0SIDH = 0x31,
            MCP_TXB0SIDL = 0x32,
            MCP_TXB0EID8 = 0x33,
            MCP_TXB0EID0 = 0x34,
            MCP_TXB0DLC = 0x35,
            MCP_TXB0DATA = 0x36,
            MCP_TXB1CTRL = 0x40,
            MCP_TXB1SIDH = 0x41,
            MCP_TXB1SIDL = 0x42,
            MCP_TXB1EID8 = 0x43,
            MCP_TXB1EID0 = 0x44,
            MCP_TXB1DLC = 0x45,
            MCP_TXB1DATA = 0x46,
            MCP_TXB2CTRL = 0x50,
            MCP_TXB2SIDH = 0x51,
            MCP_TXB2SIDL = 0x52,
            MCP_TXB2EID8 = 0x53,
            MCP_TXB2EID0 = 0x54,
            MCP_TXB2DLC = 0x55,
            MCP_TXB2DATA = 0x56,
            MCP_RXB0CTRL = 0x60,
            MCP_RXB0SIDH = 0x61,
            MCP_RXB0SIDL = 0x62,
            MCP_RXB0EID8 = 0x63,
            MCP_RXB0EID0 = 0x64,
            MCP_RXB0DLC = 0x65,
            MCP_RXB0DATA = 0x66,
            MCP_RXB1CTRL = 0x70,
            MCP_RXB1SIDH = 0x71,
            MCP_RXB1SIDL = 0x72,
            MCP_RXB1EID8 = 0x73,
            MCP_RXB1EID0 = 0x74,
            MCP_RXB1DLC = 0x75,
            MCP_RXB1DATA = 0x76
        };

        const uint DEFAULT_SPI_CLOCK = 10000000; // 10MHz

        const int N_TXBUFFERS = 3;
        const int N_RXBUFFERS = 2;

        struct TXBn_REGS
        {
            public REGISTER CTRL;
            public REGISTER SIDH;
            public REGISTER DATA;
        }

        struct RXBn_REGS
        {
            public REGISTER CTRL;
            public REGISTER SIDH;
            public REGISTER DATA;
            public CANINTF CANINTF_RXnIF;
        }

        RXBn_REGS RXB0 = new RXBn_REGS();
        RXBn_REGS RXB1 = new RXBn_REGS();
        RXBn_REGS[] RXB;

        TXBn_REGS TXB0 = new TXBn_REGS();
        TXBn_REGS TXB1 = new TXBn_REGS();
        TXBn_REGS TXB2 = new TXBn_REGS();
        TXBn_REGS[] TXB;

        SpiDevice spi;

        public MCP2515(SpiDevice spi)
        {
            this.spi = spi;

            RXB0.CTRL = REGISTER.MCP_RXB0CTRL;
            RXB0.SIDH = REGISTER.MCP_RXB0SIDH;
            RXB0.DATA = REGISTER.MCP_RXB0DATA;
            RXB0.CANINTF_RXnIF = CANINTF.CANINTF_RX0IF;

            RXB1.CTRL = REGISTER.MCP_RXB1CTRL;
            RXB1.SIDH = REGISTER.MCP_RXB1SIDH;
            RXB1.DATA = REGISTER.MCP_RXB1DATA;
            RXB1.CANINTF_RXnIF = CANINTF.CANINTF_RX1IF;

            RXB = new RXBn_REGS[] { RXB0, RXB1 };

            TXB0.CTRL = REGISTER.MCP_TXB0CTRL;
            TXB0.SIDH = REGISTER.MCP_TXB0SIDH;
            TXB0.DATA = REGISTER.MCP_TXB0DATA;

            TXB1.CTRL = REGISTER.MCP_TXB1CTRL;
            TXB1.SIDH = REGISTER.MCP_TXB1SIDH;
            TXB1.DATA = REGISTER.MCP_TXB1DATA;

            TXB2.CTRL = REGISTER.MCP_TXB2CTRL;
            TXB2.SIDH = REGISTER.MCP_TXB2SIDH;
            TXB2.DATA = REGISTER.MCP_TXB2DATA;

            TXB = new TXBn_REGS[] { TXB0, TXB1, TXB2 };
        }

        ERROR SetMode(CANCTRL_REQOP_MODE mode)
        {
            ModifyRegister(REGISTER.MCP_CANCTRL, CANCTRL_REQOP, (byte)mode);

            DateTime start = DateTime.Now;
            DateTime end = start.AddMilliseconds(10);
            bool modeMatch = false;

            while (DateTime.Now < end)
            {
                byte newMode = ReadRegister(REGISTER.MCP_CANCTRL);
                newMode &= CANSTAT_OPMOD;
                modeMatch = newMode == (byte)mode;

                if (modeMatch)
                    break;
            }

            return modeMatch ? ERROR.ERROR_OK : ERROR.ERROR_FAIL;
        }

        byte ReadRegister(REGISTER reg)
        {
            byte[] rxd = new byte[3];
            spi.TransferFullDuplex(new byte[] { INSTRUCTION_READ, (byte)reg }, 0, 2, rxd, 0, 3);
            return rxd[2];
        }

        void ReadRegisters(REGISTER reg, byte[] values, byte n)
        {
            spi.TransferSequential(new byte[] { INSTRUCTION_READ, (byte)reg }, 0, 2, values, 0, n);
        }

        void SetRegister(REGISTER reg, byte value)
        {
            spi.Write(new byte[] { INSTRUCTION_WRITE, (byte)reg, value });
        }

        void SetRegisters(REGISTER reg, byte[] values, byte n)
        {
            byte[] txd = new byte[n + 2];

            txd[0] = INSTRUCTION_WRITE;
            txd[1] = (byte)reg;

            for (int i = 2; i < n + 2; i++)
            {
                txd[i] = values[i - 2];
            }

            spi.Write(txd);
        }

        void ModifyRegister(REGISTER reg, byte mask, byte data)
        {
            spi.Write(new byte[] { INSTRUCTION_BITMOD, (byte)reg, mask, data });
        }

        void PrepareId(byte[] buffer, bool ext, uint id)
        {
            ushort canid = (ushort)(id & 0x0FFFF);

            if (ext)
            {
                buffer[MCP_EID0] = (byte)(canid & 0xFF);
                buffer[MCP_EID8] = (byte)(canid >> 8);
                canid = (ushort)(id >> 16);
                buffer[MCP_SIDL] = (byte)(canid & 0x03);
                buffer[MCP_SIDL] += (byte)((canid & 0x1C) << 3);
                buffer[MCP_SIDL] |= TXB_EXIDE_MASK;
                buffer[MCP_SIDH] = (byte)(canid >> 5);
            }
            else
            {
                buffer[MCP_SIDH] = (byte)(canid >> 3);
                buffer[MCP_SIDL] = (byte)((canid & 0x07) << 5);
                buffer[MCP_EID0] = 0;
                buffer[MCP_EID8] = 0;
            }
        }

        public ERROR Reset()
        {
            spi.Write(new byte[] { INSTRUCTION_RESET });

            byte[] zeros = new byte[14];

            SetRegisters(REGISTER.MCP_TXB0CTRL, zeros, 0);
            SetRegisters(REGISTER.MCP_TXB1CTRL, zeros, 0);
            SetRegisters(REGISTER.MCP_TXB2CTRL, zeros, 0);

            SetRegisters(REGISTER.MCP_RXB0CTRL, zeros, 0);
            SetRegisters(REGISTER.MCP_RXB1CTRL, zeros, 0);

            SetRegister(REGISTER.MCP_CANINTE, (byte)(CANINTF.CANINTF_RX0IF | CANINTF.CANINTF_RX1IF | CANINTF.CANINTF_ERRIF | CANINTF.CANINTF_MERRF));

            // receives all valid messages using either Standard or Extended Identifiers that
            // meet filter criteria. RXF0 is applied for RXB0, RXF1 is applied for RXB1
            ModifyRegister(REGISTER.MCP_RXB0CTRL,
                            RXBnCTRL_RXM_MASK | RXB0CTRL_BUKT | RXB0CTRL_FILHIT_MASK,
                           RXBnCTRL_RXM_STDEXT | RXB0CTRL_BUKT | RXB0CTRL_FILHIT);
            ModifyRegister(REGISTER.MCP_RXB1CTRL,
                           RXBnCTRL_RXM_MASK | RXB1CTRL_FILHIT_MASK,
                           RXBnCTRL_RXM_STDEXT | RXB1CTRL_FILHIT);

            // clear filters and masks
            // do not filter any standard frames for RXF0 used by RXB0
            // do not filter any extended frames for RXF1 used by RXB1
            RXF[] filters = new RXF[] { RXF.RXF0, RXF.RXF1, RXF.RXF2, RXF.RXF3, RXF.RXF4, RXF.RXF5 };
            for (int i = 0; i < 6; i++)
            {
                bool ext = (i == 1);
                ERROR result = SetFilter(filters[i], ext, 0);
                if (result != ERROR.ERROR_OK)
                {
                    return result;
                }
            }

            MASK[] masks = { MASK.MASK0, MASK.MASK1 };
            for (int i = 0; i < 2; i++)
            {
                ERROR result = SetFilterMask(masks[i], true, 0);
                if (result != ERROR.ERROR_OK)
                {
                    return result;
                }
            }

            return ERROR.ERROR_OK;
        }

        public ERROR SetConfigMode()
        {
            return SetMode(CANCTRL_REQOP_MODE.CANCTRL_REQOP_CONFIG);
        }
        public ERROR SetListenOnlyMode()
        {
            return SetMode(CANCTRL_REQOP_MODE.CANCTRL_REQOP_LISTENONLY);
        }
        public ERROR SetSleepMode()
        {
            return SetMode(CANCTRL_REQOP_MODE.CANCTRL_REQOP_SLEEP);
        }
        public ERROR SetLoopbackMode()
        {
            return SetMode(CANCTRL_REQOP_MODE.CANCTRL_REQOP_LOOPBACK);
        }
        public ERROR SetNormalMode()
        {
            return SetMode(CANCTRL_REQOP_MODE.CANCTRL_REQOP_NORMAL);
        }
        public ERROR SetClkOut(CAN_CLKOUT divisor)
        {
            if (divisor == CAN_CLKOUT.CLKOUT_DISABLE)
            {
                /* Turn off CLKEN */
                ModifyRegister(REGISTER.MCP_CANCTRL, CANCTRL_CLKEN, 0x00);

                /* Turn on CLKOUT for SOF */
                ModifyRegister(REGISTER.MCP_CNF3, CNF3_SOF, CNF3_SOF);
                return ERROR.ERROR_OK;
            }

            /* Set the prescaler (CLKPRE) */
            ModifyRegister(REGISTER.MCP_CANCTRL, CANCTRL_CLKPRE, (byte)divisor);

            /* Turn on CLKEN */
            ModifyRegister(REGISTER.MCP_CANCTRL, CANCTRL_CLKEN, CANCTRL_CLKEN);

            /* Turn off CLKOUT for SOF */
            ModifyRegister(REGISTER.MCP_CNF3, CNF3_SOF, 0x00);
            return ERROR.ERROR_OK;
        }

        public ERROR SetBitrate(CAN_SPEED canSpeed)
        {
            return SetBitrate(canSpeed, CAN_CLOCK.MCP_16MHZ);
        }
        public ERROR SetBitrate(CAN_SPEED canSpeed, CAN_CLOCK canClock)
        {
            ERROR error = SetConfigMode();

            if (error != ERROR.ERROR_OK)
                return error;

            byte set = 1, cfg1 = 0, cfg2 = 0, cfg3 = 0;

            switch (canClock)
            {
                case (CAN_CLOCK.MCP_8MHZ):
                    switch (canSpeed)
                    {
                        case (CAN_SPEED.CAN_5KBPS):                                               //   5KBPS
                            cfg1 = MCP_8MHz_5kBPS_CFG1;
                            cfg2 = MCP_8MHz_5kBPS_CFG2;
                            cfg3 = MCP_8MHz_5kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_10KBPS):                                              //  10KBPS
                            cfg1 = MCP_8MHz_10kBPS_CFG1;
                            cfg2 = MCP_8MHz_10kBPS_CFG2;
                            cfg3 = MCP_8MHz_10kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_20KBPS):                                              //  20KBPS
                            cfg1 = MCP_8MHz_20kBPS_CFG1;
                            cfg2 = MCP_8MHz_20kBPS_CFG2;
                            cfg3 = MCP_8MHz_20kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_31K25BPS):                                            //  31.25KBPS
                            cfg1 = MCP_8MHz_31k25BPS_CFG1;
                            cfg2 = MCP_8MHz_31k25BPS_CFG2;
                            cfg3 = MCP_8MHz_31k25BPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_33KBPS):                                              //  33.333KBPS
                            cfg1 = MCP_8MHz_33k3BPS_CFG1;
                            cfg2 = MCP_8MHz_33k3BPS_CFG2;
                            cfg3 = MCP_8MHz_33k3BPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_40KBPS):                                              //  40Kbps
                            cfg1 = MCP_8MHz_40kBPS_CFG1;
                            cfg2 = MCP_8MHz_40kBPS_CFG2;
                            cfg3 = MCP_8MHz_40kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_50KBPS):                                              //  50Kbps
                            cfg1 = MCP_8MHz_50kBPS_CFG1;
                            cfg2 = MCP_8MHz_50kBPS_CFG2;
                            cfg3 = MCP_8MHz_50kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_80KBPS):                                              //  80Kbps
                            cfg1 = MCP_8MHz_80kBPS_CFG1;
                            cfg2 = MCP_8MHz_80kBPS_CFG2;
                            cfg3 = MCP_8MHz_80kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_100KBPS):                                             // 100Kbps
                            cfg1 = MCP_8MHz_100kBPS_CFG1;
                            cfg2 = MCP_8MHz_100kBPS_CFG2;
                            cfg3 = MCP_8MHz_100kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_125KBPS):                                             // 125Kbps
                            cfg1 = MCP_8MHz_125kBPS_CFG1;
                            cfg2 = MCP_8MHz_125kBPS_CFG2;
                            cfg3 = MCP_8MHz_125kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_200KBPS):                                             // 200Kbps
                            cfg1 = MCP_8MHz_200kBPS_CFG1;
                            cfg2 = MCP_8MHz_200kBPS_CFG2;
                            cfg3 = MCP_8MHz_200kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_250KBPS):                                             // 250Kbps
                            cfg1 = MCP_8MHz_250kBPS_CFG1;
                            cfg2 = MCP_8MHz_250kBPS_CFG2;
                            cfg3 = MCP_8MHz_250kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_500KBPS):                                             // 500Kbps
                            cfg1 = MCP_8MHz_500kBPS_CFG1;
                            cfg2 = MCP_8MHz_500kBPS_CFG2;
                            cfg3 = MCP_8MHz_500kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_1000KBPS):                                            //   1Mbps
                            cfg1 = MCP_8MHz_1000kBPS_CFG1;
                            cfg2 = MCP_8MHz_1000kBPS_CFG2;
                            cfg3 = MCP_8MHz_1000kBPS_CFG3;
                            break;

                        default:
                            set = 0;
                            break;
                    }
                    break;

                case (CAN_CLOCK.MCP_16MHZ):
                    switch (canSpeed)
                    {
                        case (CAN_SPEED.CAN_5KBPS):                                               //   5Kbps
                            cfg1 = MCP_16MHz_5kBPS_CFG1;
                            cfg2 = MCP_16MHz_5kBPS_CFG2;
                            cfg3 = MCP_16MHz_5kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_10KBPS):                                              //  10Kbps
                            cfg1 = MCP_16MHz_10kBPS_CFG1;
                            cfg2 = MCP_16MHz_10kBPS_CFG2;
                            cfg3 = MCP_16MHz_10kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_20KBPS):                                              //  20Kbps
                            cfg1 = MCP_16MHz_20kBPS_CFG1;
                            cfg2 = MCP_16MHz_20kBPS_CFG2;
                            cfg3 = MCP_16MHz_20kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_33KBPS):                                              //  33.333Kbps
                            cfg1 = MCP_16MHz_33k3BPS_CFG1;
                            cfg2 = MCP_16MHz_33k3BPS_CFG2;
                            cfg3 = MCP_16MHz_33k3BPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_40KBPS):                                              //  40Kbps
                            cfg1 = MCP_16MHz_40kBPS_CFG1;
                            cfg2 = MCP_16MHz_40kBPS_CFG2;
                            cfg3 = MCP_16MHz_40kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_50KBPS):                                              //  50Kbps
                            cfg1 = MCP_16MHz_50kBPS_CFG1;
                            cfg2 = MCP_16MHz_50kBPS_CFG2;
                            cfg3 = MCP_16MHz_50kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_80KBPS):                                              //  80Kbps
                            cfg1 = MCP_16MHz_80kBPS_CFG1;
                            cfg2 = MCP_16MHz_80kBPS_CFG2;
                            cfg3 = MCP_16MHz_80kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_83K3BPS):                                             //  83.333Kbps
                            cfg1 = MCP_16MHz_83k3BPS_CFG1;
                            cfg2 = MCP_16MHz_83k3BPS_CFG2;
                            cfg3 = MCP_16MHz_83k3BPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_100KBPS):                                             // 100Kbps
                            cfg1 = MCP_16MHz_100kBPS_CFG1;
                            cfg2 = MCP_16MHz_100kBPS_CFG2;
                            cfg3 = MCP_16MHz_100kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_125KBPS):                                             // 125Kbps
                            cfg1 = MCP_16MHz_125kBPS_CFG1;
                            cfg2 = MCP_16MHz_125kBPS_CFG2;
                            cfg3 = MCP_16MHz_125kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_200KBPS):                                             // 200Kbps
                            cfg1 = MCP_16MHz_200kBPS_CFG1;
                            cfg2 = MCP_16MHz_200kBPS_CFG2;
                            cfg3 = MCP_16MHz_200kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_250KBPS):                                             // 250Kbps
                            cfg1 = MCP_16MHz_250kBPS_CFG1;
                            cfg2 = MCP_16MHz_250kBPS_CFG2;
                            cfg3 = MCP_16MHz_250kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_500KBPS):                                             // 500Kbps
                            cfg1 = MCP_16MHz_500kBPS_CFG1;
                            cfg2 = MCP_16MHz_500kBPS_CFG2;
                            cfg3 = MCP_16MHz_500kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_1000KBPS):                                            //   1Mbps
                            cfg1 = MCP_16MHz_1000kBPS_CFG1;
                            cfg2 = MCP_16MHz_1000kBPS_CFG2;
                            cfg3 = MCP_16MHz_1000kBPS_CFG3;
                            break;

                        default:
                            set = 0;
                            break;
                    }
                    break;

                case (CAN_CLOCK.MCP_20MHZ):
                    switch (canSpeed)
                    {
                        case (CAN_SPEED.CAN_33KBPS):                                              //  33.333Kbps
                            cfg1 = MCP_20MHz_33k3BPS_CFG1;
                            cfg2 = MCP_20MHz_33k3BPS_CFG2;
                            cfg3 = MCP_20MHz_33k3BPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_40KBPS):                                              //  40Kbps
                            cfg1 = MCP_20MHz_40kBPS_CFG1;
                            cfg2 = MCP_20MHz_40kBPS_CFG2;
                            cfg3 = MCP_20MHz_40kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_50KBPS):                                              //  50Kbps
                            cfg1 = MCP_20MHz_50kBPS_CFG1;
                            cfg2 = MCP_20MHz_50kBPS_CFG2;
                            cfg3 = MCP_20MHz_50kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_80KBPS):                                              //  80Kbps
                            cfg1 = MCP_20MHz_80kBPS_CFG1;
                            cfg2 = MCP_20MHz_80kBPS_CFG2;
                            cfg3 = MCP_20MHz_80kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_83K3BPS):                                             //  83.333Kbps
                            cfg1 = MCP_20MHz_83k3BPS_CFG1;
                            cfg2 = MCP_20MHz_83k3BPS_CFG2;
                            cfg3 = MCP_20MHz_83k3BPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_100KBPS):                                             // 100Kbps
                            cfg1 = MCP_20MHz_100kBPS_CFG1;
                            cfg2 = MCP_20MHz_100kBPS_CFG2;
                            cfg3 = MCP_20MHz_100kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_125KBPS):                                             // 125Kbps
                            cfg1 = MCP_20MHz_125kBPS_CFG1;
                            cfg2 = MCP_20MHz_125kBPS_CFG2;
                            cfg3 = MCP_20MHz_125kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_200KBPS):                                             // 200Kbps
                            cfg1 = MCP_20MHz_200kBPS_CFG1;
                            cfg2 = MCP_20MHz_200kBPS_CFG2;
                            cfg3 = MCP_20MHz_200kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_250KBPS):                                             // 250Kbps
                            cfg1 = MCP_20MHz_250kBPS_CFG1;
                            cfg2 = MCP_20MHz_250kBPS_CFG2;
                            cfg3 = MCP_20MHz_250kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_500KBPS):                                             // 500Kbps
                            cfg1 = MCP_20MHz_500kBPS_CFG1;
                            cfg2 = MCP_20MHz_500kBPS_CFG2;
                            cfg3 = MCP_20MHz_500kBPS_CFG3;
                            break;

                        case (CAN_SPEED.CAN_1000KBPS):                                            //   1Mbps
                            cfg1 = MCP_20MHz_1000kBPS_CFG1;
                            cfg2 = MCP_20MHz_1000kBPS_CFG2;
                            cfg3 = MCP_20MHz_1000kBPS_CFG3;
                            break;

                        default:
                            set = 0;
                            break;
                    }
                    break;

                default:
                    set = 0;
                    break;
            }

            if (set > 0)
            {
                SetRegister(REGISTER.MCP_CNF1, cfg1);
                SetRegister(REGISTER.MCP_CNF2, cfg2);
                SetRegister(REGISTER.MCP_CNF3, cfg3);
                return ERROR.ERROR_OK;
            }
            else
            {
                return ERROR.ERROR_FAIL;
            }
        }
        public ERROR SetFilterMask(MASK mask, bool ext, uint ulData)
        {
            ERROR res = SetConfigMode();
            if (res != ERROR.ERROR_OK)
            {
                return res;
            }

            byte[] tbufdata = new byte[4];
            PrepareId(tbufdata, ext, ulData);

            REGISTER reg;
            switch (mask)
            {
                case MASK.MASK0: reg = REGISTER.MCP_RXM0SIDH; break;
                case MASK.MASK1: reg = REGISTER.MCP_RXM1SIDH; break;
                default:
                    return ERROR.ERROR_FAIL;
            }

            SetRegisters(reg, tbufdata, 4);

            return ERROR.ERROR_OK;
        }
        public ERROR SetFilter(RXF num, bool ext, uint ulData)
        {
            ERROR res = SetConfigMode();
            if (res != ERROR.ERROR_OK)
            {
                return res;
            }

            REGISTER reg;

            switch (num)
            {
                case RXF.RXF0: reg = REGISTER.MCP_RXF0SIDH; break;
                case RXF.RXF1: reg = REGISTER.MCP_RXF1SIDH; break;
                case RXF.RXF2: reg = REGISTER.MCP_RXF2SIDH; break;
                case RXF.RXF3: reg = REGISTER.MCP_RXF3SIDH; break;
                case RXF.RXF4: reg = REGISTER.MCP_RXF4SIDH; break;
                case RXF.RXF5: reg = REGISTER.MCP_RXF5SIDH; break;
                default:
                    return ERROR.ERROR_FAIL;
            }

            byte[] tbufdata = new byte[4];
            PrepareId(tbufdata, ext, ulData);
            SetRegisters(reg, tbufdata, 4);

            return ERROR.ERROR_OK;
        }
        public ERROR SendMessage(TXBn txbn, CanFrame frame)
        {
            if (frame.can_dlc > CanFrame.CAN_MAX_DLC)
                return ERROR.ERROR_FAILTX;

            TXBn_REGS txbuf = TXB[(int)txbn];

            byte[] data = new byte[13];

            bool ext = (frame.can_id & CanFrame.CAN_EFF_FLAG) > 0;
            bool rtr = (frame.can_id & CanFrame.CAN_RTR_FLAG) > 0;

            uint id = (uint)(frame.can_id & (ext ? CanFrame.CAN_EFF_MASK : CanFrame.CAN_SFF_MASK));

            PrepareId(data, ext, id);

            data[MCP_DLC] = rtr ? (byte)(frame.can_dlc | RTR_MASK) : frame.can_dlc;

            Array.Copy(data, MCP_DATA, frame.data, 0, frame.can_dlc);

            SetRegisters(txbuf.SIDH, data, (byte)(5 + frame.can_dlc));

            ModifyRegister(txbuf.CTRL, (byte)TXBnCTRL.TXB_TXREQ, (byte)TXBnCTRL.TXB_TXREQ);

            byte ctrl = ReadRegister(txbuf.CTRL);
            if ((ctrl & (byte)(TXBnCTRL.TXB_ABTF | TXBnCTRL.TXB_MLOA | TXBnCTRL.TXB_TXERR)) != 0)
            {
                return ERROR.ERROR_FAILTX;
            }
            return ERROR.ERROR_OK;
        }
        public ERROR SendMessage(CanFrame frame)
        {
            if (frame.can_dlc > CanFrame.CAN_MAX_DLEN)
            {
                return ERROR.ERROR_FAILTX;
            }

            TXBn[] txBuffers = { TXBn.TXB0, TXBn.TXB1, TXBn.TXB2 };

            for (int i = 0; i < N_TXBUFFERS; i++)
            {
                TXBn_REGS txbuf = TXB[(int)txBuffers[i]];
                byte ctrlval = ReadRegister(txbuf.CTRL);
                if ((ctrlval & (byte)TXBnCTRL.TXB_TXREQ) == 0)
                {
                    return SendMessage(txBuffers[i], frame);
                }
            }

            return ERROR.ERROR_ALLTXBUSY;
        }
        public ERROR ReadMessage(RXBn rxbn, out CanFrame frame)
        {
            frame = new CanFrame();

            RXBn_REGS rxb = RXB[(int)rxbn];

            byte[] tbufdata = new byte[5];

            ReadRegisters(rxb.SIDH, tbufdata, 5);

            uint id = (uint)((tbufdata[MCP_SIDH] << 3) + (tbufdata[MCP_SIDL] >> 5));

            if ((tbufdata[MCP_SIDL] & TXB_EXIDE_MASK) == TXB_EXIDE_MASK)
            {
                id = (uint)((id << 2) + (tbufdata[MCP_SIDL] & 0x03));
                id = (id << 8) + tbufdata[MCP_EID8];
                id = (id << 8) + tbufdata[MCP_EID0];
                id |= (uint)CanFrame.CAN_EFF_FLAG;
            }

            byte dlc = (byte)(tbufdata[MCP_DLC] & DLC_MASK);
            if (dlc > CanFrame.CAN_MAX_DLEN)
            {
                return ERROR.ERROR_FAIL;
            }

            byte ctrl = ReadRegister(rxb.CTRL);
            if ((ctrl & RXBnCTRL_RTR) > 0)
            {
                id |= (uint)CanFrame.CAN_RTR_FLAG;
            }

            frame.can_id = id;
            frame.can_dlc = dlc;

            ReadRegisters(rxb.DATA, frame.data, dlc);

            ModifyRegister(REGISTER.MCP_CANINTF, (byte)rxb.CANINTF_RXnIF, 0);

            return ERROR.ERROR_OK;
        }
        public ERROR ReadMessage(out CanFrame frame)
        {
            frame = new CanFrame();

            ERROR rc;
            byte stat = GetStatus();

            if ((stat & (byte)STAT.STAT_RX0IF) > 0)
            {
                rc = ReadMessage(RXBn.RXB0, out frame);
            }
            else if ((stat & (byte)STAT.STAT_RX1IF) > 0)
            {
                rc = ReadMessage(RXBn.RXB1, out frame);
            }
            else
            {
                rc = ERROR.ERROR_NOMSG;
            }

            return rc;
        }
        public bool CheckReceive()
        {
            byte res = GetStatus();
            if ((res & STAT_RXIF_MASK) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckError()
        {
            byte eflg = GetErrorFlags();

            if ((eflg & EFLG_ERRORMASK) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public byte GetErrorFlags()
        {
            return ReadRegister(REGISTER.MCP_EFLG);
        }
        public void ClearRXnOVRFlags()
        {
            ModifyRegister(REGISTER.MCP_EFLG, (byte)(EFLG.EFLG_RX0OVR | EFLG.EFLG_RX1OVR), 0);
        }
        public byte GetInterrupts()
        {
            return ReadRegister(REGISTER.MCP_CANINTF);
        }
        public byte GetInterruptMask()
        {
            return ReadRegister(REGISTER.MCP_CANINTE);
        }
        public void ClearInterrupts()
        {

        }
        public void ClearTXInterrupts()
        {
            ModifyRegister(REGISTER.MCP_CANINTF, (byte)(CANINTF.CANINTF_TX0IF | CANINTF.CANINTF_TX1IF | CANINTF.CANINTF_TX2IF), 0);
        }
        public byte GetStatus()
        {
            byte[] rxd = new byte[1];
            spi.TransferSequential(new byte[] { INSTRUCTION_READ_STATUS }, 0, 1, rxd, 0, 1);
            return rxd[0];
        }
        public void ClearRXnOVR()
        {
            byte eflg = GetErrorFlags();
            if (eflg != 0)
            {
                ClearRXnOVRFlags();
                ClearInterrupts();
            }
        }
        public void ClearMERR()
        {
            ModifyRegister(REGISTER.MCP_CANINTF, (byte)CANINTF.CANINTF_MERRF, 0);
        }
        public void ClearERRIF()
        {
            ModifyRegister(REGISTER.MCP_CANINTF, (byte)CANINTF.CANINTF_ERRIF, 0);
        }
        public byte ErrorCountRX()
        {
            return ReadRegister(REGISTER.MCP_REC);
        }
        public byte ErrorCountTX()
        {
            return ReadRegister(REGISTER.MCP_TEC);
        }
    }
}
