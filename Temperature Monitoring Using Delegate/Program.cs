/* 
 * Delegate trong C# là một kiểu dữ liệu đặc biệt, nhưng thay vì lưu trữ một con số (int) hay một chuỗi ký tự (string)
 * Nó lưu trữ một tham chiếu (một địa chỉ hoặc một lối tắt) đến một hoặc nhiều phương thức
 * Nó giống như một biến có thể giữ một hoặc nhiều hàm
 * Định nghĩa một khuôn tên là ActionHandler
 * Khuôn này chỉ chấp nhận các hàm không có tham số (dấu ngoặc rỗng) và không trả về giá trị (void)
*/
public delegate void TemperatureEventHandler(double temperature, string status);

public class TemperatureSensor
{
    public event TemperatureEventHandler OnTemperatureAlarm;
    // Khai báo biến này, sự kiện này thuộc Delegate
    // Từ khóa event biến nó từ Delegate bình thường thành sự kiện
    // Cho phép thêm bớt sự kiện chứ không cho xóa những gì trong hộp
    private double minThreshold = 20.0;
    private double maxThreshold = 80.0;
    public void Read(double temperature)
    {
        Console.WriteLine($"Temperature measuring : {temperature}");
        if (temperature < minThreshold || temperature > maxThreshold)
        {
            Console.WriteLine("Detect temperature is over limit, prepare to trigger");
            OnTemperatureAlarm?.Invoke(temperature, "Alarm");// Giống như cái loa
            // Dấu ? (null-conditional operator) : giúp kiểm tra xem hiện tại có ai đang += vào sự kiện này không
            // Nếu biến này là null (chưa có ai đăng ký nghe), nó sẽ bỏ qua lệnh Invoke để tránh lỗi crash app kinh điển
            // Invoke : hành động duyệt qua tất cả các hàm đã đăng ký trong danh sách và ném 2 tham số temperature và alarm cho các hàm đó chạy đồng loạt
        }
    }
}

public class AlarmManager // Giống như cái tai để nghe
{
    public void HandleAlarm(double temperature, string status)
    {
        Console.WriteLine($"Action executed. The temperature is {temperature} with status is {status}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        TemperatureSensor sensor = new TemperatureSensor();
        AlarmManager alarmManager = new AlarmManager();
        // Giám đốc nói với sensor là khi nào cái loa OnTemperatureAlarm của cậu phát
        // Thì hãy nhớ thông báo cho phương thức HandleAlarm của chị AlarmManager nhá
        sensor.OnTemperatureAlarm += alarmManager.HandleAlarm;
        Console.WriteLine("Start Monitoring");
        sensor.Read(50.0); // Loa sẽ không được kích hoạt
        Console.WriteLine();
        sensor.Read(95.0);
        Console.WriteLine("Stop Monitoring");
    }
}
