using System;
using System.Collections.Generic;
using System.Linq; // Là công cụ xử lý danh sách

class Task
{
    public string Name; // Tên task
    public int Duration; // Thời gian chạy
    public List<string> Dependencies = new List<string>(); // Danh sách các task phải chạy xong trước
    public int StartTime;
    public int EndTime;
}

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < n; i++)
        {
            string[] parts = Console.ReadLine().Split(' ');
            Task t = new Task();
            t.Name = parts[0];
            t.Duration = int.Parse(parts[1]);
            for (int j = 2; j < parts.Length; j++)
            {
                t.Dependencies.Add(parts[j]);
            }
            tasks.Add(t);
        }

        int sequentialtotal = 0;
        for (int i = 0; i < tasks.Count; i++)
        {
            sequentialtotal += tasks[i].Duration;
        }

        Task FindTaskByName(List<Task> list, string name)
        // Tạo một hàm tên FindTaskName, hàm này nhận vào một danh sách Task tên là list
        // Và một chuỗi tên là name
        // Sau khi tìm xong, hàm trả về một kiểu task
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name == name) return list[i];
            }
            return null;
        }
        // Duyệt từng task trong danh sách list
        // Nếu task nào có Name bằng với name cần tìm, thì trả về task đó
        // Nếu duyệt hết danh sách mà vẫn không tìm thấy, trả về null

        for (int i = 0; i < tasks.Count; i++)
        {
            tasks[i].StartTime = -1; // Chưa tính
            tasks[i].EndTime = -1; // Chưa tính
        }

        bool progress;
        do // Chạy code trước sao đó mới kiểm tra điều kiện trong while
        {
            progress = false;
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].StartTime == -1) // Chưa tính
                {
                    bool canstart = true; // Cho phép tính
                    int maxdepend = 0;
                    for (int d = 0; d < tasks[i].Dependencies.Count; d++)
                    {
                        Task deptask = FindTaskByName(tasks, tasks[i].Dependencies[d]);
                        if (deptask == null)
                        {
                            continue;
                        }
                        if (deptask.EndTime == -1) // Chưa tính xong
                        {
                            canstart = false; // Thì chưa chạy được
                            break;
                        }
                        if (deptask.EndTime > maxdepend)
                        {
                            maxdepend = deptask.EndTime;
                        }    
                    }
                    // Ví dụ là task này có những thông số sau D 5 B C
                    // Ta có tasks[i].Dependencies thì sẽ là ["B","C"]
                    // Ta có tasks[i].Dependencies[d] = "B";
                    // Ta có Task deptask = FindTaskByName(tasks, "B");
                    // Hàm trên này trả về là Task B nếu Task B tồn tại trong Task
                    if (canstart)
                    {
                        tasks[i].StartTime = maxdepend;
                        tasks[i].EndTime = tasks[i].StartTime + tasks[i].Duration;
                        progress = true;
                    }    
                }    
            }    
        }
        while (progress);
        {
            tasks.Sort((a, b) => // Task thuộc list nên nó có phương thức sort
            // Phương thức sort dùng để sắp xếp các phần tử ngay trong list đó
            // Ta có (a, b) => {...} là kiểu lamda expression trong C# với a, b là tham số đầu vào
            // Ta có {...} là thân hàm
            // Khi lấy a và b ra so sánh thì chúng sẽ thuộc tasks nên có thuộc tính starttime và stoptime
            {
                int cmp = a.StartTime.CompareTo(b.StartTime);
                if (cmp != 0)
                {
                    return cmp;
                }
                return string.Compare(a.Name, b.Name, StringComparison.Ordinal);
                // StringComparison.Ordinal nghĩa là gì ?
                // So sánh theo giá trị byte của từng ký tự (theo bảng mã ASCII/Unicode), không phụ thuộc ngôn ngữ hoặc văn hóa
                // Ordinal là theo mã ASCII thuần túy, nhanh, nhất quán
                // So sánh theo bảng chữ cái ASCII thuần túy
                // Nếu < 0 thì a đứng trước b
                // Nếu > 0 thì a đứng sau b
            });
            
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{tasks[i].Name} : start={tasks[i].StartTime}ms, end={tasks[i].EndTime}ms");
            }

            int paralleltotal = 0; // Tìm task với endtime chạy muộn nhất
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].EndTime > paralleltotal)
                {
                    paralleltotal = tasks[i].EndTime;
                }
            }
            
            Console.WriteLine($"Sequential : {sequentialtotal}ms");
            Console.WriteLine($"Parallel : {paralleltotal}ms");

            double speedup = (double)sequentialtotal / paralleltotal;
            Console.WriteLine($"Speedup : {speedup:F1}x");
        }
    }
}
