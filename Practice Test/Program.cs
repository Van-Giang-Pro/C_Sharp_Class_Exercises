public sealed class ConfigManager
// Từ khóa seal để không cho class nào khác kế thừa nó
{
    private static readonly Lazy<ConfigManager> _lazy = new(() => new ConfigManager());



    /* 
    Lamda không có tham số truyền vào hàm, nhưng chưa chạy ngay, khi nào cần thì làm thế này

    Cách bình thường là private static ConfigManager _instance = new Configmanager() là tạo ngay object khi load

    Dòng này — chỉ lưu lambda, chưa tạo ConfigManager

    private static readonly Lazy<ConfigManager> _lazy = new(() => new ConfigManager());

    Dòng này — lần đầu gọi .Value → mới chạy lambda → tạo ConfigManager
    var instance = _lazy.Value; // trả về object

    Lần 2 gọi .Value → trả về object cũ, không tạo lại
    var instance2 = _lazy.Value;  // same object

    Tại sao private mà còn readonly nữa là vì :
    1. Ta có readonly là dù trong calss, cũng không được gán lại _lazy sau khi khởi tạo
    2. Ta có private là ai được truy cập, chỉ trong class này mới thấy _lazy

    Ta có _lazy là một cái hộp Lazy chứa cách tạo ConfigManager. Khi gọi _lazy.Value, nó mới tạo ra ConfigManager

    Cách hiểu là : 
    1. Ta có private là chỉ class này mới dùng được
    2. Ta có static là dùng chung cho cả class, không cần tạo instance
    3. Ta có readonly chỉ gán một lần không gán lại được
    4. Ta có Lazy<ConfigManager> kiểu dữ liệu generic class Lazy bọc ConfigManager bên trong (object thực sự bạn đang muốn dùng)
    5. Ta có _lazy là tên biến
    6. Ta có = là gán giá trị
    7. Ta có new(() => new ConfigManager()) tạo object Lazy

    Ví dụ cho dễ hiểu :
    Ta có Lazy<int> so = new Lazy<int>(() => 10) là so là một cái hộp cái hộp này sau này sẽ đưa ra một số int nhưng hiện tại nó chưa đưa ra ngay
    Ta có cái ConfigManager là cái object thật
    */
}