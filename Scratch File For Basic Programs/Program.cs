/*
namespace PC_Control_Class_3;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World");
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
namespace PC_Control_Class_3;

class Program
{
    static void Main(string[] args)
    {
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());
        Console.WriteLine($"{a + b}");
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
namespace PC_Control_Class_3;

class Program
{
    static void Main(string[] args)
    {
        int j = 0;
        string line1 = Console.ReadLine();
        string line2 = Console.ReadLine();
        string[] line1_split = line1.Split(' ');
        string[] line2_split = line2.Split(' ');
        int n = int.Parse(line1_split[0]);
        int T = int.Parse(line1_split[1]);
        int[] line2_int = new int[n];
        List<int> position = new List<int>();
        for (int i = 0; i < n; i++)
        {
            line2_int[i] = int.Parse(line2_split[i]);
        }

        for (int i = 0; i < n; i++)
        {
            if (line2_int[i] > T)
            {
                j = j + 1;
                position.Add(i);
            }
        }
        Console.WriteLine(j);
        if (j == 0)
        {
            Console.WriteLine("NONE");
        }
        else
        {
            Console.WriteLine(string.Join(" ", position)); 
        }
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
class Program
{
    static void Main(string[] args)
    {
        string line1 = Console.ReadLine();
        string line2 = Console.ReadLine();
        string[] line2_split = line2.Split(' '); // Là mảng
        int n = int.Parse(line1);
        List<int> line2_int = new List<int>(); // Là danh sách
        for (int i = 0; i < n; i++)
        {
            line2_int.Add(int.Parse(line2_split[i]));
        }
        Console.WriteLine($"Max : {line2_int.Max()}");
        Console.WriteLine($"Min : {line2_int.Min()}");
        Console.WriteLine($"AVG : {Math.Round(line2_int.Average(), 2, MidpointRounding.AwayFromZero):F2}"); 
        // Làm tròn lên 2.5 sẽ là 3
        // Dùng :F2 để hiển thị 2 số thập phân
        int range = line2_int.Max() - line2_int.Min();
        Console.WriteLine($"Range : {range}");
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
class Program
{
    static void Main(string[] args)
    {
        int gradient;
        string line1 = Console.ReadLine();
        string line2 = Console.ReadLine();
        string[] line1_split = line1.Split(' ');
        string[] line2_split = line2.Split(' '); // Tạo ra string array, có method là length
        List<int> line1_int = new List<int>(); // Tạo ra danh sách rỗng chứa các số nguyên, có method là count
        List<int> line2_int = new List<int>(); 
        List<int> gradient_int_index = new List<int>();
        for (int i = 0; i < line1_split.Length; i++)
        {
            line1_int.Add(int.Parse(line1_split[i]));
        }

        int n = line1_int[0];
        int T = line1_int[1];
        
        for (int i = 0; i < line2_split.Length; i++)
        {
            line2_int.Add(int.Parse(line2_split[i]));
        }

        for (int i = 1; i < n - 1; i++)
        {
            gradient = Math.Abs(line2_int[i + 1] - line2_int[i - 1]);
            if (gradient > T)
            {
                gradient_int_index.Add(i);
            }
        }
        Console.WriteLine(gradient_int_index.Count);
        if (gradient_int_index.Count == 0)
        {
                Console.Write("NONE"); 
                // Lưu ý đọc kỹ đề bài nha, nó kêu là in số lượng rồi sao đó là index, nếu index không có thì là NONE mà mình in NONE trước khi in số lượng nên sai
        }
        else
        {
            Console.WriteLine($"{string.Join(' ', gradient_int_index)}");
        }
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
class Program
{
    static void Main(string[] args)
    {
        int count = 0;
        String line1 = Console.ReadLine();
        String[] line1_split = line1.Split(' ');
        int m = int.Parse(line1_split[0]);
        int n = int.Parse(line1_split[1]);
        int T = int.Parse(line1_split[2]);
        int[,] array_2d_int = new int[m, n];
        for (int i = 0; i < m; i++)
        {
            String array_2d = Console.ReadLine();
            String[] array_2d_split = array_2d.Split(' ');
            for (int j = 0; j < n; j++)
            {
                array_2d_int[i, j] = int.Parse(array_2d_split[j]);
            }
        }

        for (int i = 0; i < m; i++)
        {
            count = 0;
            for (int j = 0; j < n; j++)
            {
                if (array_2d_int[i, j] >= T)
                {
                    count++;
                } 
            }
            Console.WriteLine($"Row {i}: {count}");
        }
        
        for (int j = 0; j < n; j++)
        {
            count = 0;
            for (int i = 0; i < m; i++)
            {
                if (array_2d_int[i, j] >= T)
                {
                    count++;
                }
            }
            Console.WriteLine($"Col {j}: {count}");
        }
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
class Program
{
    static void Main(string[] args)
    {
        String line1 = Console.ReadLine();
        String[] line1_split = line1.Split(' ');
        int n = int.Parse(line1_split[0]);
        int k = int.Parse(line1_split[1]);
        String line2 = Console.ReadLine();
        String[] line2_split = line2.Split(' ');
        List<int> line2_int = new List<int>();
        List<int> filter_value = new List<int>();
        List<int> result = new List<int>();
        for (int i = 0; i < n; i++) // Phải nhớ gán giá trị ban đầu cho nó nhé
        {
            line2_int.Add(int.Parse(line2_split[i]));
        }

        for (int i = 0; i < n; i++) // Phải nhớ gán giá trị ban đầu cho nó nhé
        {
            int filter_index = Math.Max(0, (i - k + 1));
            for (int j = filter_index; j <= i; j++)
            {
                filter_value.Add(line2_int[j]); // Chỗ này nó bị tích tụ dữ liệu cũ nếu chúng ta không clear dẫn đến kết quả sai
            }
            int avg = (int)filter_value.Average();
            result.Add(avg);
            filter_value.Clear();
        }
        Console.WriteLine(string.Join(' ', result));
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
class Program
{
    static void Main(string[] args)
    {
        String line1 = Console.ReadLine();
        String[] line1_split = line1.Split(' ');
        List<int> lines_int = new List<int>();
        int m = int.Parse(line1_split[0]);
        int n = int.Parse(line1_split[1]);
        int[,] matrix = new int [m, n];
        int[,] result = new int[n, m];
        for (int i = 0; i < m; i++)
        {
            String lines = Console.ReadLine();
            String[] lines_split = lines.Split(' ');
            for (int j = 0; j < n; j++)
            {
                matrix[i,j] = int.Parse(lines_split[j]);
            }
        }

        for (int r = 0; r < m; r++)
        {
            for (int c = 0; c < n; c++)
            {
                result[c, m - 1 - r] = matrix[r, c];
            }
        }

        for (int i = 0; i < n; i++)
        {
            List<int> result_int = new List<int>();
            for (int j = 0; j < m; j++)
            {
                result_int.Add(result[i, j]);
            }
            Console.WriteLine(string.Join(" ", result_int));
        }
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
using System;

class MotionAxis
{
    private double _position;
    public string Name { get; set; }

    public double Position
    {
        get { return _position; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Position không được âm");
            if (value > 1000)
                throw new ArgumentException("Vượt quá giới hạn hành trình");
            _position = value;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var axis = new MotionAxis();
        axis.Name = "X-Axis";
        axis.Position = 500;
        Console.WriteLine($"{axis.Name} : Position: {axis.Position}");

        try
        {
            axis.Position = -10;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }

        try
        {
        axis.Position = 2000;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
using System;

namespace MotionControlApp
{
    public class MotionAxis
    {
        // Các thuộc tính
        public string Name { get; set; } // Gọi là auto implement property
        public double MaxSpeed { get; set; } // Gọi là auto implement property
        public bool IsEnabled { get; private set; } // private chỉ có tác dung trong class thôi
        public MotionAxis(string name, double maxSpeed)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            IsEnabled = false;
            Console.WriteLine($"Motion axis {Name} contructor created with max speed {MaxSpeed}");
        }

        public void ShowStatus()
        {
            string status = IsEnabled ? "Đang hoạt động" : "Đang dừng";
            Console.WriteLine($"Trục : {Name} | Tốc độ tối đa : {MaxSpeed} | Trạng thái : {status}");
        }
    }

    class MyClass
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var axisX = new MotionAxis("X-Axí", 500);
            var axisY = new MotionAxis("Y-Axis", 200);
            axisX.ShowStatus();
            axisY.ShowStatus();
            Console.WriteLine("Nhấn bất kỳ phím nào để thoát");
            Console.ReadKey(); // Lệnh này bắt chương trình dừng lại và đợi người dùng nhấn một phím bất kỳ trên bàn phím. Nhấn rồi thì nó sẽ thóat và dừng chương trình.
        }
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
using System;

class MotionAxis
{
    private double _position;
    private bool _isHome;
    
    public string Name { get; }
    public double MaxTravel { get; }
    public double Position => _position; // Khai báo cách này có thể chế biến được giá trị trả về, tách ra để có thể set dữ liệu được
    public bool IsHome => _isHome; // Khai báo cách này có thể chế biến được giá trị trả về, tách ra để có thể set dữ liệu được

    public MotionAxis(string name, double maxTravel)
    {
        Name = name;
        MaxTravel = maxTravel;
    }

    public void HomeAxis()
    {
        _position = 0;
        _isHome = true;
        Console.WriteLine($"[{Name}] homing completed");
    }

    public bool MoveAbsolute(double target)
    {
        if (!_isHome)
        {
            Console.WriteLine($"[{Name}] chưa home");
            return false;
        }

        if (target < 0 || target > MaxTravel)
        {
            Console.WriteLine($"[{Name}] ngoài phạm vi");
            return false;
        }
        Console.WriteLine($"[{Name}] is moving {_position:F1} to {target:F1}");
        _position = target;
        return true;
    }

    public bool MoveRelative(double distance) => MoveAbsolute(_position + distance);

    public string GetStatus() => $"[{Name}] Pos = {_position:F2}, Homed = {_isHome}";
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var axisX = new MotionAxis("X-Axis", 1000);
        // Thử chi chuyển trước khi home
        axisX.MoveAbsolute(100);
        //Home rồi di chuyển
        axisX.HomeAxis();
        axisX.MoveAbsolute(250);
        axisX.MoveRelative(100);
        axisX.MoveAbsolute(20000); // Ngoài phạm vi
        
        Console.WriteLine(axisX.GetStatus());
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
using System;
using System.Collections.Generic;

class MotionAxis
{
    private double _speed;
    private double _position;
    private bool _isHome;
    
    public string Name { get; }
    public double Position => _position;
    public double Speed => _speed;
    public bool IsHome => _isHome;

    public MotionAxis(string name)
    {
        Name = name;
        _position = 0;
        _speed = 100;
        _isHome = false;
    }

    public string MoveAbsolute(double target)
    {
        _position = target; // Dùng bên trong class, nếu bên ngoài thì dùng Position
        return ($"{Name}: moved to {_position:f2}");
    }

    public string HomeAxis()
    {
        _position = 0;
        _isHome = true;
        return ($"{Name}: homed");
    }

    public string SetSpeed(double speed)
    {
        _speed = speed;
        return ($"{Name}: speed set to {_speed:f2}");
    }

    public string Status() => $"{Name}: Position={_position:f2} Speed={_speed:f2} IsHome={_isHome}";
    
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        string line1 = Console.ReadLine();
        int n = int.Parse(line1);
        Dictionary<string, MotionAxis> axis = new Dictionary<string, MotionAxis>();
        List<string> results = new List<string>();
        for (int i = 0; i < n; i++)
        {
            string[] commandParts = Console.ReadLine().Split(' ');
            string commandType = commandParts[0];
            string axisName = commandParts[1];

            switch (commandType)
            {
                case "CREATE":
                    axis[axisName] = new MotionAxis(axisName);
                    results.Add($"Created axis '{axisName}'");
                    break;
                case "MOVE":
                    double position = double.Parse(commandParts[2]);
                    results.Add((axis[axisName].MoveAbsolute(position)));
                    break;
                case  "SPEED":
                    double speed = double.Parse(commandParts[2]);
                    results.Add(axis[axisName].SetSpeed(speed));
                    break;
                case "STATUS":
                    results.Add(axis[axisName].Status());
                    break;
                case "HOME":
                    results.Add(axis[axisName].HomeAxis());
                    break;
            }
        }

        foreach (string result in results)
        {
            Console.WriteLine(result);
        }
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
using System;

class Cylinder
{
    public string Name { get; set; }
    public bool IsExtended { get; set; }
    public int CycleCount { get; set; }
    public int MaxCycle { get; set; }

    public Cylinder(string name, int maxCycle)
    {
        Name = name;
        MaxCycle = maxCycle;
        IsExtended = false;
        CycleCount = 0;
    }

    public void Extend()
    {
        IsExtended = true;
    }

    public void Retract()
    {
        if (IsExtended)
        {
            IsExtended = false;
            CycleCount++;
        }
    }

    public bool NeedsMaintenance()
    {
        return CycleCount >= MaxCycle();
    }
}

class CylinderStation
{
    private List<Cylinder> _cylinders;

    public CylinderStation()
    {

        _cylinders = new List<Cylinder>();
    }
    
    public void AddCylinder(Cylinder cyl)
    {
        _cylinders.Add(cyl);
    }

    public void RetractAll()
    {
        foreach (Cylinder cyl in _cylinders)
        {
            cyl.Retract();
        }
    }

    public void ExtendAll()
    {
        foreach (Cylinder cyl in _cylinders)
        {
            cyl.Extend();
        }
    }

    public void RunCycle()
    {
        ExtendAll();
        RetractAll();
    }

    public List<Cylinder> GetMaintenanceList()
    {
        return _cylinders.Where(c => c.NeedsMaintenance()).ToList();
    }

    public void PrintReport()
    {
        Console.WriteLine("Báo Cáo : ");
        foreach (var cyl in _cylinders)
        {
            string status = cyl.NeedsMaintenance() ? "Cần Bảo Trig" : "Không Cần Bảo Trì";
            Console.WriteLine($"{cyl.Name} : {cyl.CycleCount}/{cyl.MaxCycle} Cycles - {status}");
        }
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
using System;
using System.Collections.Generic;

public enum CylinderState
{
    Retracted,
    Extending,
    Extended,
    Retracting,
}

class Cylinder
{
    public string Name { get; set; }
    public CylinderState State { get; set; }
    
     // Khai báo ở đây là sai vì mỗi đối tượng sẽ có một field result để lưu các giá trị của nó
     // Nên khi quét chạy trên đối tượng đó nó sẽ in tất cả các lệnh của nó, không quan tâm thứ tự nhập vào
     // Nê nhập liên tiếp 2 lệnh create nó sẽ in ra sai thứ tự

    public Cylinder(string name) // Contractor trong C# không có trả về nha
    {
        Name = name;
        State = CylinderState.Retracted;
        // Console.WriteLine($"Cylinder '{Name}' created");
    }

    public string Extend()
    {
        if (State == CylinderState.Retracted)
        {
            State = CylinderState.Extending;
            // Console.WriteLine($"{Name}: extending");
            return ($"{Name}: extending");
        }
        else
        {
            // Console.WriteLine($"ERROR: {Name} can not extend from {State}");
            return ($"ERROR: {Name} cannot extend from {State}");
        }
    }

    public string Retract()
    {
        if (State == CylinderState.Extended)
        {
            State = CylinderState.Retracting;
            // Console.WriteLine($"{Name}: retracting");
            return ($"{Name}: retracting");
        }
        else
        {
            // Console.WriteLine($"ERROR: {Name} can not retract from {State}");
            return ($"ERROR: {Name} cannot retract from {State}");
        }
    }

    public string Update()
    {
        if (State == CylinderState.Extending)
        {
            State = CylinderState.Extended;
            // Console.WriteLine($"{Name}: Extended");
            return ($"{Name}: extended");
        }
        else if (State == CylinderState.Retracting)
        {
            State = CylinderState.Retracted;
            // Console.WriteLine($"{Name}: Retracted");
            return ($"{Name}: retracted");
        }
        else
        {
            // Console.WriteLine($"{Name}: no transition");
            return ($"{Name}: no transition");
        }
    }

    public string Status()
    {
        // Console.WriteLine($"{Name}: {State}");
        return ($"{Name}: {State}");
    }
}

class Program
{
    static void Main()
    {
        Dictionary<string, Cylinder> cylinders = new Dictionary<string, Cylinder>();
        int n = int.Parse(Console.ReadLine());
        List<string> results = new List<string>();
        for (int i = 0; i < n; i++)
        {
            string lines = Console.ReadLine();
            string[] lines_split = lines.Split(' ');
            string cmd = lines_split[0];
            string name = lines_split[1];

            switch (cmd)
            {
                case "CREATE":
                    cylinders[name] = new Cylinder(name); // Tạo đối tượng mới và lưu vào key, đối tượng mới được lưu vào biến string
                    results.Add($"Cylinder '{name}' created");
                    break;
                case "STATUS":
                    results.Add(cylinders[name].Status());
                    break;
                case "EXTEND":
                    results.Add(cylinders[name].Extend());
                    break;
                case "UPDATE" :
                    results.Add(cylinders[name].Update());
                    break;
                case "RETRACT":
                    results.Add(cylinders[name].Retract());
                    break;
            }
        }
        
        foreach (string result in results)
        {
            Console.WriteLine(result);
        }
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
using System;
using System.Collections.Generic;

class Sensor
{
    private double _value;

    public int Id { get; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public double Value => _value;
    
    public Sensor(int id, string name, string unit)
    {
        Id = id;
        Name = name;
        Unit = unit;
        _value = 0; // Không gán Value bằng 0 vì biến này chỉ được đọc thôi, nên phải gán trực tiếp vào vùng nhớ
    }

    public string Display()
    {
        return ($"[{Name}] {Value:f2} {Unit}");
    }

    public string CheckRange(double min, double max)
    {
        if (Value >= min && Value <= max)
            return ($"Sensor #{Id}: {Value:f2} {Unit} - OK");
        return ($"Sensor #{Id}: {Value:f2} {Unit} - ALARM");
    }

    public string SetValue(double value)
    {
        _value = value;
        return ($"Sensor #{Id}: {Value:f2}");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Dictionary<string, Sensor> sensor = new Dictionary<string, Sensor>();
        List<string> results = new List<string>();
        string line1 = Console.ReadLine();
        int n = int.Parse(line1);
        for (int i = 0; i < n; i++)
        {
            string[] lines = (Console.ReadLine()).Split(' ');
            string command = lines[0];

            switch (command)
            {
                case "ADD":
                {
                    int id = int.Parse(lines[1]);
                    string id_string = lines[1];
                    string name = lines[2];
                    string unit = lines[3];
                    sensor[id_string] = new Sensor(id, name, unit); // Không thể để này trong contractor được vì nó sẽ in ra liền sau khi tạo xong đối tượng
                    results.Add($"Sensor #{id} '{name}' added");
                    break;
                }
                // Chúng ta có thể đóng ngoặc nhọn 1 cái case để giới hạn phạm v truy cập của các biến trong đó


                case "SET":
                {
                    double val = double.Parse(lines[2]);
                    string id_string = lines[1];
                    results.Add(sensor[id_string].SetValue(val));
                    break;
                }

                case "DISPLAY":
                {
                    string id_string = lines[1];
                    results.Add(sensor[id_string].Display());
                    break;
                }

                case "CHECK":
                {
                    string id_string = lines[1];
                    double min = double.Parse(lines[2]);
                    double max = double.Parse(lines[3]);
                    results.Add(sensor[id_string].CheckRange(min, max));
                    break;
                }
            }
        }

        foreach (string result in results)
        {
            Console.WriteLine(result);
        }
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int pass_count = 0;
        int fail_count = 0;
        int total = 0;
        float pass_count_per = 0;
        float fail_count_per = 0;
        int n = int.Parse(Console.ReadLine());
        List<string> result = new List<string>();
        for (int i = 0; i < n; i++)
        {
            var string_line = Console.ReadLine();
            if (string_line == "FAIL")
            {
                fail_count++;
                
            }
            else if (string_line == "PASS")
            {
                pass_count++;
            }
            else if (string_line == "RESET")
            {
                pass_count = 0;
                fail_count = 0;
                total = 0;
            }
            else if (string_line == "REPORT")
            {
                total = pass_count + fail_count;
                pass_count_per = (pass_count * 100) / total;
                fail_count_per = (fail_count * 100) / total;
                result.Add($"Total: {total}");
                result.Add($"Pass: {pass_count} ({pass_count_per:F2}%)"); 
                result.Add($"Fail: {fail_count} ({fail_count_per:F2}%)");
            }
        }

        foreach (var res in result)
        {
            Console.WriteLine(res);
        }
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
using System;
using System.Collections.Generic;

class Device
{
    public string Name { get; set; }
    public bool IsConnected { get; protected set; } // IsConnected dùng protected set nên chỉ class con mới set được, bên ngoài không set trực tiếp được

    public void Connect()
    {
        IsConnected = true;
        Console.WriteLine($"{Name} connected.");
    }

    public void Disconnect()
    {
        IsConnected = false;
        Console.WriteLine($"{Name} disconnected");
    }
}

class Camera : Device
{
    public string resolution { get; set; }

    public void Capture()
    {
        Console.WriteLine($"Capturing at {resolution})");
    }
}

class PLC : Device
{
    public string IPAddress { get; set; }
}

class Robot : Device
{
    public int AxisCount { get; set; }
}

class Program
{
    static void Main()
    {
        
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
class Device
{
    public virtual void GetInfo()
    {
        Console.WriteLine("Tôi là thiêt bị");
    }
}

class Camera : Device
{
    public override void GetInfo()
    {
        Console.WriteLine("Tôi là camera");
    }
}

class PLC : Device
{
    
}

class Program
{
    static void Main()
    {
        Device d = new Camera();
        d.GetInfo();
        Device p = new PLC();
        p.GetInfo();
    }
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
class Device
{
    public string Name { get; set; } // Đây là property cho phép đọc ghi từ bên ngoài
    public bool IsConnected { get; protected set; } // Đây là property cho phép đọc từ bên ngoài còn protected thì chỉ class này và class con mới set được
}

public Device(string name) // Đây là contractor
{
    Name = name;
}

public virtual void Connect() // Có virtual thì phương thức của class con mới có thể override được
{
    IsConnected = true;
    Console.WriteLine($"[{Name}] Connected");
}

public virtual void Disconnect()
{
    IsConnected = false;
    Console.WriteLine($"[{Name}] Disconnected");
}

pubblic virtual string GetInfo()
{
    return $"Device : {Name} | Connected : {IsConnected}";
}

class Camera : Device
{
    public string Resolution { get; }
    public int FrameRate { get; }

    public Camera(string name, string resolution, int fps) : base(name) // Đưa name lên cho lớp cha xử lý, tránh việc lặp lại code
    {
        Resolution = resolution;
        FrameRate = fps;
    }

    public override string GetInfo()
    {
        return $"Camera : {Name} [{Resolution} @ {FrameRate}]";
    }

    public void Capture()
    {
        if (!IsConnected)
        {
            Console.WriteLine($"[{Name}] Cannot capture - not connected");
            return;
        }
        Console.WriteLine($"[{Name}] Captured frame at {Resolution}");
    }
}

class PLC : Device
{
    public string IPAddress { get; }

    public PLC(string name, string ip) : base(name) // Gọi contructor của device lớp cha để device tự lo việc gán Name
    {
        IPAddress = ip;
    }

    public override void Connect()
    {
        Console.WriteLine($"[{Name}] Opening TCP to {IPAddress}:502...");
        base.Connect(); // Gọi phương thức của lớp cha là Connect
    }

    public int ReadRegister(int address)
    {
        Console.WriteLine($"[{Name}] Read register D{address}");
        return 0;
    }

    public void WriteRegister(int address, int value)
    {
        Console.WriteLine($"[{Name}] Write D{address} = {value}");
    }

    public override string GetInfo()
    {
        return $"PLC : {Name} [{IPAddress}]";
    }
}

class Robot : Device
{
    public int AxisCount { get; }

    public Robot(string name, int axes) : base(name)
    {
        AxisCount = axes;
    }

    public override string GetInfo()
    {
        return $"Robot : {Name} [{AxisCount} - axis]"
    }

    public void Home()
    {
        Console.WriteLine($"[{Name}] Homing all {AxisCount} axes...");
    }

    public void MoveTo(double x, double y, double z)
    {
        Console.WriteLine($"[{Name}] Moving to {x}, {y}, {z}");
    }
}

List<Device> factory = new List<Device> // Khai báo một list danh sách các đối tượng chứa kiểu dữ liệu device
{
    new Camera("InspectCam", "4K", 60),
    new PLC("MainPLC", "192.168.1.10"),
    new Robot("PickArm", 6),
    new Camera("QualityCam", "10080p", 30),
};
// Ta có var là một từ khóa đặc biệt trong C# cho phép bạn không cần chỉ định rõ kiểu dữ liệu
// Trình biên dịch sẽ tự động suy ra kiểu dữ liệu dựa trên ngữ cảnh
foreach (var device in factory)
{
    device.Connect();
}
Console.WriteLine("\n---Device Info---");

foreach (var device in factory)
{
    Console.WriteLine(device.GetInfo());
}

foreach (var device in factory)
{
    if (device is Camera cam) cam.Capture(); // Này là kiểu pattern matching, nếu device là Camera thì tạo 1 biến mới tên cam chứa device đó
    if (device is Robot robot) robot.Home(); // Nay là kiểu pattern matching, nếu device là Robot thì tạo 1 biến mới tên robot chứa device đó

}
// Đa hình nghĩa là nhiều hình thái. Cùng một câu lệnh gọi cùng một cái tên phương thức.
// Nhưng nó sẽ tạo ra các hành động khác nhau tùy thuộc vào đối tượng đang nhận lệnh là ai.
*/

/*--------------------------------------------------------------------------------------------------------------------*/

/*
 Cách viết cũ
 if (item is Camera)
 {
    Camera cam = (Camera)item; // Đây là cách ép kiểu trong C#
    cam.Capture();
 }
 Cách viết mới
 if (item is Camera cam)
 {
    camera.Capture()
 }
*/ 

/*--------------------------------------------------------------------------------------------------------------------*/

/*
enum MachineState
{
    Idle = 0,
    Initalizing = 1,
    Running  = 2,
    Paused = 3,
    Error = 4,
    Stopped = 5
}

class Machine
{
    public string Name { get; set; }
    public MachineState State { get; set; } = MachineState.Idle; // Gán giá trị khi khai báo luôn
    // MachineState là kiểu dữ liệu tự định nghĩa, cụ thể là Enum

    public Machine(string name)
    {
        Name = name;
    }

    public void Transition(string command)
    {
        MachineState? next = GetNextState(command); // Dấu chấm hỏi biến nó thành một kiểu nullable, nó biến MachineState thành một kiểu nullable type
        if (next == null)
            Console.WriteLine($"Error : Cannot {command} from {State}");
        else
        {
            MachineState old = State;
            State = next.Value;
            Console.WriteLine($"{Name} : {old} -> {State}");
        }
    }

    private MachineState? GetNextState(string command)
    {
        switch (State) // Kiểm tra xem máy đang ở trạng thái nào
        {
            case MachineState.Idle:
                if (command == "INIT") return MachineState.Initalizing; // Trả về con số 1
                if (command == "STOP") return MachineState.Stopped;
                break;
            case MachineState.Initalizing:
                if (command == "RUN") return MachineState.Running;
                if (command == "ERROR") return MachineState.Error;
                break;
            case MachineState.Running:
                if (command == "PAUSE") return MachineState.Paused;
                if (command == "ERROR") return MachineState.Error;
                if (command == "STOP") return MachineState.Stopped;
                break;
            case MachineState.Paused:
                if (command == "RESUME") return MachineState.Running;
                if (command == "STOP") return MachineState.Stopped;
                break;
            case MachineState.Error:
                if (command == "RESET") return MachineState.Idle;
                if (command == "RESET") return MachineState.Idle;
                break;
        }

        return null;
    }
}

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        var machines = new Dictionary<string, Machine>();
        for (int i = 0; i < n; i++)
        {
            string[] line_1 = Console.ReadLine().Split(' ');
            string cmd = line_1[0];
            string name = line_1[1];
            if (cmd == "CREATE")
            {
                machines[name] = new Machine(name);
                Console.WriteLine($"Created machine '{name}'");
            }
            else if (cmd == "STATE")
            {
                Console.WriteLine("{name}: {machines[name].State}");
            }
            else
            {
                machines[name].Transition(cmd);
            }
        }
    }
}
/*
 
/*--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args) 
    // Task là đại diện công việc đang chạy
    // Ta có async là đánh dấu task này là task bất đồng bộ
    {
        Console.WriteLine("1. Start");
        await Task.Delay(3000);
        Console.WriteLine("2. Stop After 3 Seconds");
    }
}
 
/*--------------------------------------------------------------------------------------------------------------------*/

/*
enum MachineState
{
    Idle,
    Running,
    Paused,
    Stopping,
    Error
}

enum Command
{
    Start,
    Stop,
    Pause,
    Resume,
    Reset,
    DoneStop
}

static MachineState NextState(MachineState state, Command command)
{
    return (state, command) switch
    {
        (MachineState.Idle, Command.Start) => MachineState.Running,
        (MachineState.Running, Command.Pause) => MachineState.Paused,
        (MachineState.Paused, Command.Resume) => MachineState.Running,
        (MachineState.Running, Command.Stop) => MachineState.Stopping,
        (MachineState.Stopping, Command.DoneStop) => MachineState.Idle,
        (MachineState.Error, Command.Reset) => MachineState.Idle,
        _ => state
    };
}
*/

/*--------------------------------------------------------------------------------------------------------------------*/