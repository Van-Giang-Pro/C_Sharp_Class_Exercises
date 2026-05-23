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
        ImageData Capture(); // Cái nào dùng bản thiết kế này đều có hàm Capture và trả về ImageData
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
        public static ICamera CreateCamera(string cameraType) 

        // Tính đa hình
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
    
    // Phần này chúng ta có thể dùng Factory để tạo thuật toán kiểm tra, Factory có thể dùng để taọ Strategy theo cấu hình

    public static class InspectionStrategyFactory
    {
        public static IInspectionStrategy CreateStrategy(string inspectionType)
        {
            switch (inspectionType.ToLower())
            {
                case "missing":
                    return new MissingComponentInspection();
                
                case "position":
                    return new PositionOffsetInspection();
                
                case "scratch":
                    return new ScratchInspection();
                
                default:
                    throw new ArgumentException("Không hỗ trợ kiểu kiểm tra : " + inspectionType);
            }
        }
    }
    
    // Phần 3 : Observer Pattern
    // Thông báo kết quả cho nhiều nơi
    // Khi triểm tra xong cần biết kết quả : UI, Logger, PLC
    // Vision Machine không cần gọi cưng từng module
    // Nó chỉ notify cho danh sách observer

    public interface IInspectionObserver
    {
        void OnInspectionCompleted(InspectionResult result);
    }

    public class UiDisplayObserver : IInspectionObserver
    {
        public void OnInspectionCompleted(InspectionResult result)
        {
            string status = result.IsPassed ? "OK" : "NG";
            Console.WriteLine("[UI] Hiển thị kết quả lên màn hình : " + status);
            Console.WriteLine("[UI] Nội dụng : " + result.Message);
        }
    }

    public class LoggerObserver : IInspectionObserver
    {
        public void OnInspectionCompleted(InspectionResult result)
        {
            Console.WriteLine(
                "[LOG]" + 
                 DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                " | Inspection = " + result.InspectionName +
                " | Passed = " + result.IsPassed +
                " | Message = " + result.Message
            );
        }
    }

    public class PlcObserver : IInspectionObserver
    {
        public void OnInspectionCompleted(InspectionResult result)
        {
            if (result.IsPassed)
            {
                Console.WriteLine("[PLC] Gửi tín hiệu Pass. Cho băng tải chạy tiếp");
            }
            else
            {
                Console.WriteLine("[PLC] Gửi tín hiệu FAIL. Đẩy sản phẩm sang line NG");
            }
        }
    }

    public class AlarmObserver : IInspectionObserver
    {
        public void OnInspectionCompleted(InspectionResult result)
        {
            if (!result.IsPassed)
            {
                Console.WriteLine("[ALARM] Bật đèn đỏ và còi cảnh báo lỗi sản phẩm");
            }
        }
    }
    
    // Class này ứng dụng cả 3 pattern
    // Factory : camera và stategy được tạo từ bên ngoài rồi truyền vào
    // Strategy : máy dùng IInspectionStrategy để kiểm tra ảnh
    // Observer : máy thông báo kết quả cho nhiều observer

    public class VisionInspectionMachine
    {
        private readonly ICamera _camera; 

        // Máy này cần một camera để chụp ảnh, field này giữ camera đó
        // Ta có readonly nghĩa là sau khi gán trong constructor thì không đổi được nữa, vì máy đã lắp camera rồi thì không thay camera giữa chừng

        private IInspectionStrategy _strategy;

        // Máy cần một thuật toán để kiểm tra, field này giữ thuật toán hiện tại
        // Không có read only vì máy được phép đổi thuật toán giữa chừng qua hàm SetStrategy()

        private readonly List<IInspectionObserver> _observers;

        // Một danh sách nhưng nơi nhận kết quả
        // Có readonly ở đây để bản thân cái list không bị thay thế, nhưng vẫn có thể add hoặc remove các phần tử bên trong list
        // Nghĩa là sao khi khởi tạo, nó không cho gán lại nữa, cho cho thêm bớt phần tử thôi

        public VisionInspectionMachine(ICamera camera, IInspectionStrategy strategy)
        {
            _camera = camera;
            _strategy = strategy;
            _observers = new List<IInspectionObserver>();
        }

        public void SetStrategy(IInspectionStrategy strategy)
        {
            _strategy = strategy;
            Console.WriteLine("\n[Machine] Đã đổi thuật toán kiếm tra sang : " + _strategy.Name);
        }

        public void Attach(IInspectionObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IInspectionObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notify(InspectionResult result)
        {
            foreach (IInspectionObserver observer in _observers)
            {
                observer.OnInspectionCompleted(result);
            }
        }

        public void RunInspection()
        {
            Console.WriteLine("==============================================================");
            Console.WriteLine("Bắt Đầu Kiểm Tra PCB");
            Console.WriteLine("Camera : " + _camera.Name);
            Console.WriteLine("Strategy : " + _strategy.Name);
            Console.WriteLine("==============================================================");
            
            _camera.Connect();

            ImageData image = _camera.Capture();

            InspectionResult result = _strategy.Inspect(image);

            // Chấm inspect được là do _strategy có kiểu IInspectionStrategy mà interface đó có hàm Inspect()
            
            Console.WriteLine("[Machine] Kiểm tra hoàn tất. Chuẩn bị thông báo kết quả");
            
            Notify(result);

            _camera.Disconnect();
            
            Console.WriteLine("==============================================================");
            Console.WriteLine("Kết thúc kiểm tra PCB");
            Console.WriteLine("==============================================================");
        }
    }
    
    // Main Program

    public class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Demo : Observer - Strategy - Factory");
            Console.WriteLine("Ứng Dụng Machine Vision Kiểm Tra PCB");
            Console.WriteLine();
            
            // Factory
            // Code chính không tự tạo new BaslerCamera hay HikCamera
            // Code chính gọi Factory để tọa camera theo cấu hình
            
            Console.WriteLine("Chọn camera :");
            Console.WriteLine("1. Basler Camera");
            Console.WriteLine("2. Hikvision Camera");
            Console.WriteLine("3. Cognex Camera");
            Console.Write("Nhập lựa chọn camera : ");

            string cameraChoice = Console.ReadLine() ?? ""; // Là nếu vế trái bị null, người dùng không nhập gì thì trả về vế phải
            string cameraType = ConvertCameraChoice(cameraChoice);
            
            ICamera camera = CameraFactory.CreateCamera(cameraType);
            
            // Strategy
            // Code chính chọn thuật toán kiểm tra ban đầu
            // Thuật toán được tạo ra qua Factory
            
            Console.WriteLine();
            Console.WriteLine("Chọn kiểu kiểm tra : ");
            Console.WriteLine("1. Kiểm tra thiếu linh kiện");
            Console.WriteLine("2. Kiểm tra lệch vị trí linh kiện");
            Console.WriteLine("3. Kiểm tra vết xước PCB");
            Console.Write("Nhập lưa chọn kiểm tra : ");

            string inspectionChoice = Console.ReadLine() ?? "";
            string inspectionType = ConvertInspectionChoice(inspectionChoice);
            
            IInspectionStrategy strategy = InspectionStrategyFactory.CreateStrategy(inspectionType);

            // Tạo máy vision
            // Máy chỉ nhận interface IInterface và IInspectionStrategy

            VisionInspectionMachine machine = new VisionInspectionMachine(camera, strategy);

            // Observer
            // Đăng ký những nơi muốn nhận kết quả kiểm tra
            // Sau này muốn thêm EmailObserver, DatabaseObserver
            // Thì chỉ cần Attach thêm, không phải sửa RunInspection

            machine.Attach(new UiDisplayObserver());
            machine.Attach(new LoggerObserver());
            machine.Attach(new PlcObserver());
            machine.Attach(new AlarmObserver());

            // Chạy lần kiểm tra đầu tiên

            machine.RunInspection();

            // Demo đổi strategy runtime
            // Cùng một máy, cùng một camera nhưng đổi thuật toán kiểm tra

            Console.WriteLine("Demo đổi strategy trong runtime");
            Console.WriteLine("Máy sẽ đổi sang kiểm tra lệch vị trí linh kiện");

            IInspectionStrategy newStrategy = new PositionOffsetInspection();
            machine.SetStrategy(newStrategy);
            machine.RunInspection();

            Console.WriteLine("Nhấn Enter để thoát");
            Console.ReadLine();
        }

        private static string ConvertCameraChoice(string choice) 
            
         // Vì hàm này chỉ phục vụ trong class Program, không ai ở ngoài cần gọi nó
         // Ở ngoài class này không hàm nào cần gọi nó hết

        {
            switch(choice)
            {
                case "1":
                    return "basler";

                case "2":
                    return "hik";

                case "3":
                    return "cognex";

                default:
                    Console.WriteLine("Lựa chọn camera không hợp lệ. Mặc định dùng Basler");
                    return "basler";
            }
        }

        private static string ConvertInspectionChoice(string choice)
        {
            switch(choice)
            {
                case "1":
                    return "missing";

                case "2":
                    return "position";

                case "3":
                    return "scratch";

                default:
                    Console.WriteLine("Lựa chọn kiểm tra không hợp lệ. Mặc định kiểm tra thiếu linh kiện");
                    return "missing";
            }    
        }
    }
}