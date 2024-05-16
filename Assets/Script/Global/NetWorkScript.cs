using System;
using System.Net.Sockets;
using UnityEngine;
//using static ByteArray;

public class NetWorkScript
{
    private static NetWorkScript instance;//Ϊ�ľ���ȫ��ʹ��ͬһ�������������ʵ����Ϣ�Ķ��У���ʼʱ����null
    private static Socket socket;
    private static string ip = "127.0.0.1";
    private static int port = 8083;
    private static byte[] buff = new byte[1024];//python�Ǵﲻ�����Ч���� static �����ҳ־�
    public static NetWorkScript getInstance()//����
    {
        if (instance == null)//�������������--������init�����ʼ��
        {
            init();
            Debug.Log("���´δγ�ʼ��");//˵���ˣ�--�ܹ�ȥ���Ӧ�þͲ����ˣ����˼ά�ܳ�ǰ��
        }
        return instance;
    }
    public static void init()//�����ʼ������һ��Ҫ�ҵ���ť��ȥ��������ť�����Ծ�����Start()����
    {
        try
        {
            Debug.Log("66666666666666�ҹ��������û��ִ�й�666666666666666");
            instance = new NetWorkScript();//��������һ���㹻��--����֮��Ͳ�����null��
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//һ�ξ�Ӧ�ù����ˣ��Ͼ�д��python��
            socket.Connect(ip, port);
            Debug.Log("success");
            socket.BeginReceive(buff, 0, 1024, SocketFlags.None, ReceiveCallBack, buff);//�����ٻص�
        }
        catch
        {
            Debug.Log("�������ӳ�ʼ��faild");
        }
    }
    public static void ��è����()//������Ǻ�������
    {
        Debug.Log("2");
        init();
        Debug.Log("3");
    }
    void Start()
    {
        Debug.Log("��Start����Ҳ���԰��ڻ�����");
        init();
    }
    //sendmessage
    public void sendMessage(int type, int area, int command, string message)
    {

    }

    private static void ReceiveCallBack(IAsyncResult ar)//�ص����� 
    {
        try
        {
            int readCount = 0;
            readCount = socket.EndReceive(ar);//ar��ʵ���Ǵ�����������
            byte[] temp = new byte[readCount];
            Buffer.BlockCopy(buff, 0, temp, 0, readCount);
            //�ٶ��½�һ��
        }
        catch
        {
            socket.Close();
            Debug.Log("net error");
        }
        socket.BeginReceive(buff, 0, 1024, SocketFlags.None, ReceiveCallBack, buff);//�γɱջ�
    }
    private void readMessage(byte[] message)//�������յ�����Ϣ
    {

    }

}