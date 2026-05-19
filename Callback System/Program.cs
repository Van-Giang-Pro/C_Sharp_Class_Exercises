using System;
using System.Collections.Generic;

delegate void CallbackHandler();
// Hãy tưởng tượng CallbackHandler như một cái hộp được thiết kế đặc biệt.
// Bạn chỉ có thể bỏ vào hộp đó những thứ có hình dạng nhất định.
// Trong trường hợp này, hình dạng đó là một hàm không tham số, không trả về giá trị.
class Callback
{
    public string Name;
    public string Type;
    public CallbackHandler Handler;
    // Handler là tên của biến (thuộc tính) trong lớp Callback.
    // Vì nó có kiểu là CallbackHandler, biến Handler này chính là cái hộp mà chúng ta nói ở trên.
    // Nó sẵn sàng để chứa một hàm nào đó.

    public Callback(string name, string type)
    {
        Name = name;
        Type = type;
        Handler = () => Console.WriteLine($"  -> {name} executed");
        // Handler là biến mà chúng ta đã nói đến.
        // Nó có kiểu là CallbackHandler, nghĩa là nó có thể giữ một hàm nào đó không có tham số và không trả về giá trị.
        // () => là biểu thức lamda expression, nó là một cách viết tắt cực kỳ ngắn gọn để định nghĩa một hàm ngay tại chỗ mà không cần đặt tên cho nó
        // Bên trái => là () : danh sách tham số
        // Bên phải => là thân hàm body
    }
}

class CallbackManager
{
    public List<Callback> callbackList = new List<Callback>();
    public bool IsExists(string name)
    {
        foreach (Callback cb in callbackList)
        {
            if (cb.Name == name)
                return true;
        }
        return false;
    }
    public void Register(string name, string type)
    {
        if (IsExists(name))
        {
            Console.WriteLine($"Callback '{name}' already exists");
            return;
        }
        Callback newCallBack = new Callback(name, type);
        callbackList.Add(newCallBack);
        Console.WriteLine($"Registered: {name} [{type}]");
    }
    public void Trigger(string type)
    {
        bool found = false;
        foreach (Callback cb in callbackList)
        {
            if (cb.Type == type)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine($"No callbacks for '{type}'");
            return;
        }
        Console.WriteLine($"Triggering '{type}'...");
        foreach (Callback cb in callbackList)
        {
            if (cb.Type == type)
            {
                cb.Handler();
            }
        }
    }
    public void List()
    {
        if (callbackList.Count == 0)
        {
            Console.WriteLine("No callbacks");
            return;
        }
        foreach (Callback cb in callbackList)
        {
            Console.WriteLine($"{cb.Name} [{cb.Type}]");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        CallbackManager manager = new CallbackManager();
        for (int i = 0; i < n; i++)
        {
            string[] abc = Console.ReadLine().Split(' ');
            string command = abc[0];
            switch (command)
            {
                case "REGISTER":
                    string regName = abc[1];
                    string regType = abc[2];
                    manager.Register(regName, regType);
                    break;

                case "TRIGGER":
                    string trigType = abc[1];
                    manager.Trigger(trigType);
                    break;

                case "LIST":
                    manager.List();
                    break;
            }
        }
    }
}
