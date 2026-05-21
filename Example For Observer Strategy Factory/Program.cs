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
        public InspectionResult(bool isPassed, string inspectionName, string message)
        {
            IsPassed = isPassed;
            InspectionName = inspectionName;
            Message = message;
        }
    }

    // Phần 1 : Factory Pattern
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

    public class CognexCamera : ICamera
    {
        public string Name => "Cognex Camera";
        public void Connect()
        {
            Console.WriteLine("[Cognex] Kết nối camera Cognex");
        }
        public ImageData Capture()
        {
            Console.WriteLine("[Cognex] Chụp ảnh PCB bằng Cognex SDK");
            return new ImageData("pcb_image_cognex.bmp", Name);
        }
        public void Disconnect()
        {
            Console.WriteLine("[Cognex] Ngắt kết nối camera Cognex");
        }
    }

    // Đây là factory tạo camera

    public static class CameraFactory
    {
        public static ICamera CreateCamera(string cameraType) // Tính đa hình
        // Hàm này trả về một cái máy ảnh, nó không biết chắc là của hãng nào
        // Nhưng cam kết chúng có đầy đủ các tính năng của một cái máy ảnh được khai báo trong Interface ICamera
        {
            switch (cameraType.ToLower())
            {
                case "basler":
                    return new BaslerCamera();

                case "hkvision":
                    return new HikCamera();

                case "cognex":
                    return new CognexCamera();

                default: // Trường hợp ngoại lệ cuối cùng
                    throw new ArgumentException("Không hỗ trợ camera : " + cameraType);
                    // Argument Exception là lỗi do người dùng truyền vào một dữ liệu không hợp lệ
            }
        }
    }

    // Phần 2 : Strategy Pattern 
    // IInspection Strategy là interface chung cho các thuật toán kiểm tra
    // Machine có thể đổi thuật toán runtime mà không cần chỉnh sửa code máy

    public interface IInspectionStrategy
    {
        string Name { get; }
        InspectionResult Inspect(ImageData image);
    }
    
    public class MissingComponentInspection : IInspectionStrategy
    {
        public string Name => "Missing Component Inspection";

        public InspectionResult Inspect(ImageData image)
        {
            Console.WriteLine("[Strategy] Đang kiểm tra thiếu linh kiện");
            Console.WriteLine("[Strategy] Ảnh đầu vào : " + image.FileName);
            return new InspectionResult(
                true,
                Name,
                "Không phát hiện thiếu linh kiện"
            );
        }
    }

    public class PositionOffsetInspection : IInspectionStrategy
    {
        public string Name => "Position Offset Inspection";

        public InspectionResult Inspect(ImageData image)
        {
            Console.WriteLine("[Strategy] Đang kiểm tra lệch vị trí linh kiện");
            Console.WriteLine(("[Strategy] Ảnh đầu vào : " + image.FileName));
            return new InspectionResult(
                false,
                Name,
                "Phát hiện IC bị lệch 0.35mm"
            );
        }
    }

    public class ScratchInspection : IInspectionStrategy
    {
        public string Name => "Scratch Inspection";

        public InspectionResult Inspect(ImageData image)
        {
            Console.WriteLine("[Strategy] Đang kiểm tra vết xước trên PCB");
            Console.WriteLine("[Strategy] Ảnh đầu vào : " + image.FileName);
            return new InspectionResult(
                true,
                Name,
                "Không phát hiện vết xước nghiêm trọng"
            );
        }
    }
}