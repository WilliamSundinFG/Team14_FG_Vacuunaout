﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Team14
{
    public static class Resolutions
    {
        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(
              string deviceName, int modeNum, ref DEVMODE devMode);
        const int ENUM_CURRENT_SETTINGS = -1;

        const int ENUM_REGISTRY_SETTINGS = -2;

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {

            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        public class ResolutionList : IComparable<ResolutionList>
        {
            public int ResX { get; set; }
            public int ResY { get; set; }
            public int CompareTo(ResolutionList resCompare)
            {
                if (resCompare == null)
                    return 1;
                else
                    return ResX.CompareTo(resCompare.ResX);
            }
        }

        public static List<ResolutionList> AvailableResolutions()
        {
            DEVMODE vDevMode = new DEVMODE();
            int i = 0;
            List<ResolutionList> resolutionList = new List<ResolutionList>();
            int prevRes = 0;
            while (EnumDisplaySettings(null, i, ref vDevMode))
            {
                if(prevRes != vDevMode.dmPelsWidth)
                resolutionList.Add(new ResolutionList { ResX = vDevMode.dmPelsWidth, ResY = vDevMode.dmPelsHeight});
                prevRes = vDevMode.dmPelsWidth;
                i++;
            }
            resolutionList.Sort();
            return resolutionList;
        }
    }
}
