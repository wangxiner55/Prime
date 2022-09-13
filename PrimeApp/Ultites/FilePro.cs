using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrimeApp.Ultites
{
	public class FilePro
	{
		FilePro() { }

		
		bool ReadAndePrint( string _text,string _path)
		{
			try
			{
				// 创建一个 StreamReader 的实例来读取文件 
				// using 语句也能关闭 StreamReader
				using (StreamReader sr = new StreamReader(_path))
				{
					// 从文件读取并显示行，直到文件的末尾 
					while ((_text = sr.ReadLine()) != null)
					{
						Console.WriteLine(_text);
					}
				}
				return true;
			}
			catch (Exception e)
			{
				// 向用户显示出错消息
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
				return false;
			}

		}

		bool Read(ref string _text,string _path)
		{

			try
			{
				using (StreamReader sr = new StreamReader(_path))
				{

					if(sr.ReadLine() != null)
					{
						_text = sr.ReadToEnd();	
					}
				}
				return true;

			}
			catch(Exception e)
			{
				Console.WriteLine("Not Read");
				Console.WriteLine(e.Message);
				return false;
			}
			
		}

		bool IsEmpty(string _path )
		{
			try
			{
				using (StreamReader sr = new StreamReader(_path))
				{

					if (sr.ReadLine() != null)
					{
						return false;
					}
					else
						return true;
				}
			
				

			}
			catch (Exception e)
			{
				Console.WriteLine("Not Read");
				Console.WriteLine(e.Message);
				return true;
			}


		}



	}
}
