using Stateless; // Là thư viện có sẵn ở ngoài cài vào dùng để cấu hình máy trạng thái

enum VisionState
{
    PowerOff, // Máy đang tắt
    Init, // Đang kết nô camera, servo driver, IO Board
    Homing, // Đang home trục (tìm điểm zero của máy)
    Idle, // Đang chờ, sẵn sàng
    Running, // Đang chạy kiểm tra sản pẩm liên tục
    Complete, // Hoàn tất kiểm tra
    Error, // Lỗi, dừng mọi thứ, chờ operator
    Maintenance // Bảo trì, chờ kỹ thuật viên bảo dưỡng
}

enum VisionTrigger // Tín hiệu để chuyển trạng thái
{
    PowerOn, // Tín hiệu từ bên ngoài đưa vào
    InitDone, // Tín hiệu hệ thống đưa ra
    HomeDone, // Tín hiệu hệ thống đưa ra
    Start, // Tín hiệu hệ thống đưa ra
    InpectionPass,
    InspectionFail,
    BatchComplete,
    ErrorOccured,
    Reset, // Tín hiệu hệ thống đưa ra
    EnterMaintenance, 
    ExitMaintenance,
}

class VisionMachineController
{
    readonly StateMachine<VisionState, VisionTrigger> _fsm; // Biến thuộc lớp StateMachine
    // Khai báo biến trạng thái máy mà danh sách các trạng thái và trigger phải lấy từ các enum được khai báo trong <>
    // Sử dụng thư viện stateless, dấu _ là để chỉ biến nội bộ chỉ được dùng trong nội bộ class
    // Ta có readonly là ý nói biến đó chỉ cho phép gàn giá trị một lần duy nhất khi khai báo hoặc bên trong constructor
    // Sau đó thì không ai gán giá trị cho nó được nữa
    public int PassCount { get; private set; }
    public int FailCount { get; private set; }
    public double YieldRate => PassCount + FailCount == 0 ? 0 : (double)PassCount / (PassCount + FailCount) * 100;
    // Kiểu double trong C# lưu được 8 bytes 64 bits cả âm và dương
    public VisionMachineController()
    {
        _fsm = new StateMachine<VisionState, VisionTrigger>(VisionState.PowerOff); // Khởi tạo đối tượng thuộc lớp StateMchine
        // Startup sequence
        _fsm.Configure(VisionState.PowerOff) // Cấu hình trạng thái
            .Permit(VisionTrigger.PowerOn, VisionState.Init) // Nếu có trigger X thì chuyển sang trạng thái Y
            .OnEntry(() => Console.WriteLine("Bật nguồn => Kết nối camera")); // Khi vừa vào trạng thái này thì làm gì
        _fsm.Configure(VisionState.Init)
            .Permit(VisionTrigger.InitDone, VisionState.Homing)
            .OnEntry(() => Console.WriteLine("Init done => Bắt đầu homing"));
        _fsm.Configure(VisionState.Homing)
            .Permit(VisionTrigger.HomeDone, VisionState.Idle);
        // Production
        _fsm.Configure(VisionState.Idle) // Khi state machine đi vào trạng thái Idle, hãy in ra màn hình dòng chữ
            .Permit(VisionTrigger.Start, VisionState.Running)
            .OnEntry(() => Console.WriteLine("[HMI] SẴN SÀNG"));
        _fsm.Configure(VisionState.Running)
            .Permit(VisionTrigger.BatchComplete, VisionState.Complete)
            .OnEntry(() => Console.WriteLine("[HMI] ĐANG KIỂM TRA"));
        _fsm.Configure(VisionState.Complete)
            // Ta có => là dấu lamda expression là phải thực hiện đoạn code bên phải
            // Ta có {} là khối lệnh thực thi
            .Permit(VisionTrigger.Reset, VisionState.Idle) 
            .OnEntry(() => { PassCount = 0; FailCount = 0; }); // Ta có () là hàm này không nhận tham số đầu vào
        // Error Recovery
        _fsm.Configure(VisionState.Error) // Nghĩa là cấu hình trạng thái error cho máy này
            .Permit(VisionTrigger.Reset, VisionState.Idle)
            .OnEntry(() => Console.WriteLine("[HMI] MÁY LỖI"));
        // Maintenance Access
        _fsm.Configure(VisionState.Idle)
            .Permit(VisionTrigger.EnterMaintenance, VisionState.Maintenance);
        _fsm.Configure(VisionState.Maintenance)
            .Permit(VisionTrigger.ExitMaintenance, VisionState.Idle);
    }

    public void Inspect()
    {
        var pass = new Random().Next(100) > 10;
        if (pass) PassCount++; else FailCount++;
        Console.WriteLine($"Kết quả : {(pass ? "PASS" : "FAIL")} | Yield : {YieldRate:F1}%");
        // If pass là true thì in ra PASS còn ngược lại thì in ra FAIL
    }

    public void Fire(VisionTrigger trigger) // Các lớp bên ngoài gọi được Fire
    {
        _fsm.Fire(trigger);
        // Ta có _fsm là biến nằm bên trong class VisionMachineController, thường chúng ta không cho bên ngoài đụng vào biến này nên tạo một hàm public để gọi một cách an toàn hơn
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Class program không thể gọi được biến _fsm.Fire() vì nó là readonly chỉ được gán 1 lần và truy cập trong class thôi
        // Nên phải tạo method là public để làm cầu nối
        var machine = new VisionMachineController();

        // Khở động máy
        machine.Fire(VisionTrigger.PowerOn);
        machine.Fire(VisionTrigger.InitDone);
        machine.Fire(VisionTrigger.HomeDone);

        //Bắt đầu chạy sản xuất
        machine.Fire(VisionTrigger.Start);

        // Kiểm tra 5 sản phẩm
        machine.Inspect();
        machine.Inspect();
        machine.Inspect();
        machine.Inspect();
        machine.Inspect();

        // Hoàn tất batch
        machine.Fire(VisionTrigger.BatchComplete);
        machine.Fire(VisionTrigger.Reset);
    }
}