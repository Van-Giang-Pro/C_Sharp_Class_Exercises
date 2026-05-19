using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverStrategyFactoryDemo
// Dùng để phân loại gom các đoạn code có liên quan lại với nhau, giúp quản lý dự án ngăn nắp và tránh bị trùng tên
{
    public class ImageData
    {
        public string FileName { get; set; }
        public string SourceCamera { get; set; }
        public ImageData(string fileName, string sourceCamera)
        {
            FileName = fileName;
            SourceCamera = sourceCamera;
        }
    }

    public class InspectionResult
    {
        public bool IsPassed { get; set; }
        public string InspectionName { get; set; }
        public string Message { get; set; }
        public InspectionResult(bool isPassed, string inpectionName, string message)
        {
            IsPassed = isPassed;
            InspectionName = inpectionName;
            Message = message;
        }
    }

    // Factory Pattern
    // Ta có ICamera là interface chung
    // Code chính chỉ làm việc với ICamera, không phụ thuộc trực tiếp vào BaslerCamera hoặc HikCamera

    public interface ICamera
    {
        string Name { get; } // Các field hay hàm trong Interface thì không cần khai báo phạm vi
        void Connect(); // Mặc định các field hay hàm trong Interface đều là public nên không cần khai báo chi cho mắc công
        ImageData Capture();
        // Cái nào dùng bản thiết kế này đều có hàm Capture và trả về ImageData
        void Disconnect();
    }

    public class BaslerCamera : ICamera
    {
        public string Name => "Basler Camera";
        /*
        public string Name
        {
            get
            {
                return "Basler Camera";
            }
        }
        Console.WriteLine(camera.Name); // Máy sẽ in ra Basler Camera
        Đoạn code này tạo ra một thuộc tính chỉ đọc read only
        */
        public void Connect()
        {
            Console.WriteLine("[Basler] Kết nối camera Basler");
        }
        public ImageData Capture()
        {
            Console.WriteLine("[Basler] Chụp ảnh PCB bằng Basler SDK");
            return new ImageData("pcb_image_basler.bmp", Name);
        }
        public void Disconnect()
        {
            Console.WriteLine("[Basler] Ngắt kết nối camera Basler");
        }
    }

    public class HikCamera : ICamera
    {
        public string Name => "Hikvision Camera";
        public void Connect()
        {
            Console.WriteLine("[Hikvision] Kết nối camera Hikvision");
        }
        public ImageData Capture()
        {
            Console.WriteLine("[Hikvision] Chụp ảnh PCB bằng Hikvision SDK");
            return new ImageData("pcb_image_hik.bmp", Name);
        }
        public void Disconnect()
        {
            Console.WriteLine("[Hikvision] Ngắt kết nối camera Hikvision");
        }
    }
}