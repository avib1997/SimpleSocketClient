using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        // כתובת ה-IP והפורט של השרת
        IPAddress serverIP = IPAddress.Parse("127.0.0.1"); // כתובת ה-IP של השרת
        int serverPort = 8080; // פורט השרת

        try
        {
            // יצירת סוקט לשרת
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEndPoint = new IPEndPoint(serverIP, serverPort);

            // התחברות לשרת
            clientSocket.Connect(serverEndPoint);
            Console.WriteLine("התחברת לשרת");

            // שליחת הודעה לשרת
            string messageToSend = "זוהי הודעה מהלקוח";
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageToSend);
            clientSocket.Send(messageBytes);

            // קריאת תשובה מהשרת
            byte[] responseBytes = new byte[1024];
            int bytesReceived = clientSocket.Receive(responseBytes);
            string response = Encoding.UTF8.GetString(responseBytes, 0, bytesReceived);
            Console.WriteLine($"תשובה מהשרת: {response}");

            // סגירת הסוקט
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"שגיאה: {e.Message}");
        }
    }
}
