﻿using System;
using System.Runtime.InteropServices;

namespace Common
{
    public class Import
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_IPNETROW
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwIndex;
            [MarshalAs(UnmanagedType.U4)]
            public int dwPhysAddrLen;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac0;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac1;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac2;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac3;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac4;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac5;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac6;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac7;
            [MarshalAs(UnmanagedType.U4)]
            public int dwAddr;
            [MarshalAs(UnmanagedType.U4)]
            public int dwType;
        }

        [DllImport("IpHlpApi.dll")]
        public static extern long GetIpNetTable(IntPtr pIpNetTable, ref int pdwSize, bool bOrder);

        //Создание именованного канала
        [DllImport("kernel32.dll")]
        public static extern int CreateNamedPipe(string lpName,         //строка, содержащая имя канала 
                                            uint dwOpenMode,             //режим открытия канала
                                            uint dwPipeMode,             //режим работы канала
                                            uint nMaxInstances,          // максимальное количество реализаций канала
                                            uint nOutBufferSize,         // размер выходного буфера в байтах
                                            uint nInBufferSize,          // размер входного буфера в байтах
                                            int nDefaultTimeOut,        // время ожидания в мс
                                            uint lpSecurityAttributes);   //адрес структуры защиты

        // Соединение со стороны серверного процесса
        [DllImport("kernel32.dll")]
        public static extern bool ConnectNamedPipe(int hNamedPipe,         //дескриптор канала
                                            int lpOverlapped);             //режим открытия канала

        // Отключение серверного процесса от клиентского канала
        [DllImport("kernel32.dll")]
        public static extern bool DisconnectNamedPipe(int hPipe);

        //Открытие канала
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int CreateFile(string lpFileName,                                  //строка с именем канала  
                                               Types.EFileAccess dwDesiredAccess,               //режим доступа
                                               Types.EFileShare dwShareMode,                    //режим совместного использования
                                               int lpSecurityAttributes,                        //дескриптор защиты
                                               Types.ECreationDisposition dwCreationDisposition,//параметры создания
                                               int dwFlagsAndAttributes,                        //атрибуты файла
                                               int hTemplateFile);                              //идентификатор файла с атрибутами

        //Запись данных в канал
        [DllImport("kernel32.dll")]
        public static extern bool WriteFile(int hFile,                //описатель реализации канала  
                                     byte[] lpBuffer,                 //адрес буфера, данные из которого будут записаны в канал
                                     uint nNumberOfBytesToWrite,      //размер буфера
                                     ref uint lpNumberOfBytesWritten, //число байт, действительно записанных в канал
                                     int lpOverlapped);               //зависит от режима работы
        //Чтение данных из канала
        [DllImport("kernel32.dll")]
        public static extern bool ReadFile(int hFile,                 //описатель реализации канала
                                    byte[] lpBuffer,              //адрес буфера, куда будут прочитаны данные из канала
                                    uint nNumberOfBytesToRead,    //размер буфера
                                    ref uint lpNumberOfBytesRead, //количество действительно прочитанных байт из канала
                                    int lpOverlapped);         //зависит от режима работы

        // Функция, которая проверяет, что данные действительно записались в мейлслот
        [DllImport("kernel32.dll")]
        public static extern byte FlushFileBuffers(int hPipe);

        //Закрытие handle 
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(int hObject);
    }
}
